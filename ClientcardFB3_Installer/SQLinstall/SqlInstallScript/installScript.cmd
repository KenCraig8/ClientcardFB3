echo "Must run this as administrator"
echo "IMPORTANT: make sure your username isn't the same as your computer name"

set sqlInstallName=SQLEXPRWT_ENU.exe
set configFileName=ConfigSqlFullInstall.ini

echo "Determine 32 vs 64 bit"
wmic os get osarchitecture | find /i "64" > nul
set is32=%errorlevel%
echo %is32%

if %is32%==1 (
	echo "32 bit"
	
	set downloadFile=https://www.dropbox.com/s/tohv09k9os60yd1/SQLEXPRWT_x86_ENU.exe?dl=1
	
) else (
	echo "64 bit"
	
	set downloadFile=https://www.dropbox.com/s/5c1f9hosz1f8gw2/SQLEXPRWT_x64_ENU.exe?dl=1
)

powershell (New-Object System.Net.WebClient).DownloadFile(\"%downloadFile%\", \"%CD%\%sqlInstallName%\")

SQLEXPRWT_ENU.exe /IACCEPTSQLSERVERLICENSETERMS /ACTION=Install /CONFIGURATIONFILE="%CD%\%configFileName%"

START cmd /k databaseSetup.cmd