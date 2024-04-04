using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class ExpenseAdvanceRequest
    {
        public ExpenseAdvanceRequest()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int ExpenseAdvanceRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int UserId { get; set; }

        public int? ExpenseDeclarationId { get; set; }

        public byte RequestType { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ExpenseDeclaration ExpenseDeclaration { get; set; }

        public virtual User User { get; set; }

        #endregion

    }
}
