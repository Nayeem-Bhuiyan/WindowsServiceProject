using SmartAttendence.Model.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAttendence.MainService
{
    class SmartService
    {
        public void MainOpertion(SqlConnection smartdb, int companyId, int divisionId, int branchId, int month, int year, int processId, DateTime? SalaryDisbursementDay)
        {

            List<AttendanceReord> CollectProcessDataList = new List<AttendanceReord>();
            CollectProcessDataList = StartOperation(smartdb, companyId, divisionId, branchId, month, year, processId);

            EXECUTEINSERT(CollectProcessDataList, smartdb, processId);


        }

        public List<AttendanceReord> StartOperation(SqlConnection smartdb, int companyId, int divisionId, int branchId, int month, int year, int processId)
        {
            List<AttendanceReord> lstAttendanceReord = new List<AttendanceReord>();

            return lstAttendanceReord;
        }

        public void EXECUTEINSERT(List<AttendanceReord> SalaryProcesses, SqlConnection smartdb, int processId)
        {
            using (var db = new ProjectDBContext())
            {

                try
                {
                    if (db.SaveChanges() > 0)
                    {

                        string update_query = @"update PR.SalaryProcessRequests set Status = 'CP' where Id = " + processId + "";
                        SqlCommand cmd = new SqlCommand(update_query, smartdb);
                        cmd.ExecuteNonQuery();
                    }
                    if (SalaryProcesses.Count == 0)
                    {
                        string update_query = @"update PR.SalaryProcessRequests set Status = 'CP' where Id = " + processId + "";
                        SqlCommand cmd = new SqlCommand(update_query, smartdb);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
        }


    }
}
