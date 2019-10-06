using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Attiva.Freight.Authorization.Domain.Entities;

namespace Attiva.Freight.Authorization.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<UserApp> UserApp { get; set; }

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

            modelBuilder.Entity<UserApp>(entity => {
                entity.ToTable("tb_user");
                entity.HasKey(k => k.UserId);
                entity.Property(p => p.UserId).HasColumnName("cd_app_user");
                entity.Property(p => p.SecretKey).HasColumnName("vl_secret_key");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
