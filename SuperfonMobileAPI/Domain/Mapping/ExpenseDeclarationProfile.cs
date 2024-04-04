using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class ExpenseDeclarationProfile
        : AutoMapper.Profile
    {
        public ExpenseDeclarationProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.ExpenseDeclaration, SuperfonWorks.Domain.Models.ExpenseDeclarationReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationCreateModel, SuperfonWorks.Data.Entities.ExpenseDeclaration>();

            CreateMap<SuperfonWorks.Data.Entities.ExpenseDeclaration, SuperfonWorks.Domain.Models.ExpenseDeclarationUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationUpdateModel, SuperfonWorks.Data.Entities.ExpenseDeclaration>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationReadModel, SuperfonWorks.Domain.Models.ExpenseDeclarationUpdateModel>();

        }

    }
}
