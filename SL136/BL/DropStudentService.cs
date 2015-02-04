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

    public class DropStudentService
    {
        private readonly IDropStudentRepository repository;

        public DropStudentService(IDropStudentRepository repository)
        {
            this.repository = repository;
        }

        public void InsertDropStudent(DropStudent g, ref List<string> errors)
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
            if (!checkStudentId(g.StudentId, ref errors))
            {
                errors.Add("Student id format invalid");
                throw new ArgumentException();
            }

            this.repository.InsertDropStudent(g, ref errors);

        }


        public void UpdateDropStudent(DropStudent g, ref List<string> errors)
        {

            if (g.CaseId < 0)
            {
                errors.Add("Case id format invalid");
                throw new ArgumentException();

            }

            if (g.Stats == null)
            {
                errors.Add("Please Update Stats");
                throw new ArgumentException();
            }

            this.repository.UpdateDropStudent(g, ref errors);

        }

        public void DeleteDropStudent(int id, ref List<string> errors)
        {
            if (id <= 0)
            {
                errors.Add("Invalid case id");
                throw new ArgumentException();
            }

            this.repository.DeleteDropStudent(id, ref errors);
        }



        public List<DropStudent> GetDropStudentList(ref List<string> errors)
        {
            return this.repository.GetDropStudentList(ref errors);
        }



        public DropStudent GetDropStudent(int id, ref List<string> errors)
        {
            if (id < 0)
            {
                errors.Add("Invalid case id");
                throw new ArgumentException();
            }

            return this.repository.GetDropStudent(id, ref errors);
        }

        public void DropStudentFromEnrol(DropStudent d, ref List<string> errors)
        {
            this.DropStudentFromEnrol(d, ref errors);
       

        }


        public bool checkStudentId(String id, ref List<String> errors)
        {
            Regex pattern = new Regex("/^([a-zA-Z])/");

            return pattern.IsMatch(id);

        }


    }
}
