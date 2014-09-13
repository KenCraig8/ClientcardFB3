echo "Must run this as administrator"

set sqlInstallName="SQLEXPRWT_ENU.exe"
set configFileName="config.ini"

echo "Determine 32 vs 64 bit"
wmic os get osarchitecture | find /i "64" > nul
set is32=%errorlevel%
echo %is32%

REM Download the configuration file. Escape the " around the filename so it's passed to powershell. & is not allowed so it's escaped with `
powershell (New-Object System.Net.WebClient).DownloadFile(\"https://drive.google.com/uc?export=download`&id=0B1DTcD94cvvBamlaZXFzckQ3MWM\", \"%CD%\%configFileName%\")

if %is32%==1 (
	echo "32 bit"
	
	REM TODO: This doesn't work cause of google's virus scan won't download big files
	REM Use dropbox
	powershell (New-Object System.Net.WebClient).DownloadFile(\"https://docs.google.com/uc?export=download`&confirm=zb71&id=0B1DTcD94cvvBSG9Xb09hT051dm8\", \"%CD%\%sqlInstallName%\")
	
) else (
	echo "64 bit"

	powershell (New-Object System.Net.WebClient).DownloadFile(\"https://drive.google.com/uc?export=download&confirm=5LpT`&id=0B1DTcD94cvvBOUwtQlNPQTJycE0\", \"%CD%\%sqlInstallName%\")
)

SQLEXPRWT_ENU.exe /ACTION=Install /CONFIGURATIONFILE="%CD%\%configFileName%"

SQLCMD -S MYCOMPUTER\SQLEXPRESS -i RestoreDatabase.sql

SQLCMD -S MYCOMPUTER\SQLEXPRESS -i SetUser.sql

SQLCMD -S MYCOMPUTER\SQLEXPRESS -i C:\Users\Public\ClientcardFB3\Scripts\ResetCCFBUser.sql
