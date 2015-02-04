namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class CheckMaxUnitRepository : BaseRepository, IMaxUnitRepository{
        private const string GetProcedure = "check_max_unit";

        public List<Student> ShowMUStudents( ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var studentList = new List<Student>();

            try
            {
                var adapter = new SqlDataAdapter(GetProcedure, conn);


                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var student = new Student
                    {
                        StudentId = dataSet.Tables[0].Rows[i]["student_id"].ToString(),
                        
                        Course = new Course
                        {
                            unit = Convert.ToInt32(dataSet.Tables[0].Rows[i]["Total Units"].ToString()),
                            
                        }
                    };
                    studentList.Add(student);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return studentList;
        }
    }
}