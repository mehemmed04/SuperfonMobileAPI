using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class ProductVerification
    {
        public ProductVerification()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int ProductVerificationId { get; set; }

        public string ProductCode { get; set; }

        public bool IsVerified { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? VerifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public string VerifiedBy { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
