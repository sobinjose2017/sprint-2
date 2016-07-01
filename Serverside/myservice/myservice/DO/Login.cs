using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace myservice.DO
{
    public class Login
    {

        int loginId;
        string emailorphone = string.Empty;
        string password = string.Empty;
        int fk_userRoleId;
        int fk_userId;

        [DataMember]
        public int LoginId
        {
            get { return loginId; }
            set { loginId = value; }
        }
        [DataMember]
        public string Emailorphone
        {
            get { return emailorphone; }
            set { emailorphone = value; }
        }
        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public int Fk_userRoleId
        {
            get { return fk_userRoleId; }
            set { fk_userRoleId = value; }
        }
        [DataMember]
        public int Fk_userId
        {
            get { return fk_userId; }
            set { fk_userId = value; }
        }

    }
    
}