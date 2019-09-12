using System;
using System.Web.Mvc;
using TCloneApp.Models;
using TCloneApp.BusinessLayer;
using System.Windows.Forms;

namespace TCloneApp.Controllers
{
    public class UserHomeController : Controller
    {
        // GET: UserHome
        public ActionResult Home(User user)
        {
            UserHomeModel um = new UserHomeModel();
            um.User = user;
            TweetHandler twtHandlr = new TweetHandler();
            um.TweetDetails = twtHandlr.GetAllTweets(user.ID);
            UserHandler usrHandlr = new UserHandler();
            um.FollowersList = usrHandlr.GetFollowersList(user.ID);
            um.FollowingList = usrHandlr.GetFollowingList(user.ID);
            return View(um);
        }
        [HttpPost]
        public ActionResult UserHomeOperations(System.Web.Mvc.FormCollection frmCollection, string submitButton)
        {
            try
            {
                int id = 0;
                string tweetMsg;
                string errmsg = "";
                TweetHandler twtHandlr = new TweetHandler();
                if (submitButton == "share")
                {
                    id = Convert.ToInt32(frmCollection["UsrID"]);
                    tweetMsg = frmCollection["msgTweet"];
                    if (id != 0)
                        twtHandlr.PostTweet(id, tweetMsg);
                    else
                        errmsg = "Could not read user properly";

                }
                else if (submitButton.Contains("Save"))
                {
                    id = Convert.ToInt32(frmCollection["TweetID"].Split(',')[0]);
                    tweetMsg = frmCollection["msgEdtTweet"];
                    if (id != 0)
                        twtHandlr.UpdateTweet(id, tweetMsg);
                    else
                        errmsg = "Could not read tweet properly";
                }
                else if (submitButton == "Delete")
                {
                    id = Convert.ToInt32(frmCollection["TweetID"].Split(',')[0]);
                    if (id != 0)
                        twtHandlr.DeleteTweet(id);
                    else
                        errmsg = "Could not read tweet properly";
                }
                else if (submitButton == "Cancel")
                    Redirect(Request.UrlReferrer.ToString());
                else if (submitButton == "Search")
                {
                    if (string.IsNullOrEmpty(frmCollection["txtSearch"]))
                        throw new Exception("Search criteria cannot by empty");
                    else
                    {
                        SearchUser srchSearchUser = new SearchUser();
                        UserHandler usrHandlr = new UserHandler();
                        srchSearchUser = usrHandlr.GetUserDetails(frmCollection["txtSearch"]);
                        if (srchSearchUser != null)
                        {
                            srchSearchUser.SourceUserId = Convert.ToInt32(frmCollection["UsrID"]);
                            //SearchPage(srchSearchUser);
                            return RedirectToAction("SearchPage", "Search", srchSearchUser);
                        }
                    }
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        public ActionResult FollowingPage(int id)
        {
            UserHomeModel um = new UserHomeModel();
            UserHandler usrHandlr = new UserHandler();
            um.User = usrHandlr.GetUserDetails(id);
            um.FollowersList = usrHandlr.GetFollowersList(id);
            um.FollowingList = usrHandlr.GetFollowingList(id);
            return View(um);
        }
        public ActionResult FollowerPage(int id)
        {
            UserHomeModel um = new UserHomeModel();
            UserHandler usrHandlr = new UserHandler();
            um.User = usrHandlr.GetUserDetails(id);
            um.FollowersList = usrHandlr.GetFollowersList(id);
            um.FollowingList = usrHandlr.GetFollowingList(id);
            return View(um);
        }
        public ActionResult SearchPage(SearchUser usrSrch)
        {
            return RedirectToAction("SearchPage","Search", usrSrch);
        }
    }
}