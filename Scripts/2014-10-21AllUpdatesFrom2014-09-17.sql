------------------------------

Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '10/21/2014', '2014.10.21'
,'Update SP CSFPListByPeriod
, Add Table parm_CSFPStatus 
, Add Field CSFPStatus to HouseholdMembers 
, Update Func GroceryRescueWeek
, Update SP GroceryRescueLbs, InsertHouseholdMember '
,GetDate(), 'CSDG')

------------------------------------------
/****** Object:  StoredProcedure [dbo].[CSFPListByPeriod]    Script Date: 10/07/2014 10:13:13 PM ******/
DROP PROCEDURE [dbo].[CSFPListByPeriod]
GO

/****** Object:  StoredProcedure [dbo].[CSFPListByPeriod]    Script Date: 10/07/2014 10:13:13 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CSFPListByPeriod] (	@Period char(6) = '', @SortBy varchar(200) = '', @WhereClause varchar(200) = '')
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
DECLARE @sql varchar(5000)
SELECT @sql = '
		SELECT hhm.HouseholdID
			 , hhm.CSFP
             , cs.Type StatusDescr
             , hhm.CSFPStatus 
			 , hh.Inactive
			 , hh.Address
			 , hh.AptNbr
			 , hh.City
			 , CASE WHEN hhm.CSFPExpiration IS NULL THEN '''' ELSE CONVERT(varchar(10),hhm.CSFPExpiration,101)  END [CSFPExpiration]
			 , CASE WHEN cl.DistributionMethod IS NULL THEN CASE WHEN hhm.CSFPRoute is NULL THEN ''PkUp'' ELSE DfltRoute.Type END ELSE ActRoute.Type END [Route]
			 , CASE WHEN cl.DistributionMethod IS NULL THEN CASE WHEN hhm.CSFPRoute is NULL THEN 0 ELSE hhm.CSFPRoute END ELSE cl.DistributionMethod END [RouteAsInt]
			 , CASE WHEN cl.TrxDate IS NULL THEN '''' ELSE CONVERT(varchar(10),cl.TrxDate,101) END [TrxDate]
			 , CASE WHEN cl.Lbs IS NULL THEN '''' ELSE CAST(cl.Lbs as varchar(10)) END [Lbs]
			 , hhm.ID hhmID
			 , CASE WHEN cl.ID IS NULL THEN '''' ELSE CAST(cl.ID AS varchar(10)) END [LogId]
			 , ltrim(rtrim(hhm.LastName)) + '', '' + ltrim(rtrim(hhm.FirstName)) colNameLF
			 , ltrim(rtrim(hhm.FirstName)) + '' '' + ltrim(rtrim(hhm.LastName)) colNameFL
			 , CASE WHEN hhm.Phone IS NULL THEN hh.Phone 
                    WHEN hhm.Phone = '''' THEN hh.Phone
                    ELSE hhm.Phone
               END Phone 
             , (SELECT MAX(TrxDate) FROM CSFPLog WHERE MemID = hhm.ID AND Period < ' + @Period + ') PrevService
             , hhm.CSFPComments
		  FROM HouseholdMembers hhm 
		 INNER JOIN Household hh on hhm.HouseholdID = hh.ID
		  LEFT JOIN CSFPLog cl ON hhm.ID = cl.MemId AND cl.Period = ' + @Period + '
		  LEFT JOIN parm_CSFPRoutes DfltRoute ON hhm.CSFPRoute = DfltRoute.Id
		  LEFT JOIN parm_CSFPRoutes ActRoute ON cl.DistributionMethod = ActRoute.Id
          LEFT JOIN parm_CSFPStatus cs ON hhm.CSFPStatus = cs.Id'
          + @WhereClause + @SortBy
EXECUTE(@sql)
END
GO
GRANT EXECUTE ON [dbo].[CSFPListByPeriod] TO [CCFB_User]
GO
----------------------------

/****** Object:  Table [dbo].[parm_CSFPStatus]    Script Date: 10/21/2014 15:15:00 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[parm_CSFPStatus]') AND type in (N'U'))
DROP TABLE [dbo].[parm_CSFPStatus]
GO

/****** Object:  Table [dbo].[parm_CSFPStatus]    Script Date: 02/01/2011 23:12:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[parm_CSFPStatus](
	[ID] [smallint] IDENTITY(0,1) NOT NULL,
	[Type] [nvarchar](30) NULL,
	[SortOrder] [int] NULL,
	[ShortName] [nvarchar](8) NULL,
 CONSTRAINT [PK_parm_CSFPStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
GRANT DELETE,INSERT,SELECT,UPDATE,VIEW DEFINITION ON [dbo].[parm_CSFPStatus] TO [CCFB_User] AS [dbo]
GO

if (SELECT Count(*) FROM [dbo].[parm_CSFPStatus] ) = 0 
BEGIN
    SET IDENTITY_INSERT [dbo].[parm_CSFPStatus] ON
    INSERT INTO [dbo].[parm_CSFPStatus]
           ([Id]
           ,[Type]
           ,[SortOrder]
           ,[ShortName])
        VALUES (0, 'Waiting', 0, 'WAIT'),
        (1, 'Active',1, 'ACT'),
        (2, 'On Hold',2, 'HOLD'),
        (3, 'Warning Sent',3, 'WARN'),
        (4, 'Termination Sent',4, 'TERM'),
        (5, 'Terminated Other',5, 'OTH')
    SET IDENTITY_INSERT [dbo].[parm_CSFPStatus] OFF
END
go
----------------------------
/*
   Tuesday, October 21, 20143:28:43 PM
   User: 
   Server: T-QOSMIO-10\X2008
   Database: ClientcardFB3
   Application: 
*/
IF  EXISTS (SELECT * FROM sys.Columns WHERE object_id = OBJECT_ID(N'[dbo].[HouseholdMembers]') AND name = 'CSFPStatus')
	print 'CSFPStatus Already in HouseholdMembers Table'
ELSE
BEGIN
	PRINT 'Add CSFPStatus to HouseholdMembers Table'
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
        ALTER TABLE dbo.HouseholdMembers ADD CSFPStatus int NULL
        ALTER TABLE dbo.HouseholdMembers SET (LOCK_ESCALATION = TABLE)
    COMMIT
END
GO

Update HouseholdMembers Set CSFPStatus = 1 WHERE CSFP =1 and CSFPStatus IS NULL
Update HouseholdMembers Set CSFPStatus = -1 WHERE CSFP =0 and CSFPStatus IS NULL
update householdmembers set CSFPStatus = 0,LastName = REPLACE(LastName,'Waiting/','') where CSFP = 1 and left(lastname,4) = 'Wait'
GO
----------------------------
TRUNCATE TABLE parm_CSFPSortOrder
SET IDENTITY_INSERT parm_CSFPSortOrder ON
INSERT INTO parm_CSFPSortOrder (Id,[Type],SortOrder,ShortName)
VALUES (0,'<None>',0,''),	
       (1,'Last, First Name',1,'colNameLF'),
       (2,'First, Last Name',2,'colNameFL'),
       (3,'Distribution Method',3,'Route'),
       (4,'Status',4,'CSFPStatus'),
       (5,'Address',5,'Address'),
       (6,'Apt Nbr',6,'AptNbr'),
       (7,'Household ID',7,'HouseholdID'),
       (8,'Date Served',8,'TrxDate'),
       (9,'Previous Service Date',9,'PrevService'),
       (10,'Expiration Date',10,'CSFPExpiration')
SET IDENTITY_INSERT [dbo].[parm_CSFPSortOrder] OFF
GO
/*
DECLARE @RC int
DECLARE @Period char(6)
DECLARE @SortBy varchar(200)
DECLARE @WhereClause varchar(200)

SELECT @Period = '201410'
, @SortBy = ' ORDER BY Route,colNameLF'
, @WhereClause = ' WHERE hhm.ID IN (SELECT ID hhmId FROM HouseholdMembers WHERE CSFP = 1 AND Inactive = 0 AND CSFPStatus IN (0,1,2,3,4,5))'

EXECUTE @RC = [dbo].[CSFPListByPeriod] 
   @Period
  ,@SortBy
  ,@WhereClause
Print @RC
SET IDENTITY_INSERT [dbo].[parm_CSFPSortOrder] OFF
*/

-----------------------------------

/****** Object:  UserDefinedFunction [dbo].[GroceryRescueWeek]    Script Date: 10/23/2014 3:52:53 PM ******/
/****** Object:  UserDefinedFunction [dbo].[GroceryRescueWeek]    Script Date: 01/16/2013 15:55:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroceryRescueWeek]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GroceryRescueWeek]
GO

/****** Object:  UserDefinedFunction [dbo].[GroceryRescueWeek]    Script Date: 10/23/2014 3:52:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroceryRescueWeek]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'-- =============================================
-- Author:		CSDG
-- Create date: 01/16/2013
-- Updated: 10/21/2014
-- Description:	Return Value 1-6 for Grocery rescue form
-- =============================================
CREATE FUNCTION [dbo].[GroceryRescueWeek] 
(
	-- Add the parameters for the function here
	 @dateFirst DateTime
	,@dateLast  DateTime
	,@TestDate  DateTime
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int
	DECLARE @TrxDate DateTime
	DECLARE @woyMax int, @woy int, @woyOffset int, @woyAdjust int

    SELECT @woyMax = 0, @woyOffset = DatePart(WK,@dateFirst - 1), @TrxDate = @dateFirst

    while @TrxDate <= @dateLast
    begin 
        SELECT @woy = DatePart(WK,@TrxDate)
        if @woy > @woyMax 
        BEGIN
            SELECT @woyMax = @woy 
        END
        select @TrxDate = DateAdd(dd,7,@TrxDate)
    end

    SELECT @woy = DatePart(WK,@TestDate)
    IF DatePart(yy,@TestDate) <> DatePart(yy,@dateFirst)
        BEGIN
            SELECT @woy = @woy + @woyMax - 1
        END
    SELECT @Result = @woy - @woyOffset
	RETURN @Result
END
'
END
GO
 
 GRANT EXECUTE ON [dbo].[GroceryRescueWeek] TO [CCFB_User]
 GO

-------------------------------------------

/****** Object:  StoredProcedure [dbo].[GroceryRescueLbs]    Script Date: 01/16/2013 16:52:16 ******/
/****** Modified 10/23/2014 - added dateFirstWeekStart and dateLastWeekEnd ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroceryRescueLbs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GroceryRescueLbs]
GO

/****** Object:  StoredProcedure [dbo].[GroceryRescueLbs]    Script Date: 01/16/2013 16:52:16 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroceryRescueLbs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		CSDG
-- Create date: 1/16/2013
-- Updated: 10/23/2014
-- Description:	Selects Food Donations by week by food class
-- =============================================
CREATE PROCEDURE [dbo].[GroceryRescueLbs] 
	-- Add the parameters for the stored procedure here
	@dateStart          DateTime, 
	@dateEnd            DateTime,
	@DonorID            int,
    @dateFirstWeekStart DateTime,
    @dateLastWeekEnd    DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    SELECT dbo.GroceryRescueWeek(@dateFirstWeekStart, @dateLastWeekEnd, TrxDate) GRWeek, [FoodClass], SUM(Pounds) Lbs
      FROM FoodDonations 
     WHERE DonorID = @DonorID AND TrxDate BETWEEN @dateStart AND @dateEnd 
     GROUP BY dbo.GroceryRescueWeek(@dateFirstWeekStart, @dateLastWeekEnd, TrxDate),[FoodClass] 
     ORDER BY  GRWeek,[FoodClass]
END
' 
END
GO

GRANT EXECUTE ON [dbo].[GroceryRescueLbs] TO [CCFB_User] AS [dbo]
GO

/*
Execute GroceryRescueLbs '11/25/2012', '12/01/2012', 88
*/
----------------------------------------------------
/****** Object:  StoredProcedure [dbo].[InsertHouseholdMember]    Script Date: 02/22/2012 
Updated 2012-04-12 added NotCounted
Updated 2013-02-06 return new HouseholdMembers ID as Parameter @ID
Updated 2014-07-11 added Relationship through SchSupply
Updated 2014-10-23 added CSFPStatus
******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertHouseholdMember]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertHouseholdMember]
GO

CREATE PROCEDURE [dbo].[InsertHouseholdMember] 
( 	@Inactive				bit,
	@HouseholdID			int,
	@LastName				nvarchar(50),
	@FirstName				nvarchar(50),
	@Sex					char(1),
	@Birthdate				datetime,
	@AgeGroup				int,
	@SpecialDiet			bit,
	@Notes					nvarchar(MAX),
	@WorksInArea			bit,
	@Employer				nvarchar(50),
	@EmpAddress				nvarchar(50),
	@EmpCity				nvarchar(40),
	@EmpZipcode				nvarchar(10),
	@EmpPhone				nvarchar(30),
	@CreatedBy				nvarchar(50),
	@UserFlag0				bit,
	@UserFlag1				bit,
	@VolunteersAtFoodBank	bit,
	@Age					int,
	@UseAge					bit,
	@NotIncludedInClientList bit,
	@CSFP					bit,
	@HeadHH					bit,
	@Language				int,
	@IsDisabled				bit,
	@CSFPExpiration			datetime,
	@CSFPComments			nvarchar(max),
	@CSFPRoute				int,
	@MemIDNbr				varchar(50),
	@MemIDType				smallint,
	@Race					smallint,
	@Hispanic				bit,
	@BackPack				bit,
	@BPExpiration			datetime,
	@BPSize					smallint,
	@BPSchool				int,
	@BPNotes				nvarchar(MAX),
	@NotCounted				bit,
	@Relationship			int,
	@Phone					varchar(20),
	@EmailAddress			varchar(50),
    @Grade                  int,
    @SchSupply              bit,
    @SchSupplyDelivered     datetime,
    @SchSupplySchool        int,
    @CSFPStatus             int,
	@HispanicLatino			int,
	@RefugeeImmigrant		int,
	@LimitedEnglish			int,
	@MilitaryService		smallint,
	@DischargeStatus		smallint,
	@PartneredMarried		int,
	@LongTermHomeless		int,
	@ChronicallyHomeless	int,
	@Employed				int,
	@EmplolymentStatus		smallint ,
	@AmericanIndian			bit ,
	@AlaskaNative			bit ,
	@IndigenousToAmericas	bit ,
	@AsianIndian			bit ,
	@Cambodian				bit ,
	@Chinese				bit ,
	@Filipino				bit ,
	@Japanese				bit ,
	@Korean					bit ,
	@Vietnamese				bit ,
	@OtherAsian				bit ,
	@IndigenousAfricanBlack bit ,
	@AfricanAmericanBlack	bit ,
	@OtherBlack				bit ,
	@HawaiianNative			bit ,
	@Polynesian				bit ,
	@Micronesian			bit ,
	@OtherPacificIslander	bit ,
	@ArabIranianMiddleEastern bit ,
	@OtherWhiteCaucasian	bit ,
	@EthnicOther			bit ,
	@EthnicUnknown			bit ,
	@EducationLevel			smallint,
	@Homeless				smallint,
	@HomelessNbrTimes		smallint,
	@HomelessNbrMonths		smallint,
	@ID						int OUTPUT
)
AS 
BEGIN
DECLARE @Created datetime
SELECT @Created = GETDATE()

INSERT INTO [HouseholdMembers]
           ([Inactive]
           ,[HouseholdID]
           ,[LastName]
           ,[FirstName]
           ,[Sex]
           ,[Birthdate]
           ,[AgeGroup]
           ,[SpecialDiet]
           ,[Notes]
           ,[WorksInArea]
           ,[Employer]
           ,[EmpAddress]
           ,[EmpCity]
           ,[EmpZipcode]
           ,[EmpPhone]
           ,[Created]
           ,[CreatedBy]
           ,[Modified]
           ,[ModifiedBy]
           ,[UserFlag0]
           ,[UserFlag1]
           ,[VolunteersAtFoodBank]
           ,[Age]
           ,[UseAge]
           ,[NotIncludedInClientList]
           ,[CSFP]
           ,[HeadHH]
           ,[Language]
           ,[IsDisabled]
           ,[CSFPExpiration]
           ,[CSFPComments]
           ,[CSFPRoute]
           ,[MemIDNbr]
           ,[MemIDType]
           ,[Race]
           ,[Hispanic]
           ,[BackPack]
           ,[BPExpiration]
           ,[BPSize]
           ,[BPSchool]
           ,[BPNotes]
           ,[NotCounted]
           ,[Relationship]
           ,[Phone]
           ,[EmailAddress]
           ,[Grade]
           ,[SchSupply]
           ,[SchSupplyDelivered]
           ,[SchSupplySchool]
           ,[CSFPStatus]
           )
     VALUES
           (@Inactive
           ,@HouseholdID
           ,@LastName
           ,@FirstName
           ,@Sex
           ,@Birthdate
           ,@AgeGroup
           ,@SpecialDiet
           ,@Notes
           ,@WorksInArea
           ,@Employer
           ,@EmpAddress
           ,@EmpCity
           ,@EmpZipcode
           ,@EmpPhone
           ,@Created
           ,@CreatedBy
           ,NULL
           ,''
           ,@UserFlag0
           ,@UserFlag1
           ,@VolunteersAtFoodBank
           ,@Age
           ,@UseAge
           ,@NotIncludedInClientList
           ,@CSFP
           ,@HeadHH
           ,@Language
           ,@IsDisabled
           ,@CSFPExpiration
           ,@CSFPComments
           ,@CSFPRoute
           ,@MemIDNbr
           ,@MemIDType
           ,@Race
           ,@Hispanic
           ,@BackPack
           ,@BPExpiration
           ,@BPSize
           ,@BPSchool
           ,@BPNotes
           ,@NotCounted
           ,@Relationship
           ,@Phone
           ,@EmailAddress
           ,@Grade
           ,@SchSupply
           ,@SchSupplyDelivered
           ,@SchSupplySchool
           ,@CSFPStatus
           )

SELECT @ID = @@IDENTITY

INSERT INTO [ClientcardFB3].[dbo].[Demographics]
           ([ID]
           ,[HispanicLatino]
           ,[RefugeeImmigrant]
           ,[LimitedEnglish]
           ,[MilitaryService]
           ,[DischargeStatus]
           ,[PartneredMarried]
           ,[LongTermHomeless]
           ,[ChronicallyHomeless]
           ,[Employed]
           ,[EmplolymentStatus]
           ,[AmericanIndian]
           ,[AlaskaNative]
           ,[IndigenousToAmericas]
           ,[AsianIndian]
           ,[Cambodian]
           ,[Chinese]
           ,[Filipino]
           ,[Japanese]
           ,[Korean]
           ,[Vietnamese]
           ,[OtherAsian]
           ,[IndigenousAfricanBlack]
           ,[AfricanAmericanBlack]
           ,[OtherBlack]
           ,[HawaiianNative]
           ,[Polynesian]
           ,[Micronesian]
           ,[OtherPacificIslander]
           ,[ArabIranianMiddleEastern]
           ,[OtherWhiteCaucasian]
           ,[EthnicOther]
           ,[EthnicUnknown]
           ,[EducationLevel]
           ,[Homeless]
           ,[HomelessNbrTimes]
		   ,[HomelessNbrMonths]
)
     VALUES
           (@ID
           ,@HispanicLatino
           ,@RefugeeImmigrant
           ,@LimitedEnglish
           ,@MilitaryService
           ,@DischargeStatus
           ,@PartneredMarried
           ,@LongTermHomeless
           ,@ChronicallyHomeless
           ,@Employed
           ,@EmplolymentStatus
           ,@AmericanIndian
           ,@AlaskaNative
           ,@IndigenousToAmericas
           ,@AsianIndian
           ,@Cambodian
           ,@Chinese
           ,@Filipino
           ,@Japanese
           ,@Korean
           ,@Vietnamese
           ,@OtherAsian
           ,@IndigenousAfricanBlack
           ,@AfricanAmericanBlack
           ,@OtherBlack
           ,@HawaiianNative
           ,@Polynesian
           ,@Micronesian
           ,@OtherPacificIslander
           ,@ArabIranianMiddleEastern
           ,@OtherWhiteCaucasian
           ,@EthnicOther
           ,@EthnicUnknown
           ,@EducationLevel
           ,@Homeless
		   ,@HomelessNbrTimes
		   ,@HomelessNbrMonths
)

END           

GO
GRANT EXECUTE ON [dbo].[InsertHouseholdMember] TO [CCFB_User] AS [dbo]


/*
DECLARE 	@Inactive				bit,
	@HouseholdID			int,
	@LastName				nvarchar(50),
	@FirstName				nvarchar(50),
	@Sex					char(1),
	@Birthdate				datetime,
	@AgeGroup				int,
	@SpecialDiet			bit,
	@Notes					nvarchar(255),
	@WorksInArea			bit,
	@Employer				nvarchar(50),
	@EmpAddress				nvarchar(50),
	@EmpCity				nvarchar(40),
	@EmpZipcode				nvarchar(10),
	@EmpPhone				nvarchar(30),
	@CreatedBy				nvarchar(50),
	@UserFlag0				bit,
	@UserFlag1				bit,
	@VolunteersAtFoodBank	bit,
	@Age					int,
	@UseAge					bit,
	@NotIncludedInClientList bit,
	@CSFP					bit,
	@HeadHH					bit,
	@Language				int,
	@IsDisabled				bit,
	@CSFPExpiration			datetime,
	@CSFPComments			nvarchar(max),
	@CSFPRoute				int,
	@MemIDNbr				varchar(50),
	@MemIDType				smallint,
	@Race					smallint,
	@Hispanic				bit,
	@BackPack				bit,
	@BPExpiration			datetime,
	@BPSize					smallint,
	@BPSchool				int,
	@BPNotes				nvarchar(MAX),
	@NotCounted				bit,
	@Relationship			int,
	@Phone					varchar(20),
	@EmailAddress			varchar(50),
    @Grade                  int,
    @SchSupply              bit,
    @SchSupplyDelivered     datetime,
    @SchSupplySchool        int,
    @CSFPStatus             int,
	@HispanicLatino			int,
	@RefugeeImmigrant		int,
	@LimitedEnglish			int,
	@MilitaryService		smallint,
	@DischargeStatus		smallint,
	@PartneredMarried		int,
	@LongTermHomeless		int,
	@ChronicallyHomeless	int,
	@Employed				int,
	@EmplolymentStatus		smallint ,
	@AmericanIndian			bit ,
	@AlaskaNative			bit ,
	@IndigenousToAmericas	bit ,
	@AsianIndian			bit ,
	@Cambodian				bit ,
	@Chinese				bit ,
	@Filipino				bit ,
	@Japanese				bit ,
	@Korean					bit ,
	@Vietnamese				bit ,
	@OtherAsian				bit ,
	@IndigenousAfricanBlack bit ,
	@AfricanAmericanBlack	bit ,
	@OtherBlack				bit ,
	@HawaiianNative			bit ,
	@Polynesian				bit ,
	@Micronesian			bit ,
	@OtherPacificIslander	bit ,
	@ArabIranianMiddleEastern bit ,
	@OtherWhiteCaucasian	bit ,
	@EthnicOther			bit ,
	@EthnicUnknown			bit ,
	@EducationLevel			smallint,
	@Homeless				smallint,
	@HomelessNbrTimes		smallint,
	@HomelessNbrMonths		smallint,
	@ID						int

SELECT @Inactive = 0
           ,@HouseholdID = 99999
           ,@LastName = 'Tester'
           ,@FirstName = 'James'
           ,@Sex = 'M'
           ,@Birthdate= '1/4/1948'
           ,@AgeGroup = 4
           ,@SpecialDiet = 0
           ,@Notes = ''
           ,@WorksInArea = 0
           ,@Employer = ''
           ,@EmpAddress = ''
           ,@EmpCity = ''
           ,@EmpZipcode = ''
           ,@EmpPhone = ''
           ,@CreatedBy = 'TESTUSER'
           ,@UserFlag0 = 0
           ,@UserFlag1 = 0
           ,@VolunteersAtFoodBank = 0
           ,@Age = 63
           ,@UseAge = 0
           ,@NotIncludedInClientList = 0
           ,@CSFP = 0
           ,@HeadHH = 1
           ,@Language = 0
           ,@IsDisabled = 0
           ,@CSFPExpiration = null
           ,@CSFPComments = ''
           ,@CSFPRoute = 0
           ,@MemIDNbr = ''
           ,@MemIDType = 0
           ,@Race = 1
           ,@Hispanic = 0
           ,@BackPack=0
		   ,@BPExpiration = null
           ,@BPSize=1
           ,@BPSchool=0
           ,@BPNotes=''
           ,@NotCounted	=0
           ,@Relationship=0
           ,@Phone=''
           ,@EmailAddress=''
           ,@Grade = 0
           ,@SchSupply=0
           ,@SchSupplyDelivered=null
           ,@SchSupplySchool= ''
           ,@CSFPStatus=0
      ,@HispanicLatino=2
      ,@RefugeeImmigrant=2
      ,@LimitedEnglish=2
      ,@MilitaryService=0
      ,@DischargeStatus=0
      ,@PartneredMarried=2
      ,@LongTermHomeless=2
      ,@ChronicallyHomeless=2
      ,@Employed=2
      ,@EmplolymentStatus=0
      ,@AmericanIndian=0
      ,@AlaskaNative=0
      ,@IndigenousToAmericas=0
      ,@AsianIndian=0
      ,@Cambodian=0
      ,@Chinese=0
      ,@Filipino=0
      ,@Japanese=0
      ,@Korean=0
      ,@Vietnamese=0
      ,@OtherAsian=0
      ,@IndigenousAfricanBlack=0
      ,@AfricanAmericanBlack=0
      ,@OtherBlack=0
      ,@HawaiianNative=0
      ,@Polynesian=0
      ,@Micronesian=0
      ,@OtherPacificIslander=0
      ,@ArabIranianMiddleEastern=0
      ,@OtherWhiteCaucasian=0
      ,@EthnicOther=0
      ,@EthnicUnknown=0
      ,@EducationLevel=0
	  ,@Homeless = 1
	  ,@HomelessNbrTimes = 1
	  ,@HomelessNbrMonths = 7
	  ,@ID = 0

EXECUTE dbo.InsertHouseholdMember 
			@Inactive
           ,@HouseholdID
           ,@LastName
           ,@FirstName
           ,@Sex
           ,@Birthdate
           ,@AgeGroup
           ,@SpecialDiet
           ,@Notes
           ,@WorksInArea
           ,@Employer
           ,@EmpAddress
           ,@EmpCity
           ,@EmpZipcode
           ,@EmpPhone
           ,@CreatedBy
           ,@UserFlag0
           ,@UserFlag1
           ,@VolunteersAtFoodBank
           ,@Age
           ,@UseAge
           ,@NotIncludedInClientList
           ,@CSFP
           ,@HeadHH
           ,@Language
           ,@IsDisabled
           ,@CSFPExpiration
           ,@CSFPComments
           ,@CSFPRoute
           ,@MemIDNbr
           ,@MemIDType
           ,@Race
           ,@Hispanic
           ,@BackPack
		   ,@BPExpiration
           ,@BPSize
           ,@BPSchool
           ,@BPNotes
           ,@NotCounted
           ,@Relationship
           ,@Phone
           ,@EmailAddress
           ,@Grade
           ,@SchSupply
           ,@SchSupplyDelivered
           ,@SchSupplySchool
           ,@CSFPStatus
           ,@HispanicLatino
           ,@RefugeeImmigrant
           ,@LimitedEnglish
           ,@MilitaryService
           ,@DischargeStatus
           ,@PartneredMarried
           ,@LongTermHomeless
           ,@ChronicallyHomeless
           ,@Employed
           ,@EmplolymentStatus
           ,@AmericanIndian
           ,@AlaskaNative
           ,@IndigenousToAmericas
           ,@AsianIndian
           ,@Cambodian
           ,@Chinese
           ,@Filipino
           ,@Japanese
           ,@Korean
           ,@Vietnamese
           ,@OtherAsian
           ,@IndigenousAfricanBlack
           ,@AfricanAmericanBlack
           ,@OtherBlack
           ,@HawaiianNative
           ,@Polynesian
           ,@Micronesian
           ,@OtherPacificIslander
           ,@ArabIranianMiddleEastern
           ,@OtherWhiteCaucasian
           ,@EthnicOther
           ,@EthnicUnknown
           ,@EducationLevel
           ,@Homeless
           ,@HomelessNbrTimes
		   ,@HomelessNbrMonths
		   ,@ID OUTPUT

print @ID
SELECT * from householdmembers where householdid = 99999
select * from Demographics where id >= @ID

delete from HouseholdMembers where HouseholdID = 99999
delete from Demographics where NOT Exists(SELECT * FROM HouseholdMembers WHERE Demographics.ID = ID)


*/