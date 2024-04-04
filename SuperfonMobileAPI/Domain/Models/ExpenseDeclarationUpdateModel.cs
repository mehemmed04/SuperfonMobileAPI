using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class ExpenseDeclarationUpdateModel
    {
        #region Generated Properties
        public int ExpenseDeclarationId { get; set; }

        public DateTime DeclarationDate { get; set; }

        public int UserId { get; set; }

        public string DeclarationNote { get; set; }

        #endregion

    }
}
