using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class UserSafeboxPermission
    {
        public UserSafeboxPermission()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int UserSafeboxPermissionId { get; set; }

        public string SafeboxCode { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        #endregion

    }
}
