namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IAdminRepository
    {
        Admin GetAdminInfo(int adminId, ref List<string> errors);

        void UpdateAdminInfo(Admin admin, ref List<string> errors);
    }
}

