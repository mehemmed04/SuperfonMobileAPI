using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserCardCodePermissionCreateModelValidator
        : AbstractValidator<UserCardCodePermissionCreateModel>
    {
        public UserCardCodePermissionCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.CardCode).NotEmpty();
            RuleFor(p => p.CardCode).MaximumLength(17);
            #endregion
        }

    }
}
