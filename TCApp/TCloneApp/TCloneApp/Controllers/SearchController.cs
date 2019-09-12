using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TCloneApp.Models;
using TCloneApp.BusinessLayer;
using System.Windows.Forms;

namespace TCloneApp.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult SearchPage(SearchUser usr)
        {
            return View(usr);
        }
        public ActionResult Follow(SearchUser usrSrch)
        {
            try
            {
                UserHomeModel um = new UserHomeModel();
                UserHandler usrHandlr = new UserHandler();
                var uf = usrHandlr.GetFollowingList(usrSrch.SourceUserId);
                foreach (var usrFollow in uf)
                {
                    if (usrFollow.FollowingUserId == usrSrch.Id)
                        throw new Exception("You are already following this user");
                }
                if (usrHandlr.FollowUser(usrSrch.SourceUserId, usrSrch.Id))
                {
                    var usr = usrHandlr.GetUserDetails(usrSrch.SourceUserId);
                    return RedirectToAction("Home", "UserHome", usr);
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        public ActionResult MoveToHome(SearchUser usrSearch)
        {
            UserHandler usrHandlr = new UserHandler();
            var usr = usrHandlr.GetUserDetails(usrSearch.SourceUserId);
            return RedirectToAction("Home", "UserHome", usr);
        }
    }
}