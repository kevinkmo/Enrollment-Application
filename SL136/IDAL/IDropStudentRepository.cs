namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IDropStudentRepository
    {
        void InsertDropStudent(DropStudent g, ref List<string> errors);

        void UpdateDropStudent(DropStudent g, ref List<string> errors);

        void DeleteDropStudent(int id, ref List<string> errors);

        DropStudent GetDropStudent(int id, ref List<string> errors);

        List<DropStudent> GetDropStudentList(ref List<string> errors);

        void DropStudentFromEnrol(DropStudent g, ref List<string> errors);

    }
}