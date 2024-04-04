using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class SalaryAdvanceRequestCreateModelValidator
        : AbstractValidator<SalaryAdvanceRequestCreateModel>
    {
        public SalaryAdvanceRequestCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).MaximumLength(150);
            #endregion
        }

    }
}
