using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class SalaryAdvanceRequestViewCreateModelValidator
        : AbstractValidator<SalaryAdvanceRequestViewCreateModel>
    {
        public SalaryAdvanceRequestViewCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).MaximumLength(150);
            RuleFor(p => p.EFlowStatus).NotEmpty();
            RuleFor(p => p.EFlowStatus).MaximumLength(50);
            RuleFor(p => p.DepartmentManagerApproveStatus).NotEmpty();
            RuleFor(p => p.DepartmentManagerApproveStatus).MaximumLength(50);
            RuleFor(p => p.HRManagerApproveStatus).NotEmpty();
            RuleFor(p => p.HRManagerApproveStatus).MaximumLength(50);
            RuleFor(p => p.FinanceManagerApproveStatus).NotEmpty();
            RuleFor(p => p.FinanceManagerApproveStatus).MaximumLength(50);
            RuleFor(p => p.CashierApproveStatus).NotEmpty();
            RuleFor(p => p.CashierApproveStatus).MaximumLength(50);
            #endregion
        }

    }
}
