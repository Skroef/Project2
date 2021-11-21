using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
	public class AlbumUserAccess : BaseModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int AlbumId { get; set; }
		public bool IsActive { get; set; }


		public virtual User User { get; set; }
		public virtual Album Album { get; set; }


		public AlbumUserAccessDTO DisplayDTO =>
		   new AlbumUserAccessDTO
		   {
			   Id = this.Id,
			   UserId = this.UserId,
			   AlbumId = this.AlbumId
		   };



	}
}
