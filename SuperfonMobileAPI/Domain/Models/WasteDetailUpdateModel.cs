using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class WasteDetailUpdateModel
    {
        #region Generated Properties
        public int WasteDetailId { get; set; }

        public int WasteMasterId { get; set; }

        public string ProductCode { get; set; }

        public string Barcode { get; set; }

        public decimal Quantity { get; set; }

        #endregion

    }
}
