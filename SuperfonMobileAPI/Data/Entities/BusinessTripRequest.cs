using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class BusinessTripRequest
    {
        public BusinessTripRequest()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int BusinessTripRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int TripDaysCount { get; set; }

        public int UserId { get; set; }

        public int? BusinessTripDeclarationId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual BusinessTripDeclaration BusinessTripDeclaration { get; set; }

        public virtual User User { get; set; }

        #endregion

    }
}
