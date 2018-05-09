using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMWeb.Models
{
    public class Section
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "CRN")]
        public int SectionID { get; set; }

        //[Display(Name = "Code")]
        [StringLength(50, MinimumLength = 1)]
        public string Code { get; set; }

        [Display(Name = "Course")]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        [Display(Name = "Semester")]
        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        public string Room { get; set; }

        [Display(Name = "Class Instructor")]
        [ForeignKey("ClassInstructor")]
        public int? ClassInstructorID { get; set; }
        public virtual Instructor ClassInstructor { get; set; }

        [Display(Name = "Lab Instructor")]
        [ForeignKey("LabInstructor")]
        public int? LabInstructorID { get; set; }
        public virtual Instructor LabInstructor { get; set; }

        //public virtual ICollection<Enrollment> Enrolls { get; set; }
        public virtual ICollection<Enroll> Enrolls { get; set; }

        [Display(Name = "Grade Distribution")]
        public int GradeDistributionID { get; set; }
        public virtual GradeDistribution GradeDistribution { get; set; }
    }
}