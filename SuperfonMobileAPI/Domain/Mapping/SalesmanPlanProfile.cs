using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class SalesmanPlanProfile
        : AutoMapper.Profile
    {
        public SalesmanPlanProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.SalesmanPlan, SuperfonWorks.Domain.Models.SalesmanPlanReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.SalesmanPlanCreateModel, SuperfonWorks.Data.Entities.SalesmanPlan>();

            CreateMap<SuperfonWorks.Data.Entities.SalesmanPlan, SuperfonWorks.Domain.Models.SalesmanPlanUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.SalesmanPlanUpdateModel, SuperfonWorks.Data.Entities.SalesmanPlan>();

            CreateMap<SuperfonWorks.Domain.Models.SalesmanPlanReadModel, SuperfonWorks.Domain.Models.SalesmanPlanUpdateModel>();

        }

    }
}
