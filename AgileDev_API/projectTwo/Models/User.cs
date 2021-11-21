using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class User : BaseModel
    {
        //[Key]

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }
        public int? UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
        
        public virtual List<PostLog> PostLogs { get; set; }
        public virtual List<PostComment> PostComment { get; set; }
        public virtual List<ViewPost> ViewPost { get; set; }
        public virtual List<Friends> Friends { get; set; }
        public virtual List<Album> Album { get; set; }
        public virtual List<AlbumUserAccess> AlbumUserAccess { get; set; }


        //public virtual Status Status { get; set; }
        //public string HashPassword { get; set; }
        //public string About { get; set; }
        //public int StatusId { get; set; }

        public AuthInfoDTO AuthInfoDto =>
            new AuthInfoDTO
            {
                //About = this.About,
                DisplayName = this.Name + " " + this.Surname,
                //PhotoURL = this.ImageUrl,
                PhotoURL = "https://forty5degreesstorage.blob.core.windows.net/user/avatar_default.png",
                UId = this.Id,
                //Status = this.Status.Name,
                UserRole = this.UserRole.Name
            };

        public UserProfileDTO DisplayDTO =>
            new UserProfileDTO
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                Email = this.Email,
                MobileNumber = this.MobileNumber,
                ImageUrl = this.ImageUrl,
            };

        public UserProfileDTO DisplayPfpDTO =>
            new UserProfileDTO
            {
                Id = this.Id,
                Name = this.Name,
                Surname = this.Surname,
                ImageUrl = this.ImageUrl,
                Email = this.Email,
            };
        
        public UserProfileDTO DisplaySpecificDTO =>
            new UserProfileDTO
            {
                Id = this.Id,
                FullName = this.Name + " " + this.Surname,
                Email = this.Email,
            };

    }
}
