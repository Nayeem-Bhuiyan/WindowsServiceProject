using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAttendence.Model.EntityModel
{
   public class AttendanceReord
    {
        public int Id { get; set; }
        public int LoginTime { get; set; }
        public int LogoutTime { get; set; }
    }
}
