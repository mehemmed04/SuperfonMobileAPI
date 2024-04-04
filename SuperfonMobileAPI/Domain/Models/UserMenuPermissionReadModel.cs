using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class UserMenuPermissionReadModel
    {
        #region Generated Properties
        public int UserMenuPermissionId { get; set; }

        public int MenuPermissionId { get; set; }

        public int? UserId { get; set; }

        public int? GroupId { get; set; }

        #endregion

    }
}
