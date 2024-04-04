using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserBranchPermissionProfile
        : AutoMapper.Profile
    {
        public UserBranchPermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserBranchPermission, SuperfonWorks.Domain.Models.UserBranchPermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserBranchPermissionCreateModel, SuperfonWorks.Data.Entities.UserBranchPermission>();

            CreateMap<SuperfonWorks.Data.Entities.UserBranchPermission, SuperfonWorks.Domain.Models.UserBranchPermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserBranchPermissionUpdateModel, SuperfonWorks.Data.Entities.UserBranchPermission>();

            CreateMap<SuperfonWorks.Domain.Models.UserBranchPermissionReadModel, SuperfonWorks.Domain.Models.UserBranchPermissionUpdateModel>();

        }

    }
}
