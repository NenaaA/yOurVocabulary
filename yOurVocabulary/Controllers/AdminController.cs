using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using yOurVocabulary.Models;

namespace yOurVocabulary.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }
        public ActionResult RoleList()
        {
            return View(new Admin.RoleListModel()
            {
                RoleList = db.Roles.ToList(),
                DefaultRole = db.Roles.Where(r => r.Name == "User").Select(r => r).FirstOrDefault()
            }) ;
        }
        public ActionResult UserList()
        {
            return View(new Admin.UserListModel() {
                UserList = db.Users.ToList(),
                RoleList = db.Roles.ToList(),
                CreatorCandidates = db.CreatorApplications.ToList()
            });
        }
        public ActionResult ChangeRole(string id)
        {
            var user = db.Users.Where(u => u.Id == id.ToString()).Select(u => u).First();
            var roleId = user.Roles.First().RoleId;
            
            return View(new Admin.ChangeRoleModel()
            {
                UserId = id,
                Email = user.UserName,
                CurrentRoleName = db.Roles.Where(r=>r.Id==roleId.ToString()).Select(r=>r.Name).First(),
                RoleList = db.Roles.ToList()
            });
        }
        [HttpPost]
        public ActionResult ChangeRole(Admin.ChangeRoleModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUserManager UserManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var role = db.Roles.Where(r => r.Id == model.SelectedRoleId.ToString()).Select(r => r.Name).First();

                UserManager.RemoveFromRole(model.UserId, model.CurrentRoleName);
                UserManager.AddToRole(model.UserId, role);

                //delete the creator application if exists
                if (role.Equals("Creator"))
                {
                    var application = db.CreatorApplications.FirstOrDefault(c => c.UserId==model.UserId);
                    if (application != null)
                    {
                        db.CreatorApplications.Remove(application);
                        db.SaveChanges();
                    }
                }

                return RedirectToAction("UserList");
            }
            return View(model);
        }
        public ActionResult DeleteApplication(int id)
        {
            var candidate = db.CreatorApplications.Find(id);
            db.CreatorApplications.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }
    }
}