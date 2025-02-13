using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
namespace PHCLT.Controllers
{
    public class AddAttanceController : Controller
    {
        ClsSystem ob = new ClsSystem();
        // GET: AddAttance
        public ActionResult Index()
        {
            return View();
        }
        public class StudentModel
        {
            public int Id { get; set; } // Primary Key
            public string Name { get; set; }
            public bool IsChecked { get; set; }

            public string Standard { get; set; }
            public string Div { get; set; }

            
        }
        [HttpGet]
        public JsonResult GetStudents(string standard,string Div)
        {
            List<StudentModel> students = new List<StudentModel>();
            DataTable dt = ob.Returntable("select * from StudentMaster where Dname='" + standard + "' and div='" + Div + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    students.Add(new StudentModel
                    {
                        Id = Convert.ToInt32(dt.Rows[i]["Studentid"]),
                        Name = dt.Rows[i]["Sname"].ToString(),
                        IsChecked = false
                    });
                }

            }

            return Json(students, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveStudents(List<StudentModel> students)
        {
            string docno = ob.FindOneString("select isnull(max(id),0)+1 from Studentatd");
            foreach (var student in students)
            {
                string ap = "";
                if (student.IsChecked == true)
                {
                    ap = "P";
                }
                else
                {
                    ap = "A";
                }

                ob.excute("insert into Studentatd(id, adate, sid, sname, attand, std, div, year_id, remarks) values(" + docno + ",'" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "'," + student.Id + ",'" + student.Name + "','" + ap + "','" + student.Standard + "','" + student.Div + "','2024-2025','-')");
            }
            return Json(new { success = true, message = "Students saved successfully!" });
        }
    }
}