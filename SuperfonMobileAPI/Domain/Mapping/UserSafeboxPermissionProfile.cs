using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserSafeboxPermissionProfile
        : AutoMapper.Profile
    {
        public UserSafeboxPermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserSafeboxPermission, SuperfonWorks.Domain.Models.UserSafeboxPermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserSafeboxPermissionCreateModel, SuperfonWorks.Data.Entities.UserSafeboxPermission>();

            CreateMap<SuperfonWorks.Data.Entities.UserSafeboxPermission, SuperfonWorks.Domain.Models.UserSafeboxPermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserSafeboxPermissionUpdateModel, SuperfonWorks.Data.Entities.UserSafeboxPermission>();

            CreateMap<SuperfonWorks.Domain.Models.UserSafeboxPermissionReadModel, SuperfonWorks.Domain.Models.UserSafeboxPermissionUpdateModel>();

        }

    }
}
