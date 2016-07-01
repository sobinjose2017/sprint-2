using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace myservice.DO
{
    [DataContract]

    public class User
    {
        string firstName = string.Empty;
        string lastname = string.Empty;
        string email = string.Empty;
        string password = string.Empty;
        string dob = string.Empty;
        string gender = string.Empty;

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        [DataMember]
        public string Dob
        {
            get { return dob; }
            set { dob = value; }
        }
        [DataMember]
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
    }
}