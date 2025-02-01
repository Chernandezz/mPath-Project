using Microsoft.EntityFrameworkCore;
using mPathProject.Domain.Entities;

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
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            modelBuilder.Entity<User>()
                .HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Patient)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.Id);
        }
    }
}
