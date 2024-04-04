using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class ExpenseAdvanceRequestReadModel
    {
        #region Generated Properties
        public int ExpenseAdvanceRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int UserId { get; set; }

        public int? ExpenseDeclarationId { get; set; }

        public byte RequestType { get; set; }

        #endregion

    }
}
