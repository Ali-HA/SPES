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
    public class PIVM
    {
        [Display(Name = "SO")]
        public string SO { get; set; }

        [Display(Name = "PI Number")]
        public int Number { get; set; }

        public string PI { get; set; }

        [Display(Name = "Description")]
        public string desc { get; set; }
    }
}