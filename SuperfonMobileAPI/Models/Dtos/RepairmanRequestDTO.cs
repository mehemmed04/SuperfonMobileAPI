using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class RepairmanRequestDTO
    {
        public decimal SparePartPrice { get; set; }

        public decimal RepairFee { get; set; }

        public string Note { get; set; }
    }
}
