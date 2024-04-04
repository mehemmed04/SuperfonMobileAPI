using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class EquipmentRequestViewCreateModel
    {
        #region Generated Properties
        public int EquipmentRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public string RequestDescription { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }

        public string Note { get; set; }

        public string EFlowStatus { get; set; }

        #endregion

    }
}
