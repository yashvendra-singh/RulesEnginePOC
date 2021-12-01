using System;
using System.Collections.Generic;
using System.Text;

namespace RulesEnginePOC.Model
{
    internal class VisitWorklistSummary
    {
        public int VisitKey { get; set; }
        public int FacilityKey { get; set; }
        public int PatientKey { get; set; }
        public string PatientName { get; set; }
        public DateTime AdmitDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string ChiefComplaint { get; set; }
        public int CDIPriorityKey { get; set; }
        public PriorityFactor PriorityFactor { get; set; }
    }

    internal class PriorityFactor
    {
        public int CDIPriorityFactorKey { get; set; }
    }
}
