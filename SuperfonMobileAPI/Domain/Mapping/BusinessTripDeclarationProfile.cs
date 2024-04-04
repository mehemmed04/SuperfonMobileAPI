using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class BusinessTripDeclarationProfile
        : AutoMapper.Profile
    {
        public BusinessTripDeclarationProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.BusinessTripDeclaration, SuperfonWorks.Domain.Models.BusinessTripDeclarationReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripDeclarationCreateModel, SuperfonWorks.Data.Entities.BusinessTripDeclaration>();

            CreateMap<SuperfonWorks.Data.Entities.BusinessTripDeclaration, SuperfonWorks.Domain.Models.BusinessTripDeclarationUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripDeclarationUpdateModel, SuperfonWorks.Data.Entities.BusinessTripDeclaration>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripDeclarationReadModel, SuperfonWorks.Domain.Models.BusinessTripDeclarationUpdateModel>();

        }

    }
}
