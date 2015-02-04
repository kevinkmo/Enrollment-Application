namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class AssignGradeController : ApiController
    {
        private readonly AssignGradeService service = new AssignGradeService(new AssignGradeRepository());
        private List<string> errors = new List<string>();

    
        [HttpGet]
        public string Assign(string s_id, int schedule_id, string grade)
        {
            this.service.Assign(s_id, schedule_id, grade, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public string DeleteGrade(string s_id, int schedule_id)
        {
            this.service.DeleteGrade(s_id, schedule_id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        
    }
}