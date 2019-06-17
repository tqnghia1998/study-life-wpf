using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    class CUser
    {
        public CUser(string userid, string password, string firstname, string lastname, string email, string faculty, string userType)
        {
            this.userid = userid;
            this.password = password;
            this.firstname = firstname;
            this.lastname = lastname;
            this.email = email;
            this.faculty = faculty;
            this.userType = userType;
        }

        public string userid { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string faculty { get; set; }
        public string userType { get; set; }

        public CUser()
        {
        }
    }
}
