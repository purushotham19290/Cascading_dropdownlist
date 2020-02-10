using SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SA.Controllers
{
    public class SAController : Controller
    {
        staffargumentsEntities1 db = new staffargumentsEntities1();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
      
        public ActionResult Create()

        { 
            var projects = db.Projects.ToList();
            List<SelectListItem> liprojects = new List<SelectListItem>();
            liprojects.Add(new SelectListItem() { Text="select project" ,Value="0"});
            foreach (var item in projects)
            {
                liprojects.Add(new SelectListItem() { Text = item.Project1, Value = item.ProjectId.ToString() });
            }
            TempData["lipro"] = liprojects;
          
            return View();
        }
        public JsonResult GetProjectnames( string id)
        {
            var pnames = db.Projectnames.Where(x => x.ProjectId.ToString() == id);
            List<SelectListItem> lipnames = new List<SelectListItem>();
            lipnames.Add(new SelectListItem() { Text = "select projectname",Value="1" });
            foreach (var item in pnames)
            {
                lipnames.Add(new SelectListItem() { Text=item.ProjectName1,Value=item.ProjectNameId.ToString()});
            }
            return Json(new SelectList(lipnames, "value", "text", JsonRequestBehavior.AllowGet));
        }
        [HttpPost]
        public ActionResult Create(FormCollection f)
        {
            var msg = "";
            if (ModelState.IsValid)
            {
               
            Resource r = new Resource();
            //r.EmpId = Convert.ToInt32(f[0]);
            r.EmpName = f[1];
            r.EmailId = f[2];
            r.YearOfPassing =DateTime.Parse(f[3]);
            r.DOJ =DateTime.Parse(f[4]);
            //r.CostCenter = f["];
            r.Aging =(f[5]);
                r.ProjectType = f[6];
                r.Project = f[7];
            r.ProjectName = f[8];
            
            db.Resources.Add(r);
            db.SaveChanges();
            msg = "success";
        }
            else
            {
                msg = "Something invalid";
            }
            ViewBag.Message = msg;
            return RedirectToAction("Create");
        }
        
    }
   
}