namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IGradeChangeRepository
    {
        void InsertGradeChange(GradeChange g, ref List<string> errors);

        void UpdateGradeChange(GradeChange g, ref List<string> errors);

        void DeleteGradeChange(int id, ref List<string> errors);

        GradeChange GetGradeChange(int id, ref List<string> errors);

        List<GradeChange> GetGradeChangeList(ref List<string> errors);

        
    }
}