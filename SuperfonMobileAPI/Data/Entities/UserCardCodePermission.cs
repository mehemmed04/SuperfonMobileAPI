using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class UserCardCodePermission
    {
        public UserCardCodePermission()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int UserCardCodePermissionId { get; set; }

        public byte CardPermissionTypeId { get; set; }

        public string CardCode { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        #endregion

    }
}
