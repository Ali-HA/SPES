using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class AssessmentType
    {
        public int AssessmentTypeID { get; set; }

        [Display(Name = "Assessment Type")]
        public string Type { get; set; }

        //public virtual ICollection<Enroll> Enrolls { get; set; }
        public virtual ICollection<GradeItem> GradeItems { get; set; }
    }
}