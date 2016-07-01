using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using myservice.DO;

namespace myservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        string InsertIntoRegister(string fname, string lname, string username, string password, string gender, string dob,int status);

        [OperationContract]
        string ValidateUserLogin(string username, string password);

        [OperationContract]
        String showusers();


    }
}
