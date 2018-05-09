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
    public class GradeItemVM
    {
        [Display(Name = "Assessment Type")]
        public string AssessType { get; set; }

        public int Number { get; set; }

        public int Criterion { get; set; }

        public double Weight { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

    }
}