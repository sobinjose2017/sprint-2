using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace myservice
{
    public class Concls
    {
        public static string ConnStr()
        {
            return ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        }
    }
}
