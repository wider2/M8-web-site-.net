using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M8Solutions.DAL;
//using M8Solutions.DAL;

namespace M8Solutions.Controllers
{
    public class HomeController : Controller
    {
        //entity to MSSQL server
        //DB_FinansistEntities entities = new DB_FinansistEntities();

        //entity to local MDF database
        DataAccess entities = new DataAccess();


        public ActionResult Index()
        {
            ViewBag.StatusMessage = "";
            var list = entities.M8task.ToList();
            //var list = entities.M8task.ToList().OrderByDescending(i => i.Id);
            //var list = entities.M8task.ToList().Where(i => i.Id > 0);
            if (list.Count() <= 0) ViewBag.StatusMessage = "There are no records found yet. Please add new one.";
            return View(list);
            //return View(entities.M8task.ToList());
            //return View();
        }


        [HttpPost]
        public JsonResult SaveItem(M8task model)
        {
            var id = model.Id;
            var subject = model.Subject;
            var description = model.Description;
            var footer = model.Footer;
            Boolean valid = checkInput(model);

            if (model != null && valid)
            {
                if (id != 0) //update mode
                {
                    M8task m = entities.M8task.FirstOrDefault(x => x.Id == id);
                    m.Subject = subject;
                    m.Description = description;
                    m.Footer = footer;
                    m.Date_pub = DateTime.Now;
                    entities.SaveChanges();
                }
                else
                { //insert mode

                    M8task task = new M8task
                    {
                        Id = id,
                        Subject = subject,
                        Description = description,
                        Footer = footer
                    };
                    entities.M8task.Add(task);
                    entities.SaveChanges();
                }
                //return Json(task, JsonRequestBehavior.AllowGet);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult DeleteItem(int? id)
        {
            var entryToUpdate = entities.M8task.Find(id);

            try
            {
                entities.M8task.Remove(entryToUpdate);
                entities.SaveChanges();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult AddNewItem(M8task model)
        {
            var id = model.Id;
            var subject = model.Subject;
            var description = model.Description;
            var footer = model.Footer;
            Boolean valid = checkInput(model);

            if (model != null && valid)
            {
                M8task task = new M8task
                {
                    Id = id,
                    Subject = subject,
                    Description = description,
                    Footer = footer,
                    Date_pub = DateTime.Now
                };
                entities.M8task.Add(task);
                entities.SaveChanges();
                
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        private Boolean checkInput(M8task model)
        {
            Boolean valid = false;
            if (model.Subject.Length > 0 && model.Description.Length > 0 && model.Footer.Length > 0) valid = true;
            return valid;
        }
    }
}