using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class SalaryAdvanceRequestCreateModel
    {
        #region Generated Properties
        public int SalaryAdvanceRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int UserId { get; set; }

        public int PartitionCount { get; set; }

        #endregion

    }
}
