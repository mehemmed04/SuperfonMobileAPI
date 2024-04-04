using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserGroupProfile
        : AutoMapper.Profile
    {
        public UserGroupProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.UserGroup, SuperfonWorks.Domain.Models.UserGroupReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserGroupCreateModel, SuperfonWorks.Data.Entities.UserGroup>();

            CreateMap<SuperfonWorks.Data.Entities.UserGroup, SuperfonWorks.Domain.Models.UserGroupUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserGroupUpdateModel, SuperfonWorks.Data.Entities.UserGroup>();

            CreateMap<SuperfonWorks.Domain.Models.UserGroupReadModel, SuperfonWorks.Domain.Models.UserGroupUpdateModel>();

        }

    }
}
