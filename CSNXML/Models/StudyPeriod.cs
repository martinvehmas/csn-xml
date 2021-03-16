using System;
using System.Collections.Generic;
using System.Text;

namespace CSNXML.Models
{
    public class StudyPeriod
    {
        public string SwedishSocialSecurityNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Scope { get; set; }
        public DateTime? ChangeDate { get; set; }
        public bool? Termination { get; set; }
        public DateTime? TerminationDate { get; set; }
        public bool? Result { get; set; }
        public DateTime? ResultDate { get; set; }
        public bool? CommissionedEducation { get; set; }
    }
}
