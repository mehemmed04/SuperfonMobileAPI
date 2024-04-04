using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class EquipmentRequest
    {
        public EquipmentRequest()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int EquipmentRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public string RequestDescription { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }

        public string Note { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        #endregion

    }
}
