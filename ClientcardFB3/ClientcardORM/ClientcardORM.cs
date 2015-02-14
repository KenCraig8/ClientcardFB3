namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ClientcardORM : DbContext
    {
        public ClientcardORM()
            : base("name=ClientcardORMCon")
        {
        }

        public virtual DbSet<AccessReport> AccessReports { get; set; }
        public virtual DbSet<BackPackLog> BackPackLogs { get; set; }
        public virtual DbSet<CashDonation> CashDonations { get; set; }
        public virtual DbSet<CSFPLog> CSFPLogs { get; set; }
        public virtual DbSet<DaysOpen> DaysOpens { get; set; }
        public virtual DbSet<Default> Defaults { get; set; }
        public virtual DbSet<Demographic> Demographics { get; set; }
        public virtual DbSet<Donor> Donors { get; set; }
        public virtual DbSet<EmailRecipient> EmailRecipients { get; set; }
        public virtual DbSet<FamilyCardSig> FamilyCardSigs { get; set; }
        public virtual DbSet<FB3> FB3 { get; set; }
        public virtual DbSet<FBJobsActual> FBJobsActuals { get; set; }
        public virtual DbSet<FBJobsPlan> FBJobsPlans { get; set; }
        public virtual DbSet<FoodDonation> FoodDonations { get; set; }
        public virtual DbSet<HDBuilding> HDBuildings { get; set; }
        public virtual DbSet<HDDeliveryDay> HDDeliveryDays { get; set; }
        public virtual DbSet<HDLbsPerService> HDLbsPerServices { get; set; }
        public virtual DbSet<HDRoute> HDRoutes { get; set; }
        public virtual DbSet<HDRouteSheet> HDRouteSheets { get; set; }
        public virtual DbSet<HDRSClient> HDRSClients { get; set; }
        public virtual DbSet<HHPoint> HHPoints { get; set; }
        public virtual DbSet<Household> Households { get; set; }
        public virtual DbSet<HouseholdMember> HouseholdMembers { get; set; }
        public virtual DbSet<IncomeGroup> IncomeGroups { get; set; }
        public virtual DbSet<IncomeMatrix> IncomeMatrices { get; set; }
        public virtual DbSet<MonthlyReport> MonthlyReports { get; set; }
        public virtual DbSet<parm_AddressID> parm_AddressID { get; set; }
        public virtual DbSet<parm_BackPackSchool> parm_BackPackSchool { get; set; }
        public virtual DbSet<parm_BackPackSize> parm_BackPackSize { get; set; }
        public virtual DbSet<parm_ClientType> parm_ClientType { get; set; }
        public virtual DbSet<parm_CSFPRoutes> parm_CSFPRoutes { get; set; }
        public virtual DbSet<parm_CSFPSortOrder> parm_CSFPSortOrder { get; set; }
        public virtual DbSet<parm_CSFPStatus> parm_CSFPStatus { get; set; }
        public virtual DbSet<parm_DonationType> parm_DonationType { get; set; }
        public virtual DbSet<parm_DonorType> parm_DonorType { get; set; }
        public virtual DbSet<parm_EducationLevel> parm_EducationLevel { get; set; }
        public virtual DbSet<parm_EmploymentStatus> parm_EmploymentStatus { get; set; }
        public virtual DbSet<parm_FBJobs> parm_FBJobs { get; set; }
        public virtual DbSet<parm_FBProgram> parm_FBProgram { get; set; }
        public virtual DbSet<parm_FoodClass> parm_FoodClass { get; set; }
        public virtual DbSet<parm_Gender> parm_Gender { get; set; }
        public virtual DbSet<parm_HDBldgOperator> parm_HDBldgOperator { get; set; }
        public virtual DbSet<parm_HDPrograms> parm_HDPrograms { get; set; }
        public virtual DbSet<parm_HDRouteSheetStatus> parm_HDRouteSheetStatus { get; set; }
        public virtual DbSet<parm_HomeDeliveryRoutes> parm_HomeDeliveryRoutes { get; set; }
        public virtual DbSet<parm_HUDCategory> parm_HUDCategory { get; set; }
        public virtual DbSet<parm_IDType> parm_IDType { get; set; }
        public virtual DbSet<parm_IncomeProcessID> parm_IncomeProcessID { get; set; }
        public virtual DbSet<parm_LanguageType> parm_LanguageType { get; set; }
        public virtual DbSet<parm_MilitaryDischarge> parm_MilitaryDischarge { get; set; }
        public virtual DbSet<parm_MilitaryService> parm_MilitaryService { get; set; }
        public virtual DbSet<parm_PhoneType> parm_PhoneType { get; set; }
        public virtual DbSet<parm_Race> parm_Race { get; set; }
        public virtual DbSet<parm_Relationship> parm_Relationship { get; set; }
        public virtual DbSet<parm_RouteSheetStatus> parm_RouteSheetStatus { get; set; }
        public virtual DbSet<parm_SchSupplyRegistration> parm_SchSupplyRegistration { get; set; }
        public virtual DbSet<parm_SchSupplySchool> parm_SchSupplySchool { get; set; }
        public virtual DbSet<parm_ServiceGroup> parm_ServiceGroup { get; set; }
        public virtual DbSet<parm_ServiceMethod> parm_ServiceMethod { get; set; }
        public virtual DbSet<parm_SurveyFields> parm_SurveyFields { get; set; }
        public virtual DbSet<parm_SvcCategory> parm_SvcCategory { get; set; }
        public virtual DbSet<parm_SvcRules> parm_SvcRules { get; set; }
        public virtual DbSet<parm_Transportation> parm_Transportation { get; set; }
        public virtual DbSet<parm_TrueFalse> parm_TrueFalse { get; set; }
        public virtual DbSet<parm_VolunteerGroups> parm_VolunteerGroups { get; set; }
        public virtual DbSet<parm_VolunteerType> parm_VolunteerType { get; set; }
        public virtual DbSet<parm_VoucherType> parm_VoucherType { get; set; }
        public virtual DbSet<parm_YesNoUnk> parm_YesNoUnk { get; set; }
        public virtual DbSet<Preference> Preferences { get; set; }
        public virtual DbSet<ServiceItem> ServiceItems { get; set; }
        public virtual DbSet<SignaturePrompt> SignaturePrompts { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TrxLog> TrxLogs { get; set; }
        public virtual DbSet<TrxLogSig> TrxLogSigs { get; set; }
        public virtual DbSet<UserField> UserFields { get; set; }
        public virtual DbSet<Userlist> Userlists { get; set; }
        public virtual DbSet<VolunteerHour> VolunteerHours { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<VoucherItem> VoucherItems { get; set; }
        public virtual DbSet<VoucherLog> VoucherLogs { get; set; }
        public virtual DbSet<Zipcode> Zipcodes { get; set; }
        public virtual DbSet<HDItem> HDItems { get; set; }
        public virtual DbSet<VolunteerGroup> VolunteerGroups { get; set; }
        public virtual DbSet<zData> zDatas { get; set; }
        public virtual DbSet<TrxLogHouseholdsCalFirstService> TrxLogHouseholdsCalFirstServices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessReport>()
                .Property(e => e.Grouping)
                .IsUnicode(false);

            modelBuilder.Entity<CashDonation>()
                .Property(e => e.DollarValue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CSFPLog>()
                .Property(e => e.Period)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DaysOpen>()
                .Property(e => e.SpecialItems)
                .IsUnicode(false);

            modelBuilder.Entity<FamilyCardSig>()
                .Property(e => e.DocPath)
                .IsUnicode(false);

            modelBuilder.Entity<FamilyCardSig>()
                .Property(e => e.SigString)
                .IsUnicode(false);

            modelBuilder.Entity<FamilyCardSig>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FB3>()
                .Property(e => e.ExeVersion)
                .IsUnicode(false);

            modelBuilder.Entity<FB3>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<FB3>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsActual>()
                .Property(e => e.ShiftStart)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsActual>()
                .Property(e => e.ShiftEnd)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsActual>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsActual>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsActual>()
                .Property(e => e.TimePostedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsPlan>()
                .Property(e => e.JobTitle)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsPlan>()
                .Property(e => e.ShiftStart)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsPlan>()
                .Property(e => e.ShiftEnd)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsPlan>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FBJobsPlan>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FoodDonation>()
                .Property(e => e.DollarValue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.BldgName)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.BldgAddress)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.BldgCity)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.BldgState)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.BldgZip)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.BldgOperator)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.ContactName)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.ContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.ContactAptNbr)
                .IsUnicode(false);

            modelBuilder.Entity<HDBuilding>()
                .Property(e => e.ContactEmail)
                .IsUnicode(false);

            modelBuilder.Entity<HDDeliveryDay>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<HDLbsPerService>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<HDRoute>()
                .Property(e => e.RouteTitle)
                .IsUnicode(false);

            modelBuilder.Entity<HDRoute>()
                .Property(e => e.FBContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<HDRoute>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<HDRouteSheet>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<HDRouteSheet>()
                .Property(e => e.FBContactPhone)
                .IsUnicode(false);

            modelBuilder.Entity<HDRouteSheet>()
                .Property(e => e.BagDescr)
                .IsUnicode(false);

            modelBuilder.Entity<HDRSClient>()
                .Property(e => e.ClientComments)
                .IsUnicode(false);

            modelBuilder.Entity<HDRSClient>()
                .Property(e => e.DriverNotes)
                .IsUnicode(false);

            modelBuilder.Entity<HDRSClient>()
                .Property(e => e.CreatedBY)
                .IsUnicode(false);

            modelBuilder.Entity<HDRSClient>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Household>()
                .Property(e => e.BabySvcDescr)
                .IsUnicode(false);

            modelBuilder.Entity<Household>()
                .Property(e => e.AptNbr)
                .IsUnicode(false);

            modelBuilder.Entity<Household>()
                .Property(e => e.DriverNotes)
                .IsUnicode(false);

            modelBuilder.Entity<Household>()
                .Property(e => e.AlertText)
                .IsUnicode(false);

            modelBuilder.Entity<HouseholdMember>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HouseholdMember>()
                .Property(e => e.MemIDNbr)
                .IsUnicode(false);

            modelBuilder.Entity<HouseholdMember>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<HouseholdMember>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeGroup>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeGroup>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeMatrix>()
                .Property(e => e.Label1)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeMatrix>()
                .Property(e => e.Label2)
                .IsUnicode(false);

            modelBuilder.Entity<IncomeMatrix>()
                .Property(e => e.Label3)
                .IsUnicode(false);

            modelBuilder.Entity<MonthlyReport>()
                .Property(e => e.ReportName)
                .IsUnicode(false);

            modelBuilder.Entity<MonthlyReport>()
                .Property(e => e.ReportPath)
                .IsUnicode(false);

            modelBuilder.Entity<MonthlyReport>()
                .Property(e => e.DocType)
                .IsUnicode(false);

            modelBuilder.Entity<parm_SurveyFields>()
                .Property(e => e.CtrlSource)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceItem>()
                .Property(e => e.SvcMask)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SignaturePrompt>()
                .Property(e => e.PromptText)
                .IsUnicode(false);

            modelBuilder.Entity<SignaturePrompt>()
                .Property(e => e.RightButtonText)
                .IsUnicode(false);

            modelBuilder.Entity<TrxLog>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<TrxLog>()
                .Property(e => e.HHMemID)
                .IsUnicode(false);

            modelBuilder.Entity<TrxLog>()
                .Property(e => e.Gender)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TrxLogSig>()
                .Property(e => e.SigString)
                .IsUnicode(false);

            modelBuilder.Entity<TrxLogSig>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<UserField>()
                .Property(e => e.AutoAlertText)
                .IsUnicode(false);

            modelBuilder.Entity<VolunteerHour>()
                .Property(e => e.VolTimeIn)
                .IsUnicode(false);

            modelBuilder.Entity<VolunteerHour>()
                .Property(e => e.VolTimeOut)
                .IsUnicode(false);

            modelBuilder.Entity<Volunteer>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Volunteer>()
                .Property(e => e.FBIDNbr)
                .IsUnicode(false);

            modelBuilder.Entity<Volunteer>()
                .Property(e => e.EMailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<VoucherLog>()
                .Property(e => e.Amount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HDItem>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<HDItem>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<zData>()
                .Property(e => e.DBVersion)
                .IsUnicode(false);

            modelBuilder.Entity<zData>()
                .Property(e => e.ExeVersion)
                .IsUnicode(false);

            modelBuilder.Entity<zData>()
                .Property(e => e.LicensedTo)
                .IsUnicode(false);

            modelBuilder.Entity<zData>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TrxLogHouseholdsCalFirstService>()
                .Property(e => e.FiscalQuarter)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TrxLogHouseholdsCalFirstService>()
                .Property(e => e.FiscalPeriod)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TrxLogHouseholdsCalFirstService>()
                .Property(e => e.YearMonth)
                .IsUnicode(false);

            modelBuilder.Entity<TrxLogHouseholdsCalFirstService>()
                .Property(e => e.ZipCode)
                .IsUnicode(false);
        }
    }
}
