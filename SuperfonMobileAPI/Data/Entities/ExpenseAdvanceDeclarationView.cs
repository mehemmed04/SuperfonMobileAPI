using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class ExpenseAdvanceDeclarationView
    {
        public ExpenseAdvanceDeclarationView()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int ExpenseDeclarationId { get; set; }

        public DateTime DeclarationDate { get; set; }

        public int UserId { get; set; }

        public string DeclarationNote { get; set; }

        public string EFlowStatus { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
