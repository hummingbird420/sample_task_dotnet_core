using Microsoft.EntityFrameworkCore;


namespace SampleTaskApp.Models
{
    public class SampleTaskDbContext : DbContext
    {
        public SampleTaskDbContext(DbContextOptions<SampleTaskDbContext> options) : base(options)
        {
        }

        // DbSets for your entities
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<SystemPageAndAction> SystemPageAndActions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<BedsAlotement> BedsAlotements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BedsAlotements configuration
            modelBuilder.Entity<BedsAlotement>()
                .HasKey(ba => ba.AlotementId);

            modelBuilder.Entity<BedsAlotement>()
                .HasOne(ba => ba.Patient)
                .WithMany()
                .HasForeignKey(ba => ba.PatientId)
                .OnDelete(DeleteBehavior.Cascade); // Or Restrict if required

            modelBuilder.Entity<BedsAlotement>()
                .HasOne(ba => ba.Bed)
                .WithMany(b => b.BedAllotments)
                .HasForeignKey(ba => ba.BedId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete here

            modelBuilder.Entity<BedsAlotement>()
                .HasOne(ba => ba.Doctor)
                .WithMany()
                .HasForeignKey(ba => ba.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete here

            base.OnModelCreating(modelBuilder);
        }


    }
}
