using System;
using FluentValidation;
using SuperfonWorks.Domain.Models;

namespace SuperfonWorks.Domain.Validation
{
    public partial class MenuPermissionCreateModelValidator
        : AbstractValidator<MenuPermissionCreateModel>
    {
        public MenuPermissionCreateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.PermissionName).NotEmpty();
            RuleFor(p => p.PermissionName).MaximumLength(80);
            RuleFor(p => p.KeyWord).NotEmpty();
            RuleFor(p => p.KeyWord).MaximumLength(30);
            RuleFor(p => p.IconName).MaximumLength(100);
            RuleFor(p => p.Link).MaximumLength(255);
            #endregion
        }

    }
}
