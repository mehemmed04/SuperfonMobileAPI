using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class UserCardCodePermissionCreateModel
    {
        #region Generated Properties
        public int UserCardCodePermissionId { get; set; }

        public byte CardPermissionTypeId { get; set; }

        public string CardCode { get; set; }

        public int UserId { get; set; }

        #endregion

    }
}
