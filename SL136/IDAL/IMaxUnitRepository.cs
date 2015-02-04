using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IMaxUnitRepository
    {
        List<Student> ShowMUStudents(ref List<string> errors);
    }
}
