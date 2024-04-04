using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class WasteDetail
    {
        public WasteDetail()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int WasteDetailId { get; set; }

        public int WasteMasterId { get; set; }

        public string ProductCode { get; set; }

        public string Barcode { get; set; }

        public decimal Quantity { get; set; }

        #endregion

        #region Generated Relationships
        public virtual WasteMaster WasteMaster { get; set; }

        #endregion

    }
}
