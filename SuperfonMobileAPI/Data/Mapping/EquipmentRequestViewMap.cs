using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class EquipmentRequestViewMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.EquipmentRequestView>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.EquipmentRequestView> builder)
        {
            #region Generated Configure
            // table
            builder.ToView("EquipmentRequestView", "dbo");

            // key
            builder.HasNoKey();

            // properties
            builder.Property(t => t.EquipmentRequestId)
                .IsRequired()
                .HasColumnName("EquipmentRequestId")
                .HasColumnType("int");

            builder.Property(t => t.RequestDate)
                .IsRequired()
                .HasColumnName("RequestDate")
                .HasColumnType("datetime");

            builder.Property(t => t.RequestDescription)
                .IsRequired()
                .HasColumnName("RequestDescription")
                .HasColumnType("nvarchar(150)")
                .HasMaxLength(150);

            builder.Property(t => t.Quantity)
                .IsRequired()
                .HasColumnName("Quantity")
                .HasColumnType("int");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.Note)
                .HasColumnName("Note")
                .HasColumnType("nvarchar(250)")
                .HasMaxLength(250);

            builder.Property(t => t.EFlowStatus)
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
            public const string Name = "EquipmentRequestView";
        }

        public struct Columns
        {
            public const string EquipmentRequestId = "EquipmentRequestId";
            public const string RequestDate = "RequestDate";
            public const string RequestDescription = "RequestDescription";
            public const string Quantity = "Quantity";
            public const string UserId = "UserId";
            public const string Note = "Note";
            public const string EFlowStatus = "EFlowStatus";
        }
        #endregion
    }
}
