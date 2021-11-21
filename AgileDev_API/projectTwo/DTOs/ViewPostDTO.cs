using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
    public class ViewPostDTO
    {
        public int Id { get; set; }
        public List <int> UserId { get; set; }
        public int PostLogId { get; set; }
        public int FriendUserId { get; set; }

        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string FullName { get; set; }
        public string FriendPfp { get; set; }
        public int ParentUserId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
