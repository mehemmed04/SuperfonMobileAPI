using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserUpdateModelValidator
        : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(25);
            RuleFor(p => p.DisplayName).NotEmpty();
            RuleFor(p => p.DisplayName).MaximumLength(100);
            RuleFor(p => p.PassHash).NotEmpty();
            RuleFor(p => p.PassHash).MaximumLength(255);
            RuleFor(p => p.Email).MaximumLength(255);
            RuleFor(p => p.Phone).MaximumLength(30);
            RuleFor(p => p.UserPID).MaximumLength(7);
            #endregion
        }

    }
}
