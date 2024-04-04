using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class ExpenseAdvanceRequestViewMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.ExpenseAdvanceRequestView> builder)
        {
            #region Generated Configure
            // table
            builder.ToView("ExpenseAdvanceRequestView", "dbo");

            // key
            builder.HasNoKey();

            // properties
            builder.Property(t => t.ExpenseAdvanceRequestId)
                .IsRequired()
                .HasColumnName("ExpenseAdvanceRequestId")
                .HasColumnType("int");

            builder.Property(t => t.RequestDate)
                .IsRequired()
                .HasColumnName("RequestDate")
                .HasColumnType("datetime");

            builder.Property(t => t.RequestAmount)
                .IsRequired()
                .HasColumnName("RequestAmount")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.RequestDescription)
                .HasColumnName("RequestDescription")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.ExpenseDeclarationId)
                .HasColumnName("ExpenseDeclarationId")
                .HasColumnType("int");

            builder.Property(t => t.RequestType)
                .IsRequired()
                .HasColumnName("RequestType")
                .HasColumnType("tinyint");

            builder.Property(t => t.EFlowStatus)
                .IsRequired()
                .HasColumnName("EFlowStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.ManagerApproveStatus)
                .IsRequired()
                .HasColumnName("ManagerApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.BudgetApproveStatus)
                .IsRequired()
                .HasColumnName("BudgetApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.FinanceApproveStatus)
                .IsRequired()
                .HasColumnName("FinanceApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.DeclarationStatus)
                .IsRequired()
                .HasColumnName("DeclarationStatus")
                .HasColumnType("nvarchar(15)")
                .HasMaxLength(15);

            builder.Property(t => t.IsDeclared)
                .HasColumnName("IsDeclared")
                .HasColumnType("bit");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "ExpenseAdvanceRequestView";
        }

        public struct Columns
        {
            public const string ExpenseAdvanceRequestId = "ExpenseAdvanceRequestId";
            public const string RequestDate = "RequestDate";
            public const string RequestAmount = "RequestAmount";
            public const string RequestDescription = "RequestDescription";
            public const string UserId = "UserId";
            public const string ExpenseDeclarationId = "ExpenseDeclarationId";
            public const string RequestType = "RequestType";
            public const string EFlowStatus = "EFlowStatus";
            public const string ManagerApproveStatus = "ManagerApproveStatus";
            public const string BudgetApproveStatus = "BudgetApproveStatus";
            public const string FinanceApproveStatus = "FinanceApproveStatus";
            public const string DeclarationStatus = "DeclarationStatus";
            public const string IsDeclared = "IsDeclared";
        }
        #endregion
    }
}
