using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public User(int _id,string _username,string _password)
        {
            id = _id;
            username = _username;
            password = _password;
        }
    }
}
