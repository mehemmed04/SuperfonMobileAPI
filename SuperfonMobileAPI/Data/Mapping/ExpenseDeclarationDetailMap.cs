using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class ExpenseDeclarationDetailMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.ExpenseDeclarationDetail> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("ExpenseDeclarationDetail", "dbo");

            // key
            builder.HasKey(t => t.ExpenseDeclarationDetailId);

            // properties
            builder.Property(t => t.ExpenseDeclarationDetailId)
                .IsRequired()
                .HasColumnName("ExpenseDeclarationDetailId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ExpenseDeclarationId)
                .IsRequired()
                .HasColumnName("ExpenseDeclarationId")
                .HasColumnType("int");

            builder.Property(t => t.ExpenseDescription)
                .HasColumnName("ExpenseDescription")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.ExpenseAmount)
                .IsRequired()
                .HasColumnName("ExpenseAmount")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("datetime");

            // relationships
            builder.HasOne(t => t.ExpenseDeclaration)
                .WithMany(t => t.ExpenseDeclarationDetails)
                .HasForeignKey(d => d.ExpenseDeclarationId)
                .HasConstraintName("FK_ExpenseDeclarationDetail_ExpenseDeclaration");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "ExpenseDeclarationDetail";
        }

        public struct Columns
        {
            public const string ExpenseDeclarationDetailId = "ExpenseDeclarationDetailId";
            public const string ExpenseDeclarationId = "ExpenseDeclarationId";
            public const string ExpenseDescription = "ExpenseDescription";
            public const string ExpenseAmount = "ExpenseAmount";
            public const string Date = "Date";
        }
        #endregion
    }
}
