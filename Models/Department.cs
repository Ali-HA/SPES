using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACMWeb.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Display(Name = "Department")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [Display(Name = "Full Department Name")]
        [StringLength(50, MinimumLength = 3)]
        public string FullName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<LO> LOs { get; set; }
    }
}