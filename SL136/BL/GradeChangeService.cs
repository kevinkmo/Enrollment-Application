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

    public class GradeChangeService
    {
         private readonly IGradeChangeRepository repository;

         public GradeChangeService(IGradeChangeRepository repository)
        {
            this.repository = repository;
        }

         public void InsertGradeChange(GradeChange g, ref List<string> errors)
         {
             if (g == null)
             {
                 errors.Add("Request cannot be null");
                 throw new ArgumentException();
             }

           

             if (g.CaseId < 0)
             {
                 errors.Add("Case id format invalid");
                 throw new ArgumentException();

             }

             if (g.StudentId == null)
             {
                 errors.Add("Student id format invalid");
                 throw new ArgumentException();

             }

             if (g.ScheduleId < 0)
             {
                 errors.Add("ScheduleId format invalid");
                 throw new ArgumentException();
             }


             //check if first letter of student id contains letter
             if (!checkStudentId(g.StudentId,ref errors))
             {
                 errors.Add("Student id format invalid");
                 throw new ArgumentException();
             }

             this.repository.InsertGradeChange(g, ref errors);

         }


         public void UpdateGradeChange(GradeChange g, ref List<string> errors)
         {
           
             if (g.CaseId < 0)
             {
                 errors.Add("Case id format invalid");
                 throw new ArgumentException();

             }

             if (g.Stats ==null)
             {
                 errors.Add("Please Update Stats");
                 throw new ArgumentException();
             }

             this.repository.UpdateGradeChange(g, ref errors);

         }

         public void DeleteGradeChange(int id, ref List<string> errors)
         {
             if (id <= 0)
             {
                 errors.Add("Invalid case id");
                 throw new ArgumentException();
             }

             this.repository.DeleteGradeChange(id, ref errors);
         }



        public List<GradeChange> GetGradeChangeList(ref List<string> errors)
        {
            return this.repository.GetGradeChangeList(ref errors);
        }



         public GradeChange GetGradeChange(int id, ref List<string> errors)
        {
            if (id<0)
            {
                errors.Add("Invalid case id");
                throw new ArgumentException();
            }

            return this.repository.GetGradeChange(id, ref errors);
        }



     

         public bool checkStudentId(String id, ref List<String> errors)
         {
             Regex pattern=new Regex("/^([a-zA-Z])/");

             return pattern.IsMatch(id);

         }


    }
}
