using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserSafeboxPermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserSafeboxPermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserSafeboxPermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserSafeboxPermission", "dbo");

            // key
            builder.HasKey(t => t.UserSafeboxPermissionId);

            // properties
            builder.Property(t => t.UserSafeboxPermissionId)
                .IsRequired()
                .HasColumnName("UserSafeboxPermissionId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.SafeboxCode)
                .IsRequired()
                .HasColumnName("SafeboxCode")
                .HasColumnType("varchar(17)")
                .HasMaxLength(17);

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserSafeboxPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserSafeboxPermission_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserSafeboxPermission";
        }

        public struct Columns
        {
            public const string UserSafeboxPermissionId = "UserSafeboxPermissionId";
            public const string SafeboxCode = "SafeboxCode";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
