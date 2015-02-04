namespace Web136.Controllers
{
    using System.Web.Mvc;

    public class StaffController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult EditStudent(string id)
        {
            return this.View();
        }

        //check Student with Max units
        public ActionResult CheckMaxUnits()
        {
            return this.View();
        }

        public ActionResult CourseList()
        {
            return this.View();
        
        }

        public ActionResult CreateCourse()
        {
            return this.View();
        }

        public ActionResult UpdateCourse(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

    }
}
