using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
	public class PostLogDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostLogId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FullName { get; set; }
        public string FriendPfp { get; set; }
        public virtual List<PostCommentDTO> postComments { get; set; }
    }
}
