using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class WasteMasterReadModel
    {
        #region Generated Properties
        public int WasteMasterId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public int Warehouse { get; set; }

        public bool IsIntegrated { get; set; }

        public DateTime? IntegratedDate { get; set; }

        #endregion

    }
}
