using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public int PostLogId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
