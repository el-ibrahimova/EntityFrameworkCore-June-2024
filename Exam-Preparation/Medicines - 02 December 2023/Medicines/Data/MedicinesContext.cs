﻿using Medicines.Data.Models;

namespace Medicines.Data
{
    using Microsoft.EntityFrameworkCore;
    public class MedicinesContext : DbContext
    {
        public MedicinesContext()
        {
        }

        public MedicinesContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        public DbSet<Medicine> Medicines { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Pharmacy> Pharmacies { get; set; } = null!;
        public DbSet<PatientMedicine> PatientsMedicines { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                      modelBuilder.Entity<PatientMedicine>()
                .HasKey(pm => new { pm.PatientId, pm.MedicineId});

        }
    }
}
