using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class SafeAmountTransferViewUpdateModelValidator
        : AbstractValidator<SafeAmountTransferViewUpdateModel>
    {
        public SafeAmountTransferViewUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.SourceSafeboxCode).NotEmpty();
            RuleFor(p => p.SourceSafeboxCode).MaximumLength(17);
            RuleFor(p => p.DestinationCode).NotEmpty();
            RuleFor(p => p.DestinationCode).MaximumLength(17);
            RuleFor(p => p.Note).MaximumLength(150);
            RuleFor(p => p.SourceSafeboxName).MaximumLength(51);
            RuleFor(p => p.DestinationName).MaximumLength(51);
            RuleFor(p => p.EFlowStatus).NotEmpty();
            RuleFor(p => p.EFlowStatus).MaximumLength(50);
            #endregion
        }

    }
}
