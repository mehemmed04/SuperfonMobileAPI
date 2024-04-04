using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class ExpenseAdvanceRequestViewCreateModelValidator
        : AbstractValidator<ExpenseAdvanceRequestViewCreateModel>
    {
        public ExpenseAdvanceRequestViewCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).MaximumLength(150);
            RuleFor(p => p.EFlowStatus).NotEmpty();
            RuleFor(p => p.EFlowStatus).MaximumLength(50);
            RuleFor(p => p.ManagerApproveStatus).NotEmpty();
            RuleFor(p => p.ManagerApproveStatus).MaximumLength(50);
            RuleFor(p => p.BudgetApproveStatus).NotEmpty();
            RuleFor(p => p.BudgetApproveStatus).MaximumLength(50);
            RuleFor(p => p.FinanceApproveStatus).NotEmpty();
            RuleFor(p => p.FinanceApproveStatus).MaximumLength(50);
            RuleFor(p => p.DeclarationStatus).NotEmpty();
            RuleFor(p => p.DeclarationStatus).MaximumLength(15);
            #endregion
        }

    }
}
