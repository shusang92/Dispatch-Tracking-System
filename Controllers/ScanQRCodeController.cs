using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImportExcel.Models;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace ImportExcel.Controllers
{
    public class ScanQRCodeController : clsBase
    {
        ImportExcelDBEntities db = new ImportExcelDBEntities();
        // GET: ScanQRCode
        public ActionResult Index()
        {
            ViewBag.Project = db.tblProjects.Select(x => x.Project).ToList(); 
            List<string> VehicleNo = new List<string>();
            VehicleNo.Add("GJ1");
            VehicleNo.Add("GJ5");
            ViewBag.VehicleNo = VehicleNo;
            return View();
        }
        #region Grid Data

        [SessionExpireFilter]
        public ActionResult OpenQRScanner(string Project, string VehicleNo)
        {
            ViewBag.Project = Project;
            ViewBag.VehicleNo = VehicleNo;
            return View();
        }
        [SessionExpireFilter]
        public ActionResult QRCodeResult(string Proj, string VNo, string text)
        {
            ViewBag.Project = db.tblProjects.Select(x => x.Project).ToList();
            List<string> VehicleNo = new List<string>();
            VehicleNo.Add("GJ1");
            VehicleNo.Add("GJ5");
            ViewBag.VehicleNo = VehicleNo;
            ViewBag.Proj = Proj;
            ViewBag.VNo = VNo;
            ViewBag.Item = text;
            return View("Index");
        }
        [HttpPost]
        public JsonResult GetProjectItemDetails(string Project ,string Item)
        {
            ItemResponseMsg objresponse = new ItemResponseMsg();
            objresponse.Key = false;
            var projData = db.tblProjects.Where(x => x.Project == Project).FirstOrDefault();
            if (projData != null)
            {
                int projID = projData.ID;
                var markNo = Item.Split('~')[0];
                var batch = Item.Split('~')[1];
                var ItemData = db.tblTGBuxars.Where(x => x.ProjectId == projID && x.MarkNo == markNo && x.Batch == batch).FirstOrDefault();
                objresponse.Key = true;
                objresponse.data = ItemData;
            }
            return Json(objresponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult LoadProjectItemDetails(string markNo, string batch, string vehicleNo)
        {
            ItemResponseMsg objresponse = new ItemResponseMsg();
            objresponse.Key = false;
            var ItemData = db.tblTGBuxars.Where(x => x.MarkNo == markNo && x.Batch == batch).FirstOrDefault();
            if (ItemData != null)
            {
                ItemData.IsApprove = true;
                ItemData.VehicleNo = vehicleNo;
                ItemData.ApprovedOn = DateTime.Now;
                ItemData.ApprovedBy = objClsLoginInfo.UserName;
                db.SaveChanges();

                objresponse.Key = true;
                objresponse.data = ItemData;
            }
            return Json(objresponse, JsonRequestBehavior.AllowGet);
        }

        public class ItemResponseMsg
        {
            public ItemResponseMsg()
            {
                new tblTGBuxar();
            }
            public bool Key;
            public tblTGBuxar data;
        }
        #endregion
    }
}