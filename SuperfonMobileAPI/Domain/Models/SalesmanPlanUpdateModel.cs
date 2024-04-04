using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class SalesmanPlanUpdateModel
    {
        #region Generated Properties
        public int Id { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }

        public string SalesmanCode { get; set; }

        public double? PlannedSale { get; set; }

        #endregion

    }
}
