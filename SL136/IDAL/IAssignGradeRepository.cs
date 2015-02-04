using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IAssignGradeRepository
    {
        void Assign(string s_id, int schedule_id, string grade, ref List<string> errors);
        void DeleteGrade(string s_id, int schedule_id, ref List<string> errors);
    }
}
