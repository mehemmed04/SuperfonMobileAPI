using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class ExpenseDTO
    {
        public byte Priority { get; set; }
        public string ServiceCardCode { get; set; }
        public string Note { get; set; }
        public decimal ExpenseAmount { get; set; }

    }
}
