using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class BusinessTripDeclaration
    {
        public BusinessTripDeclaration()
        {
            #region Generated Constructor
            BusinessTripDeclarationDetails = new HashSet<BusinessTripDeclarationDetail>();
            BusinessTripRequests = new HashSet<BusinessTripRequest>();
            #endregion
        }

        #region Generated Properties
        public int BusinessTripDeclarationId { get; set; }

        public DateTime DeclarationDate { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual ICollection<BusinessTripDeclarationDetail> BusinessTripDeclarationDetails { get; set; }

        public virtual ICollection<BusinessTripRequest> BusinessTripRequests { get; set; }

        public virtual User User { get; set; }

        #endregion

    }
}
