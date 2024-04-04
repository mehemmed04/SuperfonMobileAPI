using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            #region Generated Constructor
            GroupUserMenuPermissions = new HashSet<UserMenuPermission>();
            Users = new HashSet<User>();
            #endregion
        }

        #region Generated Properties
        public int UserGroupId { get; set; }

        public string UserGroupName { get; set; }

        public bool IsActive { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<UserMenuPermission> GroupUserMenuPermissions { get; set; }

        public virtual ICollection<User> Users { get; set; }

        #endregion

    }
}
