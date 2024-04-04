using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class ExpenseAdvanceRequestView
    {
        public ExpenseAdvanceRequestView()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int ExpenseAdvanceRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int UserId { get; set; }

        public int? ExpenseDeclarationId { get; set; }

        public byte RequestType { get; set; }

        public string EFlowStatus { get; set; }

        public string ManagerApproveStatus { get; set; }

        public string BudgetApproveStatus { get; set; }

        public string FinanceApproveStatus { get; set; }

        public string DeclarationStatus { get; set; }

        public bool? IsDeclared { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
