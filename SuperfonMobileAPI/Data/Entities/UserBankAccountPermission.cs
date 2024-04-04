using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class UserBankAccountPermission
    {
        public UserBankAccountPermission()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public int UserBankAccountPermissionId { get; set; }

        public string BankAccountCode { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Generated Relationships
        public virtual User User { get; set; }

        #endregion

    }
}
