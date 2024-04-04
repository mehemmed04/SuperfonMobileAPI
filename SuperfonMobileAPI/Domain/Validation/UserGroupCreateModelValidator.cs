using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserGroupCreateModelValidator
        : AbstractValidator<UserGroupCreateModel>
    {
        public UserGroupCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.UserGroupName).NotEmpty();
            RuleFor(p => p.UserGroupName).MaximumLength(100);
            #endregion
        }

    }
}
