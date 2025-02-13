using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHCLT.Controllers
{
    public class ExamTypeMasterController : Controller
    {
        // GET: ExamTypeMaster
        ClsSystem ob = new ClsSystem();
        public ActionResult Index()
        {
            return View();
        }
        public class Resultpass<T>
        {
            public bool opstatus { get; set; }
            public string opmessage { get; set; }
        }
        [HttpPost]
        public JsonResult AddExamtype(string examtype)
        {
            Resultpass<object> result = new Resultpass<object>();
            try
            {
                string billno = ob.FindOneString("select isnull(max(Id),0)+1 from ExamtypeMaster");
                ob.excute("insert into ExamtypeMaster(Id, Examtype,Isdelete) values(" + billno + ",'" + examtype + "',0)");
                result.opstatus = true;
                result.opmessage = "0";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ExamtypeMasterList()
        {
            List<Exantypemaster> distMasters = Getsubjectmaster();
            ViewBag.PHCdetailList = distMasters;
            return View();
        }
        private List<Exantypemaster> Getsubjectmaster()
        {
            List<Exantypemaster> distMasters = new List<Exantypemaster>();
            DataTable dt = ob.Returntable("select * from ExamtypeMaster where Isdelete=0 order by id");
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Exantypemaster distMaster = new Exantypemaster
                {
                    id = (int)dt.Rows[i]["id"],
                    Examtype = dt.Rows[i]["Examtype"].ToString()
                };
                distMasters.Add(distMaster);
            }
            return distMasters;
        }
        public class Exantypemaster
        {
            public int id { get; set; }
            public string Examtype { get; set; }

        }

        [HttpPost]
        public JsonResult DeleteId(string id)
        {

            ob.excute("update ExamtypeMaster set Isdelete=1 where id=" + Convert.ToInt32(id.ToString()) + "");

            return Json("Save", JsonRequestBehavior.AllowGet);
        }
    }
}