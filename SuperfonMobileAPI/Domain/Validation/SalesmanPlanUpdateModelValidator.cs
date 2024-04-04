using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class SalesmanPlanUpdateModelValidator
        : AbstractValidator<SalesmanPlanUpdateModel>
    {
        public SalesmanPlanUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.SalesmanCode).NotEmpty();
            RuleFor(p => p.SalesmanCode).MaximumLength(25);
            #endregion
        }

    }
}
