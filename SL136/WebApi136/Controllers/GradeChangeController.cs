namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class GradeChangeController : ApiController
    {
        private readonly GradeChangeService service = new GradeChangeService(new GradeChangeRepository());
        private List<string> errors = new List<string>();

        [HttpGet]
        public List<GradeChange> GetGradeChangeList()
        {
            var service = new GradeChangeService(new GradeChangeRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetGradeChangeList(ref errors);
        }


        [HttpGet]
        public GradeChange GetGradeChange(int id)
        {
            var service = new GradeChangeService(new GradeChangeRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetGradeChange(id, ref errors);
        }

        //// you can add more [HttpGet] and [HttpPost] methods as you need

        [HttpPost]
        public string InsertGradeChange(GradeChange c)
        {
            this.service.InsertGradeChange(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdatetGradeChange(GradeChange c)
        {
            this.service.UpdateGradeChange(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteGradeChange(int id)
        {
            this.service.DeleteGradeChange(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        
    }
}