using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class ExpenseDeclarationDetailReadModel
    {
        #region Generated Properties
        public int ExpenseDeclarationDetailId { get; set; }

        public int ExpenseDeclarationId { get; set; }

        public string ExpenseDescription { get; set; }

        public decimal ExpenseAmount { get; set; }

        public DateTime Date { get; set; }

        #endregion

    }
}
