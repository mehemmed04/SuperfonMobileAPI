using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserCardCodePermissionProfile
        : AutoMapper.Profile
    {
        public UserCardCodePermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserCardCodePermission, SuperfonWorks.Domain.Models.UserCardCodePermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserCardCodePermissionCreateModel, SuperfonWorks.Data.Entities.UserCardCodePermission>();

            CreateMap<SuperfonWorks.Data.Entities.UserCardCodePermission, SuperfonWorks.Domain.Models.UserCardCodePermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserCardCodePermissionUpdateModel, SuperfonWorks.Data.Entities.UserCardCodePermission>();

            CreateMap<SuperfonWorks.Domain.Models.UserCardCodePermissionReadModel, SuperfonWorks.Domain.Models.UserCardCodePermissionUpdateModel>();

        }

    }
}
