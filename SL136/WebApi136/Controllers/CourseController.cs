namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class CourseController : ApiController
    {
        private readonly CourseService service = new CourseService(new CourseRepository());
        private List<string> errors = new List<string>();

        [HttpGet]
        public List<Course> GetCourseList()
        {
            var service = new CourseService(new CourseRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseList(ref errors);
        }

        [HttpGet]
        public Course GetCourse(int id)
        {
            var service = new CourseService(new CourseRepository());
            var errors = new List<string>();

            return service.GetCourse(id, ref this.errors);
          
        }

        //// you can add more [HttpGet] and [HttpPost] methods as you need

        [HttpPost]
        public string InsertCourse(Course c)
        {
            this.service.InsertCourse(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateCourse(Course c)
        {
            this.service.UpdateCourse(c, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteCourse(int id)
        {
            this.service.DeleteCourse(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

       [HttpPost]
       public string UpdatePreReq(int id,string pre)
       {
            this.service.UpdatePreReq(id, pre, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
       } 
    }
}