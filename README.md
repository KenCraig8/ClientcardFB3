ClientCardFB3
==========

An application to allow food banks to store and view their data

View our planning page here: https://trello.com/clientcardfb3

Getting started with the project
Contact me if you would like to contribute

Download these files:
ClientCard install: http://1drv.ms/1p9tSYe FB3Installion-2014-04-27.zip

Install Client Card:
	Run .msi installer
	
Install Microsoft Access 2010 or better

Next install the SQL server:
IMPORTANT: For both options, your computer name must be different than your user name. If they are the same, you won't be able to log into the sql server on your computer.

Automatic install:
I created a simple script to make it easier to install the Sql server properly. 
You can download it here: https://www.dropbox.com/s/3f6h84t2kge28ax/SqlInstallScript.zip?dl=0 Just unzip the folder and run the "installScript.cmd".

This will automaticly download the correct SQL server installer. Then it will install this with the correct parameters.

Here's a few errors that are common and easy to fix:
Error with line: "powershell (New-Object System.Net.WebClient).DownloadFile(\"%downloadFile%\", \"%CD%\%sqlInstallName%\")"
For some reason, the exe file can't be automaticly downloaded. 
Solution: Download the file: https://www.dropbox.com/s/tohv09k9os60yd1/SQLEXPRWT_x86_ENU.exe?dl=1 or https://www.dropbox.com/s/5c1f9hosz1f8gw2/SQLEXPRWT_x64_ENU.exe?dl=1 depending on your operating system. Then rename this to "SQLEXPRWT_ENU.exe" and put it in the SQLInstallScript folder. Just remove the above line and run the script again.

Error: SQLCMD command not found
This is because the command prompt commands haven't been updated yet.
Solution: Run the "databaseSetup.cmd" file in a separate command prompt window.


Manual install:
SQL server 2008 R2 Express with tools: http://www.microsoft.com/en-us/download/details.aspx?id=30438
Configuration file: https://drive.google.com/file/d/0B1DTcD94cvvBamlaZXFzckQ3MWM/edit?usp=sharing
The file SQLEXPRWT_x64_ENU.exe or SQLEXPRWT_x86_ENU.exe
		Change these options to install. Everything else is the default:
			Server configuration: Set Browser Service to Automatic.
			Database Engine config: Mixed Mode Authentication with a SA password (I use ccfb3)

Run SQL Management Studio
LogIn to SQL Management Studio using Windows Authentication or the SA user
	Right click on Database folder, Restore Database:
		To database: ClientcardFB3
		In General tab: select Source-> from device-> add->		C:\Users\Public\ClientcardFB3\Database\FB3x8Prod.BAK to database ClientcardFB3
	Create New LogIn: Right click on Security folder, under server (not the one under databases) Select SQL server authentication. Username: CCFB_User pw: 19800612
	Reset permissions:  Open file icon, open: C:\Users\Public\ClientcardFB3\Scripts\ResetCCFBUser.sql, click execute

Run Client Card with login Admin, password: master


Getting started mini guide:

If you would like to use Git and Github for to work with the project:
Download it here: http://git-scm.com/downloads
I think it will be helpful to skim the sections up to 1.5 of this tutorial: http://git-scm.com/book/en/v2/Getting-Started-About-Version-Control Don't get caught up in the details, just try to get a sense of the general workflow and philosophy of git.

If you would like to use the github client instead of command line:
To install the github client: https://windows.github.com/
The getting started guide is also helpful for how to use the tool: https://windows.github.com/help.html​

For working with this project specifically:
Create a github account and send me your username. I will add you as a collaborator so you can push to the repository.
Clone the repository, then you have a copy of everything on your computer.
To test if it's working, make a change to one of the images or something and push it to Github. You should see your change on the website in a few minutes.
Have fun!

We need lots of help with testing:
Follow these steps if you would like to contribute.
We're using Nunit, NunitForms and Moq for testing.
These are easy to install, just follow this guide: http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog





