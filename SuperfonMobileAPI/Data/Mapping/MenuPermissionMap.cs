using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class MenuPermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.MenuPermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.MenuPermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("MenuPermission", "dbo");

            // key
            builder.HasKey(t => t.MenuPermissionId);

            // properties
            builder.Property(t => t.MenuPermissionId)
                .IsRequired()
                .HasColumnName("MenuPermissionId")
                .HasColumnType("int");

            builder.Property(t => t.ParentId)
                .IsRequired()
                .HasColumnName("ParentId")
                .HasColumnType("int");

            builder.Property(t => t.MenuPermissionTypeId)
                .IsRequired()
                .HasColumnName("MenuPermissionTypeId")
                .HasColumnType("tinyint");

            builder.Property(t => t.PermissionName)
                .IsRequired()
                .HasColumnName("PermissionName")
                .HasColumnType("nvarchar(80)")
                .HasMaxLength(80);

            builder.Property(t => t.KeyWord)
                .IsRequired()
                .HasColumnName("KeyWord")
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("bit");

            builder.Property(t => t.IconName)
                .HasColumnName("IconName")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Link)
                .HasColumnName("Link")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "MenuPermission";
        }

        public struct Columns
        {
            public const string MenuPermissionId = "MenuPermissionId";
            public const string ParentId = "ParentId";
            public const string MenuPermissionTypeId = "MenuPermissionTypeId";
            public const string PermissionName = "PermissionName";
            public const string KeyWord = "KeyWord";
            public const string IsActive = "IsActive";
            public const string IconName = "IconName";
            public const string Link = "Link";
        }
        #endregion
    }
}
