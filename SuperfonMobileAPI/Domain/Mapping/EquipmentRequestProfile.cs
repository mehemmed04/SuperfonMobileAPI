using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class EquipmentRequestProfile
        : AutoMapper.Profile
    {
        public EquipmentRequestProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.EquipmentRequest, SuperfonWorks.Domain.Models.EquipmentRequestReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.EquipmentRequestCreateModel, SuperfonWorks.Data.Entities.EquipmentRequest>();

            CreateMap<SuperfonWorks.Data.Entities.EquipmentRequest, SuperfonWorks.Domain.Models.EquipmentRequestUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.EquipmentRequestUpdateModel, SuperfonWorks.Data.Entities.EquipmentRequest>();

            CreateMap<SuperfonWorks.Domain.Models.EquipmentRequestReadModel, SuperfonWorks.Domain.Models.EquipmentRequestUpdateModel>();

        }

    }
}
