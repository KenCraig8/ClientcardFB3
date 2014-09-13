echo "starting"
:r ./RestoreDatabase.sql
echo "restore done"
:r ./SetUser.sql
echo "set user done"
:r "C:\Users\Public\ClientcardFB3\Scripts\ResetCCFBUser.sql"
echo "reset user done"