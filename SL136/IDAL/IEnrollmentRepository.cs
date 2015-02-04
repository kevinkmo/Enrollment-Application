namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IEnrollmentRepository
    {
        List<Enrollment> GetEnrollmentList(int student_id,ref List<string> errors);

        void InsertEnrollment(Enrollment e, ref List<string> errors);

        void DeleteEnrollment(int student_id, int schedule_id, ref List<string> errors);

    }
}