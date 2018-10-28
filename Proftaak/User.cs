using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proftaak
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public User(int id, string username)
        {
            Id = id;
            Username = username;
        }
        
    }
}
