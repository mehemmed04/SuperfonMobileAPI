using System;
using AutoMapper;
using SuperfonWorks.Data.Entities;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Mapping
{
    public partial class ExpenseDeclarationDetailProfile
        : AutoMapper.Profile
    {
        public ExpenseDeclarationDetailProfile()
        {
            CreateMap<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail, SuperfonWorks.Domain.Models.ExpenseDeclarationDetailReadModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationDetailCreateModel, SuperfonWorks.Data.Entities.ExpenseDeclarationDetail>();

            CreateMap<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail, SuperfonWorks.Domain.Models.ExpenseDeclarationDetailUpdateModel>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationDetailUpdateModel, SuperfonWorks.Data.Entities.ExpenseDeclarationDetail>();

            CreateMap<SuperfonWorks.Domain.Models.ExpenseDeclarationDetailReadModel, SuperfonWorks.Domain.Models.ExpenseDeclarationDetailUpdateModel>();

        }

    }
}
