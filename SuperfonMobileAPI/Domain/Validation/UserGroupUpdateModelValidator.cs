using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserGroupUpdateModelValidator
        : AbstractValidator<UserGroupUpdateModel>
    {
        public UserGroupUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.UserGroupName).NotEmpty();
            RuleFor(p => p.UserGroupName).MaximumLength(100);
            #endregion
        }

    }
}
