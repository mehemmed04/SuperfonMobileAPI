using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class WasteDetailProfile
        : AutoMapper.Profile
    {
        public WasteDetailProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.WasteDetail, SuperfonWorks.Domain.Models.WasteDetailReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.WasteDetailCreateModel, SuperfonWorks.Data.Entities.WasteDetail>();

            CreateMap<SuperfonWorks.Data.Entities.WasteDetail, SuperfonWorks.Domain.Models.WasteDetailUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.WasteDetailUpdateModel, SuperfonWorks.Data.Entities.WasteDetail>();

            CreateMap<SuperfonWorks.Domain.Models.WasteDetailReadModel, SuperfonWorks.Domain.Models.WasteDetailUpdateModel>();

        }

    }
}
