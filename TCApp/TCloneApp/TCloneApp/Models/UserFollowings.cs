namespace TCloneApp.Models
{
    public class UserFollowings
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowingUserId { get; set; }
        public string UserDisplayName { get; set; }
    }
}