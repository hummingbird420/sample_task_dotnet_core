using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SampleTaskApp.Models
{
    public class SampleTaskDbContext : DbContext
    {
        public SampleTaskDbContext(DbContextOptions<SampleTaskDbContext> options) : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Hospitals> Hospitals { get; set; }
        public DbSet<Beds> Beds { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<BedsAlotements> BedsAlotements { get; set; }
        public DbSet<Notifications> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships and constraints

            modelBuilder.Entity<Doctors>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Beds>()
                .HasOne(b => b.Hospital)
                .WithMany(h => h.Beds)
                .HasForeignKey(b => b.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BedsAlotements>()
                .HasOne(ba => ba.Bed)
                .WithMany(b => b.BedAllotments)
                .HasForeignKey(ba => ba.BedId)
                .OnDelete(DeleteBehavior.Cascade);

            // Change cascading behavior to restrict for BedsAlotements to Doctors and Patients
            modelBuilder.Entity<BedsAlotements>()
                .HasOne(ba => ba.Doctor)
                .WithMany()
                .HasForeignKey(ba => ba.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);  // or DeleteBehavior.NoAction

            modelBuilder.Entity<BedsAlotements>()
                .HasOne(ba => ba.Patient)
                .WithMany()
                .HasForeignKey(ba => ba.PatientId)
                .OnDelete(DeleteBehavior.Restrict);  // or DeleteBehavior.NoAction

            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.Doctor)
                .WithMany()
                .HasForeignKey(n => n.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.Patient)
                .WithMany()
                .HasForeignKey(n => n.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes (optional but recommended for performance)
            modelBuilder.Entity<UserInfo>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Doctors>()
                .HasIndex(d => d.DoctorEmail)
                .IsUnique();

            modelBuilder.Entity<Patients>()
                .HasIndex(p => p.PatientEmail)
                .IsUnique();
        }

    }
}
