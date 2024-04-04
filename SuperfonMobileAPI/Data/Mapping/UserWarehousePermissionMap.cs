using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserWarehousePermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserWarehousePermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserWarehousePermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserWarehousePermission", "dbo");

            // key
            builder.HasKey(t => t.UserWarehousePermissionId);

            // properties
            builder.Property(t => t.UserWarehousePermissionId)
                .IsRequired()
                .HasColumnName("UserWarehousePermissionId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.WarehousePermissionTypeId)
                .IsRequired()
                .HasColumnName("WarehousePermissionTypeId")
                .HasColumnType("tinyint");

            builder.Property(t => t.WarehouseNumber)
                .IsRequired()
                .HasColumnName("WarehouseNumber")
                .HasColumnType("int");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserWarehousePermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserWarehousePermission_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserWarehousePermission";
        }

        public struct Columns
        {
            public const string UserWarehousePermissionId = "UserWarehousePermissionId";
            public const string WarehousePermissionTypeId = "WarehousePermissionTypeId";
            public const string WarehouseNumber = "WarehouseNumber";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
