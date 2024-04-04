using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SuperfonWorks.Data
{
    public partial class SuperfonWorksContext : DbContext
    {
        public SuperfonWorksContext(DbContextOptions<SuperfonWorksContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> BusinessTripDeclarationDetails { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.BusinessTripDeclaration> BusinessTripDeclarations { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.BusinessTripRequest> BusinessTripRequests { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.EquipmentRequest> EquipmentRequests { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.EquipmentRequestView> EquipmentRequestViews { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> ExpenseAdvanceRequests { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView> ExpenseAdvanceRequestViews { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> ExpenseDeclarationDetails { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.ExpenseDeclaration> ExpenseDeclarations { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.ExpenseDeclarationView> ExpenseDeclarationViews { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.MenuPermission> MenuPermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.ProductVerification> ProductVerifications { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.RepairmanRequest> RepairmanRequests { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.SafeAmountTransfer> SafeAmountTransfers { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.SafeAmountTransferView> SafeAmountTransferViews { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> SalaryAdvanceRequests { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.SalaryAdvanceRequestView> SalaryAdvanceRequestViews { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.SalesmanPlan> SalesmanPlans { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserBankAccountPermission> UserBankAccountPermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserBranchPermission> UserBranchPermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserCardCodePermission> UserCardCodePermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserGroup> UserGroups { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserMenuPermission> UserMenuPermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.User> Users { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserSafeboxPermission> UserSafeboxPermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.UserWarehousePermission> UserWarehousePermissions { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.WasteDetail> WasteDetails { get; set; }

        public virtual DbSet<SuperfonWorks.Data.Entities.WasteMaster> WasteMasters { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.BusinessTripDeclarationDetailMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.BusinessTripDeclarationMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.BusinessTripRequestMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.EquipmentRequestMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.EquipmentRequestViewMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.ExpenseAdvanceRequestMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.ExpenseAdvanceRequestViewMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.ExpenseDeclarationDetailMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.ExpenseDeclarationMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.ExpenseDeclarationViewMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.MenuPermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.ProductVerificationMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.RepairmanRequestMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.SafeAmountTransferMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.SafeAmountTransferViewMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.SalaryAdvanceRequestMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.SalaryAdvanceRequestViewMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.SalesmanPlanMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserBankAccountPermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserBranchPermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserCardCodePermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserGroupMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserMenuPermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserSafeboxPermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.UserWarehousePermissionMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.WasteDetailMap());
            modelBuilder.ApplyConfiguration(new SuperfonWorks.Data.Mapping.WasteMasterMap());
            #endregion
        }
    }
}
