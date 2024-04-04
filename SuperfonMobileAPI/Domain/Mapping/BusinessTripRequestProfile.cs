using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class BusinessTripRequestProfile
        : AutoMapper.Profile
    {
        public BusinessTripRequestProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.BusinessTripRequest, SuperfonWorks.Domain.Models.BusinessTripRequestReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripRequestCreateModel, SuperfonWorks.Data.Entities.BusinessTripRequest>();

            CreateMap<SuperfonWorks.Data.Entities.BusinessTripRequest, SuperfonWorks.Domain.Models.BusinessTripRequestUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripRequestUpdateModel, SuperfonWorks.Data.Entities.BusinessTripRequest>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripRequestReadModel, SuperfonWorks.Domain.Models.BusinessTripRequestUpdateModel>();

        }

    }
}
