using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
    public class AlbumPhotosDTO
    {
        public int Id { get; set; }
		public int AlbumId { get; set; }
		public int PostLogId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
    }
}
