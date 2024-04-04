using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class ExpenseAdvanceDeclarationViewMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.ExpenseAdvanceDeclarationView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.ExpenseAdvanceDeclarationView> builder)
        {
            #region Generated Configure
            // table
            builder.ToView("ExpenseAdvanceDeclarationView", "dbo");

            // key
            builder.HasNoKey();

            // properties
            builder.Property(t => t.ExpenseDeclarationId)
                .IsRequired()
                .HasColumnName("ExpenseDeclarationId")
                .HasColumnType("int");

            builder.Property(t => t.DeclarationDate)
                .IsRequired()
                .HasColumnName("DeclarationDate")
                .HasColumnType("datetime");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.DeclarationNote)
                .IsRequired()
                .HasColumnName("DeclarationNote")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            builder.Property(t => t.EFlowStatus)
                .IsRequired()
                .HasColumnName("EFlowStatus")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "ExpenseAdvanceDeclarationView";
        }

        public struct Columns
        {
            public const string ExpenseDeclarationId = "ExpenseDeclarationId";
            public const string DeclarationDate = "DeclarationDate";
            public const string UserId = "UserId";
            public const string DeclarationNote = "DeclarationNote";
            public const string EFlowStatus = "EFlowStatus";
        }
        #endregion
    }
}
