using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class ExpenseAdvanceRequestProfile
        : AutoMapper.Profile
    {
        public ExpenseAdvanceRequestProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest, SuperfonWorks.Domain.Models.ExpenseAdvanceRequestReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseAdvanceRequestCreateModel, SuperfonWorks.Data.Entities.ExpenseAdvanceRequest>();

            CreateMap<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest, SuperfonWorks.Domain.Models.ExpenseAdvanceRequestUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseAdvanceRequestUpdateModel, SuperfonWorks.Data.Entities.ExpenseAdvanceRequest>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseAdvanceRequestReadModel, SuperfonWorks.Domain.Models.ExpenseAdvanceRequestUpdateModel>();

        }

    }
}
