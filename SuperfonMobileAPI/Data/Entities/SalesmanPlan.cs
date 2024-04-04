using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class SalesmanPlan
    {
        public SalesmanPlan()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int Id { get; set; }

        public int? Year { get; set; }

        public int? Month { get; set; }

        public string SalesmanCode { get; set; }

        public double? PlannedSale { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
