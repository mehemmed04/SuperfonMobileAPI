using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class MenuPermission
    {
        public MenuPermission()
        {
            #region Generated Constructor
            UserMenuPermissions = new HashSet<UserMenuPermission>();
            #endregion
        }

        #region Generated Properties
        public int MenuPermissionId { get; set; }

        public int ParentId { get; set; }

        public byte MenuPermissionTypeId { get; set; }

        public string PermissionName { get; set; }

        public string KeyWord { get; set; }

        public bool IsActive { get; set; }

        public string IconName { get; set; }

        public string Link { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<UserMenuPermission> UserMenuPermissions { get; set; }

        #endregion

    }
}
