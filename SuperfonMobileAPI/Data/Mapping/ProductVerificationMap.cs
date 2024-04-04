using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class ProductVerificationMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.ProductVerification>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.ProductVerification> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("ProductVerification", "dbo");

            // key
            builder.HasKey(t => t.ProductVerificationId);

            // properties
            builder.Property(t => t.ProductVerificationId)
                .IsRequired()
                .HasColumnName("ProductVerificationId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.ProductCode)
                .IsRequired()
                .HasColumnName("ProductCode")
                .HasColumnType("varchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.IsVerified)
                .IsRequired()
                .HasColumnName("IsVerified")
                .HasColumnType("bit");

            builder.Property(t => t.CreatedDate)
                .IsRequired()
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime");

            builder.Property(t => t.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("datetime");

            builder.Property(t => t.VerifiedDate)
                .HasColumnName("VerifiedDate")
                .HasColumnType("datetime");

            builder.Property(t => t.CreatedBy)
                .IsRequired()
                .HasColumnName("CreatedBy")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.ModifiedBy)
                .HasColumnName("ModifiedBy")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.VerifiedBy)
                .HasColumnName("VerifiedBy")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "ProductVerification";
        }

        public struct Columns
        {
            public const string ProductVerificationId = "ProductVerificationId";
            public const string ProductCode = "ProductCode";
            public const string IsVerified = "IsVerified";
            public const string CreatedDate = "CreatedDate";
            public const string ModifiedDate = "ModifiedDate";
            public const string VerifiedDate = "VerifiedDate";
            public const string CreatedBy = "CreatedBy";
            public const string ModifiedBy = "ModifiedBy";
            public const string VerifiedBy = "VerifiedBy";
        }
        #endregion
    }
}
