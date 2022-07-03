using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UMH_STUDENT_SERVICE.Models;

namespace UMH_STUDENT_SERVICE.Context
{
    public partial class appDbContext : DbContext
    {
        public appDbContext()
        {
        }

        public appDbContext(DbContextOptions<appDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campus> Campuses { get; set; } = null!;
        public virtual DbSet<Career> Careers { get; set; } = null!;
        public virtual DbSet<CareerCampus> CareerCampuses { get; set; } = null!;
        public virtual DbSet<CareerCourse> CareerCourses { get; set; } = null!;
        public virtual DbSet<ComplementLearning> ComplementLearnings { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Period> Periods { get; set; } = null!;
        public virtual DbSet<RequirementCourse> RequirementCourses { get; set; } = null!;
        public virtual DbSet<StudentCourseCareer> StudentCourseCareers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserStatus> UserStatuses { get; set; } = null!;
        public virtual DbSet<UsertType> UsertTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;port=5433;Username=postgres;Password=root;Database=control_class_student_umh");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campus>(entity =>
            {
                entity.ToTable("campus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Long).HasColumnName("long");

                entity.Property(e => e.MailManager).HasColumnName("mail_manager");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.NameManager).HasColumnName("name_manager");
            });

            modelBuilder.Entity<Career>(entity =>
            {
                entity.ToTable("career");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<CareerCampus>(entity =>
            {
                entity.HasKey(e => e.Join)
                    .HasName("career_campus_pkey");

                entity.ToTable("career_campus");

                entity.Property(e => e.Join).HasColumnName("join");

                entity.Property(e => e.IdCampus).HasColumnName("id_campus");

                entity.Property(e => e.IdCareer).HasColumnName("id_career");
            });

            modelBuilder.Entity<CareerCourse>(entity =>
            {
                entity.HasKey(e => e.Join)
                    .HasName("career_course_pkey");

                entity.ToTable("career_course");

                entity.Property(e => e.Join).HasColumnName("join");

                entity.Property(e => e.AssignedDate).HasColumnName("assigned_date");

                entity.Property(e => e.HaveRequeriment).HasColumnName("have_requeriment");

                entity.Property(e => e.IdCareer).HasColumnName("id_career");

                entity.Property(e => e.IdCourse).HasColumnName("id_course");

                entity.Property(e => e.OrderNumber).HasColumnName("order_number");

                entity.Property(e => e.Percentaje).HasColumnName("percentaje");

                entity.Property(e => e.SuggestedPeriod).HasColumnName("suggested_period");
            });

            modelBuilder.Entity<ComplementLearning>(entity =>
            {
                entity.ToTable("complement_learning");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Notes).HasColumnName("notes");

                entity.Property(e => e.StudentLike).HasColumnName("student_like");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("period");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<RequirementCourse>(entity =>
            {
                entity.HasKey(e => e.Join)
                    .HasName("requirement_course_pkey");

                entity.ToTable("requirement_course");

                entity.Property(e => e.Join).HasColumnName("join");

                entity.Property(e => e.IdCareer).HasColumnName("id_career");

                entity.Property(e => e.IdCourse).HasColumnName("id_course");

                entity.Property(e => e.IdCourseRequirement).HasColumnName("id_course_requirement");
            });

            modelBuilder.Entity<StudentCourseCareer>(entity =>
            {
                entity.HasKey(e => e.Join)
                    .HasName("student_course_career_pkey");

                entity.ToTable("student_course_career");

                entity.Property(e => e.Join).HasColumnName("join");

                entity.Property(e => e.AccountStudent).HasColumnName("account_student");

                entity.Property(e => e.AssignedDate).HasColumnName("assigned_date");

                entity.Property(e => e.Comments).HasColumnName("comments");

                entity.Property(e => e.IdCareer).HasColumnName("id_career");

                entity.Property(e => e.IdCourse).HasColumnName("id_course");

                entity.Property(e => e.IdPeriod).HasColumnName("id_period");

                entity.Property(e => e.Percentaje).HasColumnName("percentaje");

                entity.Property(e => e.Stars).HasColumnName("stars");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.NumberAcount)
                    .HasName("user_pkey");

                entity.ToTable("user");

                entity.Property(e => e.NumberAcount).HasColumnName("number_acount");

                entity.Property(e => e.FullName).HasColumnName("full_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.IdCampus).HasColumnName("id_campus");

                entity.Property(e => e.IdCareer).HasColumnName("id_career");

                entity.Property(e => e.IdUserStatus).HasColumnName("id_user_status");

                entity.Property(e => e.IdUserType).HasColumnName("id_user_type");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.RegistrerDate).HasColumnName("registrer_date");

                entity.Property(e => e.StartDate).HasColumnName("start_date");

                entity.Property(e => e.VerifiedEmail).HasColumnName("verified_email");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("user_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate).HasColumnName("created_date");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<UsertType>(entity =>
            {
                entity.ToTable("usert_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
