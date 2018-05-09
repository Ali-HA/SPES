using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class LetterGrade
    {
        public int LetterGradeID { get; set; }
        public string Grade { get; set; }
        public double MinGrade { get; set; }
        public double MaxGrade { get; set; }
        public virtual ICollection<Enroll> Enrolls { get; set; }
    }
}