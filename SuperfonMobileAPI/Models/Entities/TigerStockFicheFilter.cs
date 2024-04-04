using System;

namespace SuperfonMobileAPI.Models.Entities
{
    public class TigerStockFicheFilter
    {
        public int? StockFicheId { get; set; }
        public short[] TransactionCodes { get; set; }
        public string Ficheno { get; set; }
        public string SourceDocumentNumber { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string CyphCode { get; set; }
        public string SpeCode { get; set; }
        public int[] SourceWarehouses { get; set; }
        public int[] DestWarehouses { get; set; }
        public short[] SourceBranches { get; set; }
        public short[] DestBranches { get; set; }
        public short[] RecordStatuses { get; set; }
        public bool? Printed { get; set; }
        public bool? Cancelled { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }
        public int? WFStatus { get; set; }
        public string DocTrackingNumber { get; set; }
    }
}
