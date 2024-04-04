using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class WasteProductDTO
    {
        public int CustomerId { get; set; }
        public List<WasteProductDetailsDTO> Details { get; set; }
        public class WasteProductDetailsDTO
        {
            public string ProductCode { get; set; }
            public string Barcode { get; set; }
            public decimal Quantity { get; set; }
        }
    }
}
