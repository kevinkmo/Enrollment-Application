namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class CheckMaxUnitController : ApiController
    {
        private readonly CheckMaxUnitService service = new CheckMaxUnitService(new CheckMaxUnitRepository());
        private List<string> errors = new List<string>();

        [HttpGet]
        public List<Student> ShowMUStudents()
        {
            var service = new CheckMaxUnitService(new CheckMaxUnitRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.ShowMUStudents(ref errors);
        }

        
    }
}