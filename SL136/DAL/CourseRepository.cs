namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;

    public class CourseRepository : BaseRepository, ICourseRepository
    {
        private const string GetCourseListProcedure = "spGetCourseList";
        private const string InsertCourseP = "insert_course";
        private const string DeleteCourseP = "delete_course";
        private const string UpdateCourseP = "update_course";
        private const string GetCourseP = "get_Course";

        //
        private const string PreReqUpdate = "prereq_update";

        //insert data for course
        public void InsertCourse(Course c, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertCourseP, conn);

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_title", SqlDbType.VarChar, 100));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_level", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_description", SqlDbType.VarChar, -1));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@prereq", SqlDbType.VarChar, 25));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@unit", SqlDbType.Int));




                adapter.SelectCommand.Parameters["@course_id"].Value = c.CourseId;
                adapter.SelectCommand.Parameters["@course_title"].Value = c.Title;
                adapter.SelectCommand.Parameters["@course_level"].Value = c.CourseLevel;
                adapter.SelectCommand.Parameters["@course_description"].Value = c.Description;
                adapter.SelectCommand.Parameters["@prereq"].Value = c.prereq;
                adapter.SelectCommand.Parameters["@unit"].Value = c.unit;



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

        //update data for course
        public void UpdateCourse(Course c, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(UpdateCourseP, conn);

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_title", SqlDbType.VarChar, 100));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_level", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_description", SqlDbType.VarChar, -1));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@prereq", SqlDbType.VarChar, 25));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@unit", SqlDbType.Int));




                adapter.SelectCommand.Parameters["@course_id"].Value = c.CourseId;
                adapter.SelectCommand.Parameters["@course_title"].Value = c.Title;
                adapter.SelectCommand.Parameters["@course_level"].Value = c.CourseLevel;
                adapter.SelectCommand.Parameters["@course_description"].Value = c.Description;
                adapter.SelectCommand.Parameters["@prereq"].Value = c.prereq;
                adapter.SelectCommand.Parameters["@unit"].Value = c.unit;



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

        //delete data fro course
        public void DeleteCourse(int id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteCourseP, conn)
                {
                    SelectCommand =
                    {
                        CommandType =CommandType.StoredProcedure
                    }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = id;

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


        public List<Course> GetCourseList(ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var courseList = new List<Course>();

            try
            {
                var adapter = new SqlDataAdapter(GetCourseListProcedure, conn)
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
                    var pre="None";
                    var u=0;

                    if (dataSet.Tables[0].Rows[i]["prereq"].Equals(null))
                    {
                        pre = "None";
                    }
                    else
                    {
                        pre = dataSet.Tables[0].Rows[i]["prereq"].ToString();

                    }

                    if (dataSet.Tables[0].Rows[i]["unit"].Equals(null))
                    {

                        u = 0;
                    }
                    else
                    {

                        u = Convert.ToInt32(dataSet.Tables[0].Rows[i]["unit"].ToString());
                    }

                    var course = new Course
                                     {
                                         CourseId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["course_id"].ToString()),
                                         Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(),
                                         CourseLevel =(CourseLevel)Enum.Parse(typeof(CourseLevel),dataSet.Tables[0].Rows[i]["course_level"].ToString()),
                                         Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(),
                                         prereq = pre,
                                         unit = u
                                     };
                    courseList.Add(course);
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

            return courseList;
        }

        //update PreReq
        public void UpdatePreReq(int course_id, string prereq, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            
            try
            {
                var adapter = new SqlDataAdapter(PreReqUpdate, conn);



                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters["@course_id"].Value = course_id;

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@grade", SqlDbType.VarChar, 25));
                adapter.SelectCommand.Parameters["@prereq"].Value = prereq;


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

        //getCourse
        public Course GetCourse(int course_id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Course course = null;

            try
            {
                var adapter = new SqlDataAdapter(GetCourseP, conn)
                {
                    SelectCommand =
                    {
                        CommandType = CommandType.StoredProcedure
                    }
                };

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@course_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@course_id"].Value = course_id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

             
                    var pre = "None";
                    var u = 0;

                    if (dataSet.Tables[0].Rows[0]["prereq"].Equals(null))
                    {
                        pre = "None";
                    }
                    else
                    {
                        pre = dataSet.Tables[0].Rows[0]["prereq"].ToString();

                    }

                    if (dataSet.Tables[0].Rows[0]["unit"].Equals(null))
                    {

                        u = 0;
                    }
                    else
                    {

                        u = Convert.ToInt32(dataSet.Tables[0].Rows[0]["unit"].ToString());
                    }

                    course = new Course
                    {
                        CourseId = Convert.ToInt32(dataSet.Tables[0].Rows[0]["course_id"].ToString()),
                        Title = dataSet.Tables[0].Rows[0]["course_title"].ToString(),
                        CourseLevel = (CourseLevel)Enum.Parse(typeof(CourseLevel), dataSet.Tables[0].Rows[0]["course_level"].ToString()),
                        Description = dataSet.Tables[0].Rows[0]["course_description"].ToString(),
                        prereq = pre,
                        unit = u
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

            return course;
        }

    }
}
