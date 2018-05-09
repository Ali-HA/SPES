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
    public class GradeVM
    {
        public int CRN { get; set; }

        [Display(Name = "Student ID")]
        public int StudentId { get; set; }

        [Display(Name = "Assessment")]
        public string Code { get; set; }

        
        public double Grade { get; set; }

        [FieldNullValue(typeof(double?), null)]
        public double? GradeS { get; set; }
    }
}