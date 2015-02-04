namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class DropStudentRepository : BaseRepository, IDropStudentRepository
    {
        private const string InsertProcedure = "insert_drop_request";
        private const string UpdateProcedure = "update_drop_request";
        private const string GetProcedure = "read_drop_request";
        private const string GetListProcedure = "read_drop_request_list";
        private const string DeletedProcedure = "deleted_drop_request";
        private const string DropStudentProcedure = "drop_student";

        //Insert Into GradeChange table base on GradeChange object

        public void InsertDropStudent(DropStudent d, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertProcedure, conn);

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@case_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@description", SqlDbType.VarChar, 100));
                


                adapter.SelectCommand.Parameters["@case_id"].Value = d.CaseId;
                adapter.SelectCommand.Parameters["@student_id"].Value = d.StudentId;
                adapter.SelectCommand.Parameters["@course_schedule_id"].Value = d.ScheduleId;
                adapter.SelectCommand.Parameters["@description"].Value = d.Description;
               


                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);


            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }


        }

        //update GradeChange table base on GradeChange object
        public void UpdateDropStudent(DropStudent d, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            try
            {
                var adapter = new SqlDataAdapter(UpdateProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@case_id", SqlDbType.Int));
               
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@stats", SqlDbType.VarChar, 20));


                adapter.SelectCommand.Parameters["@case_id"].Value = d.CaseId;
                
                adapter.SelectCommand.Parameters["@stats"].Value = d.Stats;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        //Deleted gradeChange from table base on case_id

        public void DeleteDropStudent(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeletedProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@case_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@case_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        //getGradeChange base on case_id

        public DropStudent GetDropStudent(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            DropStudent d = null;

            try
            {
                var adapter = new SqlDataAdapter(GetListProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@case_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@case_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                d = new DropStudent
                {
                    CaseId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["case_id"].ToString()),
                    StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                    ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_schedule_id"].ToString()),
                    Description = dataSet.Tables[0].Rows[0]["description"].ToString(),
                    Stats = dataSet.Tables[0].Rows[0]["stats"].ToString(),

                };


            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return d;
        }


        //Get List of the gradechange table

        public List<DropStudent> GetDropStudentList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var DropStudentList = new List<DropStudent>();

            try
            {
                var adapter = new SqlDataAdapter(GetListProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var student = new DropStudent
                    {
                        CaseId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["case_id"].ToString()),
                        StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_schedule_id"].ToString()),
                        Description = dataSet.Tables[0].Rows[0]["description"].ToString(),
                        Stats = dataSet.Tables[0].Rows[0]["stats"].ToString(),
                    };
                    DropStudentList.Add(student);
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

            return DropStudentList;
        }



        public void DropStudentFromEnrol(DropStudent d, ref List<string> errors)
        {

            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DropStudentProcedure, conn)
                {
                    SelectCommand =
                    {
                        CommandType =
                            CommandType
                            .StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = d.StudentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = d.ScheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

        }


    }
}