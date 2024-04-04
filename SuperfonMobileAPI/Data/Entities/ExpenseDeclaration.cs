using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class ExpenseDeclaration
    {
        public ExpenseDeclaration()
        {
            #region Generated Constructor
            ExpenseAdvanceRequests = new HashSet<ExpenseAdvanceRequest>();
            ExpenseDeclarationDetails = new HashSet<ExpenseDeclarationDetail>();
            #endregion
        }

        #region Generated Properties
        public int ExpenseDeclarationId { get; set; }

        public DateTime DeclarationDate { get; set; }

        public int UserId { get; set; }

        public string DeclarationNote { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<ExpenseAdvanceRequest> ExpenseAdvanceRequests { get; set; }

        public virtual ICollection<ExpenseDeclarationDetail> ExpenseDeclarationDetails { get; set; }

        public virtual User User { get; set; }

        #endregion

    }
}
