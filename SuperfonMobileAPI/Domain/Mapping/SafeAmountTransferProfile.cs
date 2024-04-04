using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class SafeAmountTransferProfile
        : AutoMapper.Profile
    {
        public SafeAmountTransferProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.SafeAmountTransfer, SuperfonWorks.Domain.Models.SafeAmountTransferReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.SafeAmountTransferCreateModel, SuperfonWorks.Data.Entities.SafeAmountTransfer>();

            CreateMap<SuperfonWorks.Data.Entities.SafeAmountTransfer, SuperfonWorks.Domain.Models.SafeAmountTransferUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.SafeAmountTransferUpdateModel, SuperfonWorks.Data.Entities.SafeAmountTransfer>();

            CreateMap<SuperfonWorks.Domain.Models.SafeAmountTransferReadModel, SuperfonWorks.Domain.Models.SafeAmountTransferUpdateModel>();

        }

    }
}
