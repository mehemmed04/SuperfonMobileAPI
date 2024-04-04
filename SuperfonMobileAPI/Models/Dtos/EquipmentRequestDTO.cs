using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class EquipmentRequestDTO
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Note { get; set; }
    }
}
