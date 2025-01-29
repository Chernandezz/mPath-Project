using Microsoft.EntityFrameworkCore;
using mPathProject.Models;
using System;

namespace mPathProject.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { id = 1, firstName = "John", lastName = "Doe", active = true, email = "john.doe@example.com" },
                new Doctor { id = 2, firstName = "Jane", lastName = "Smith", active = true, email = "jane.smith@example.com" }
            );

            // Seed data for Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient { id = 1, firstName = "Alice", lastName = "Johnson", address = "123 Main St", phoneNumber = "1234567890", email = "alice.johnson@example.com", observations = "" },
                new Patient { id = 2, firstName = "Bob", lastName = "Brown", address = "456 Elm St", phoneNumber = "0987654321", email = "bob.brown@example.com", observations = "" }
            );

            // Seed data for Admissions
            modelBuilder.Entity<Admission>().HasData(
                new Admission { id = 1, patientName = "Alice Johnson", admissionDate = new DateTime(2023, 10, 1), diagnosis = "Flu", observation = "Mild symptoms", doctorId = 1, patientId = 1 },
                new Admission { id = 2, patientName = "Bob Brown", admissionDate = new DateTime(2023, 10, 2), diagnosis = "Broken arm", observation = "Needs surgery", doctorId = 2, patientId = 2 }
            );

            // Seed data for Discharges
            modelBuilder.Entity<Discharge>().HasData(
                new Discharge { id = 1, treatment = "Antibiotics", doctorName = "John Doe", patientName = "Alice Johnson", dischargeDate = new DateTime(2023, 10, 5), amount = 100.00m, doctorId = 1, admissionId = 1 },
                new Discharge { id = 2, treatment = "Surgery", doctorName = "Jane Smith", patientName = "Bob Brown", dischargeDate = new DateTime(2023, 10, 6), amount = 500.00m, doctorId = 2, admissionId = 2 }
            );

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { id = 1, email = "doctor@doctor.com", password = "AQAAAAIAAYagAAAAEMPv0sp+FoVDuxBgcsym2p7fRtW3+em2g2W7wd0pgNFKwmXtkjCFcJIEyy2cmXK6pw==", userRole = "Doctor" },
                new User { id = 2, email = "admin@admin.com", password = "AQAAAAIAAYagAAAAEHAgp0HFG9qc9eOr9W15Dp/qcrk3rbaCDo97hBui3SIlwim0ojzgn8lUQxBwEyWIaQ==", userRole = "Admin" }
            );
        }
    }
}
