using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class SafeAmountTransferCreateModel
    {
        #region Generated Properties
        public int SafeAmountTransferId { get; set; }

        public int UserId { get; set; }

        public byte TransferType { get; set; }

        public string SourceSafeboxCode { get; set; }

        public string DestinationCode { get; set; }

        public double Amount { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }

        #endregion

    }
}
