namespace Web136.Controllers
{
    using System.Web.Mvc;

    public class ScheduleController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Index");
        }
    }
}
