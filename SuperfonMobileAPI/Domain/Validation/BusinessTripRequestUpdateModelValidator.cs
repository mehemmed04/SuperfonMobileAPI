using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class BusinessTripRequestUpdateModelValidator
        : AbstractValidator<BusinessTripRequestUpdateModel>
    {
        public BusinessTripRequestUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.RequestDescription).MaximumLength(201);
            #endregion
        }

    }
}
