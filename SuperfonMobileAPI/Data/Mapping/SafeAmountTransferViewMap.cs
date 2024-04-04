using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class SafeAmountTransferViewMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.SafeAmountTransferView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.SafeAmountTransferView> builder)
        {
            #region Generated Configure
            // table
            builder.ToView("SafeAmountTransferView", "dbo");

            // key
            builder.HasNoKey();

            // properties
            builder.Property(t => t.SafeAmountTransferId)
                .IsRequired()
                .HasColumnName("SafeAmountTransferId")
                .HasColumnType("int");

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

            builder.Property(t => t.SourceSafeboxName)
                .HasColumnName("SourceSafeboxName")
                .HasColumnType("varchar(51)")
                .HasMaxLength(51);

            builder.Property(t => t.DestinationName)
                .HasColumnName("DestinationName")
                .HasColumnType("varchar(51)")
                .HasMaxLength(51);

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
            public const string Name = "SafeAmountTransferView";
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
            public const string SourceSafeboxName = "SourceSafeboxName";
            public const string DestinationName = "DestinationName";
            public const string EFlowStatus = "EFlowStatus";
        }
        #endregion
    }
}
