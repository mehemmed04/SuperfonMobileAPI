using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class SalaryAdvanceRequestUpdateModelValidator
        : AbstractValidator<SalaryAdvanceRequestUpdateModel>
    {
        public SalaryAdvanceRequestUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).MaximumLength(150);
            #endregion
        }

    }
}
