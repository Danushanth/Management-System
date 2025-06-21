using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTICManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public int? CourseId { get; set; }

        public int? UserId { get; set; }  

       
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } 
    }

}
