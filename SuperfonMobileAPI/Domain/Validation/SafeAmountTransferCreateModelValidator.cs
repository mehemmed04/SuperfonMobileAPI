using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class SafeAmountTransferCreateModelValidator
        : AbstractValidator<SafeAmountTransferCreateModel>
    {
        public SafeAmountTransferCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.SourceSafeboxCode).NotEmpty();
            RuleFor(p => p.SourceSafeboxCode).MaximumLength(17);
            RuleFor(p => p.DestinationCode).NotEmpty();
            RuleFor(p => p.DestinationCode).MaximumLength(17);
            RuleFor(p => p.Note).MaximumLength(150);
            #endregion
        }

    }
}
