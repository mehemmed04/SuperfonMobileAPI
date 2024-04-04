using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class BusinessTripDeclarationMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.BusinessTripDeclaration>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.BusinessTripDeclaration> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("BusinessTripDeclaration", "dbo");

            // key
            builder.HasKey(t => t.BusinessTripDeclarationId);

            // properties
            builder.Property(t => t.BusinessTripDeclarationId)
                .IsRequired()
                .HasColumnName("BusinessTripDeclarationId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.DeclarationDate)
                .IsRequired()
                .HasColumnName("DeclarationDate")
                .HasColumnType("datetime");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.BusinessTripDeclarations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_BusinessTripDeclaration_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "BusinessTripDeclaration";
        }

        public struct Columns
        {
            public const string BusinessTripDeclarationId = "BusinessTripDeclarationId";
            public const string DeclarationDate = "DeclarationDate";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
