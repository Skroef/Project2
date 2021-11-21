using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
	public class AlbumPhotos : BaseModel
	{
		public int Id { get; set; }
		public int AlbumId { get; set; }
		public int PostLogId { get; set; }
		public bool IsActive { get; set; }


		//public virtual User User { get; set; }
		public virtual PostLog PostLog { get; set; }
		public virtual Album Album { get; set; }
		//public virtual List<AlbumUserAccess> AlbumUserAccess { get; set; }


		public AlbumPhotosDTO DisplayAlbumImgDTO =>
		   new AlbumPhotosDTO
		   {
			   Id = this.PostLog.Id,
			   ImageUrl = this.PostLog.ImageUrl,
			   IsActive = this.PostLog.IsActive,
			   Caption = this.PostLog.Caption
		   };



	}
}
