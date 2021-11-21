using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
	public class ViewPost : BaseModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int PostLogId { get; set; }
		public bool IsActive { get; set; }


		public virtual User User { get; set; }
		public virtual PostLog PostLog { get; set; }


		public PostLogDTO DisplayLogDTO =>
		   new PostLogDTO
		   {
			   Id = this.Id,
			   PostLogId = this.PostLogId,
			   ImageUrl = this.PostLog.ImageUrl,
			   Caption = this.PostLog.Caption,
			   CreatedOn = this.PostLog.CreatedOn,
			   FullName = this.User.Name + " " + this.User.Surname,
			   FriendPfp = this.User.ImageUrl
		   };


		public ViewPostDTO DisplayDTO =>
		   new ViewPostDTO
		   {
			   Id = this.Id,
			   PostLogId = this.PostLogId,
			   ImageUrl = this.PostLog.ImageUrl,
			   Caption = this.PostLog.Caption,
			   CreatedOn = this.PostLog.CreatedOn,
			   FullName = this.User.Name + " " + this.User.Surname,
			   FriendPfp = this.User.ImageUrl
		   };



	}
}
