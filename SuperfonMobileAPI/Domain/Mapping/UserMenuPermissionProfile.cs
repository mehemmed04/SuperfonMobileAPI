using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserMenuPermissionProfile
        : AutoMapper.Profile
    {
        public UserMenuPermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserMenuPermission, SuperfonWorks.Domain.Models.UserMenuPermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserMenuPermissionCreateModel, SuperfonWorks.Data.Entities.UserMenuPermission>();

            CreateMap<SuperfonWorks.Data.Entities.UserMenuPermission, SuperfonWorks.Domain.Models.UserMenuPermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserMenuPermissionUpdateModel, SuperfonWorks.Data.Entities.UserMenuPermission>();

            CreateMap<SuperfonWorks.Domain.Models.UserMenuPermissionReadModel, SuperfonWorks.Domain.Models.UserMenuPermissionUpdateModel>();

        }

    }
}
