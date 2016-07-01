using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Collections;
using System.ServiceModel.Web;
using myservice.DO;
using System.Configuration;
using System.Web.Mail;

namespace myservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {
        ErrorLog log = new ErrorLog();
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public string InsertIntoRegister(string fname, string lname, string username, string password, string gender, string datestirng,int status)
        {

            SqlConnection connection = null;
            try
            {

                using (connection = new SqlConnection(Concls.ConnStr()))
                {
                    connection.Open();
                    DateTime dob = Convert.ToDateTime(datestirng);
                    DateTime now = DateTime.Today;
                    int diff = now.Year - dob.Year;
                                                       
                    if (diff >= 13)
                    {
                        //string sql = "INSERT INTO tbl_users(vchr_fname,vchr_lname,vchr_username,vchr_password,vchr_gender,vchr_dob,int_status) VALUES(@vchar_fname,@vchr_lname,@vchr_username,@vchr_password,@vchr_gender,@vchr_dob,@int_status)";

                        SqlCommand cmd = new SqlCommand(("csp_insert_users"), connection);
                        // cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@vchr_fname", fname);
                        cmd.Parameters.AddWithValue("@vchr_lname", lname);
                        cmd.Parameters.AddWithValue("@vchr_username", username);
                        cmd.Parameters.AddWithValue("@vchr_password", password);
                        cmd.Parameters.AddWithValue("@vchr_gender", gender);
                        cmd.Parameters.AddWithValue("@vchr_dob", dob);
                        cmd.Parameters.AddWithValue("@int_status", status);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MailMessage msg = new MailMessage();
                        msg.To = "sobinjose993@gmail.com";
                        msg.From ="sobin@baabte.com";
                        msg.Subject ="test" ;
                        msg.Body = "welcome";
                        

                        SmtpMail.Send(msg);
                        return username;
                        
                    }
                    else
                    {
                        return "Minimum age for joining facebook is 13 years";
                    }

                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                log.WriteErrorLog(error);
                return error;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

            }
        }
    
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public string ValidateUserLogin(string username, string password)
        {
            string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Select * from tbl_login where vchr_username='" + username + "' and vchr_password='" + password + "'", connection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dtb = new DataTable();
                    dtb.Load(dr);
                   connection.Close();
                    if (dtb.Rows.Count > 0)
                    {
                        dtb.Columns.Add("ResponseCode", typeof(System.Int32));
                        dtb.Columns["ResponseCode"].Expression = "'200'";
                        dtb.Columns.Add("Msg", typeof(System.String));
                        dtb.Columns["Msg"].Expression = "'Login Successful'";
                        string a = DataTableToJSONWithJSONNet(dtb);
                        return a;
                    }

                    else
                    {

                     dtb = CheckEmailExist(username, password);
                     if (dtb.Rows.Count > 0)
                     {

                         dtb.Columns.Add("ResponseCode", typeof(System.Int32));
                         dtb.Columns["ResponseCode"].Expression = "'200'";
                         dtb.Columns.Add("Msg", typeof(System.String));
                         dtb.Columns["Msg"].Expression = "'password Incorrect,Login Failed'";
                         string a = DataTableToJSONWithJSONNet(dtb);
                         return a;
                     }
                         
                     else
                     {

                         dtb.Columns.Add("ResponseCode", typeof(System.Int32));
                         dtb.Columns["ResponseCode"].Expression = "'500'";
                         dtb.Columns.Add("Msg", typeof(System.String));
                         dtb.Columns["Msg"].Expression = "'Email doesnt exist,Login Failed'";
                         string a = DataTableToJSONWithJSONNet(dtb);
                         return a;
                         
                     }

                        
                    }


                }
            }
            catch (Exception ex)
            {
             
                log.WriteErrorLog(ex.ToString());
                return ex.ToString();
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
         
        }
        public DataTable  CheckEmailExist(string username, string password)
        {
            
            string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Select * from tbl_login where vchr_username='" + username + "'", connection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dtb = new DataTable();
                     dtb.Load(dr);
                    connection.Close();
                    return dtb;
                }

            }

            catch (Exception ex)
            {
                log.WriteErrorLog(ex.ToString());
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        public DataTable CheckPasswordExist(string username, string password)
        {
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(Concls.ConnStr()))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Select vchr_password from tbl_login where vchr_username='" + username + "' and vchr_password='" + password + "'", connection);
                   
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dtb = new DataTable();
                    dtb.Load(dr);
                    connection.Close();
                    return dtb;

                }
            }

            catch (Exception ex)
            {
                
                log.WriteErrorLog(ex.ToString());
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
        }
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public String showusers()
        {
            string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("Select vchr_fname from tbl_users", connection);
                    SqlDataReader dr = cmd.ExecuteReader();
                    DataTable dtb = new DataTable();
                    dtb.Load(dr);
                    connection.Close();
                    string a = DataTableToJSONWithJSONNet(dtb);
                    return a;
                }

            }

            catch (Exception ex)
            {
                log.WriteErrorLog(ex.ToString());
                return null;
            }
            finally
            {
                if (connection != null)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }

          
        }


    }
}

