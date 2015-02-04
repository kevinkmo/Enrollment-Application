using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCO
{
    public class GradeChange
    {
        public int CaseId { get; set; }

        public string StudentId { get; set; }

        public int ScheduleId { get; set; }

        public string Description { get; set; }

        public string Stats { get; set; }

        public override string ToString()
        {
            return this.CaseId + "-" + this.StudentId + "-" + this.ScheduleId + "-" + this.Description + "-" + this.Stats;
        }
    }
}
