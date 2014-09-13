echo "Must run this as administrator"

SQLCMD -S MYCOMPUTER\SQLEXPRESS -i RestoreDatabase.sql

SQLCMD -S MYCOMPUTER\SQLEXPRESS -i SetUser.sql

SQLCMD -S MYCOMPUTER\SQLEXPRESS -i C:\Users\Public\ClientcardFB3\Scripts\ResetCCFBUser.sql
