using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class BusinessTripDeclarationDetailProfile
        : AutoMapper.Profile
    {
        public BusinessTripDeclarationDetailProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail, SuperfonWorks.Domain.Models.BusinessTripDeclarationDetailReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripDeclarationDetailCreateModel, SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail>();

            CreateMap<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail, SuperfonWorks.Domain.Models.BusinessTripDeclarationDetailUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripDeclarationDetailUpdateModel, SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail>();

            CreateMap<SuperfonWorks.Domain.Models.BusinessTripDeclarationDetailReadModel, SuperfonWorks.Domain.Models.BusinessTripDeclarationDetailUpdateModel>();

        }

    }
}
