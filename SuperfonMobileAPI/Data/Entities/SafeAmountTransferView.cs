using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class SafeAmountTransferView
    {
        public SafeAmountTransferView()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int SafeAmountTransferId { get; set; }

        public int UserId { get; set; }

        public byte TransferType { get; set; }

        public string SourceSafeboxCode { get; set; }

        public string DestinationCode { get; set; }

        public double Amount { get; set; }

        public string Note { get; set; }

        public DateTime DateCreated { get; set; }

        public string SourceSafeboxName { get; set; }

        public string DestinationName { get; set; }

        public string EFlowStatus { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
