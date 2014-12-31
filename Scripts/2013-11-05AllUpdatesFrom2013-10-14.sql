------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '11/05/2013', '2013.11.05'
,'Tables parm_ServiceGroup, parm_Transportation
; Add DefaultSvcGrp to ServiceItems
; Add ServiceGroup to TrxLog
; Add Transportation to Household
; Func GetFiscalPeriod, GetFiscalMonth
; Pref   EnableServiceGroups,AlertMinMonthsText,AlertMinimumMonths'
,GetDate(), 'CSDG')
SELECT * FROM FB3 order BY EXEVersion
GO

------------------------------
/*
   Thursday, October 31, 20135:03:38 PM
   User: 
   Server: T-QOSMIO-10\X2008
   Database: ClientcardFB3
   Application: 
*/
IF  EXISTS (SELECT * FROM sys.Columns WHERE object_id = OBJECT_ID(N'[dbo].[TrxLog]') AND name = 'ServiceGroup')
	print 'ServiceGroup Already in TrxLog Table'
ELSE
BEGIN
	PRINT 'Add ServiceGroup to TrxLog'
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
	    ServiceGroup int NULL
    ALTER TABLE dbo.TrxLog SET (LOCK_ESCALATION = TABLE)
    COMMIT
END
GO

------------------------------

/****** Object:  Table [dbo].[parm_ServiceGroup]    Script Date: 10/31/2013 ******/
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[parm_ServiceGroup]') AND type in (N'U'))
begin
    print 'Add table parm_ServiceGroup'
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[parm_ServiceGroup](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Type] [nvarchar](30) NULL,
	[SortOrder] [int] NULL,
	[ShortName] [nvarchar](4) NULL,
 CONSTRAINT [PK_parm_ServiceGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
if (SELECT Count(*) FROM [dbo].[parm_ServiceGroup] ) = 0 
BEGIN
    SET IDENTITY_INSERT [dbo].[parm_ServiceGroup] ON
    INSERT INTO [dbo].[parm_ServiceGroup]
           ([Id]
           ,[Type]
           ,[SortOrder]
           ,[ShortName])
        VALUES (0, 'No Selection', 9, 'NO'),
        (1, 'Distribution Center',0, 'DC'),
        (2, 'Northwest Harvest',0, 'NWH'),
        (3, 'Food Lifeline',0, 'FLL'),
        (4, 'Independant',0, 'IND')
    SET IDENTITY_INSERT [dbo].[parm_ServiceGroup] OFF
END
go
GRANT DELETE,INSERT,SELECT,UPDATE,VIEW DEFINITION ON [dbo].[parm_ServiceGroup] TO [CCFB_User] AS [dbo]
GO

------------------------------

/*
   Saturday, November 02, 20134:25:32 PM
   User: 
   Server: T-QOSMIO-10\X2008
   Database: ClientcardFB3
   Application: 
*/
IF  EXISTS (SELECT * FROM sys.Columns WHERE object_id = OBJECT_ID(N'[dbo].[ServiceItems]') AND name = 'DefaultSvcGrp')
	print 'DefaultSvcGrp Already in ServiceItems Table'
ELSE
BEGIN
	PRINT 'Add DefaultSvcGrp to ServiceItems'
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
    	DefaultSvcGrp int NULL
    ALTER TABLE dbo.ServiceItems SET (LOCK_ESCALATION = TABLE)
    COMMIT
END
GO
UPDATE dbo.ServiceItems SET DefaultSvcGrp = 0 WHERE DefaultSvcGrp IS NULL

------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'EnableServiceGroups')
BEGIN
	print 'Add Preferences EnableServiceGroups'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('EnableServiceGroups','0',0,'Features','Enable Service Groups','Enable Service Groups','Yes|No','combo',80)
END
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'AlertMinMonthsText')
BEGIN
	print 'Add Preferences AlertMinMonthsText'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('AlertMinMonthsText','LAST SERVICE LESS THAN MINIMUM Months',0,'Features','Alert Less Than Number of Months Text','Alert Less Than Number of Months Text','Text','textbox',400)
END
GO
/*AlertMinMonthsText	LAST SERVICE LESS THAN MINIMUM Months	0	Features	Alert Less Than Number of Months Text	Alert Less Than Number of Months Text	Text	textbox	400*/
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'AlertMinimumMonths')
BEGIN
	print 'Add Preferences AlertMinimumMonths'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('AlertMinimumMonths','0',0,'Features','Alert Less Than Number of Months','Alert Less Than Number of Months','Integer','textbox',100)
END
GO
/*AlertMinimumMonths	0	0	Features	Alert Less Than Number of Months	Alert Less Than Number of Months	Integer	textbox	100*/
------------------------------

/****** Object:  Table [dbo].[parm_Transportation]    Script Date: 11/05/2013 ******/
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[parm_Transportation]') AND type in (N'U'))
begin
    print 'Add table parm_Transportation'
    SET ANSI_NULLS ON
    SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[parm_Transportation](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Type] [nvarchar](30) NULL,
	[SortOrder] [int] NULL,
	[ShortName] [nvarchar](4) NULL,
 CONSTRAINT [PK_parm_Transportation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
GRANT DELETE,INSERT,SELECT,UPDATE,VIEW DEFINITION ON [dbo].[parm_Transportation] TO [CCFB_User] AS [dbo]
GO

if (SELECT Count(*) FROM [dbo].[parm_Transportation] ) = 0 
BEGIN
    SET IDENTITY_INSERT [dbo].[parm_Transportation] ON
    INSERT INTO [dbo].[parm_Transportation]
           ([Id]
           ,[Type]
           ,[SortOrder]
           ,[ShortName])
        VALUES (0, 'No Selection', 9, 'NO'),
        (1, 'Bicycle',3, 'BIKE'),
        (2, 'Bus',2, 'BUS'),
        (3, 'Car',1, 'CAR'),
        (4, 'Walking',4, 'WALK')
    SET IDENTITY_INSERT [dbo].[parm_Transportation] OFF
END
go
------------------------------
/*
   Tuesday, November 05, 2013   9:33:56 PM
   Table: Household 
   Application: ClientcardFB3 
*/
IF  EXISTS (SELECT * FROM sys.Columns WHERE object_id = OBJECT_ID(N'[dbo].[Household]') AND name = 'Transportation')
	print 'Transportation Already in Household Table'
ELSE
BEGIN
	PRINT 'Add Transportation to Household'
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
    ALTER TABLE dbo.Household ADD
	    Transportation int NULL
    ALTER TABLE dbo.Household SET (LOCK_ESCALATION = TABLE)
    COMMIT
END
GO
UPDATE Household SET Transportation = 0 WHERE Transportation IS NULL

---------------

/****** Object:  UserDefinedFunction [dbo].[GetFiscalPeriod]    Script Date: 06/21/2011 08:50:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFiscalPeriod]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetFiscalPeriod]
GO

CREATE FUNCTION [dbo].[GetFiscalPeriod] (@Period char(6))
RETURNS char(6)
AS
BEGIN
DECLARE @FiscalYear CHAR(4), @FiscalPeriod CHAR(6)
DECLARE @PeriodMonth int, @FiscalMonthStart int, @FiscalMonth int
SELECT @FiscalMonthStart = CAST(FldVal AS int)-1 FROM Preferences WHERE FldName = 'FiscalYearStartMonth'
IF @FiscalMonthStart < 1
    SELECT @FiscalPeriod = @Period
ELSE
  BEGIN
	SELECT @PeriodMonth = CAST(RIGHT(@Period,2) as int)
	SELECT @FiscalYear = 
          CASE WHEN @PeriodMonth > @FiscalMonthStart 
                    THEN CAST(CAST(LEFT(@Period,4) AS INT) + 1 AS CHAR(4)) 
               ELSE LEFT(@Period,4) 
          END
    SELECT @FiscalMonth = 
          CASE WHEN @PeriodMonth > @FiscalMonthStart  
                    THEN @PeriodMonth - @FiscalMonthStart
               ELSE @PeriodMonth + (12 - @FiscalMonthStart)
          END 
	IF @FiscalMonth < 10
	    SELECT @FiscalPeriod = @FiscalYear + '0' + CAST(@FiscalMonth as char(1))
	ELSE
	    SELECT @FiscalPeriod = @FiscalYear + CAST(@FiscalMonth as char(2))
  END

RETURN @FiscalPeriod
 
END
GO

GRANT EXECUTE ON [dbo].[GetFiscalPeriod] TO [CCFB_User]
-------------------
/****** Object:  UserDefinedFunction [dbo].[GetFiscalMonth]    Script Date: 03/01/2011 14:39:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFiscalMonth]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetFiscalMonth]
GO

/****** Object:  UserDefinedFunction [dbo].[GetFiscalMonth]    Script Date: 03/01/2011 14:39:59 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetFiscalMonth] (@Period char(6))
RETURNS char(2)
AS
BEGIN
DECLARE @CalMonth int, @FiscalMonth int, @FiscalMonthStart int
DECLARE @FiscalMonthStr char(2)
SELECT @FiscalMonthStart = CAST(FldVal AS int) - 1 FROM Preferences WHERE FldName = 'FiscalYearStartMonth'
SELECT @CalMonth = CAST(RIGHT(@Period,2) as int)
SELECT @FiscalMonth = 
        CASE WHEN @CalMonth>@FiscalMonthStart  THEN @CalMonth - @FiscalMonthStart  
                                               ELSE @CalMonth + (12 - @FiscalMonthStart) 
        END 
IF @FiscalMonth < 10
	SELECT @FiscalMonthStr = '0' + CAST(@FiscalMonth as char(1))
ELSE
	SELECT @FiscalMonthStr = CAST(@FiscalMonth as char(2))
RETURN @FiscalMonthStr
END


GO

GRANT EXECUTE ON [dbo].[GetFiscalMonth] TO [CCFB_User]
