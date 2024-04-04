using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.User, SuperfonWorks.Domain.Models.UserReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserCreateModel, SuperfonWorks.Data.Entities.User>();

            CreateMap<SuperfonWorks.Data.Entities.User, SuperfonWorks.Domain.Models.UserUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.UserUpdateModel, SuperfonWorks.Data.Entities.User>();

            CreateMap<SuperfonWorks.Domain.Models.UserReadModel, SuperfonWorks.Domain.Models.UserUpdateModel>();

        }

    }
}
