using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuperfonWorks.Data.Mapping
{
    public partial class SalesmanPlanMap
        : IEntityTypeConfiguration<SuperfonWorks.Data.Entities.SalesmanPlan>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SuperfonWorks.Data.Entities.SalesmanPlan> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("SalesmanPlan", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Year)
                .HasColumnName("Year_")
                .HasColumnType("int");

            builder.Property(t => t.Month)
                .HasColumnName("Month_")
                .HasColumnType("int");

            builder.Property(t => t.SalesmanCode)
                .IsRequired()
                .HasColumnName("SalesmanCode")
                .HasColumnType("varchar(25)")
                .HasMaxLength(25);

            builder.Property(t => t.PlannedSale)
                .HasColumnName("PlannedSale")
                .HasColumnType("float");

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "SalesmanPlan";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string Year = "Year_";
            public const string Month = "Month_";
            public const string SalesmanCode = "SalesmanCode";
            public const string PlannedSale = "PlannedSale";
        }
        #endregion
    }
}
