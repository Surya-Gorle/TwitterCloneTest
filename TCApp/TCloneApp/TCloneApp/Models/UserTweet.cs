using System;

namespace TCloneApp.Models
{
    public class UserTweet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        
    }
}