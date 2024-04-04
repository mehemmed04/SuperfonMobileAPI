using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class TayqaRefundCheckingDTO
    {
        public int WhouseNr { get; set; }
        public int ClientId { get; set; }  
        public string DocNumber { get; set; } 
        public string Specode { get; set; } 
        public string DocId { get; set; }
        public string Note { get; set; } 
        public int SalesmanRef { get; set; } 
        public int Firm { get; set; }
        public long ProcessDateTimestamp { get; set; }

        public List<TayqaRefundLineDTO> Lines { get; set; }
    }

    public class TayqaRefundResultDTO
    {
        public List<TayqaRefundLineDTO> Lines { get; set; }
    }

    public class TayqaRefundLineDTO
    {
        public int ItemId { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public string ItemUnitCode { get; set; }
        public string[] SerialNumbers { get; set; }
    }
}
