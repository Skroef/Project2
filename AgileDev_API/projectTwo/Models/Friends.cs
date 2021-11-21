using projectTwo.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Models
{
    public class Friends : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public bool IsActive { get; set; }
        public bool IsWaiting { get; set; }
        public DateTime CreatedOn { get; set; }


        public virtual User User { get; set; }


		public FriendsDTO DisplayFriendGridDTO =>
		   new FriendsDTO
		   {
			   Id = this.Id,
               parentUserId = this.ParentId,
               FullName = this.User.Name + " " + this.User.Surname,
               Email = this.User.Email,
               ImageUrl = this.User.ImageUrl,
               CreatedOn = this.CreatedOn,
               IsActive = this.IsActive,
               Status = this.IsActive ? "Friends" : "Pending Invite"
           };

        //public FriendsDTO DisplayFollowingFriendGridDTO =>
        //   new FriendsDTO
        //   {
        //       Id = this.Id,
        //       parentUserId = this.ParentId,
        //       FullName = this.User.Name.Where(User.Id == ParentId) ,
        //       Email = this.User.Email,
        //       ImageUrl = this.User.ImageUrl,
        //       CreatedOn = this.CreatedOn,
        //       IsActive = this.IsActive,
        //       Status = this.IsActive ? "Friends" : "Pending Invite"
        //   };

        public FriendsDTO DisplayGetFriendDTO =>
           new FriendsDTO
           {
               Id = this.User.Id,
               parentUserId = this.ParentId,
               FullName = this.User.Name + " " + this.User.Surname,
               Email = this.User.Email,
               ImageUrl = this.User.ImageUrl,
               CreatedOn = this.CreatedOn,
               IsActive = this.IsActive,
               Status = this.IsActive ? "Friends" : "Pending Invite"
           };

        public FriendsDTO DisplayDTO =>
           new FriendsDTO
           {
               parentUserId = ParentId
           };



    }
}
