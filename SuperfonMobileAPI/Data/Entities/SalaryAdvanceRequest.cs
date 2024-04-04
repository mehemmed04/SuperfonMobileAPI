using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class SalaryAdvanceRequest
    {
        public SalaryAdvanceRequest()
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

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        #endregion

    }
}
