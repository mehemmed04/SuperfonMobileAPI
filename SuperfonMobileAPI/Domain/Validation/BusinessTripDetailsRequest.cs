using System;

namespace SuperfonMobileAPI.Domain.Validation
{
    public class BusinessTripDetailsRequest
    {
        public int BusinessTripId { get; set; }
        public DateTime Date { get; set; }
        public string BusinessTripNumber { get; set; }
        public string Note { get; set; }
        public string CashboxCode { get; set; }
        public int StatusId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
        public string StatusName { get; set; }
        public DateTime RequestDate { get; set; }
        public float RequestTotal { get; set; }
        public string RequestDescription { get; set; }
    }
}
