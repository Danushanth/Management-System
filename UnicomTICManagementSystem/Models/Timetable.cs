using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTICManagementSystem.Models
{
    public class TimetableModel
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public string Subject { get; set; }
        public string RoomType { get; set; }
        public string TimeSlot { get; set; }  
    }

}
