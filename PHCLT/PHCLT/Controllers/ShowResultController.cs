using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHCLT.Controllers
{
    public class ShowResultController : Controller
    {
        // GET: ShowResult
        ClsSystem ob = new ClsSystem();
        public ActionResult Index()
        {
            return View();
        }
        public class Examtypes
        {
            public int Id { get; set; }
            public string Examtype { get; set; }
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
        public class Result
        {
            public int Id { get; set; }
            public string Subjectname { get; set; }
            public string marks { get; set; }
            public string PassingMark { get; set; }

        }
        [HttpGet]
        public JsonResult GetResult(string examtype)
        {
            List<Result> Result = new List<Result>();
            DataTable dt = ob.Returntable("select * from Result where studentid='" + Convert.ToInt32(Session["UserId"]) + "' and examtypeid='" + examtype + "'");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Result.Add(new Result
                    {
                        Id = Convert.ToInt32(dt.Rows[i]["Studentid"]),
                        Subjectname = dt.Rows[i]["subjectname"].ToString(),
                        marks = dt.Rows[i]["mark"].ToString(),
                        PassingMark = dt.Rows[i]["totalmark"].ToString()
                    });
                }

            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}