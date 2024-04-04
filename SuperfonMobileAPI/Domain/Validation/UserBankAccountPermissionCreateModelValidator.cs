using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class UserBankAccountPermissionCreateModelValidator
        : AbstractValidator<UserBankAccountPermissionCreateModel>
    {
        public UserBankAccountPermissionCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.BankAccountCode).NotEmpty();
            RuleFor(p => p.BankAccountCode).MaximumLength(17);
            #endregion
        }

    }
}
