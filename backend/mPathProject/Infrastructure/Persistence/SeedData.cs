using Microsoft.EntityFrameworkCore;
using mPathProject.Domain.Entities;

namespace mPathProject.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            // Seed Users
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { Email = "doctor1@mpath.com", Password = "AQAAAAIAAYagAAAAEHpzzGduxSaFNFRg1NXvlnSvyaNodZhb4X2JAagrvv9lYjWu5TE+eTjJuxW7mYSPxg==", UserRole = "Doctor" },
                    new User { Email = "doctor2@mpath.com", Password = "AQAAAAIAAYagAAAAEAwrieiPPgidWdj2lJRmwhUd99kSXzzEYuncTQwmT8m/PBp5/8lSu5flU+KI51gWrg==", UserRole = "Doctor" },
                    new User { Email = "doctor3@mpath.com", Password = "AQAAAAIAAYagAAAAEBElaSGBBQQWFJ3uMzRThhXtjMidHGBKxx+zwn5br/gKVRi6pm8kiIX15CwR0ViOww==", UserRole = "Doctor" },
                    new User { Email = "doctor4@mpath.com", Password = "AQAAAAIAAYagAAAAEIfMmfWaejYvBxevXbU+zMq4ZOs6klJzocuAVw7hQ47iKPoHHvP9HWVWf7G6P2RUmg==", UserRole = "Doctor" },
                    new User { Email = "admin@mpath.com", Password = "AQAAAAIAAYagAAAAEB26ifLwjK8pAEO+TqAl2o69NT2sqP4q7SIxD6nPAoP9QGz6juG04htWQCvGsyBTnQ==", UserRole = "Admin" },
                    new User { Email = "patient1@mpath.com", Password = "AQAAAAIAAYagAAAAEH11M9u+V8SZaRZ5qT73R2C9WkBZzXzo2yE1fW6qCvNNprrYy6EB2fji0QFIox82Ow==", UserRole = "Patient" },
                    new User { Email = "patient2@mpath.com", Password = "AQAAAAIAAYagAAAAEJL1d/dzqJTKTbwCwEOLjFLiL7jEm/PynXCkI8rY+tNfze1OQbsEZ8qBq7IKHJmLkg==", UserRole = "Patient" },
                    new User { Email = "patient3@mpath.com", Password = "AQAAAAIAAYagAAAAEINuyY0qz0W9k0h4sFQ2230H1seVmEzonizJ/M6Wb6+S6JmNgp/QUURqsUxswlQo3A==", UserRole = "Patient" },
                    new User { Email = "patient4@mpath.com", Password = "AQAAAAIAAYagAAAAEGF9/FBqefRSxaLtROHejXQjlfJGHK2bdLHkEODyviuMKF36F6CoV2TXa11u40Vgog==", UserRole = "Patient" }
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }

            var doctorUsers = context.Users.Where(u => u.UserRole == "Doctor").ToList();
            var patientUsers = context.Users.Where(u => u.UserRole == "Patient").ToList();

            // Seed Doctors
            if (!context.Doctors.Any() && doctorUsers.Count >= 4)
            {
                var newDoctors = new List<Doctor>
                {
                    new Doctor { Id = doctorUsers[0].Id, FirstName = "Mary", LastName = "Hobza", Active = true },
                    new Doctor { Id = doctorUsers[1].Id, FirstName = "Jane", LastName = "Smith", Active = true },
                    new Doctor { Id = doctorUsers[2].Id, FirstName = "Cristian", LastName = "Hernandez", Active = true },
                    new Doctor { Id = doctorUsers[3].Id, FirstName = "Thomas", LastName = "Traverzo", Active = true }
                };

                context.Doctors.AddRange(newDoctors);
                context.SaveChanges();
            }

            // Seed Patients
            if (!context.Patients.Any() && patientUsers.Count >= 4)
            {
                var newPatients = new List<Patient>
                {
                    new Patient { Id = patientUsers[0].Id, FirstName = "Alice", LastName = "Johnson", Address = "123 Main St", PhoneNumber = "1234567890", Observations = "Diabetic" },
                    new Patient { Id = patientUsers[1].Id, FirstName = "Bob", LastName = "Brown", Address = "456 Elm St", PhoneNumber = "0987654321", Observations = "Asthma" },
                    new Patient { Id = patientUsers[2].Id, FirstName = "Clara", LastName = "Davis", Address = "789 Oak St", PhoneNumber = "1112223333", Observations = "Hypertension" },
                    new Patient { Id = patientUsers[3].Id, FirstName = "David", LastName = "Martinez", Address = "321 Pine St", PhoneNumber = "4445556666", Observations = "No allergies" }
                };

                context.Patients.AddRange(newPatients);
                context.SaveChanges();
            }

            var doctors = context.Doctors.ToList();
            var patients = context.Patients.ToList();

            // Seed Admissions
            if (!context.Admissions.Any() && doctors.Any() && patients.Any())
            {
                var newAdmissions = new List<Admission>
                {
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-50), Diagnosis = "Pneumonia", Observation = "Severe cough and fever.", DoctorId = doctors[0].Id, PatientId = patients[0].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-48), Diagnosis = "Diabetic Ketoacidosis", Observation = "Critical blood sugar levels.", DoctorId = doctors[1].Id, PatientId = patients[0].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-45), Diagnosis = "Asthma Attack", Observation = "Difficulty breathing, wheezing.", DoctorId = doctors[2].Id, PatientId = patients[1].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-42), Diagnosis = "Fractured Arm", Observation = "Fall injury while exercising.", DoctorId = doctors[3].Id, PatientId = patients[2].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-40), Diagnosis = "Appendicitis", Observation = "Severe abdominal pain.", DoctorId = doctors[0].Id, PatientId = patients[3].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-38), Diagnosis = "Hypertension Crisis", Observation = "Blood pressure dangerously high.", DoctorId = doctors[1].Id, PatientId = patients[2].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-35), Diagnosis = "Migraine", Observation = "Intense headache, light sensitivity.", DoctorId = doctors[2].Id, PatientId = patients[1].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-33), Diagnosis = "COVID-19", Observation = "Severe respiratory distress.", DoctorId = doctors[3].Id, PatientId = patients[0].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-30), Diagnosis = "Food Poisoning", Observation = "Vomiting and dehydration.", DoctorId = doctors[0].Id, PatientId = patients[3].Id },
                    new Admission { AdmissionDate = DateTime.UtcNow.AddDays(-28), Diagnosis = "Stroke", Observation = "Slurred speech, weakness.", DoctorId = doctors[1].Id, PatientId = patients[2].Id }
                };

                context.Admissions.AddRange(newAdmissions);
                context.SaveChanges();
            }

            var admissions = context.Admissions.ToList();

            // Seed Discharges
            if (!context.Discharges.Any() && admissions.Any())
            {
                var discharges = new List<Discharge>
                {
                    new Discharge { AdmissionId = admissions[0].Id, DischargeDate = admissions[0].AdmissionDate.AddDays(7), Recommendation = "Continue antibiotics, follow-up in 2 weeks.", Amount = 500.00m },
                    new Discharge { AdmissionId = admissions[1].Id, DischargeDate = admissions[1].AdmissionDate.AddDays(5), Recommendation = "Monitor blood glucose levels, hydrate well.", Amount = 350.00m },
                    new Discharge { AdmissionId = admissions[2].Id, DischargeDate = admissions[2].AdmissionDate.AddDays(4), Recommendation = "Use prescribed inhalers, avoid allergens.", Amount = 400.00m, IsPaid = true},
                    new Discharge { AdmissionId = admissions[3].Id, DischargeDate = admissions[3].AdmissionDate.AddDays(6), Recommendation = "Wear cast for 4 weeks, physical therapy needed.", Amount = 700.00m },
                    new Discharge { AdmissionId = admissions[4].Id, DischargeDate = admissions[4].AdmissionDate.AddDays(3), Recommendation = "Surgery completed, avoid heavy lifting.", Amount = 1200.00m, IsPaid = true},
                    new Discharge { AdmissionId = admissions[5].Id, DischargeDate = admissions[5].AdmissionDate.AddDays(10), Recommendation = "Daily BP monitoring, low-sodium diet.", Amount = 600.00m, IsPaid = true },
                    new Discharge { AdmissionId = admissions[6].Id, DischargeDate = admissions[6].AdmissionDate.AddDays(2), Recommendation = "Avoid bright lights, take prescribed meds.", Amount = 250.00m }
                };

                context.Discharges.AddRange(discharges);
                context.SaveChanges();
            }
        }
    }
}
