using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserSafeboxPermissionUpdateModelValidator
        : AbstractValidator<UserSafeboxPermissionUpdateModel>
    {
        public UserSafeboxPermissionUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.SafeboxCode).NotEmpty();
            RuleFor(p => p.SafeboxCode).MaximumLength(17);
            #endregion
        }

    }
}
