using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
    public class AlbumUserAccessDTO
    {
		public int Id { get; set; }
		public int UserId { get; set; }
		public int AlbumId { get; set; }
        public bool IsActive { get; set; }
    }
}
