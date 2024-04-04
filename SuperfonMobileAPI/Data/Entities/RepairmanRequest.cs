using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class RepairmanRequest
    {
        public RepairmanRequest()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int RepairmanRequestId { get; set; }

        public string Ficheno { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal SparePartPrice { get; set; }

        public decimal RepairFee { get; set; }

        public string Note { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
