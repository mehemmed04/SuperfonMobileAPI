using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class ExpenseDeclarationViewCreateModelValidator
        : AbstractValidator<ExpenseDeclarationViewCreateModel>
    {
        public ExpenseDeclarationViewCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.DeclarationNote).NotEmpty();
            RuleFor(p => p.DeclarationNote).MaximumLength(150);
            RuleFor(p => p.EFlowStatus).NotEmpty();
            RuleFor(p => p.EFlowStatus).MaximumLength(50);
            #endregion
        }

    }
}
