﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace defacebook.Models
{
    public class login
    {
        [Required(ErrorMessage = "Username is required")] // make the field required
        [Display(Name = "Username")]  // Set the display name of the field
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string password { get; set; }
        public bool checkUser(string username, string password) //This method check the user existence
        {
            bool flag = false;
            string connString = ConfigurationManager.ConnectionStrings["Facebook"].ConnectionString; // Read the connection string from the web.config file
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select count(*) from Login where username='" + username + "' and password='" + password + "'", conn);
                flag = Convert.ToBoolean(cmd.ExecuteScalar());
               
            }
             return flag;
        }
    }
    }