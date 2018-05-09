using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMWeb.Models
{
    public enum SemesterName
    {
        Fall, Spring, Summer
    }
    public class Semester
    {
        public int SemesterID { get; set; }

        [Display(Name = "Semester")]
        public SemesterName Name { get; set; }
        public int year { get; set; }
        [Display(Name = "Code")]
        public string ShortName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<PIMapping> PIMappings { get; set; }
        //public virtual ICollection<Enrollment> Enrolls { get; set; }
    }
}