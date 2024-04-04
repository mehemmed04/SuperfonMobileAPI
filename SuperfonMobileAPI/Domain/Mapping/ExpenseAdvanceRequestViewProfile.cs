using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class ExpenseAdvanceRequestViewProfile
        : AutoMapper.Profile
    {
        public ExpenseAdvanceRequestViewProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView, SuperfonWorks.Domain.Models.ExpenseAdvanceRequestViewReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseAdvanceRequestViewCreateModel, SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView>();

            CreateMap<SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView, SuperfonWorks.Domain.Models.ExpenseAdvanceRequestViewUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseAdvanceRequestViewUpdateModel, SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseAdvanceRequestViewReadModel, SuperfonWorks.Domain.Models.ExpenseAdvanceRequestViewUpdateModel>();

        }

    }
}
