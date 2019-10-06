using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Attiva.Freight.Tracking.Domain.Entities;

namespace Attiva.Freight.Tracking.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Carrier> Carriers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var settings = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .Build();

            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseOracle(
                        settings.GetConnectionString("Freight")
                        );
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("api_schema");

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("tb_vehicle");
                entity.HasKey(k => k.Tag);
                entity.Property(p => p.Tag).HasColumnName("cd_tag");
                entity.Property(p => p.Model).HasColumnName("ds_model");
                entity.HasOne(fk => fk.Carrier).WithMany(m => m.Vehicles).HasForeignKey(fk => fk.id_carrier);
                //entity.Ignore(i => i.Routes);
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.ToTable("tb_carrier");
                entity.HasKey(k => k.Id);
                entity.Property(p => p.Id).HasColumnName("id_carrier");
                entity.Property(p => p.Name).HasColumnName("nm_carrier");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
