using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class BusinessTripRequestMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.BusinessTripRequest>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.BusinessTripRequest> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("BusinessTripRequest", "dbo");

            // key
            builder.HasKey(t => t.BusinessTripRequestId);

            // properties
            builder.Property(t => t.BusinessTripRequestId)
                .IsRequired()
                .HasColumnName("BusinessTripRequestId")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.RequestDate)
                .IsRequired()
                .HasColumnName("RequestDate")
                .HasColumnType("datetime");

            builder.Property(t => t.RequestAmount)
                .IsRequired()
                .HasColumnName("RequestAmount")
                .HasColumnType("decimal(9,2)");

            builder.Property(t => t.RequestDescription)
                .HasColumnName("RequestDescription")
                .HasColumnType("nvarchar(201)")
                .HasMaxLength(201);

            builder.Property(t => t.TripDaysCount)
                .IsRequired()
                .HasColumnName("TripDaysCount")
                .HasColumnType("int");

            builder.Property(t => t.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("int");

            builder.Property(t => t.BusinessTripDeclarationId)
                .HasColumnName("BusinessTripDeclarationId")
                .HasColumnType("int");

            // relationships
            builder.HasOne(t => t.BusinessTripDeclaration)
                .WithMany(t => t.BusinessTripRequests)
                .HasForeignKey(d => d.BusinessTripDeclarationId)
                .HasConstraintName("FK_BusinessTripRequest_BusinessTripDeclaration");

            builder.HasOne(t => t.User)
                .WithMany(t => t.BusinessTripRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_BusinessTripRequest_User_");

            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "BusinessTripRequest";
        }

        public struct Columns
        {
            public const string BusinessTripRequestId = "BusinessTripRequestId";
            public const string RequestDate = "RequestDate";
            public const string RequestAmount = "RequestAmount";
            public const string RequestDescription = "RequestDescription";
            public const string TripDaysCount = "TripDaysCount";
            public const string UserId = "UserId";
            public const string BusinessTripDeclarationId = "BusinessTripDeclarationId";
        }
        #endregion
    }
}
