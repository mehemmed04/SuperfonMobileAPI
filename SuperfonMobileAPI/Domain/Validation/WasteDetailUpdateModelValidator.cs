using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class WasteDetailUpdateModelValidator
        : AbstractValidator<WasteDetailUpdateModel>
    {
        public WasteDetailUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ProductCode).NotEmpty();
            RuleFor(p => p.ProductCode).MaximumLength(25);
            RuleFor(p => p.Barcode).NotEmpty();
            RuleFor(p => p.Barcode).MaximumLength(100);
            #endregion
        }

    }
}
