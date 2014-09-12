ClientCard
==========

An application to allow food banks to store and view their data


Getting started with the project

Download these files:
ClientCard install: http://1drv.ms/1p9tSYe FB3Installion-2014-04-27.zip

Install Client Card:
	Run .msi installer

Install these:
Microsoft Access 2010 or better
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
		In General tab: select Source-> from device-> add->		C:\Users\Public\ClientcardFB3\Database\FB3x8Prod.BAK to database ClientCardFB3
	Create New LogIn: Right click on Security folder, under server (not the one under databases) Select SQL server authentication. Username: CCFB_User pw: 19800612
	Reset permissions:  Open file icon, open: C:\Users\Public\ClientcardFB3\Scripts\ResetCCFBUser.sql, click execute

Run Client Card with login Admin, password: master