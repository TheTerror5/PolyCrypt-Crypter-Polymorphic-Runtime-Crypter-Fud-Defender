Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions

'Namespace BatchProtect
Public Module Obfuscator

        Private random As Random = New Random()
        Public Function GetRandomString(ByVal Optional length As Integer = 10) As String
        Dim chars = "енгшҳзעיחלךdLfiહદજોે્િુપೀೂಬಹಗㅐㅔㅁㄴກດഝഢഞ१२३४५६अखदलत्तरकतλζχψωβνμμ΅כעיなにちしきازشچطئؤرلاىةوشسيبلاzxcvbnmasdfghjklqwertyuiopتنمككطضصثقفغعهخحبنمّْٰքፕልክଆଈщасдਰਕਤਚਟமதநயฐฅฮฉฬవఔఐერტყខចវزشچطبنمّْٰڑٹَئِۃُآصڈغھضخذژثظںّْٰٹ"
        Return New String(Enumerable.Repeat(chars, length).[Select](Function(s) s(random.Next(s.Length))).ToArray())
        End Function

        Public Function OneLineIF(ByVal code As String) As String
            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            Dim inIf = False
            code = ""

            For i = 0 To splittedCode.Length - 1
                If splittedCode(i).Contains("(") Then inIf = True
                If splittedCode(i).Contains(")") AndAlso Not splittedCode(i).ToUpper().Contains("ELSE") Then inIf = False
                If inIf Then
                    code += splittedCode(i)
                    If splittedCode.Length > i + 1 AndAlso splittedCode(i + 1).Contains(")") Then Continue For
                    If splittedCode(i).Contains("(") Then Continue For
                    code += " && "
                End If
                If Not inIf Then code += splittedCode(i) & Environment.NewLine
            Next
            Return code
        End Function

        'Remove trailing spaces and empty lines
        Public Function TrimSpace(ByVal code As String) As String
            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            code = ""
            For Each line In splittedCode
                code += line.TrimStart(" "c, Microsoft.VisualBasic.Strings.ChrW(9)) & Environment.NewLine
            Next
            Return code
        End Function

        'remove comment
        Public Function RemoveCommentary(ByVal code As String) As String
            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            code = ""
            For Each line In splittedCode
                If line.Length > 2 AndAlso Not Equals(line.Substring(0, 3).ToUpper(), "REM") AndAlso Not Equals(line.Substring(0, 2), "::") Then
                    code += line & Environment.NewLine
                End If
            Next
            Return code
        End Function

        Public Function getVariables(ByVal code As String) As List(Of String)
            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            Dim variables As List(Of String) = New List(Of String)()

            For Each line In splittedCode
                If line.Length >= 3 AndAlso line.ToUpper().Contains("SET ") AndAlso line.Contains("=") Then
                    Dim clearLine = line.Replace(" =", "=").Replace("/A ", "").Replace("/a ", "")
                    Dim words As String() = New Regex(" (.*?)=").Split(clearLine)
                    Dim variableName = words(1)
                    If variableName.Contains(" ") Then variableName = variableName.Split(" "c).Last()
                    If variableName.Contains("[") Then variableName = variableName.Split("["c).First() 'array
                    variableName = Regex.Replace(variableName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled)
                    If Not variables.Contains(variableName) Then variables.Add(variableName)
                End If
            Next
            Return variables
        End Function

        Public Function getLabels(ByVal code As String) As List(Of String)
            Dim labels As List(Of String) = New List(Of String)()
            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            For Each line In splittedCode
                If line.Length > 1 AndAlso Equals(line.Substring(0, 1), ":") AndAlso Not Equals(line.Substring(1, 1), ":") Then
                    Dim subName = line.Replace(":", "")
                    If Not labels.Contains(subName) Then labels.Add(subName)
                End If
            Next
            Return labels
        End Function

        Public Function RandomSubroutineName(ByVal code As String) As String
            Dim labels = getLabels(code)
            For Each label In labels
                Dim newSubName As String = GetRandomString()
                code = code.Replace(":" & label, ":" & newSubName)
                code = code.Replace("GOTO " & label, "GOTO " & newSubName)
                code = code.Replace("goto " & label, "GOTO " & newSubName)
            Next
            Return code
        End Function

        Public Function RandomVariableName(ByVal code As String) As String
            Dim variables = getVariables(code)

            For Each variable In variables
                Dim newVariableName As String = GetRandomString()
                code = code.Replace("%" & variable & "%", "%" & newVariableName & "%")
                code = code.Replace("%" & variable & ":", "%" & newVariableName & ":")
                code = code.Replace(variable & "=", newVariableName & "=")
                code = code.Replace(variable & " =", newVariableName & "=")
                code = code.Replace(variable & "[", newVariableName & "[")
                code = code.Replace("[%" & variable & "%]", "[%" & newVariableName & "%]")
            Next
            Return code
        End Function

        Public Function SubstringEncode(ByVal code As String) As Tuple(Of String, String)
            Dim letters As Char() = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray().OrderBy(Function(x) Guid.NewGuid()).ToArray()
            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            Dim varName As String = GetRandomString()
            Dim setStr As String = "SET " & varName & "=" & New String(letters) & Environment.NewLine 'SET
            Dim result = ""
            Dim i = 0  'i is the row count counter
            Dim j = 0  'j is the "word" counter
            For Each line In splittedCode   'iterate over each row
                i += 1
                If line.ToUpper().Contains("SET") OrElse line.Contains(":") Then
                    result += line
                Else
                    Dim splittedLine = line.Split(" "c)    'Split lines into "words" by spaces
                    For Each word In splittedLine   'Iterate through each "word" of the split
                        j += 1
                        Dim characters As Char() = word.ToCharArray()
                        ' split "word" by character
                        If word.Contains("%"c) Then 'Include% put directly into result
                            result += word & " "
                        Else
                            For Each character In characters  'Iterate over the split characters
                                If letters.Contains(character) Then    'Whether the characters are contained in letters
                                    Dim lettersStr As String = New String(letters)    'Just add confusion and put result
                                    result += "%" & varName & ":~" & lettersStr.IndexOf(character).ToString() & ",1%"
                                Else
                                    result += character    'Put the result directly if it is not there
                                End If
                            Next
                            If splittedLine.Count() <> j Then result += " "   'One "word" is obfuscated, add spaces, obfuscate the next "word"
                        End If
                    Next
                End If

                If i < splittedCode.Count() Then result += Environment.NewLine

            Next
            Return New Tuple(Of String, String)(setStr, result)
        End Function

        Public Function ControlFlow(ByVal code As String) As String
            code = OneLineIF(code)

            Dim splittedCode = code.Split({Microsoft.VisualBasic.Constants.vbCrLf, Microsoft.VisualBasic.Constants.vbLf, Microsoft.VisualBasic.Constants.vbCr}, StringSplitOptions.RemoveEmptyEntries)
            Dim shuffledCode As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)()
            Dim lNumber = 0
            While code.Contains(":l" & Convert.ToString(lNumber))
                lNumber += 1
            End While
            Dim startNumber = lNumber
            Dim eoc As String = GetRandomString()
            For Each line In splittedCode
                While code.Contains(":l" & Convert.ToString(lNumber))
                    lNumber += 1
                End While
                shuffledCode.Add(lNumber, line)
                lNumber += 1
            Next
            shuffledCode = shuffledCode.OrderBy(Function(x) Guid.NewGuid()).ToDictionary(Function(item) item.Key, Function(item) item.Value)
            code = ""
            For Each kvPair In shuffledCode
                code += ":l" & kvPair.Key.ToString() & Environment.NewLine
                code += kvPair.Value & Environment.NewLine
                code += "GOTO l" & (kvPair.Key + 1).ToString() & Environment.NewLine
            Next

            code = "GOTO l" & startNumber.ToString() & Environment.NewLine & code & ":l" & shuffledCode.Count.ToString()
            Return RandomSubroutineName(code)
        End Function
    End Module
'End Namespace
