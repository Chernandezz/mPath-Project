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
                new User { Id = 1, Email = "doctor1@mpath.com", Password = "AQAAAAIAAYagAAAAEHpzzGduxSaFNFRg1NXvlnSvyaNodZhb4X2JAagrvv9lYjWu5TE+eTjJuxW7mYSPxg==", UserRole = "Doctor" },
                new User { Id = 2, Email = "doctor2@mpath.com", Password = "AQAAAAIAAYagAAAAEAwrieiPPgidWdj2lJRmwhUd99kSXzzEYuncTQwmT8m/PBp5/8lSu5flU+KI51gWrg==", UserRole = "Doctor" },
                new User { Id = 3, Email = "doctor3@mpath.com", Password = "AQAAAAIAAYagAAAAEBElaSGBBQQWFJ3uMzRThhXtjMidHGBKxx+zwn5br/gKVRi6pm8kiIX15CwR0ViOww==", UserRole = "Doctor" },
                new User { Id = 4, Email = "doctor4@mpath.com", Password = "AQAAAAIAAYagAAAAEIfMmfWaejYvBxevXbU+zMq4ZOs6klJzocuAVw7hQ47iKPoHHvP9HWVWf7G6P2RUmg==", UserRole = "Doctor" },
                new User { Id = 5, Email = "admin@mpath.com", Password = "AQAAAAIAAYagAAAAEB26ifLwjK8pAEO+TqAl2o69NT2sqP4q7SIxD6nPAoP9QGz6juG04htWQCvGsyBTnQ==", UserRole = "Admin" },
                new User { Id = 6, Email = "patient1@mpath.com", Password = "AQAAAAIAAYagAAAAEH11M9u+V8SZaRZ5qT73R2C9WkBZzXzo2yE1fW6qCvNNprrYy6EB2fji0QFIox82Ow==", UserRole = "Patient" },
                new User { Id = 7, Email = "patient2@mpath.com", Password = "AQAAAAIAAYagAAAAEJL1d/dzqJTKTbwCwEOLjFLiL7jEm/PynXCkI8rY+tNfze1OQbsEZ8qBq7IKHJmLkg==", UserRole = "Patient" },
                new User { Id = 8, Email = "patient3@mpath.com", Password = "AQAAAAIAAYagAAAAEINuyY0qz0W9k0h4sFQ2230H1seVmEzonizJ/M6Wb6+S6JmNgp/QUURqsUxswlQo3A==", UserRole = "Patient" },
                new User { Id = 9, Email = "patient4@mpath.com", Password = "AQAAAAIAAYagAAAAEGF9/FBqefRSxaLtROHejXQjlfJGHK2bdLHkEODyviuMKF36F6CoV2TXa11u40Vgog==", UserRole = "Patient" }
            );

            // Seed data for Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { Id = 1, FirstName = "Mary", LastName = "Hobza", Active = true },
                new Doctor { Id = 2, FirstName = "Jane", LastName = "Smith", Active = true },
                new Doctor { Id = 3, FirstName = "Cristian", LastName = "Hernandez", Active = true },
                new Doctor { Id = 4, FirstName = "Thomas", LastName = "Traverzo", Active = true }
            );

            // Seed data for Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient { Id = 6, FirstName = "Alice", LastName = "Johnson", Address = "123 Main St", PhoneNumber = "1234567890", Observations = "Diabetic" },
                new Patient { Id = 7, FirstName = "Bob", LastName = "Brown", Address = "456 Elm St", PhoneNumber = "0987654321", Observations = "Asthma" },
                new Patient { Id = 8, FirstName = "Alice", LastName = "Johnson", Address = "123 Main St", PhoneNumber = "1234567890", Observations = "Diabetic" },
                new Patient { Id = 9, FirstName = "Bob", LastName = "Brown", Address = "456 Elm St", PhoneNumber = "0987654321", Observations = "Asthma" }
            );

            // Seed data for Admissions
            modelBuilder.Entity<Admission>().HasData(
                new Admission { Id = 1, AdmissionDate = new DateTime(2023, 10, 1), Diagnosis = "Flu", Observation = "Mild symptoms", DoctorId = 1, PatientId = 6 },
                new Admission { Id = 2, AdmissionDate = new DateTime(2023, 10, 5), Diagnosis = "Broken Arm", Observation = "Needs surgery", DoctorId = 2, PatientId = 7 },
                new Admission { Id = 3, AdmissionDate = new DateTime(2024, 1, 15), Diagnosis = "Migraine", Observation = "Severe headaches", DoctorId = 1, PatientId = 6 },
                new Admission { Id = 4, AdmissionDate = new DateTime(2021, 2, 10), Diagnosis = "Pneumonia", Observation = "Requires oxygen therapy", DoctorId = 3, PatientId = 8 },
                new Admission { Id = 5, AdmissionDate = new DateTime(2022, 3, 5), Diagnosis = "Food Poisoning", Observation = "Dehydration", DoctorId = 4, PatientId = 9 },
                new Admission { Id = 6, AdmissionDate = new DateTime(2024, 3, 20), Diagnosis = "Fractured Rib", Observation = "Pain management required", DoctorId = 2, PatientId = 7 },
                new Admission { Id = 7, AdmissionDate = new DateTime(2024, 4, 1), Diagnosis = "Appendicitis", Observation = "Surgery needed", DoctorId = 1, PatientId = 8 },
                new Admission { Id = 8, AdmissionDate = new DateTime(2024, 4, 12), Diagnosis = "COVID-19", Observation = "Respiratory distress", DoctorId = 3, PatientId = 9 },
                new Admission { Id = 9, AdmissionDate = new DateTime(2024, 5, 3), Diagnosis = "Kidney Infection", Observation = "High fever", DoctorId = 2, PatientId = 6 },
                new Admission { Id = 10, AdmissionDate = new DateTime(2024, 5, 18), Diagnosis = "Hypertension", Observation = "Blood pressure monitoring", DoctorId = 4, PatientId = 7 },
                new Admission { Id = 11, AdmissionDate = new DateTime(2024, 6, 2), Diagnosis = "Dengue Fever", Observation = "Severe fatigue", DoctorId = 1, PatientId = 9 },
                new Admission { Id = 12, AdmissionDate = new DateTime(2024, 6, 15), Diagnosis = "Allergic Reaction", Observation = "Severe swelling", DoctorId = 3, PatientId = 8 },
                new Admission { Id = 13, AdmissionDate = new DateTime(2025, 1, 1), Diagnosis = "Stroke", Observation = "Immediate intervention required", DoctorId = 4, PatientId = 6 },
                new Admission { Id = 14, AdmissionDate = new DateTime(2025, 1, 10), Diagnosis = "Gallstones", Observation = "Surgical consultation", DoctorId = 2, PatientId = 7 },
                new Admission { Id = 15, AdmissionDate = new DateTime(2025, 1, 25), Diagnosis = "Depression", Observation = "Psychiatric evaluation", DoctorId = 1, PatientId = 8 }
            );

            // Seed data for Discharges
            modelBuilder.Entity<Discharge>().HasData(
                new Discharge { Id = 1, Treatment = "Antibiotics", DischargeDate = new DateTime(2023, 10, 5), Amount = 100.00m, IsPaid = true, AdmissionId = 1 },
                new Discharge { Id = 2, Treatment = "Surgery", DischargeDate = new DateTime(2023, 10, 10), Amount = 500.00m, IsPaid = false, AdmissionId = 2 },
                new Discharge { Id = 3, Treatment = "Painkillers", DischargeDate = new DateTime(2024, 1, 20), Amount = 75.00m, IsPaid = false, AdmissionId = 3 },
                new Discharge { Id = 4, Treatment = "Oxygen Therapy", DischargeDate = new DateTime(2024, 2, 15), Amount = 300.00m, IsPaid = true, AdmissionId = 4 },
                new Discharge { Id = 5, Treatment = "IV Fluids", DischargeDate = new DateTime(2024, 3, 7), Amount = 150.00m, IsPaid = true, AdmissionId = 5 },
                new Discharge { Id = 6, Treatment = "Pain Management", DischargeDate = new DateTime(2024, 3, 25), Amount = 200.00m, IsPaid = false, AdmissionId = 6 },
                new Discharge { Id = 7, Treatment = "Appendectomy", DischargeDate = new DateTime(2024, 4, 5), Amount = 800.00m, IsPaid = false, AdmissionId = 7 },
                new Discharge { Id = 8, Treatment = "Ventilator Support", DischargeDate = new DateTime(2024, 4, 20), Amount = 1000.00m, IsPaid = false, AdmissionId = 8 },
                new Discharge { Id = 9, Treatment = "Antibiotics", DischargeDate = new DateTime(2024, 5, 8), Amount = 120.00m, IsPaid = true, AdmissionId = 9 },
                new Discharge { Id = 10, Treatment = "Blood Pressure Medication", DischargeDate = new DateTime(2024, 5, 22), Amount = 80.00m, IsPaid = true, AdmissionId = 10 },
                new Discharge { Id = 11, Treatment = "Hydration Therapy", DischargeDate = new DateTime(2024, 6, 5), Amount = 200.00m, IsPaid = false, AdmissionId = 11 },
                new Discharge { Id = 12, Treatment = "Antihistamines", DischargeDate = new DateTime(2024, 6, 18), Amount = 50.00m, IsPaid = true, AdmissionId = 12 },
                new Discharge { Id = 13, Treatment = "Rehabilitation Therapy", DischargeDate = new DateTime(2025, 1, 5), Amount = 600.00m, IsPaid = false, AdmissionId = 13 },
                new Discharge { Id = 14, Treatment = "Cholecystectomy", DischargeDate = new DateTime(2025, 1, 12), Amount = 750.00m, IsPaid = true, AdmissionId = 14 },
                new Discharge { Id = 15, Treatment = "Counseling and Medication", DischargeDate = new DateTime(2025, 1, 30), Amount = 400.00m, IsPaid = false, AdmissionId = 15 }
            );
        }
    }
}