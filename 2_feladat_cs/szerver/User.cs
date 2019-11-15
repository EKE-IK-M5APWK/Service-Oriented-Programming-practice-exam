using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_feladat_szerver
{
    class User
    {

        public User(string Username, string Password) 
        {
            this.username = Username;
            this.password = Password;
        }
        private string username;

        public string Username
        {
            get { return this.username; }
            set { this.username = Username; }
        }
        private string password;

        public string Password
        {
            get { return this.password; }
            set { this.username = Username; }
        }
        

        
    }
}
