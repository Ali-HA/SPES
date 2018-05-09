using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class PI
    {
        public int PIID { get; set; }

        [Display(Name = "Learning Outcome")]
        public int LOID { get; set; }
        public virtual LO LO { get; set; }

        public string PILetter { get; set; }

        public int PINo { get; set; }

        [Display(Name = "PI Description")]
        public string PIDesc { get; set; }

        public virtual ICollection<PIMapping> PIMappings { get; set; }
    }
}