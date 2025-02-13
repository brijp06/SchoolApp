using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHCLT.Controllers
{
    public class SubjectMasterController : Controller
    {
        // GET: SubjectMaster
        ClsSystem ob = new ClsSystem();
        public ActionResult Index()
        {
            List<standermaster> Standermaster = new List<standermaster>();
            
            var phcList = GetstanderList();
            ViewBag.standerList = phcList;
            return View();
        }
        private List<standermaster> GetstanderList()
        {
            List<standermaster> stdMasters = new List<standermaster>();
            DataTable dt = ob.Returntable("select * from StdMaster order by id");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                standermaster distMaster = new standermaster
                {
                    id = (int)dt.Rows[i]["id"],
                    Stdname = dt.Rows[i]["Stdname"].ToString()
               
                };
                stdMasters.Add(distMaster);
            }
            return stdMasters;
        }
        public class standermaster
        {
            public int id { get; set; }
            public string Stdname { get; set; }
        }
        public class Resultpass<T>
        {
            public bool opstatus { get; set; }
            public string opmessage { get; set; }
        }
        [HttpPost]
        public JsonResult Addsubject(string subjectname, string stdname)
        {
            Resultpass<object> result = new Resultpass<object>();
            try
            {


                string billno = ob.FindOneString("select isnull(max(Id),0)+1 from SubjectMaster");
                ob.excute("insert into SubjectMaster(Id, SubjectName, STD,Isdelete) values(" + billno + ",'" + subjectname + "','" + stdname + "',0)");
                result.opstatus = true;
                result.opmessage = "0";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult SubjectMasterList()
        {
            List<subjectmaster> distMasters = Getsubjectmaster();
            ViewBag.PHCdetailList = distMasters;
            return View();
        }
        private List<subjectmaster> Getsubjectmaster()
        {
            List<subjectmaster> distMasters = new List<subjectmaster>();
            DataTable dt = ob.Returntable("select * from SubjectMaster where Isdelete=0 order by id");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                subjectmaster distMaster = new subjectmaster
                {
                    id = (int)dt.Rows[i]["id"],
                    STD = dt.Rows[i]["STD"].ToString(),
                    SubjectName = dt.Rows[i]["SubjectName"].ToString()
                };
                distMasters.Add(distMaster);
            }
            return distMasters;
        }
        public class subjectmaster
        {
            public int id { get; set; }
            public string STD { get; set; }
            public string SubjectName { get; set; }

        }

        [HttpPost]
        public JsonResult DeleteId(string id)
        {
            
            ob.excute("update SubjectMaster set Isdelete=1 where id=" + Convert.ToInt32(id.ToString()) + "");

            return Json("Save", JsonRequestBehavior.AllowGet);
        }

    }
}