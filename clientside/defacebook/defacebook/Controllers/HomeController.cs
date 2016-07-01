using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;
namespace defacebook.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        defacebookservice.Service2Client fbobj = new defacebookservice.Service2Client();

        public ActionResult Index()
        {
            
            //ViewBag.text = obj.validateuser("test@test.com", "1234");

            return View("login");
        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }
      
        [HttpPost]
        public ActionResult Login(FormCollection fc )
        {
            string name = fc["txtemail"];
            string password = fc["password"];
            //string login = "test";
            
              //ViewBag.text = result;
            ViewBag.login = fbobj.ValidateUserLogin(name, password);
              return View("home");
         
           
        }
        public ActionResult error(string message)
        {
            string msg = Request.QueryString["msg"];
            string photo = Request.QueryString["photo"];
            ViewBag.name = msg;
            ViewBag.photo = photo;
           
            return View("Error");
        }
        public ActionResult sucess()
        {
            string name = Request.QueryString["message"];
            string photo = Request.QueryString["photo"];
            ViewBag.name = name;
            ViewBag.photo = photo;
            
            //string users = fbobj.showusers();
            //ViewData["user"] = users;
            return View("home");
        }
        public ActionResult Register(FormCollection fc)
        {
            string fname = fc["txtfname"];
            string lname = fc["txtlname"];
            string username = fc["txtusername"];
            string reusername = fc["txtreusername"];
            string password = fc["txtpassword"];
            string month = fc["month"];
            string day = fc["day"];
            string year = fc["year"];
            var datestring = year + "-" + month + "-" + day;
            DateTime date = DateTime.Parse(datestring);
            string.Format("{0:yyyy-mm-dd}", date);
           // string dob =day+month+year;

            string gender = fc["rdogender"];

            //var fileName = Path.GetFileName(file.FileName);
            //var path = Path.Combine(Server.MapPath("~/images"), fileName);
            //file.SaveAs(path);
            if (fname.Length > 3)
            {
                ViewData["length"] = "firstname must have atleast 3 characters";
            }
            if (username != reusername)
            {
                ViewData["matching"] = "email Not Mached";
            }
            if (username.Contains("@"))
            {
                var flag = 2;
            }
            else
            {
                ViewData["email"] = "Please Enter a valid email";
            }
           
            var re = @"(?=.*[A-Z])(?=.*\d)(?!.*(.)\1\1)[a-zA-Z0-9@]{8,20}";
            if (System.Text.RegularExpressions.Regex.IsMatch(password, re))
            {
                var flag = 3;
            }
            else
            {
                ViewData["password"] = "password must have atleast 1 Capital letter,1 digit,1symbol and 8 characters";
            }

            if (username == reusername)
            {

                string a = fbobj.InsertIntoRegister(fname, lname, username, password, gender, datestring);
                if (a != null)
                {
                    ViewBag.message = "Registration Sucessfull";
                    ViewBag.email = username;

                    string from = "sobin@baabte.com"; //any valid GMail ID  
                    string to = "sobinjose993@gmail.com";
                    using (MailMessage mail = new MailMessage(from,to))
                    {
                        mail.Subject = "test";
                        mail.Body = "test";
                        //if (fileUploader != null)
                        //{
                        //    string fileName = Path.GetFileName(fileUploader.FileName);
                        //    mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        //}
                        mail.IsBodyHtml = false;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredential = new NetworkCredential(from,"sobin001?");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = 587;
                        smtp.Host = "localhost";
                        smtp.Send(mail);
                        ViewBag.Message = "Sent";
                       
                    }  
                    return View("success");
                }
                else
                {
                    ViewBag.msg = a;
                    return View("login");
                }

            }
            else
            {
                ViewBag.same = "Username mis mach";
                ViewBag.length = "firstname must have atleast 3 characters";
                ViewData["password"] = "password must have atleast 1 Capital letter,1 digit,1symbol and 8 characters";
                
                return View("login");
            }

            

        }

        [HttpPost]
        public string showusers(string like)
        {
            string connString = ConfigurationManager.ConnectionStrings["Facebook"].ConnectionString;
            SqlConnection connection = new SqlConnection(connString);
            if (connection != null)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("Select pk_int_user_id,vchr_fname from tbl_users where vchr_fname like '" + like + "%'", connection);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dtb = new DataTable();
                dtb.Load(dr);
                connection.Close();
                string a =  JsonConvert.SerializeObject(dtb);
                return a;
            }
            else
            {
                return "not conntected";
            }
        }
             }
}
