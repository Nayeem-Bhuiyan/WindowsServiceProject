using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAttendence
{
   public class ProjectDBContext:DbContext
    {
        public ProjectDBContext()
       : base("name=smartdb")
        {
            Database.SetInitializer<ProjectDBContext>(null);
        }
        //public DbSet<SalaryProcesses> SalaryProcesses { get; set; }
    }
}
