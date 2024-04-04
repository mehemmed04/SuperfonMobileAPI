using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class BusinessTripDeclarationDetail
    {
        public BusinessTripDeclarationDetail()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int BusinessTripDeclarationDetailId { get; set; }

        public int BusinessTripDeclarationId { get; set; }

        public string ExpenseDescription { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        #endregion

        #region Generated Relationships
        public virtual BusinessTripDeclaration BusinessTripDeclaration { get; set; }

        #endregion

    }
}
