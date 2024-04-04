using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class BusinessTripDeclarationDetailMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.BusinessTripDeclarationDetail> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("BusinessTripDeclarationDetail", "dbo");

            // key
            builder.HasKey(t => t.BusinessTripDeclarationDetailId);

            // properties
            builder.Property(t => t.BusinessTripDeclarationDetailId)
                .IsRequired()
                .HasColumnName("BusinessTripDeclarationDetailId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.BusinessTripDeclarationId)
                .IsRequired()
                .HasColumnName("BusinessTripDeclarationId")
                .HasColumnType("int");

            builder.Property(t => t.ExpenseDescription)
                .IsRequired()
                .HasColumnName("ExpenseDescription")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.Price)
                .IsRequired()
                .HasColumnName("Price")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("date");

            // relationships
            builder.HasOne(t => t.BusinessTripDeclaration)
                .WithMany(t => t.BusinessTripDeclarationDetails)
                .HasForeignKey(d => d.BusinessTripDeclarationId)
                .HasConstraintName("FK_BusinessTripDeclarationDetail_BusinessTripDeclaration");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "BusinessTripDeclarationDetail";
        }

        public struct Columns
        {
            public const string BusinessTripDeclarationDetailId = "BusinessTripDeclarationDetailId";
            public const string BusinessTripDeclarationId = "BusinessTripDeclarationId";
            public const string ExpenseDescription = "ExpenseDescription";
            public const string Quantity = "Quantity";
            public const string Price = "Price";
            public const string Date = "Date";
        }
        #endregion
    }
}
