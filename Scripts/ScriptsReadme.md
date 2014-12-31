The scripts in this folder are updated versions of those in C:\Users\Public\ClientcardFB3\Scripts.
They are used for setting up the database and keeping it up to date.

The FB3Stats.sql is used to check the update history of the database.
ResetCCFBUser.sql is used when ClientCard installs to setup the database.
The other scripts are for updating the tables in the database.

How to run a script:
Open SQL management studio
Use open file to open the script.
Use the database selection drop down (next to the !Execute button) to select the database you want the script to modify.
Click "!Execute"

If you try to run the code and get an error about a table in the database, this is most likely because the code is now referencing a table that doesn't exist in the database. This will probably happen when you run the code for the first time. It also happens sometimes when you pull from github because one of your team-mates made a change to the code to reference new database table.

Whoever made a change to the code also should have pushed an update script to add or update the table in the database.
To see what updates have been applied to your database, run FB3Stats.sql to see the last database update applied.
Then run the updates dated after the last update.
If the code still has errors, or there wern't any more updates, try running the "ResetCCFBUser.sql". This script could have been modified since the last time it was run to make other changes to the database.
If you're still getting errors, it's possible that someone forgot to include the script for the update they just applied. Talk to your team-mates and work this out.
