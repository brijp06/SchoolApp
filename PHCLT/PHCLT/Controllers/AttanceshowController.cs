using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
namespace PHCLT.Controllers
{
    public class AttanceshowController : Controller
    {
        // GET: Attanceshow
        ClsSystem ob = new ClsSystem();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Getattancedata(string startdate, string enddate)
        {
            try
            {
                DateTime start = DateTime.Parse(startdate);
                DateTime end = DateTime.Parse(enddate);
                DateTime today = DateTime.Today;

                // Fetch attendance records from the database
                DataTable dt = ob.Returntable("SELECT adate, attand FROM Studentatd WHERE sid = " + Convert.ToInt32(Session["UserId"]) +
                                              " AND adate BETWEEN '" + start.ToString("yyyy-MM-dd") + "' AND '" + end.ToString("yyyy-MM-dd") + "'");

                var events = new List<object>();
                var allDates = new HashSet<string>(); // Store existing attendance records

                // Process fetched records
                foreach (DataRow row in dt.Rows)
                {
                    string date = Convert.ToDateTime(row["adate"]).ToString("yyyy-MM-dd");
                    string attendanceStatus = row["attand"].ToString();
                    string eventColor = (attendanceStatus == "P") ? "#28a745" : "#dc3545"; // Green for present, Red for absent

                    events.Add(new { date = date, color = eventColor });
                    allDates.Add(date);
                }

                // Fill missing past dates with Red (Absent)
                for (DateTime date = start; date <= end; date = date.AddDays(1))
                {
                    string formattedDate = date.ToString("yyyy-MM-dd");
                    if (!allDates.Contains(formattedDate) && date < today) // Only mark past dates as absent
                    {
                        events.Add(new { date = formattedDate, color = "#dc3545" }); // Red for absent past dates
                    }
                }

                return Json(events);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }



    }
}