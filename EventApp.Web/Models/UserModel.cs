using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Web.Models
{
    public class UserModel
    {
        public int Id { get; internal set; }
        public string Address { get; internal set; }
        public int Age { get; internal set; }
        public string Email { get; internal set; }
        public string FirstName { get; internal set; }
        public float Height { get; internal set; }
        public int Weight { get; internal set; }
        public string LastName { get; internal set; }
        public string Phone { get; internal set; }
    }
}
