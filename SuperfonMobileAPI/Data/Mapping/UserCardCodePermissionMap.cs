using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserCardCodePermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserCardCodePermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserCardCodePermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserCardCodePermission", "dbo");

            // key
            builder.HasKey(t => t.UserCardCodePermissionId);

            // properties
            builder.Property(t => t.UserCardCodePermissionId)
                .IsRequired()
                .HasColumnName("UserCardCodePermissionId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.CardPermissionTypeId)
                .IsRequired()
                .HasColumnName("CardPermissionTypeId")
                .HasColumnType("tinyint");

            builder.Property(t => t.CardCode)
                .IsRequired()
                .HasColumnName("CardCode")
                .HasColumnType("varchar(17)")
                .HasMaxLength(17);

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserCardCodePermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserCardCodePermission_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserCardCodePermission";
        }

        public struct Columns
        {
            public const string UserCardCodePermissionId = "UserCardCodePermissionId";
            public const string CardPermissionTypeId = "CardPermissionTypeId";
            public const string CardCode = "CardCode";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
