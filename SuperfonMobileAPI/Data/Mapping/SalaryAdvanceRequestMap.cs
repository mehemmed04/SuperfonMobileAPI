using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class SalaryAdvanceRequestMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.SalaryAdvanceRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.SalaryAdvanceRequest> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("SalaryAdvanceRequest", "dbo");

            // key
            builder.HasKey(t => t.SalaryAdvanceRequestId);

            // properties
            builder.Property(t => t.SalaryAdvanceRequestId)
                .IsRequired()
                .HasColumnName("SalaryAdvanceRequestId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

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
                .HasColumnType("int")
                .HasDefaultValueSql("((1))");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.SalaryAdvanceRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SalaryAdvanceRequest_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "SalaryAdvanceRequest";
        }

        public struct Columns
        {
            public const string SalaryAdvanceRequestId = "SalaryAdvanceRequestId";
            public const string RequestDate = "RequestDate";
            public const string RequestAmount = "RequestAmount";
            public const string RequestDescription = "RequestDescription";
            public const string UserId = "UserId";
            public const string PartitionCount = "PartitionCount";
        }
        #endregion
    }
}
