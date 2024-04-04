using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class BusinessTripDeclarationDetailCreateModelValidator
        : AbstractValidator<BusinessTripDeclarationDetailCreateModel>
    {
        public BusinessTripDeclarationDetailCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ExpenseDescription).NotEmpty();
            RuleFor(p => p.ExpenseDescription).MaximumLength(150);
            #endregion
        }

    }
}
