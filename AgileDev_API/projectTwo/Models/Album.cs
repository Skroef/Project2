using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
	public class Album : BaseModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		//public int PostLogId { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }


		public virtual User User { get; set; }
		public virtual List<AlbumPhotos> AlbumPhotos { get; set; }
		//public virtual PostLog PostLog { get; set; }
		public virtual List<AlbumUserAccess> AlbumUserAccess { get; set; }


		public AlbumDTO DisplayDTO =>
		   new AlbumDTO
		   {
			   Id = this.Id,
			   UserId = this.UserId,
			   Name = this.Name,
		   };



	}
}
