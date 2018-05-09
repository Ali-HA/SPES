using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACMWeb.Models
{
    public class AssessmentWeight
    {
        public int AssessmentWeightID { get; set; }
        public int SectionID { get; set; }
        public int AssessmentTypeID { get; set; }
        public int AssessmentNo { get; set; }

        public virtual AssessmentType AssessmentType { get; set; }
        public virtual Section Section { get; set; }
    }
}