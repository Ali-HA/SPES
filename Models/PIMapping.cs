using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public enum PIType {
        Summative, Formative1, Formative2
    }

    public enum CalcType
    {
        Single, AverageCRs
    }

    public class PIMapping
    {

        public int PIMappingID { get; set; }

        [Display(Name = "Semester")]
        [Range(1, int.MaxValue, ErrorMessage = "Semester has to be selected")]
        [Index("IX_PI_Mapping", 1, IsUnique = true)]
        [ForeignKey("Semester")]
        public int SemesterID { get; set; }
        public virtual Semester Semester { get; set; }

        [Display(Name = "Performance Indicator (PI)")]
        [Range(1, int.MaxValue, ErrorMessage = "PI has to be selected")]
        [Index("IX_PI_Mapping", 2, IsUnique = true)]
        [ForeignKey("PI")]
        public int PIID { get; set; }
        public virtual PI PI { get; set; }

        [Display(Name = "Course")]
        [Range(1, int.MaxValue, ErrorMessage = "Course has to be selected")]
        [Index("IX_PI_Mapping", 3, IsUnique = true)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

        [Display(Name = "Assessment")]
        [Range(1, int.MaxValue, ErrorMessage = "Grade Item has to be selected")]
        [Index("IX_PI_Mapping", 4, IsUnique = true)]
        [ForeignKey("GradeItem")]
        public int GradeItemID { get; set; }
        public virtual GradeItem GradeItem { get; set; }

        public bool Active { get; set; }

        public PIType Type { get; set; }

        [Display(Name = "Calculation Type")]
        public CalcType CType { get; set; }

    }
}