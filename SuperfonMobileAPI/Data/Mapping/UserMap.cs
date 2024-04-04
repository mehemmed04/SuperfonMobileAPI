using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.User> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("User_", "dbo");

            // key
            builder.HasKey(t => t.UserId);

            // properties
            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Username)
                .IsRequired()
                .HasColumnName("Username")
                .HasColumnType("varchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.DisplayName)
                .IsRequired()
                .HasColumnName("DisplayName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.PassHash)
                .IsRequired()
                .HasColumnName("PassHash")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("bit");

            builder.Property(t => t.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(t => t.Phone)
                .HasColumnName("Phone")
                .HasColumnType("varchar(30)")
                .HasMaxLength(30);

            builder.Property(t => t.UserPID)
                .HasColumnName("UserPID")
                .HasColumnType("varchar(7)")
                .HasMaxLength(7);

            builder.Property(t => t.UserGroupId)
                .HasColumnName("UserGroupId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.UserGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserGroupId)
                .HasConstraintName("FK_User__UserGroup");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "User_";
        }

        public struct Columns
        {
            public const string UserId = "UserId";
            public const string Username = "Username";
            public const string DisplayName = "DisplayName";
            public const string PassHash = "PassHash";
            public const string IsActive = "IsActive";
            public const string Email = "Email";
            public const string Phone = "Phone";
            public const string UserPID = "UserPID";
            public const string UserGroupId = "UserGroupId";
        }
        #endregion
    }
}
