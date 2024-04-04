using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class ProductVerificationProfile
        : AutoMapper.Profile
    {
        public ProductVerificationProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.ProductVerification, SuperfonWorks.Domain.Models.ProductVerificationReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.ProductVerificationCreateModel, SuperfonWorks.Data.Entities.ProductVerification>();

            CreateMap<SuperfonWorks.Data.Entities.ProductVerification, SuperfonWorks.Domain.Models.ProductVerificationUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.ProductVerificationUpdateModel, SuperfonWorks.Data.Entities.ProductVerification>();

            CreateMap<SuperfonWorks.Domain.Models.ProductVerificationReadModel, SuperfonWorks.Domain.Models.ProductVerificationUpdateModel>();

        }

    }
}
