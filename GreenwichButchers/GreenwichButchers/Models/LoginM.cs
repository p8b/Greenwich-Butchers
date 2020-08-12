using GreenwichButchers.SystemClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace GreenwichButchers.Models
{
    [Serializable]
    public class LoginM
    {
        public bool Status { get; private set; }
        public string PersonType { get; private set; }
        public int ID { get; private set; }

        // Checks if the login details are valid
        public void LoginCheck(string Email, string Password)
        {
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
            
            try // Error Handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("LoginCheck", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = Email;
                    sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar, 130).Value = Password;

                    // Open the database connection
                    sqlConn.Open();

                    // Receive all the relevant records by calling the
                    // "ExecuteReader" method of "sqlCommand" Object
                    var RecReader = sqlCommand.ExecuteReader();

                    // Check if the there are any records
                    if (RecReader.HasRows)
                    {
                        // Loop through the records
                        while (RecReader.Read())
                        {
                            // Set the Login check information
                            this.Status = (bool)RecReader["Status"];
                            this.PersonType = RecReader["PersonType"].ToString().TrimEnd();
                            this.ID = (int)RecReader["ID"];
                        }
                    }
                }
            }
            // Catching the exception error
            catch (Exception e)
            {
                // For debugging
                Debug.WriteLine(e.Message);
            }
            finally
            {
                // Make sure the connection to the DB is closed
                sqlConn.Close();
            }
        }
    }
}