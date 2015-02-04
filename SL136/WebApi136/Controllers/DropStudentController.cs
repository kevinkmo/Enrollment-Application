namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class DropStudentController : ApiController
    {
        private readonly DropStudentService service = new DropStudentService(new DropStudentRepository());
        private List<string> errors = new List<string>();

        [HttpGet]
        public List<DropStudent> GetDropStudentList()
        {
            var service = new DropStudentService(new DropStudentRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetDropStudentList(ref errors);
        }


        [HttpGet]
        public DropStudent GetDropStudent(int id)
        {
            var service = new DropStudentService(new DropStudentRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetDropStudent(id,ref errors);
        }

        //// you can add more [HttpGet] and [HttpPost] methods as you need

        [HttpPost]
        public string InsertDropStudent(DropStudent c)
        {
            this.service.InsertDropStudent(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdatetDropStudent(DropStudent c)
        {
            this.service.UpdateDropStudent(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteDropStudent(int id)
        {
            this.service.DeleteDropStudent(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DropStudentFromEnrol(DropStudent c)
        {
            this.service.DropStudentFromEnrol(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }
    }
}