using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class ExpenseDeclarationMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.ExpenseDeclaration>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.ExpenseDeclaration> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("ExpenseDeclaration", "dbo");

            // key
            builder.HasKey(t => t.ExpenseDeclarationId);

            // properties
            builder.Property(t => t.ExpenseDeclarationId)
                .IsRequired()
                .HasColumnName("ExpenseDeclarationId")
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

            builder.Property(t => t.DeclarationNote)
                .HasColumnName("DeclarationNote")
                .HasColumnType("nvarchar(max)");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.ExpenseDeclarations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ExpenseDeclaration_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "ExpenseDeclaration";
        }

        public struct Columns
        {
            public const string ExpenseDeclarationId = "ExpenseDeclarationId";
            public const string DeclarationDate = "DeclarationDate";
            public const string UserId = "UserId";
            public const string DeclarationNote = "DeclarationNote";
        }
        #endregion
    }
}
