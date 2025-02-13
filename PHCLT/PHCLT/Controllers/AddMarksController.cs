using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHCLT.Controllers
{
    public class AddMarksController : Controller
    {
        ClsSystem ob = new ClsSystem();
        // GET: AddMarks
        public ActionResult Index()
        {
            return View();
        }

        public class StudentModel
        {
            public int Id { get; set; } // Primary Key
            public string Name { get; set; }

            public string Standard { get; set; }
            public string Marks { get; set; }
            public string passmark { get; set; }
            public string subname { get; set; }
            public string exmtype { get; set; }
            public string Div { get; set; }
            public string subid { get; set; }
            public string exmid { get; set; }



        }
        public class Subject
        {
            public int Id { get; set; }
            public string Subjectname { get; set; }
        }
        public class Examtypes
        {
            public int Id { get; set; }
            public string Examtype { get; set; }
        }
        [HttpGet]
        public JsonResult GetStudents(string standard, string Div)
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
                        Marks = "0"
                    });
                }

            }

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubject(string standard, string Div)
        {
            List<Subject> Subject = new List<Subject>();
            DataTable dt = ob.Returntable("select * from SubjectMaster where STD='" + standard + "' and Isdelete=0");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Subject.Add(new Subject
                    {
                        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                        Subjectname = dt.Rows[i]["SubjectName"].ToString()
                    });
                }

            }

            return Json(Subject, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetExamtype(string standard, string Div)
        {
            List<Examtypes> Examtypes = new List<Examtypes>();
            DataTable dt = ob.Returntable("select * from ExamtypeMaster where Isdelete=0");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Examtypes.Add(new Examtypes
                    {
                        Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                        Examtype = dt.Rows[i]["Examtype"].ToString()
                    });
                }

            }

            return Json(Examtypes, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveMarks(List<StudentModel> students)
        {
            string docno = ob.FindOneString("select isnull(max(id),0)+1 from Result");
            double i = 0;
            foreach (var student in students)
            {
                i = i + 1;
                ob.excute("insert into Result(id, examdate, deptid, studentid, studentname, subjectname, mark, totalmark,examtype, div,subjectid,examtypeid,srno) values(" + docno + ",'" + DateTime.Now.Date.ToString("yyyy/MM/dd") + "','" + student.Standard + "'," + student.Id + ",'" + student.Name + "','" + student.subname + "'," + student.Marks + "," + student.passmark + ",'" + student.exmtype + "','" + student.Div + "'," + student.subid + "," + student.exmid + "," + i + ")");
            }
            return Json(new { success = true, message = "Students saved successfully!" });
        }
    }
}