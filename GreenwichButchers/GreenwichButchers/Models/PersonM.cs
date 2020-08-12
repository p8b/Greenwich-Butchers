using GreenwichButchers.SystemClasses;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    [Serializable]
    public abstract class PersonM
    {
        public int PersonID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        /// <summary>
        /// This method is used to update the Customer or employee password
        /// </summary>
        /// <param name="Id">Customer or Employee ID</param>
        /// <param name="OldPass">Old Password </param>
        /// <param name="PType">Person Type (Customer or Employee) </param>
        /// <param name="NewPass">New Password </param>
        /// <returns>True if successful else false</returns>
        internal async Task<bool> UpdatePasswordAsync(int Id, string PType, string OldPass, string NewPass)
        {
            var ReturnValue = false;

            // Adding asynchronous capability.
            // by doing so the following lines of code will
            // become asynchronous
            await Task.Run(() =>
            {
                // Instantiate new SQL Connection and pass the "DbConnectionString"
                // from the "SystemSetting" class as parameter
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("UpdatePassword", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                        sqlCommand.Parameters.Add("@PersonType", SqlDbType.VarChar, 130).Value = PType;
                        sqlCommand.Parameters.Add("@OldPassword", SqlDbType.VarChar, 130).Value = OldPass;
                        sqlCommand.Parameters.Add("@NewPassword", SqlDbType.VarChar, 130).Value = NewPass;

                        // Open the database connection
                        sqlConn.Open();

                        sqlCommand.ExecuteNonQuery();
                        ReturnValue = true;
                    }
                }
                // Exception error handler
                catch (Exception e)
                {
                    // For debugging purposes
                    Debug.WriteLine(e.Message);
                }
                finally
                {
                    // Make sure the db connection is closed
                    sqlConn.Close();
                }
            }).ConfigureAwait(false);

            return ReturnValue;
        }

    }
}