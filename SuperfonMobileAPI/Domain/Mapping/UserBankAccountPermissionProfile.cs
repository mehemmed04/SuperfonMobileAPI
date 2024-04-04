using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserBankAccountPermissionProfile
        : AutoMapper.Profile
    {
        public UserBankAccountPermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserBankAccountPermission, SuperfonWorks.Domain.Models.UserBankAccountPermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserBankAccountPermissionCreateModel, SuperfonWorks.Data.Entities.UserBankAccountPermission>();

            CreateMap<SuperfonWorks.Data.Entities.UserBankAccountPermission, SuperfonWorks.Domain.Models.UserBankAccountPermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserBankAccountPermissionUpdateModel, SuperfonWorks.Data.Entities.UserBankAccountPermission>();

            CreateMap<SuperfonWorks.Domain.Models.UserBankAccountPermissionReadModel, SuperfonWorks.Domain.Models.UserBankAccountPermissionUpdateModel>();

        }

    }
}
