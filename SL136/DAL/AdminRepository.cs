namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using IRepository;

    using POCO;
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        private const string update = "update_Admin";
        private const string get = "get_Admin";

        public void UpdateAdminInfo(Admin a, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(update, conn);

                adapter.SelectCommand.Parameters.Add(new SqlParameter("@admin_id", SqlDbType.Int));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar, 10));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar, 10));
               




                adapter.SelectCommand.Parameters["@admin_id"].Value = a.Id;
                adapter.SelectCommand.Parameters["@first_name"].Value = a.FirstName;
                adapter.SelectCommand.Parameters["@last_name"].Value = a.LastName;
               
              



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

        public Admin GetAdminInfo(int a_id, ref List<string> errors)
        {
           
            var conn = new SqlConnection(ConnectionString);
            Admin admin = null;

            try
            {
                var adapter = new SqlDataAdapter(get, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@admin_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@admin_id"].Value = a_id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                admin = new Admin
                              {
                                  Id = Convert.ToInt32(dataSet.Tables[0].Rows[0]["admin_id"].ToString()),
                                  FirstName = dataSet.Tables[0].Rows[0]["first_name"].ToString(),
                                  LastName = dataSet.Tables[0].Rows[0]["last_name"].ToString(),
                               
                                  Email = dataSet.Tables[0].Rows[0]["email"].ToString(),
                                  Password = dataSet.Tables[0].Rows[0]["password"].ToString(),
                              
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

            return admin;
        
        }


    }
}
