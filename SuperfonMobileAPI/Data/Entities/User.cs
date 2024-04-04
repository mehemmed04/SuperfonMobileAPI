using System;
using System.Collections.Generic;

namespace SuperfonWorks.Data.Entities
{
    public partial class User
    {
        public User()
        {
            #region Generated Constructor
            BusinessTripDeclarations = new HashSet<BusinessTripDeclaration>();
            BusinessTripRequests = new HashSet<BusinessTripRequest>();
            EquipmentRequests = new HashSet<EquipmentRequest>();
            ExpenseAdvanceRequests = new HashSet<ExpenseAdvanceRequest>();
            ExpenseDeclarations = new HashSet<ExpenseDeclaration>();
            SafeAmountTransfers = new HashSet<SafeAmountTransfer>();
            SalaryAdvanceRequests = new HashSet<SalaryAdvanceRequest>();
            UserBankAccountPermissions = new HashSet<UserBankAccountPermission>();
            UserBranchPermissions = new HashSet<UserBranchPermission>();
            UserCardCodePermissions = new HashSet<UserCardCodePermission>();
            UserMenuPermissions = new HashSet<UserMenuPermission>();
            UserSafeboxPermissions = new HashSet<UserSafeboxPermission>();
            UserWarehousePermissions = new HashSet<UserWarehousePermission>();
            WasteMasters = new HashSet<WasteMaster>();
            #endregion
        }

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

        #region Generated Relationships
        public virtual ICollection<BusinessTripDeclaration> BusinessTripDeclarations { get; set; }

        public virtual ICollection<BusinessTripRequest> BusinessTripRequests { get; set; }

        public virtual ICollection<EquipmentRequest> EquipmentRequests { get; set; }

        public virtual ICollection<ExpenseAdvanceRequest> ExpenseAdvanceRequests { get; set; }

        public virtual ICollection<ExpenseDeclaration> ExpenseDeclarations { get; set; }

        public virtual ICollection<SafeAmountTransfer> SafeAmountTransfers { get; set; }

        public virtual ICollection<SalaryAdvanceRequest> SalaryAdvanceRequests { get; set; }

        public virtual ICollection<UserBankAccountPermission> UserBankAccountPermissions { get; set; }

        public virtual ICollection<UserBranchPermission> UserBranchPermissions { get; set; }

        public virtual ICollection<UserCardCodePermission> UserCardCodePermissions { get; set; }

        public virtual UserGroup UserGroup { get; set; }

        public virtual ICollection<UserMenuPermission> UserMenuPermissions { get; set; }

        public virtual ICollection<UserSafeboxPermission> UserSafeboxPermissions { get; set; }

        public virtual ICollection<UserWarehousePermission> UserWarehousePermissions { get; set; }

        public virtual ICollection<WasteMaster> WasteMasters { get; set; }

        #endregion

    }
}
