using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class UserBranchPermission
    {
        public UserBranchPermission()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int UserBranchPermissionId { get; set; }

        public int BranchNumber { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        #endregion

    }
}
