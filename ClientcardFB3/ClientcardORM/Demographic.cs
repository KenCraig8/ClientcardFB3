namespace ClientcardFB3.ClientcardORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Demographic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? HispanicLatino { get; set; }

        public int? RefugeeImmigrant { get; set; }

        public int? LimitedEnglish { get; set; }

        public short? MilitaryService { get; set; }

        public short? DischargeStatus { get; set; }

        public int? PartneredMarried { get; set; }

        public int? LongTermHomeless { get; set; }

        public int? ChronicallyHomeless { get; set; }

        public int? Employed { get; set; }

        public short? EmplolymentStatus { get; set; }

        public bool? AmericanIndian { get; set; }

        public bool? AlaskaNative { get; set; }

        public bool? IndigenousToAmericas { get; set; }

        public bool? AsianIndian { get; set; }

        public bool? Cambodian { get; set; }

        public bool? Chinese { get; set; }

        public bool? Filipino { get; set; }

        public bool? Japanese { get; set; }

        public bool? Korean { get; set; }

        public bool? Vietnamese { get; set; }

        public bool? OtherAsian { get; set; }

        public bool? IndigenousAfricanBlack { get; set; }

        public bool? AfricanAmericanBlack { get; set; }

        public bool? OtherBlack { get; set; }

        public bool? HawaiianNative { get; set; }

        public bool? Polynesian { get; set; }

        public bool? Micronesian { get; set; }

        public bool? OtherPacificIslander { get; set; }

        public bool? ArabIranianMiddleEastern { get; set; }

        public bool? OtherWhiteCaucasian { get; set; }

        public bool? EthnicOther { get; set; }

        public bool? EthnicUnknown { get; set; }

        public short? EducationLevel { get; set; }

        public short? Homeless { get; set; }

        public short? HomelessNbrTimes { get; set; }

        public short? HomelessNbrMonths { get; set; }
    }
}
