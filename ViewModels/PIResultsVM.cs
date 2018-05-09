using FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACMWeb.ViewModels
{

    public class PIResultsVM
    {
        public string semester { get; set; }
        public int CourseId { get; set; }
        public int SemesterId { get; set; }
        public int gradeItemId { get; set; }

        [Display(Name = "SO")]
        public string SO { get; set; }

        [Display(Name = "PI Number")]
        public int PIno { get; set; }

        public string PI { get; set; }

        [Display(Name = "Description")]
        public string desc { get; set; }

        public int rc1
        {
            get
            {
                return Sections.Sum(x => x.rc1);
            }
        }
        public int rc2
        {
            get
            {
                return Sections.Sum(x => x.rc2);
            }
        }
        public int rc3
        {
            get
            {
                return Sections.Sum(x => x.rc3);
            }
        }
        public int rc4
        {
            get
            {
                return Sections.Sum(x => x.rc4);
            }
        }
        public int rc
        {
            get
            {
                return rc1 + rc2 + rc3 + rc4;
            }
        }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public Double Result
        {
            get
            {
                if (rc == 0) { return 0.0; }
                else { return (double)(rc3 + rc4) / rc * 100; ;}
            }
        }

        public List<SectionPIResultsVM> Sections { get; set; }
    }
    public class SectionPIResultsVM {
        public int SectionId { get; set; }
        public string Section { get; set; }
        public int rc1 { get; set; }    //count the number of 1's in the Grade4 column
        public int rc2 { get; set; }
        public int rc3 { get; set; }
        public int rc4 { get; set; }
        public int rc
        {
            get
            {
                return rc1 + rc2 + rc3 + rc4;
            }
        }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public Double Result
        {
            get
            {
                if (rc == 0) { return 0.0; }
                else { return (double)(rc3 + rc4) / rc * 100; ; }
            }
        }
    }
}