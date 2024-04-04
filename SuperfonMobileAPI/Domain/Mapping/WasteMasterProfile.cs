using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class WasteMasterProfile
        : AutoMapper.Profile
    {
        public WasteMasterProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.WasteMaster, SuperfonWorks.Domain.Models.WasteMasterReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.WasteMasterCreateModel, SuperfonWorks.Data.Entities.WasteMaster>();

            CreateMap<SuperfonWorks.Data.Entities.WasteMaster, SuperfonWorks.Domain.Models.WasteMasterUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.WasteMasterUpdateModel, SuperfonWorks.Data.Entities.WasteMaster>();

            CreateMap<SuperfonWorks.Domain.Models.WasteMasterReadModel, SuperfonWorks.Domain.Models.WasteMasterUpdateModel>();

        }

    }
}
