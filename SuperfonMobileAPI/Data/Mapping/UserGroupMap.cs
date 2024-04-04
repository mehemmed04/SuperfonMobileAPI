using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class UserGroupMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.UserGroup>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.UserGroup> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("UserGroup", "dbo");

            // key
            builder.HasKey(t => t.UserGroupId);

            // properties
            builder.Property(t => t.UserGroupId)
                .IsRequired()
                .HasColumnName("UserGroupId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.UserGroupName)
                .IsRequired()
                .HasColumnName("UserGroupName")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.IsActive)
                .IsRequired()
                .HasColumnName("IsActive")
                .HasColumnType("bit");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "UserGroup";
        }

        public struct Columns
        {
            public const string UserGroupId = "UserGroupId";
            public const string UserGroupName = "UserGroupName";
            public const string IsActive = "IsActive";
        }
        #endregion
    }
}
