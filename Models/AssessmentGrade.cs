using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class AssessmentGrade
    {
        public int AssessmentGradeID { get; set; }

        //[Display(Name = "Assessment Type")]
        public double? Grade { get; set; }
        public double? Grade4 { get; set; }  //grade out of 4 (0-4)
        public double? GradeP { get; set; }  //partial grade as a fraction of the total assessment grade (i.e. if 20% then max will be 20)
        public double? GradeS { get; set; }  // scalled grade to 100, calculated from its max grade
        //public double Weight { get; set; }
        //public double MaxGrade { get; set; }
        //public int GradeNo { get; set; }    // 0 for total assessment grade, 1 for criterion 1 and so on..

       
        [Display(Name = "Assessment")]
        [Index("IX_Grade_Enroll", 1, IsUnique = true)]
        public int GradeItemID { get; set; }
        public virtual GradeItem GradeItem { get; set; }

        [Display(Name = "Enrollement")]
        [Index("IX_Grade_Enroll", 2, IsUnique = true)]
        public int EnrollID { get; set; }
        public virtual Enroll Enroll { get; set; }
    }
}