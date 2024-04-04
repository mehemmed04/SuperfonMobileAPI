using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class WasteDetailMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.WasteDetail>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.WasteDetail> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("WasteDetail", "dbo");

            // key
            builder.HasKey(t => t.WasteDetailId);

            // properties
            builder.Property(t => t.WasteDetailId)
                .IsRequired()
                .HasColumnName("WasteDetailId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.WasteMasterId)
                .IsRequired()
                .HasColumnName("WasteMasterId")
                .HasColumnType("int");

            builder.Property(t => t.ProductCode)
                .IsRequired()
                .HasColumnName("ProductCode")
                .HasColumnType("varchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.Barcode)
                .IsRequired()
                .HasColumnName("Barcode")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("decimal(9,2)");

            // relationships
            builder.HasOne(t => t.WasteMaster)
                .WithMany(t => t.WasteDetails)
                .HasForeignKey(d => d.WasteMasterId)
                .HasConstraintName("FK_WasteDetail_WasteMaster");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "WasteDetail";
        }

        public struct Columns
        {
            public const string WasteDetailId = "WasteDetailId";
            public const string WasteMasterId = "WasteMasterId";
            public const string ProductCode = "ProductCode";
            public const string Barcode = "Barcode";
            public const string Quantity = "Quantity";
        }
        #endregion
    }
}
