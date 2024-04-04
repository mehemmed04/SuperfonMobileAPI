using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class SafeAmountTransferViewProfile
        : AutoMapper.Profile
    {
        public SafeAmountTransferViewProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.SafeAmountTransferView, SuperfonWorks.Domain.Models.SafeAmountTransferViewReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.SafeAmountTransferViewCreateModel, SuperfonWorks.Data.Entities.SafeAmountTransferView>();

            CreateMap<SuperfonWorks.Data.Entities.SafeAmountTransferView, SuperfonWorks.Domain.Models.SafeAmountTransferViewUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.SafeAmountTransferViewUpdateModel, SuperfonWorks.Data.Entities.SafeAmountTransferView>();

            CreateMap<SuperfonWorks.Domain.Models.SafeAmountTransferViewReadModel, SuperfonWorks.Domain.Models.SafeAmountTransferViewUpdateModel>();

        }

    }
}
