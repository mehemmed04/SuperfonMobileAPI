using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class SalaryAdvanceRequestViewProfile
        : AutoMapper.Profile
    {
        public SalaryAdvanceRequestViewProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.SalaryAdvanceRequestView, SuperfonWorks.Domain.Models.SalaryAdvanceRequestViewReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.SalaryAdvanceRequestViewCreateModel, SuperfonWorks.Data.Entities.SalaryAdvanceRequestView>();

            CreateMap<SuperfonWorks.Data.Entities.SalaryAdvanceRequestView, SuperfonWorks.Domain.Models.SalaryAdvanceRequestViewUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.SalaryAdvanceRequestViewUpdateModel, SuperfonWorks.Data.Entities.SalaryAdvanceRequestView>();

            CreateMap<SuperfonWorks.Domain.Models.SalaryAdvanceRequestViewReadModel, SuperfonWorks.Domain.Models.SalaryAdvanceRequestViewUpdateModel>();

        }

    }
}
