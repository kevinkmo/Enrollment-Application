namespace Web136.Controllers
{
    using System.Web.Mvc;

    public class SharedController : Controller
    {
       

        public ActionResult Error()
        {
            return this.View();
        }



        public ActionResult SharedEditStudent(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult SharedStudentEnrollment(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult CreateEnrollment(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

        public ActionResult UpdateGrade(string id,string sid)
        {
            ViewBag.Id = id;
            ViewBag.Sid = sid;
            return this.View();
        }

    }
}