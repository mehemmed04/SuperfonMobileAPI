using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Entities
{
    public class TigerSTLine
    { /// <summary>
      /// DB : GLOBTRANS. Discount / Surcharge and Promotion Lines  ;0 Line;1 General
      /// </summary>
        public byte DetailLevel { get; set; }
        /// <summary>
        /// Calculation Type ;0 Percentage %;1 Function f(x);2 Amount  
        /// </summary>
        public byte CalcType { get; set; }
        public int LineId { get; set; }
        public int LineNumber { get; set; }
        public byte LineType { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string UnitCode { get; set; }
        public string MainUnitCode { get; set; }
        public string SpeCode { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal UnitConv { get; set; }
        public int OrderLineRef { get; set; }
        public int CampaignLineRef { get; set; }
        public string CampaignCode1 { get; set; }
        public string CampaignCode2 { get; set; }
        public string CampaignCode3 { get; set; }
        public int? CampaignLineNumber { get; set; }
        public decimal DiscountRate { get; set; }
    }
}
