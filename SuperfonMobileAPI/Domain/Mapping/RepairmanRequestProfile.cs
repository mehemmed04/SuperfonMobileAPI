using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class RepairmanRequestProfile
        : AutoMapper.Profile
    {
        public RepairmanRequestProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.RepairmanRequest, SuperfonWorks.Domain.Models.RepairmanRequestReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.RepairmanRequestCreateModel, SuperfonWorks.Data.Entities.RepairmanRequest>();

            CreateMap<SuperfonWorks.Data.Entities.RepairmanRequest, SuperfonWorks.Domain.Models.RepairmanRequestUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.RepairmanRequestUpdateModel, SuperfonWorks.Data.Entities.RepairmanRequest>();

            CreateMap<SuperfonWorks.Domain.Models.RepairmanRequestReadModel, SuperfonWorks.Domain.Models.RepairmanRequestUpdateModel>();

        }

    }
}
