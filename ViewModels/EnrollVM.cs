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
    public class EnrollVM
    {
        public string Course { get; set; }

        public string Section { get; set; }

        public int CRN { get; set; }

        [Display(Name = "Student Id")]
        public int StudentId { get; set; }

        [Display(Name = "")]
        public bool Status { get; set; }
    }
}