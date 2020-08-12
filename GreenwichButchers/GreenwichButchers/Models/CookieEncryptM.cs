using GreenwichButchers.SystemClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenwichButchers.Models
{
    public class CookieEncryptM
    {
        public string HashCookieID { get; private set; }
        public string Base64Value { get; private set; }

        public bool WriteRead(string HashCookieID, string Base64Value)
        {
            var ReturnValue = false;
                // Create a new SQL Connection with the "DbConnectionString"
                // property of the static "SystemSetting" class
                var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);
                try // Error handler
                {
                    // Creating a SQL command to call a stored procedure
                    // First parameter is the name of the stored procedure
                    // Second parameter is the database connection
                    using (var sqlCommand = new SqlCommand("WriteReadEncryptCookie", sqlConn)
                    {
                        // Specify the command type as stored procedure
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        // Passing the parameter values for the stored procedure
                        sqlCommand.Parameters.Add("@HashCookieID", SqlDbType.VarChar, -1).Value = HashCookieID ?? "";
                        sqlCommand.Parameters.Add("@NewBase64Value", SqlDbType.VarChar, -1).Value = Base64Value ?? "";

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
                                // set the data received from db to the current Object
                                this.HashCookieID = RecReader["HashCookieID"].ToString();
                                this.Base64Value = RecReader["Base64Value"].ToString();

                            }
                            ReturnValue = true;
                        }
                    }
                }
                // Catching all the exception
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

            return ReturnValue;
        }

        public bool Read(string HashCookieID)
        {
            if (HashCookieID == "")
            {
                return false;
            }

            var ReturnValue = false;
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("ReadEncryptCookie", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@HashCookieID", SqlDbType.VarChar, -1).Value = HashCookieID;

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
                            // set the data received from db to the current Object
                            HashCookieID = RecReader["HashCookieID"].ToString();
                            Base64Value = RecReader["Base64Value"].ToString();

                        }
                        ReturnValue = true;
                    }
                }
            }
            // Catching all the exception
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


            return ReturnValue;
        }

        
        public bool Delete(string HashCookieID)
        {
            var ReturnValue = false;
            // Receive a SQL connection from "dbConnectionManager" method of
            // "dbConnection" Class
            var sqlConn = new SqlConnection(SystemSetting.DbConnectionString);

            try // Error handler
            {
                // Creating a SQL command to call a stored procedure
                // First parameter is the name of the stored procedure
                // Second parameter is the database connection
                using (var sqlCommand = new SqlCommand("DeleteEncryptCookie", sqlConn)
                {
                    // Specify the command type as stored procedure
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    // Passing the parameter values for the stored procedure
                    sqlCommand.Parameters.Add("@HashCookieID", SqlDbType.VarChar, -1).Value = HashCookieID;

                    // Open the database connection
                    sqlConn.Open();

                    sqlCommand.ExecuteNonQuery();
                }
                ReturnValue = true;
            }
            // Catching all the exception
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

            return ReturnValue;
        }

    }
}