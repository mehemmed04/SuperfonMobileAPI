using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class RepairmanRequestCreateModelValidator
        : AbstractValidator<RepairmanRequestCreateModel>
    {
        public RepairmanRequestCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Ficheno).NotEmpty();
            RuleFor(p => p.Ficheno).MaximumLength(15);
            RuleFor(p => p.Note).NotEmpty();
            RuleFor(p => p.Note).MaximumLength(100);
            #endregion
        }

    }
}
