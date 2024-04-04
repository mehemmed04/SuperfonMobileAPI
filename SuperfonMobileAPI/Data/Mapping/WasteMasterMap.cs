using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class WasteMasterMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.WasteMaster>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.WasteMaster> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("WasteMaster", "dbo");

            // key
            builder.HasKey(t => t.WasteMasterId);

            // properties
            builder.Property(t => t.WasteMasterId)
                .IsRequired()
                .HasColumnName("WasteMasterId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.CreatedDate)
                .IsRequired()
                .HasColumnName("CreatedDate")
                .HasColumnType("datetime");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.CustomerId)
                .IsRequired()
                .HasColumnName("CustomerId")
                .HasColumnType("int");

            builder.Property(t => t.Warehouse)
                .IsRequired()
                .HasColumnName("Warehouse")
                .HasColumnType("int");

            builder.Property(t => t.IsIntegrated)
                .IsRequired()
                .HasColumnName("IsIntegrated")
                .HasColumnType("bit");

            builder.Property(t => t.IntegratedDate)
                .HasColumnName("IntegratedDate")
                .HasColumnType("datetime");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.WasteMasters)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_WasteMaster_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "WasteMaster";
        }

        public struct Columns
        {
            public const string WasteMasterId = "WasteMasterId";
            public const string CreatedDate = "CreatedDate";
            public const string UserId = "UserId";
            public const string CustomerId = "CustomerId";
            public const string Warehouse = "Warehouse";
            public const string IsIntegrated = "IsIntegrated";
            public const string IntegratedDate = "IntegratedDate";
        }
        #endregion
    }
}
