using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class SafeAmountTransferMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.SafeAmountTransfer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.SafeAmountTransfer> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("SafeAmountTransfer", "dbo");

            // key
            builder.HasKey(t => t.SafeAmountTransferId);

            // properties
            builder.Property(t => t.SafeAmountTransferId)
                .IsRequired()
                .HasColumnName("SafeAmountTransferId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.TransferType)
                .IsRequired()
                .HasColumnName("TransferType")
                .HasColumnType("tinyint");

            builder.Property(t => t.SourceSafeboxCode)
                .IsRequired()
                .HasColumnName("SourceSafeboxCode")
                .HasColumnType("varchar(17)")
                .HasMaxLength(17);

            builder.Property(t => t.DestinationCode)
                .IsRequired()
                .HasColumnName("DestinationCode")
                .HasColumnType("varchar(17)")
                .HasMaxLength(17);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnName("Amount")
                .HasColumnType("float");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            builder.Property(t => t.DateCreated)
                .IsRequired()
                .HasColumnName("DateCreated")
                .HasColumnType("datetime");

            // relationships
            builder.HasOne(t => t.User)
                .WithMany(t => t.SafeAmountTransfers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SafeAmountTransfer_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "SafeAmountTransfer";
        }

        public struct Columns
        {
            public const string SafeAmountTransferId = "SafeAmountTransferId";
            public const string UserId = "UserId";
            public const string TransferType = "TransferType";
            public const string SourceSafeboxCode = "SourceSafeboxCode";
            public const string DestinationCode = "DestinationCode";
            public const string Amount = "Amount";
            public const string Note = "Note";
            public const string DateCreated = "DateCreated";
        }
        #endregion
    }
}
