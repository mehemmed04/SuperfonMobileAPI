using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class EquipmentRequestViewProfile
        : AutoMapper.Profile
    {
        public EquipmentRequestViewProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.EquipmentRequestView, SuperfonWorks.Domain.Models.EquipmentRequestViewReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.EquipmentRequestViewCreateModel, SuperfonWorks.Data.Entities.EquipmentRequestView>();

            CreateMap<SuperfonWorks.Data.Entities.EquipmentRequestView, SuperfonWorks.Domain.Models.EquipmentRequestViewUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.EquipmentRequestViewUpdateModel, SuperfonWorks.Data.Entities.EquipmentRequestView>();

            CreateMap<SuperfonWorks.Domain.Models.EquipmentRequestViewReadModel, SuperfonWorks.Domain.Models.EquipmentRequestViewUpdateModel>();

        }

    }
}
