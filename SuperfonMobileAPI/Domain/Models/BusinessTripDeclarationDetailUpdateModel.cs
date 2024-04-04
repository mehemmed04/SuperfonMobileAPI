using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class BusinessTripDeclarationDetailUpdateModel
    {
        #region Generated Properties
        public int BusinessTripDeclarationDetailId { get; set; }

        public int BusinessTripDeclarationId { get; set; }

        public string ExpenseDescription { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }

        #endregion

    }
}
