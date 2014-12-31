------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '07/17/2014', '2014.07.17'
,'Added parm_SchSupplyRegistration, parm_SchSupplySchol
,InsertHouseholdMembers'
,GetDate(), 'CSDG')
SELECT * FROM FB3 order BY EXEVersion
GO

TRUNCATE TABLE [dbo].[parm_SchSupplyRegistration]
SET IDENTITY_INSERT   [dbo].[parm_SchSupplyRegistration] ON

INSERT INTO [dbo].[parm_SchSupplyRegistration] (ID,[Type],[SortOrder],[ShortName])
VALUES (0, 'Foodbank',0, 'FB')

SET IDENTITY_INSERT   [dbo].[parm_SchSupplyRegistration] OFF

TRUNCATE TABLE [dbo].[parm_SchSupplySchool]
SET IDENTITY_INSERT   [dbo].[parm_SchSupplySchool] ON

INSERT INTO [dbo].[parm_SchSupplySchool] (ID,[Type],[SortOrder],[ShortName])
VALUES (0, 'Not Selected',0, 'NS')

SET IDENTITY_INSERT   [dbo].[parm_SchSupplySchool] OFF

GO

/****** Object:  StoredProcedure [dbo].[InsertHouseholdMember]    Script Date: 07/25/2014 10:54:46 AM ******/
DROP PROCEDURE [dbo].[InsertHouseholdMember]
GO

/****** Object:  StoredProcedure [dbo].[InsertHouseholdMember]    Script Date: 07/25/2014 10:54:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
GO



