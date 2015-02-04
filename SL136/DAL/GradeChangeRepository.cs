namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class GradeChangeRepository : BaseRepository, IGradeChangeRepository
    {
        private const string InsertProcedure = "insert_grade_change_request";
        private const string UpdateProcedure = "update_grade_change_request";
        private const string GetProcedure = "read_grade_change_request";
        private const string GetListProcedure = "read_grade_change_request_list";
        private const string DeletedProcedure = "deleted_grade_change_request";

//Insert Into GradeChange table base on GradeChange object

        public void InsertGradeChange(GradeChange g, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertProcedure, conn);

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@case_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));            
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_schedule_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@description", SqlDbType.VarChar, 100));        
               


                adapter.SelectCommand.Parameters["@case_id"].Value = g.CaseId;
                adapter.SelectCommand.Parameters["@student_id"].Value =g.StudentId;
                adapter.SelectCommand.Parameters["@course_schedule_id"].Value = g.ScheduleId;
                adapter.SelectCommand.Parameters["@description"].Value = g.Description;
                


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
        public void UpdateGradeChange(GradeChange g, ref List<string> errors)
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


                adapter.SelectCommand.Parameters["@case_id"].Value = g.CaseId;
               
                adapter.SelectCommand.Parameters["@stats"].Value = g.Stats;

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

        public void DeleteGradeChange(int id, ref List<string> errors)
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

        public GradeChange GetGradeChange(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            GradeChange g = null;

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

                g = new GradeChange
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

            return g;
        }


//Get List of the gradechange table

        public List<GradeChange> GetGradeChangeList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var GradeChangeList = new List<GradeChange>();

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
                    var student = new GradeChange
                    {
                        CaseId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["case_id"].ToString()),
                        StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_schedule_id"].ToString()),
                        Description = dataSet.Tables[0].Rows[0]["description"].ToString(),
                        Stats = dataSet.Tables[0].Rows[0]["stats"].ToString(),
                    };
                    GradeChangeList.Add(student);
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

            return GradeChangeList;
        }


    }
}