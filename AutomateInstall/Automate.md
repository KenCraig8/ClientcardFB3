Run making config file: SQLEXPRWT_x64_ENU.exe /ACTION=Install /UIMODE=Normal
install with config: 
mkdir "c:\sqlTmp\PCUSOURCE"
SQLEXPRWT_x64_ENU.exe /ACTION=Install /CONFIGURATIONFILE="C:\Users\Nevan\Documents\foodBank\ClientcardFB3\AutomateInstall\ConfigurationFile.ini"
SQL server install options: http://msdn.microsoft.com/en-us/library/ms144259.aspx
SQL query from cmd http://msdn.microsoft.com/en-us/library/ms162773.aspx
Other database stuff cmd: http://msdn.microsoft.com/en-us/library/ms162827.aspx
Download files: Won't work: bitsadmin.exe /transfer "JobName" https://docs.google.com/document/d/16lLN_RGdUMpHBVuHT3eLXs1BoQ1oPXAOuUqrD6k6w18/edit?usp=sharing C:\Users\Nevan\Downloads

Power shell script: http://teusje.wordpress.com/2011/02/19/download-file-with-powershell/
$webclient = New-Object System.Net.WebClient
$url =  "https://docs.google.com/document/d/16lLN_RGdUMpHBVuHT3eLXs1BoQ1oPXAOuUqrD6k6w18/edit?usp=sharing" 
$file =  "C:\Users\Nevan\Downloads"
$webclient.DownloadFile($url,$file)

Running: http://ss64.com/ps/syntax-run.html

Automated todo: 
1. Check operating system. 32/64 bit, windows version
2. Download Sql server and config file
3. Run sql server with config file
4. Restore database
5. Create New LogIn
6. Run reset permission script
