using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class ExpenseDeclarationDTO
    {
        public int[] ExpenseAdvanceRequestIds { get; set; }
        public string DeclarationNote { get; set; }
        public List<ExpenseDeclarationDetailDTO> Details { get; set; }

        public class ExpenseDeclarationDetailDTO
        {
            public string ExpenseDescription {get;set;}
            public decimal ExpenseAmount { get; set; }
            public DateTime Date { get; set; }
        }
    }

}
