using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class PostLog : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }


        public virtual User User { get; set; }
        public virtual List<PostComment> PostComment { get; set; }
        public virtual List<ViewPost> ViewPost { get; set; }
		public virtual List<AlbumPhotos> AlbumPhotos { get; set; }


		public PostLogDTO DisplayDTO =>
           new PostLogDTO
           {
               Id = this.Id,
               ImageUrl = this.ImageUrl,
               Caption = this.Caption,
               postComments = this.PostComment != null ? this.PostComment.Select(x => x.DisplayDTO).ToList() : null
           };
        
        public PostLogDTO DisplayLogDTO =>
           new PostLogDTO
           {
               Id = this.Id,
               ImageUrl = this.ImageUrl,
               Caption = this.Caption,
               CreatedOn = this.CreatedOn,
               FullName = this.User.Name + " " + this.User.Surname,
               FriendPfp = this.User.ImageUrl,
           };


        //public PostLogDTO DisplayAlbumImgDTO =>
        //   new PostLogDTO
        //   {
        //       Id = this.Id,
        //       ImageUrl = this.ImageUrl,
        //   };


    }
}
