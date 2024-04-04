using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class SalaryAdvanceRequestViewMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.SalaryAdvanceRequestView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.SalaryAdvanceRequestView> builder)
        {
            #region Generated Configure
            // table
            builder.ToView("SalaryAdvanceRequestView", "dbo");

            // key
            builder.HasNoKey();

            // properties
            builder.Property(t => t.SalaryAdvanceRequestId)
                .IsRequired()
                .HasColumnName("SalaryAdvanceRequestId")
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

            builder.Property(t => t.PartitionCount)
                .IsRequired()
                .HasColumnName("PartitionCount")
                .HasColumnType("int");

            builder.Property(t => t.EFlowStatus)
                .IsRequired()
                .HasColumnName("EFlowStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.DepartmentManagerApproveStatus)
                .IsRequired()
                .HasColumnName("DepartmentManagerApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.HRManagerApproveStatus)
                .IsRequired()
                .HasColumnName("HRManagerApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.FinanceManagerApproveStatus)
                .IsRequired()
                .HasColumnName("FinanceManagerApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.CashierApproveStatus)
                .IsRequired()
                .HasColumnName("CashierApproveStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "SalaryAdvanceRequestView";
        }

        public struct Columns
        {
            public const string SalaryAdvanceRequestId = "SalaryAdvanceRequestId";
            public const string RequestDate = "RequestDate";
            public const string RequestAmount = "RequestAmount";
            public const string RequestDescription = "RequestDescription";
            public const string UserId = "UserId";
            public const string PartitionCount = "PartitionCount";
            public const string EFlowStatus = "EFlowStatus";
            public const string DepartmentManagerApproveStatus = "DepartmentManagerApproveStatus";
            public const string HRManagerApproveStatus = "HRManagerApproveStatus";
            public const string FinanceManagerApproveStatus = "FinanceManagerApproveStatus";
            public const string CashierApproveStatus = "CashierApproveStatus";
        }
        #endregion
    }
}
