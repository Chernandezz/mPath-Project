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
                var newAdmissions = patients.Select((patient, index) => new Admission
                {
                    AdmissionDate = DateTime.UtcNow.AddDays(-index * 5),
                    Diagnosis = $"Diagnosis {index + 1}",
                    Observation = $"Observation {index + 1}",
                    DoctorId = doctors[index % doctors.Count].Id,
                    PatientId = patient.Id
                }).ToList();

                context.Admissions.AddRange(newAdmissions);
                context.SaveChanges();
            }

            var admissions = context.Admissions.ToList();

            // Seed Discharges
            if (!context.Discharges.Any() && admissions.Any())
            {
                var newDischarges = admissions.Select((admission, index) => new Discharge
                {
                    Treatment = $"Treatment {index + 1}",
                    DischargeDate = admission.AdmissionDate.AddDays(7),
                    Amount = (index + 1) * 100.00m,
                    IsPaid = index % 2 == 0,
                    AdmissionId = admission.Id
                }).ToList();

                context.Discharges.AddRange(newDischarges);
                context.SaveChanges();
            }
        }
    }
}
