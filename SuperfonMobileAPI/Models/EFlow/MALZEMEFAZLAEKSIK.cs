using System;

namespace SuperfonMobileAPI.Models.EFlow
{
    public class MALZEMEFAZLAEKSIK
    {
        public int REFERANSID { get; set; }
        public int MALZEME_ID { get; set; }
        public string MALZEME_KOD { get; set; }
        public string MALZEME_ADI { get; set; }
        public int ANA_AMBAR_ID { get; set; }
        public string ANA_AMBAR_ADI { get; set; }
        public int GIREN_AMBAR_ID { get; set; }
        public string GIREN_AMBAR_ADI { get; set; }
        public int TESLIM_ALAN_ID { get; set; }
        public string TESLIM_ALAN_ADI { get; set; }
        public DateTime AMBAR_FIS_TARIHI { get; set; }
        public string AMBAR_FIS_NO { get; set; }
        public double FISTEKI_MIKTAR { get; set; }
        public double TESLIM_ALINAN_MIKTAR { get; set; }
        public string ACIKLAMA { get; set; }
        public int EFLOW_KONTROL { get; set; }
    }
}
