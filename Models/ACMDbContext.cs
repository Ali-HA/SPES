using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class ACMDbContext : DbContext
    {
        public ACMDbContext() : base("ACMConnection")
        {

        }

        //public DbSet<Person> Persons { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        //public DbSet<Person> People { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<GradeItem> GradeItems { get; set; }
        public DbSet<GradeDistribution> GradeDistributions { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Enroll> Enrolls { get; set; }
        public DbSet<AssessmentGrade> AssessmentGrades { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<LetterGrade> LetterGrades { get; set; }
        public DbSet<LO> LOs { get; set; }
        public DbSet<PI> PIs { get; set; }
        public DbSet<PIMapping> PIMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Course>()
                .HasRequired(s => s.Department)
                .WithMany(a => a.Courses).HasForeignKey(s => s.DepartmentID)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<GradeItem>()
            //    .HasRequired(s => s.GradeDistribution)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);


            modelBuilder.Entity<GradeItem>()
                .HasRequired(s => s.GradeDistribution)
                .WithMany(a => a.GradeItems).HasForeignKey(s => s.GradeDistributionID)
                .WillCascadeOnDelete(false);

           // modelBuilder.Entity<Enrollment>()
           //.HasKey(c => new { c.SectionID, c.StudentID });

            
        }




        public System.Data.Entity.DbSet<ACMWeb.Models.Semester> Semesters { get; set; }
    }
}