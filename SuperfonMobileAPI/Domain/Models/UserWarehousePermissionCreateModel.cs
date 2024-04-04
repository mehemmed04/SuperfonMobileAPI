using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class UserWarehousePermissionCreateModel
    {
        #region Generated Properties
        public int UserWarehousePermissionId { get; set; }

        public byte WarehousePermissionTypeId { get; set; }

        public int WarehouseNumber { get; set; }

        public int UserId { get; set; }

        #endregion

    }
}
