using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class EquipmentRequestViewCreateModelValidator
        : AbstractValidator<EquipmentRequestViewCreateModel>
    {
        public EquipmentRequestViewCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).NotEmpty();
            RuleFor(p => p.RequestDescription).MaximumLength(150);
            RuleFor(p => p.Note).MaximumLength(250);
            RuleFor(p => p.EFlowStatus).MaximumLength(50);
            #endregion
        }

    }
}
