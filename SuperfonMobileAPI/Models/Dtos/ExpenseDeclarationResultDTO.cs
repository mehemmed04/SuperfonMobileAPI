using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class ExpenseDeclarationResultDTO
    {
        public int ExpenseDeclarationId { get; set; }

        public DateTime DeclarationDate { get; set; }

        public int UserId { get; set; }

        public string DeclarationNote { get; set; }
        public string EFlowStatus { get; set; }

        public List<ExpenseDeclarationResultDetailDTO> Details { get; set; }
        public class ExpenseDeclarationResultDetailDTO
        {
            public int ExpenseDeclarationDetailId { get; set; }

            public int ExpenseDeclarationId { get; set; }

            public string ExpenseDescription { get; set; }

            public decimal ExpenseAmount { get; set; }

            public DateTime Date { get; set; }
        }
    }
}
