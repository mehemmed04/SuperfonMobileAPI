using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class ProductVerificationUpdateModelValidator
        : AbstractValidator<ProductVerificationUpdateModel>
    {
        public ProductVerificationUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.ProductCode).NotEmpty();
            RuleFor(p => p.ProductCode).MaximumLength(25);
            RuleFor(p => p.CreatedBy).NotEmpty();
            RuleFor(p => p.CreatedBy).MaximumLength(50);
            RuleFor(p => p.ModifiedBy).MaximumLength(50);
            RuleFor(p => p.VerifiedBy).MaximumLength(50);
            #endregion
        }

    }
}
