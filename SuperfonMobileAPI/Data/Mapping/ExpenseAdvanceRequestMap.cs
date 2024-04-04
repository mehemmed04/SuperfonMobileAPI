using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class ExpenseAdvanceRequestMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.ExpenseAdvanceRequest> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("ExpenseAdvanceRequest", "dbo");

            // key
            builder.HasKey(t => t.ExpenseAdvanceRequestId);

            // properties
            builder.Property(t => t.ExpenseAdvanceRequestId)
                .IsRequired()
                .HasColumnName("ExpenseAdvanceRequestId")
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
                .HasColumnType("nvarchar(max)");

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

            // relationships
            builder.HasOne(t => t.ExpenseDeclaration)
                .WithMany(t => t.ExpenseAdvanceRequests)
                .HasForeignKey(d => d.ExpenseDeclarationId)
                .HasConstraintName("FK_ExpenseAdvanceRequest_ExpenseDeclaration");

            builder.HasOne(t => t.User)
                .WithMany(t => t.ExpenseAdvanceRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ExpenseAdvanceRequest_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "ExpenseAdvanceRequest";
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
        }
        #endregion
    }
}
