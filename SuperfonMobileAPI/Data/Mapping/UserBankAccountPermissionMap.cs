using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserBankAccountPermissionMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserBankAccountPermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserBankAccountPermission> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserBankAccountPermission", "dbo");

            // key
            builder.HasKey(t => t.UserBankAccountPermissionId);

            // properties
            builder.Property(t => t.UserBankAccountPermissionId)
                .IsRequired()
                .HasColumnName("UserBankAccountPermissionId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.BankAccountCode)
                .IsRequired()
                .HasColumnName("BankAccountCode")
                .HasColumnType("varchar(17)")
                .HasMaxLength(17);

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.UserBankAccountPermissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserBankAccountPermission_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserBankAccountPermission";
        }

        public struct Columns
        {
            public const string UserBankAccountPermissionId = "UserBankAccountPermissionId";
            public const string BankAccountCode = "BankAccountCode";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
