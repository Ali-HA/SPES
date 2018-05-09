using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class GradeItem
    {
        public int GradeItemID { get; set; }

        [Display(Name = "Grade Item Code")]
        public string Name { get; set; } // A short name to identify the grade item. like: LE5, ME2.Q3, LE2.Cr.5

        [Display(Name = "Assessment Type")]
        public int AssessmentTypeID { get; set; }   //Assessment type: Quiz, Lab Experiment, Report
        public virtual AssessmentType AssessmentType { get; set; }

        [Display(Name = "Number")]
        public int AssessmentTNo { get; set; }  //number of the assessment, like Q1, Lab2

        [Display(Name = "Criterion Number")]
        public int CrNo { get; set; } //criterion number, 0 for total
        public double Weight { get; set; }

        [Display(Name = "Grade Distribution")]
        public int GradeDistributionID { get; set; }
        public virtual GradeDistribution GradeDistribution { get; set; }



        public virtual ICollection<AssessmentGrade> AssessmentGrades { get; set; }
        public virtual ICollection<PIMapping> PIMappings { get; set; }
    }
}