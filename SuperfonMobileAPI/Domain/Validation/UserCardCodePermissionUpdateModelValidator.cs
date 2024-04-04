using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserCardCodePermissionUpdateModelValidator
        : AbstractValidator<UserCardCodePermissionUpdateModel>
    {
        public UserCardCodePermissionUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.CardCode).NotEmpty();
            RuleFor(p => p.CardCode).MaximumLength(17);
            #endregion
        }

    }
}
