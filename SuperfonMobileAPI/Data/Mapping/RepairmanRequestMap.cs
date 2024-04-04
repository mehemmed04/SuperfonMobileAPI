using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class RepairmanRequestMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.RepairmanRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.RepairmanRequest> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("RepairmanRequest", "dbo");

            // key
            builder.HasKey(t => t.RepairmanRequestId);

            // properties
            builder.Property(t => t.RepairmanRequestId)
                .IsRequired()
                .HasColumnName("RepairmanRequestId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Ficheno)
                .IsRequired()
                .HasColumnName("Ficheno")
                .HasColumnType("varchar(15)")
                .HasMaxLength(15);

            builder.Property(t => t.RequestDate)
                .IsRequired()
                .HasColumnName("RequestDate")
                .HasColumnType("datetime");

            builder.Property(t => t.SparePartPrice)
                .IsRequired()
                .HasColumnName("SparePartPrice")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.RepairFee)
                .IsRequired()
                .HasColumnName("RepairFee")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.Note)
                .IsRequired()
                .HasColumnName("Note")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "RepairmanRequest";
        }

        public struct Columns
        {
            public const string RepairmanRequestId = "RepairmanRequestId";
            public const string Ficheno = "Ficheno";
            public const string RequestDate = "RequestDate";
            public const string SparePartPrice = "SparePartPrice";
            public const string RepairFee = "RepairFee";
            public const string Note = "Note";
            public const string UserId = "UserId";
        }
        #endregion
    }
}
