using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class ExpenseDeclarationDetail
    {
        public ExpenseDeclarationDetail()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int ExpenseDeclarationDetailId { get; set; }

        public int ExpenseDeclarationId { get; set; }

        public string ExpenseDescription { get; set; }

        public decimal ExpenseAmount { get; set; }

        public DateTime Date { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ExpenseDeclaration ExpenseDeclaration { get; set; }

        #endregion

    }
}
