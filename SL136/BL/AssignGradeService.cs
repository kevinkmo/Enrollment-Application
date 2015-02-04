using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class AssignGradeService
    {
        private readonly IAssignGradeRepository repository;

         public AssignGradeService(IAssignGradeRepository repository)
        {
            this.repository = repository;
        }

         public void Assign(string s_id, int schedule_id, string grade, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(s_id) || schedule_id <0)
            {
                errors.Add("Invalid Student or Class information.");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(grade))
            {
                errors.Add("Invalid Grade enter");
                throw new ArgumentException();
            }

             this.repository.Assign(s_id, schedule_id, grade, ref errors);
        }

         public void DeleteGrade(string s_id, int schedule_id, ref List<string> errors)
         {
             if (string.IsNullOrEmpty(s_id) || schedule_id < 0)
             {
                 errors.Add("Invalid Student or Class information.");
                 throw new ArgumentException();
             }

            

             this.repository.DeleteGrade(s_id, schedule_id, ref errors);
         }
    }
    
}
