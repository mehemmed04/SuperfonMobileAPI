using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Entities
{
    public class TigerSTFiche
    {
        public int StockFicheId { get; set; }
        public short TransactionCode { get; set; }
        public short GroupCode { get; set; }
        public string Ficheno { get; set; }
        public string SourceDocumentNumber { get; set; }
        public DateTime Date { get; set; }
        public int SourceWarehouse { get; set; }
        public string SourceWarehouseName { get; set; }
        public int DestWarehouse { get; set; }
        public string DestWarehouseName { get; set; }
        public short SourceBranch { get; set; }
        public short DestBranch { get; set; }
        public List<TigerSTLine> Lines { get; set; }
        public string DocTrackingNumber { get; set; }
        public int DocumentType { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string CyphCode { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }
        public string PaymentCode { get; set; }
        public string SpeCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public int WFStatus { get; set; }
    }
}
