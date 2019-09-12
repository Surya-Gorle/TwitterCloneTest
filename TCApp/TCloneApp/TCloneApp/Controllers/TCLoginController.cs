using System;
using System.Linq;
using System.Web.Mvc;
using TCloneApp.Models;

namespace TCloneApp.Controllers
{
    public class TcLoginController : Controller
    {
        private TCAppEntities _db = new TCAppEntities();
        // GET: TCLogin
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register(User user)
        {
            return View();
        }
        public RedirectToRouteResult InsertUser(User user)
        {
            user.Password = EncryptUtil.Md5Hash(user.Password);
            user.Active = true;
            user.Joined = DateTime.Now;
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Home", "UserHome", user);
        }
        [HttpPost]
        public ActionResult MovetoHome(User user)
        {
            user.Password = EncryptUtil.Md5Hash(user.Password);
            var usr = _db.Users.Where(x => x.DisplayName == user.DisplayName && x.Password == user.Password
            && x.Active).ToList();
            if (usr.Count > 0)
                return RedirectToAction("Home", "UserHome", usr[0]);
            else
                return RedirectToAction("Login", "TcLogin");
            
        }
    }
}