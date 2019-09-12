using System;
using System.Web.Mvc;
using TCloneApp.Models;
using TCloneApp.BusinessLayer;

namespace TCloneApp.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult EditProfile(User usr)
        {
            usr.Password = string.Empty;
            return View(usr);
        }
        [HttpPost]
        public ActionResult ChangeUser(FormCollection frmCollection, string submitButton)
        {
            var id = Convert.ToInt32(frmCollection["UsrID"]);
            var usrHandlr = new UserHandler();
            var usr = usrHandlr.GetUserDetails(id);
            if (submitButton == "Save")
            {
                string pwd = frmCollection["pwdVal"];
                string email = frmCollection["UserEmail"];
                if (!(string.IsNullOrEmpty(pwd)))
                    pwd = EncryptUtil.Md5Hash(pwd);
                if (!(string.IsNullOrEmpty(pwd)))
                    usr.Password = pwd;
                usr.Email = email;
                usrHandlr.UpdateUser(usr);
                return RedirectToAction("Home", "UserHome", usr);
            }

            if(submitButton == "Exit from Application")
            {
                usrHandlr.RemoveUser(Convert.ToInt32(frmCollection["UsrID"]));
                return RedirectToAction("Login", "TcLogin");
            }
            return RedirectToAction("Home", "UserHome", usr);
        }
    }
}