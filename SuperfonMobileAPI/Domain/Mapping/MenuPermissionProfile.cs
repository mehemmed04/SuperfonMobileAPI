using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class MenuPermissionProfile
        : AutoMapper.Profile
    {
        public MenuPermissionProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.MenuPermission, SuperfonWorks.Domain.Models.MenuPermissionReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.MenuPermissionCreateModel, SuperfonWorks.Data.Entities.MenuPermission>();

            CreateMap<SuperfonWorks.Data.Entities.MenuPermission, SuperfonWorks.Domain.Models.MenuPermissionUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.MenuPermissionUpdateModel, SuperfonWorks.Data.Entities.MenuPermission>();

            CreateMap<SuperfonWorks.Domain.Models.MenuPermissionReadModel, SuperfonWorks.Domain.Models.MenuPermissionUpdateModel>();

        }

    }
}
