using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class EquipmentRequestUpdateModelValidator
        : AbstractValidator<EquipmentRequestUpdateModel>
    {
        public EquipmentRequestUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).NotEmpty();
            RuleFor(p => p.RequestDescription).MaximumLength(150);
            RuleFor(p => p.Note).MaximumLength(250);
            #endregion
        }

    }
}
