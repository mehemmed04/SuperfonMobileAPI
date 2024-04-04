using System;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class BusinessTripDetailDTO
    {
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; } 
        public string Note { get; set; }
    }
}
