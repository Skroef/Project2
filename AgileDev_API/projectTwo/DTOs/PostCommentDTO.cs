using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
    public class PostCommentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostLogId { get; set; }
        public string Text { get; set; }
        public string PfpImageUrl { get; set; }
        public string CommentorFullName { get; set; }
    }
}
