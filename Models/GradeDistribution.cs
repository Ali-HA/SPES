using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class GradeDistribution
    {
        public int GradeDistributionID { get; set; }
        public string Name { get; set; }   // Name of the grade distribution

        [DataType(DataType.Date)]
        [Display(Name = "Effective Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        public virtual ICollection<Section> Sections { get; set; }

        public virtual ICollection<GradeItem> GradeItems { get; set; }
    }
}