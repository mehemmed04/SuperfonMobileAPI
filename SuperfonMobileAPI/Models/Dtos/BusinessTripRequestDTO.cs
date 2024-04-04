using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperfonMobileAPI.Models.Dtos
{
    public class BusinessTripRequestDTO
    {
        public decimal RequestAmount { get; set; }
        public string RequestDescription { get; set; }
        public int TripDaysCount { get; set; }

    }
}
