using Catalog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalog.DAL.EF
{
    public class DALContext : DbContext
    {
        public DALContext(DbContextOptions<DALContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<MedicalStaff> MedicalStaffs { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<MedicalCheckup> MedicalCheckups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Employee>("Employee")
                .HasValue<MedicalStaff>("MedicalStaff")
                .HasValue<Admin>("Admin");

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Employee)
                .WithMany(e => e.MedicalRecords)
                .HasForeignKey(m => m.employeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicalCheckup>()
                .HasOne(mc => mc.MedicalRecord)
                .WithMany(mr => mr.MedicalCheckups)
                .HasForeignKey(mc => mc.recordId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Doctor)
                .WithMany(d => d.Schedules)
                .HasForeignKey(s => s.doctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Schedule)
                .WithMany(s => s.Notifications)
                .HasForeignKey(n => n.scheduleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Doctor)
                .WithMany(d => d.Notifications)
                .HasForeignKey(n => n.doctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>().Property(u => u.name).HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.email).HasMaxLength(200);
        }
    }
}
