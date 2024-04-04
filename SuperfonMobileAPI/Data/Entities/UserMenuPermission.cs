using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class UserMenuPermission
    {
        public UserMenuPermission()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int UserMenuPermissionId { get; set; }

        public int MenuPermissionId { get; set; }

        public int? UserId { get; set; }

        public int? GroupId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual UserGroup GroupUserGroup { get; set; }

        public virtual MenuPermission MenuPermission { get; set; }

        public virtual User User { get; set; }

        #endregion

    }
}
