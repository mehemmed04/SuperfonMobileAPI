using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class SalaryAdvanceRequestView
    {
        public SalaryAdvanceRequestView()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int SalaryAdvanceRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int UserId { get; set; }

        public int PartitionCount { get; set; }

        public string EFlowStatus { get; set; }

        public string DepartmentManagerApproveStatus { get; set; }

        public string HRManagerApproveStatus { get; set; }

        public string FinanceManagerApproveStatus { get; set; }

        public string CashierApproveStatus { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
