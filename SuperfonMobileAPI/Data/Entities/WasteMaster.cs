using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class WasteMaster
    {
        public WasteMaster()
        {
            #region Generated Constructor
            WasteDetails = new HashSet<WasteDetail>();
            #endregion
        }

        #region Generated Properties
        public int WasteMasterId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public int Warehouse { get; set; }

        public bool IsIntegrated { get; set; }

        public DateTime? IntegratedDate { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        public virtual ICollection<WasteDetail> WasteDetails { get; set; }

        #endregion

    }
}
