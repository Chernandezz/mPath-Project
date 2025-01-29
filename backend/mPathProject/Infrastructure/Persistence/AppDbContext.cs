using Microsoft.EntityFrameworkCore;
using mPathProject.Domain.Entities;
using System;

namespace mPathProject.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Admission>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Discharge>()
                .HasOne(d => d.Admission)
                .WithMany()
                .HasForeignKey(d => d.AdmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "doctor1@hospital.com", Password = "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", UserRole = "Doctor" },
                new User { Id = 2, Email = "doctor2@hospital.com", Password = "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", UserRole = "Doctor" },
                new User { Id = 3, Email = "admin@hospital.com", Password = "AQAAAAIAAYagAAAAEHAgp0HFG9qc9eOr9W15Dp/qcrk3rbaCDo97hBui3SIlwim0ojzgn8lUQxBwEyWIaQ==", UserRole = "Admin" },
                new User { Id = 4, Email = "patient1@hospital.com", Password = "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", UserRole = "Patient" },
                new User { Id = 5, Email = "patient2@hospital.com", Password = "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", UserRole = "Patient" }
            );

            // Seed data for Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FirstName = "John", LastName = "Doe", Active = true },
                new Doctor { Id = 2, FirstName = "Jane", LastName = "Smith", Active = true }
            );

            // Seed data for Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 1, FirstName = "Alice", LastName = "Johnson", Address = "123 Main St", PhoneNumber = "1234567890", Observations = "Diabetic" },
                new Patient { Id = 2, FirstName = "Bob", LastName = "Brown", Address = "456 Elm St", PhoneNumber = "0987654321", Observations = "Asthma" }
            );

            // Seed data for Admissions
            modelBuilder.Entity<Admission>().HasData(
                new Admission { Id = 1, AdmissionDate = new DateTime(2023, 10, 1), Diagnosis = "Flu", Observation = "Mild symptoms", DoctorId = 1, PatientId = 1 },
                new Admission { Id = 2, AdmissionDate = new DateTime(2023, 10, 5), Diagnosis = "Broken Arm", Observation = "Needs surgery", DoctorId = 2, PatientId = 2 },
                new Admission { Id = 3, AdmissionDate = new DateTime(2024, 1, 15), Diagnosis = "Migraine", Observation = "Severe headaches", DoctorId = 1, PatientId = 1 }
            );

            // Seed data for Discharges
            modelBuilder.Entity<Discharge>().HasData(
                new Discharge { Id = 1, Treatment = "Antibiotics", DischargeDate = new DateTime(2023, 10, 5), Amount = 100.00m, IsPaid = true, AdmissionId = 1 },
                new Discharge { Id = 2, Treatment = "Surgery", DischargeDate = new DateTime(2023, 10, 10), Amount = 500.00m, IsPaid = false, AdmissionId = 2 },
                new Discharge { Id = 3, Treatment = "Painkillers", DischargeDate = new DateTime(2024, 1, 20), Amount = 75.00m, IsPaid = false, AdmissionId = 3 }
            );
        }
    }
}