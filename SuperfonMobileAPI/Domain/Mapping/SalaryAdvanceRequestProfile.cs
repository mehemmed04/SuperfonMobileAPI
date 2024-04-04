using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class SalaryAdvanceRequestProfile
        : AutoMapper.Profile
    {
        public SalaryAdvanceRequestProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.SalaryAdvanceRequest, SuperfonWorks.Domain.Models.SalaryAdvanceRequestReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.SalaryAdvanceRequestCreateModel, SuperfonWorks.Data.Entities.SalaryAdvanceRequest>();

            CreateMap<SuperfonWorks.Data.Entities.SalaryAdvanceRequest, SuperfonWorks.Domain.Models.SalaryAdvanceRequestUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.SalaryAdvanceRequestUpdateModel, SuperfonWorks.Data.Entities.SalaryAdvanceRequest>();

            CreateMap<SuperfonWorks.Domain.Models.SalaryAdvanceRequestReadModel, SuperfonWorks.Domain.Models.SalaryAdvanceRequestUpdateModel>();

        }

    }
}
