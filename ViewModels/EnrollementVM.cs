using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.ViewModels
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    public class EnrollementVM
    {
        [Display(Name = "Code")]
        public string CourseCode { get; set; }

        public string Section { get; set; }

        [Display(Name = "CRN")]
        public int SectionID { get; set; }

        [Display(Name = "Student ID")]
        public int StudentID { get; set; }

        //[Display(Name = "Enrollment Date")]
        //[FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        //public DateTime EnrollmentDate { get; set; }

        [FieldConverter(ConverterKind.Boolean)]
        public bool Status { get; set; }
    }
}