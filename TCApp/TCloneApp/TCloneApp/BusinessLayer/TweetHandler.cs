using System.Collections.Generic;
using System.Linq;
using TCloneApp.Models;
using System.Data.SqlClient;

namespace TCloneApp.BusinessLayer
{
    public class TweetHandler
    {
        public IEnumerable<UserTweet> GetAllTweets(int id)
        {
            IEnumerable<UserTweet> usrTweets;
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@id", id);
                var tweetDetails = ctx.Database.SqlQuery<UserTweet>(
                "exec GetAllTweets @id", idParam).ToList();
                usrTweets = tweetDetails;
            }
            return usrTweets;
        }
        public void PostTweet(int id, string tweetMsg)
        {
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@UserID", id);
                var messageParam = new SqlParameter("@Message", tweetMsg);
                ctx.Database.SqlQuery<UserHomeModel>(
                 "exec InsertTweet @UserID,@Message", idParam, messageParam).ToList();
            }
        }
        public void UpdateTweet(int id, string tweetMsg)
        {
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@TweetID", id);
                var messageParam = new SqlParameter("@Message", tweetMsg);
                ctx.Database.SqlQuery<UserHomeModel>(
                 "exec EditTweet @TweetID,@Message", idParam, messageParam).ToList();
            }
        }
        public void DeleteTweet(int id)
        {
            using (var ctx = new TCAppEntities())
            {
                var idParam = new SqlParameter("@TweetID", id);
                ctx.Database.SqlQuery<UserHomeModel>(
                 "exec DeleteTweet @TweetID", idParam).ToList();
            }
        }
    }
}