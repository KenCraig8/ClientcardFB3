------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '11/18/2013', '2013.11.18'
,'Add FastTrack, PrintReceipt, FullService to ServiceItems
; Add FastTrack, PrintReceipt, FullService to TrxLog
;Pref IncludeLbsOnSvcList'
,GetDate(), 'CSDG')
SELECT * FROM FB3 order BY EXEVersion
GO
------------------------------
/*
   Thursday, November 15, 2013
   User: 
   Server: T-QOSMIO-10\X2008
   Database: ClientcardFB3
   Application: 
*/
IF  EXISTS (SELECT * FROM sys.Columns WHERE object_id = OBJECT_ID(N'[dbo].[TrxLog]') AND name = 'FastTrack')
	print 'FastTrack Already in TrxLog Table'
ELSE
BEGIN
	PRINT 'Add FastTrack to TrxLog'
    BEGIN TRANSACTION
    SET QUOTED_IDENTIFIER ON
    SET ARITHABORT ON
    SET NUMERIC_ROUNDABORT OFF
    SET CONCAT_NULL_YIELDS_NULL ON
    SET ANSI_NULLS ON
    SET ANSI_PADDING ON
    SET ANSI_WARNINGS ON
    COMMIT
    BEGIN TRANSACTION
        ALTER TABLE dbo.TrxLog ADD
	        FastTrack bit NULL,
	        PrintReceipt bit NULL,
	        FullService bit NULL
        ALTER TABLE dbo.TrxLog SET (LOCK_ESCALATION = TABLE)
    COMMIT
END
GO
update TrxLog set FastTrack = 0, PrintReceipt = 0, FullService = 0 WHERE FastTrack is NULL
GO
---------------------------------------------
/*
   Friday, November 15, 2013
   User: 
   Server: T-QOSMIO-10\X2008
   Database: ClientcardFB3
   Application: 
*/

IF  EXISTS (SELECT * FROM sys.Columns WHERE object_id = OBJECT_ID(N'[dbo].[ServiceItems]') AND name = 'FastTrack')
	print 'FastTrack Already in ServiceItems Table'
ELSE
BEGIN
	PRINT 'Add FastTrack to Service Items'
    BEGIN TRANSACTION
    SET QUOTED_IDENTIFIER ON
    SET ARITHABORT ON
    SET NUMERIC_ROUNDABORT OFF
    SET CONCAT_NULL_YIELDS_NULL ON
    SET ANSI_NULLS ON
    SET ANSI_PADDING ON
    SET ANSI_WARNINGS ON
    COMMIT
    BEGIN TRANSACTION
        ALTER TABLE dbo.ServiceItems ADD
	        FastTrack bit NULL,
	        PrintReceipt bit NULL,
	        FullService bit NULL
        ALTER TABLE dbo.ServiceItems SET (LOCK_ESCALATION = TABLE)
    COMMIT
END
GO
update [ClientcardFB3].[dbo].[ServiceItems] set FastTrack = 0, PrintReceipt = 0, FullService = 0 WHERE FastTrack is NULL
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'IncludeLbsOnSvcList')
BEGIN
	print 'Add Preferences IncludeLbsOnSvcList'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('IncludeLbsOnSvcList','0',1,'Features','Include Lbs On Food Service List','Include Lbs On Food Service List','Yes|No','combo',80)
END
GO
---------------------------------------------
