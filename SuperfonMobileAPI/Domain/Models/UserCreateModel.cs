using System;
using System.Collections.Generic;

namespace SuperfonWorks.Domain.Models
{
    public partial class UserCreateModel
    {
        #region Generated Properties
        public int UserId { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public string PassHash { get; set; }

        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string UserPID { get; set; }

        public int? UserGroupId { get; set; }

        #endregion

    }
}
