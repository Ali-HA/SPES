using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMWeb.Models
{
    public class Enroll
    {
        public int EnrollID { get; set; }

        [Display(Name = "Section")]
        [Index("IX_Enrol_Student_Section", 1, IsUnique = true)]
        [ForeignKey("Section"), Column(Order = 0)]
        public int SectionID { get; set; }
        public virtual Section Section { get; set; }

        [Display(Name = "Student")]
        [Index("IX_Enrol_Student_Section", 2, IsUnique = true)]
        [ForeignKey("Student"), Column(Order = 1)]
        public int StudentID { get; set; }
        public virtual Student Student { get; set; }

        [Display(Name = "Letter Grade")]
        [ForeignKey("LetterGrade"), Column(Order = 2)]
        public int? LetterGradeID { get; set; }
        public virtual LetterGrade LetterGrade { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        public bool Status { get; set; }
    }
}