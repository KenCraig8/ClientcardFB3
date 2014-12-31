------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '10/15/2013', '2013.10.15'
,'Tables Add FirstCalService, LastSupplService to Household
 SP     UpdateHouseholdTrxDate
 Add    BMAC Report
 Add    BMAC EmailRecipient
 Pref   AlertMinDaysText, AlertMinimumDays'
,GetDate(), 'CSDG')

SELECT * FROM FB3 order BY EXEVersion
GO
------------------------------

Print 'Insert BMACReport into Monthly Reports'
------------------------------
DECLARE @ReportName varchar(50), @ReportPath varchar(255), @DocType varchar(8)
SELECT @ReportName = 'BMAC Monthly Report', @ReportPath = 'C:\ClientCardFB3\Templates\BMACReport.xls', @DocType = '.xls'    
IF EXISTS(SELECT * FROM MonthlyReports WHERE ReportName = @ReportName)
	UPDATE MonthlyReports 
	SET ReportPath = @ReportPath, DocType = @DocType
	WHERE ReportName = @ReportName
ELSE
INSERT INTO [dbo].[MonthlyReports]([ID],[ReportName],[EmailAddresses],[ReportPath],[ReportActive],[GroupingBy],[DocType])
     VALUES(16,@ReportName,'gailm@bmacww.org|',@ReportPath,1,0,@DocType)     
GO

--------------------------
PRINT 'Update EmailRecipients for BMAC'
DECLARE @RecipientName nvarchar(100), @EmailAddress nvarchar(255)

SELECT @RecipientName = 'Blue Mountain Action Council', @EmailAddress = 'gailm@bmacww.org'
IF NOT Exists(SELECT * FROM EmailRecipients WHERE RecipientName = @RecipientName)
	INSERT INTO [dbo].[EmailRecipients]([RecipientName],[EmailAddress],[Reports],[CreatedBy],[Created],[ModifiedBy],[Modified],[CreatedPC],[ModifiedPC])
     VALUES (@RecipientName,@EmailAddress,null,'Admin', GetDate(),'',NULL, 'CSDG','CSDG')

GO

--------------------------
PRINT 'Add FirstCalService and LastSupplService to Household table'

/*
   Tuesday, October 15, 2013 11:16:00 PM
   User: 
   Server: T-QOSMIO-10\X2008
   Database: ClientcardFB3
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
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
GO
ALTER TABLE dbo.Household ADD
	FirstCalService datetime NULL,
	LastSupplService datetime NULL
GO
ALTER TABLE dbo.Household SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO

Update hh set 
FirstCalService = (SELECT MIN(TrxDate) FROM TrxLog
                    WHERE HouseholdID = hh.ID AND TrxDate BETWEEN '01/01/' + RIGHT(CONVERT(varchar(10),GetDATE(),101),4) AND '12/31/' + RIGHT(CONVERT(varchar(10),GetDATE(),101),4))
FROM Household hh
GO
Update hh set 
LastSupplService = (SELECT MAX(TrxDate) FROM TrxLog
                    WHERE HouseholdID = hh.ID 
                    AND TrxDate BETWEEN '01/01/' + RIGHT(CONVERT(varchar(10),GetDATE(),101),4) AND '12/31/' + RIGHT(CONVERT(varchar(10),GetDATE(),101),4)
                    AND RcvdSupplemental = 1)
FROM Household hh
GO

--------------------------
PRINT 'SP UpdateHouseholdTrxDate'

/****** Object:  StoredProcedure [dbo].[UpdateHouseholdTrxDates]    Script Date: 06/14/2011 16:09:09 ******/
/* Added LastSupplSerivce  10/15/2013 */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateHouseholdTrxDates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateHouseholdTrxDates]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateHouseholdTrxDates] ( @HHId int, @LowDate DateTime, @HiDate DateTime, @ServiceDate DateTime )
AS 
BEGIN
DECLARE	  @LastCommodityService		datetime
		, @LatestService			datetime
		, @FirstServiceThisYear		datetime
        , @LastSupplService         datetime
        , @FirstCalService          datetime
        , @ServiceYear              char(4)

SELECT @ServiceYear = RIGHT(CONVERT(varchar(10),@ServiceDate,101),4)
		
SELECT @LastCommodityService = MAX(TrxDate) 
  FROM TrxLog tx
 WHERE tx.TrxStatus < 2 AND tx.HouseholdID = @HHId AND tx.RcvdCommodity = 1

SELECT @LatestService = MAX(TrxDate)
  FROM TrxLog tx 
 WHERE tx.TrxStatus < 2 AND tx.HouseholdID = @HHId AND RcvdSupplemental = 0

SELECT @FirstServiceThisYear = MIN(TrxDate)
  FROM TrxLog tx 
 WHERE tx.TrxStatus < 2 AND tx.HouseholdID = @HHId AND tx.TrxDate Between @LowDate AND @HiDate

SELECT @FirstCalService = MIN(TrxDate)
  FROM TrxLog tx 
 WHERE tx.TrxStatus < 2 AND tx.HouseholdID = @HHId AND tx.TrxDate Between '01/01/' + @ServiceYear AND '12/31/' + @ServiceYear

SELECT @LastSupplService = MAX(TrxDate)
  FROM TrxLog tx 
 WHERE tx.TrxStatus < 2 AND tx.HouseholdID = @HHId AND RcvdSupplemental = 1

UPDATE Household SET  LatestService = @LatestService
					, LastCommodityService = @LastCommodityService
					, FirstSvcThisYear = @FirstServiceThisYear
                    , FirstCalService = @FirstCalService
                    , LastSupplService = @LastSupplService 
WHERE ID = @HHId

END


GO

Grant Execute ON [dbo].[UpdateHouseholdTrxDates] TO CCFB_User

GO

--------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'AlertMinDaysText')
BEGIN
	Print 'Preference AlertMinDaysText'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('AlertMinDaysText','LAST SERVICE LESS THAN MINIMUM DAYS',0,'Features','Alert Less Than Number of Days Text','Alert Less Than Number of Days Text','Text','textbox',400)
END
GO

IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'AlertMinimumDays')
BEGIN
	Print 'Preference AlertMinimumDays'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('AlertMinimumDays','0',0,'Features','Alert Less Than Number of Days','Alert Less Than Number of Days','Integer','textbox',100)
END
GO

IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'DonorIdEFAP')
BEGIN
	Print 'Preference DonorIdEFAP'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('DonorIdEFAP','0',0,'Features','DonorId of EFAP Donor','DonorId of EFAP Donor','Integer','textbox',100)
END
GO

IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'DonorIdTEFAP')
BEGIN
	Print 'Preference DonorIdTEFAP'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('DonorIdTEFAP','0',0,'Features','DonorId of TEFAP Donor','DonorId of TEFAP Donor','Integer','textbox',100)
END
GO

IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'DonorId2Harvest')
BEGIN
	Print 'Preference DonorId2Harvest'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('DonorId2Harvest','0',0,'Features','DonorId of 2nd Harvest','DonorId of 2nd Harvest','Integer','textbox',100)
END
GO
