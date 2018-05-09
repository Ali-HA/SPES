using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class LO
    {
        public int LOID { get; set; }
        public string LearningOutcome { get; set; }

        [Display(Name = "LO Description")]
        public string LODesc { get; set; }

        [Display(Name = "Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<PI> PIs { get; set; }
    }
}