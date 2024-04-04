using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserBranchPermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserBranchPermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserBranchPermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserBranchPermission", "dbo");

            // key
            builder.HasKey(t => t.UserBranchPermissionId);

            // properties
            builder.Property(t => t.UserBranchPermissionId)
                .IsRequired()
                .HasColumnName("UserBranchPermissionId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.BranchNumber)
                .IsRequired()
                .HasColumnName("BranchNumber")
                .HasColumnType("int");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserBranchPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserBranchPermission_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserBranchPermission";
        }

        public struct Columns
        {
            public const string UserBranchPermissionId = "UserBranchPermissionId";
            public const string BranchNumber = "BranchNumber";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
