namespace TCloneApp.Models
{
    public class SearchUser
    {
        public int Id { get; set; }
        public string UserPassword { get; set; }
        public string UserFullName { get; set; }
        public string UserDisplayName { get; set; }
        public string UserEmail { get; set; }
        public System.DateTime JoinDate { get; set; }
        public bool ActiveFlag { get; set; }
        public string UsrSearch { get; set; }
        public int SourceUserId { get; set; }
    }
}