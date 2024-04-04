using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserWarehousePermissionProfile
        : AutoMapper.Profile
    {
        public UserWarehousePermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserWarehousePermission, SuperfonWorks.Domain.Models.UserWarehousePermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserWarehousePermissionCreateModel, SuperfonWorks.Data.Entities.UserWarehousePermission>();

            CreateMap<SuperfonWorks.Data.Entities.UserWarehousePermission, SuperfonWorks.Domain.Models.UserWarehousePermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserWarehousePermissionUpdateModel, SuperfonWorks.Data.Entities.UserWarehousePermission>();

            CreateMap<SuperfonWorks.Domain.Models.UserWarehousePermissionReadModel, SuperfonWorks.Domain.Models.UserWarehousePermissionUpdateModel>();

        }

    }
}
