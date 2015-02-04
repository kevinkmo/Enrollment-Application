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

   public class AdminService
    {
       private readonly IAdminRepository repository;

       public AdminService(IAdminRepository repository)
        {
            this.repository = repository;
        }

       public void UpdateAdminInfo(Admin a, ref List<string> errors)
       {

           if (a.Id < 0)
           {
               errors.Add("Admin id format invalid");
               throw new ArgumentException();

           }

           

           this.repository.UpdateAdminInfo(a, ref errors);

       }

       public Admin GetAdminInfo(int id, ref List<string> errors)
       {
           if (id < 0)
           {
               errors.Add("Invalid Admin id");
               throw new ArgumentException();
           }

           return this.repository.GetAdminInfo(id, ref errors);
       }


    }
}
