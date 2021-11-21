using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
	public class PostComment : BaseModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int PostLogId { get; set; }
		public string Text { get; set; }
		public bool IsActive { get; set; }


		public virtual User User { get; set; }
		public virtual PostLog PostLog { get; set; }


		public PostCommentDTO DisplayDTO =>
		   new PostCommentDTO
		   {
			   Id = this.Id,
			   UserId = this.UserId,
			   PostLogId = this.PostLogId,
			   Text = this.Text,
			   PfpImageUrl = User == null ? null : User.ImageUrl,
			   CommentorFullName = User == null ? null : User.Name + " " + User.Surname
		   };



	}
}
