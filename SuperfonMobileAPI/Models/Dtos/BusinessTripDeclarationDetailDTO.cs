using System;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class BusinessTripDeclarationDetailDTO
    {
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
    }
}
