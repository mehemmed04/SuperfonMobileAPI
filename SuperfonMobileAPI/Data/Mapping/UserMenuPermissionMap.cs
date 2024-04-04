using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserMenuPermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserMenuPermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserMenuPermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserMenuPermission", "dbo");

            // key
            builder.HasKey(t => t.UserMenuPermissionId);

            // properties
            builder.Property(t => t.UserMenuPermissionId)
                .IsRequired()
                .HasColumnName("UserMenuPermissionId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.MenuPermissionId)
                .IsRequired()
                .HasColumnName("MenuPermissionId")
                .HasColumnType("int");

            builder.Property(t => t.UserId)
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.GroupId)
                .HasColumnName("GroupId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.MenuPermission)
                .WithMany(t => t.UserMenuPermissions)
                .HasForeignKey(d => d.MenuPermissionId)
                .HasConstraintName("FK_UserMenuPermission_MenuPermission");

            builder.HasOne(t => t.User)
                .WithMany(t => t.UserMenuPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserMenuPermission_User_");

            builder.HasOne(t => t.GroupUserGroup)
                .WithMany(t => t.GroupUserMenuPermissions)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_UserMenuPermission_UserGroup");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserMenuPermission";
        }

        public struct Columns
        {
            public const string UserMenuPermissionId = "UserMenuPermissionId";
            public const string MenuPermissionId = "MenuPermissionId";
            public const string UserId = "UserId";
            public const string GroupId = "GroupId";
        }
        #endregion
    }
}
