using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class ExpenseDeclarationViewProfile
        : AutoMapper.Profile
    {
        public ExpenseDeclarationViewProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.ExpenseDeclarationView, SuperfonWorks.Domain.Models.ExpenseDeclarationViewReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationViewCreateModel, SuperfonWorks.Data.Entities.ExpenseDeclarationView>();

            CreateMap<SuperfonWorks.Data.Entities.ExpenseDeclarationView, SuperfonWorks.Domain.Models.ExpenseDeclarationViewUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationViewUpdateModel, SuperfonWorks.Data.Entities.ExpenseDeclarationView>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationViewReadModel, SuperfonWorks.Domain.Models.ExpenseDeclarationViewUpdateModel>();

        }

    }
}
