using System.Collections.Generic;
using System.Linq;
using TCloneApp.Models;
using System.Data.SqlClient;
namespace TCloneApp.BusinessLayer
{
    public class UserHandler
    {
        private TCAppEntities _db = new TCAppEntities();
        public IEnumerable<UserFollowings> GetFollowersList(int id)
        {
            IEnumerable<UserFollowings> usrFollowers;
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@id", id);
                var followersList = ctx.Database.SqlQuery<UserFollowings>(
                 "exec GetFollowersList @id", idParam).ToList();
                usrFollowers = followersList;
            }
            return usrFollowers;
        }
        public IEnumerable<UserFollowings> GetFollowingList(int id)
        {
            IEnumerable<UserFollowings> usrFollowing;
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@id", id);
                var followingList = ctx.Database.SqlQuery<UserFollowings>(
                 "exec GetFollowingList @id", idParam).ToList();
                usrFollowing = followingList;
            }
            return usrFollowing;
        }
        public User GetUserDetails(int id)
        {
            List<User> users = new List<User>();
            foreach(var usr in _db.Users)
            {
                User user = new User();
                user.ID = usr.ID;
                user.DisplayName = usr.DisplayName;
                user.Email = usr.Email;
                user.Password = usr.Password;
                users.Add(user);
            }
            return users.Find(x => x.ID == id);
        }
        public bool UpdateUser(User usr)
        {
            var selUsr = _db.Users.Single(x => x.ID == usr.ID);
            if (selUsr != null)
            {
                selUsr.Password = usr.Password;
                selUsr.Email = usr.Email;
                _db.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool FollowUser(int id, int followUserId)
        {
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@ID", id);
                var followUserIdParam = new SqlParameter("@FollowingUserID", followUserId);
                ctx.Database.SqlQuery<UserFollowings>(
                    "exec InsertUserFollowing @ID,@FollowingUserID", idParam,followUserIdParam).ToList<UserFollowings>();
            }
            return true;
        }
        public SearchUser GetUserDetails(string srchString)
        {
            SearchUser usrSrch = new SearchUser();
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@displayName", srchString);
                var usr = ctx.Database.SqlQuery<SearchUser>(
                 "exec SearchUser @displayName", idParam).FirstOrDefault();
                if (usr!=null)
                {
                    usrSrch.UserDisplayName = usr.UserDisplayName;
                    usrSrch.UserEmail = usr.UserEmail;
                    usrSrch.UserFullName = usr.UserFullName;
                    usrSrch.Id = usr.Id;
                }
            }
            return usrSrch;
            
        }
        public bool RemoveUser(int usrId)
        {
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@UserID", usrId);
                ctx.Database.SqlQuery<dynamic>("exec DeleteUser @UserID", idParam).ToList();
            }
            return true;
        }
    }
}