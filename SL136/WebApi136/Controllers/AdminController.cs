namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class AdminController : ApiController
    {
        private readonly AdminService service = new AdminService(new AdminRepository());
        private List<string> errors = new List<string>();

        [HttpGet]
        public Admin GetAdminInfo(int adminId)
        {
            var service = new AdminService(new AdminRepository());
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetAdminInfo(adminId, ref errors);
        }

        [HttpPost]
        public string UpdateAdminInfo(Admin admin)
        {
            this.service.UpdateAdminInfo(admin, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }
    }
}