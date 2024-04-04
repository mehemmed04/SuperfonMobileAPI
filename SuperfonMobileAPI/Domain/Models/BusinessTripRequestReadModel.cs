using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class BusinessTripRequestReadModel
    {
        #region Generated Properties
        public int BusinessTripRequestId { get; set; }

        public DateTime RequestDate { get; set; }

        public decimal RequestAmount { get; set; }

        public string RequestDescription { get; set; }

        public int TripDaysCount { get; set; }

        public int UserId { get; set; }

        public int? BusinessTripDeclarationId { get; set; }

        #endregion

    }
}
