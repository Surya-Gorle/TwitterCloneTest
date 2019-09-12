using System.Collections.Generic;

namespace TCloneApp.Models
{
    public class UserHomeModel
    {
        public User User;
        public IEnumerable<UserTweet> TweetDetails;
        public IEnumerable<UserFollowings> FollowingList;
        public IEnumerable<UserFollowings> FollowersList;
        public string TweetMessageText { get; set; }
    }
}