using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class GradeDistSection
    {
        public int GradeDistSectionID { get; set; }

        [Index("IX_SectionGradeDist", 1, IsUnique = true)]
        public int GradeDistributionID { get; set; }
        public virtual GradeDistribution GradeDistribution { get; set; }

        [Index("IX_SectionGradeDist", 2, IsUnique = true)]
        public int SectionID { get; set; }
        public virtual Section Section { get; set; }
    }
}