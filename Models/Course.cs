using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMWeb.Models
{
    public class Course
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Display(Name = "Number")]
        public int CourseID { get; set; }

        //[Display(Name = "Code")]
        [StringLength(50, MinimumLength = 3)]
        public string Code { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 9)]
        public int Credits { get; set; }

        [Display(Name = "Department")]
        //[ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<PIMapping> PIMappings { get; set; }
    }
}