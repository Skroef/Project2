using projectTwo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using projectTwo.DTOs;
using projectTwo.Database;
using projectTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace projectTwo.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public FriendsService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }

        public void SendFriendRequest(SendFriendRequestDTO sendFriend)
        {
            var parent = _projectTwoUnitOfWork.User.Query(x => x.Email == sendFriend.Email).SingleOrDefault();

            var user = _projectTwoUnitOfWork.User.Query(x => x.Id == sendFriend.ParentUserId).SingleOrDefault();

            var friendToSave = new Friends
            {
                UserId = user.Id,
                ParentId = parent.Id,
                IsActive = false,
                IsWaiting = true,
                CreatedOn = DateTime.Now,
            };

            _projectTwoUnitOfWork.Friends.Add(friendToSave);

            _projectTwoUnitOfWork.Save();

            _projectTwoUnitOfWork.Save();
        }

        public List<UserProfileDTO> GetgetFriendRequest(int logedinUserId)
        {
            var parent = _projectTwoUnitOfWork.User.Query(x => x.Id == logedinUserId).SingleOrDefault();

            var friends = _projectTwoUnitOfWork.Friends.Query(x => x.ParentId == parent.Id && x.IsWaiting)
                                                       .Include("User")
                                                       .ToList()
                                                       .Select(x => x.User.DisplayPfpDTO)
                                                       .ToList();



            return friends;

        }

        public List<UserProfileDTO> GetSpecificFriend(int logedinUserId)
        {
            var parent = _projectTwoUnitOfWork.User.Query(x => x.Id == logedinUserId).SingleOrDefault();

            var friends = _projectTwoUnitOfWork.Friends.Query(x => x.ParentId == parent.Id && x.IsActive)
                                                       .Include("User")
                                                       .ToList()
                                                       .Select(x => x.User.DisplaySpecificDTO)
                                                       .ToList();



            return friends;

        }

        public void AcceptFriend(int id)
        {
            var user = _projectTwoUnitOfWork.Friends.Query(x => x.UserId == id && x.IsWaiting).SingleOrDefault();

            user.IsActive = true;
            user.IsWaiting = false;
           

            _projectTwoUnitOfWork.Save();
        }

        public List<FriendsDTO> GetFriend(int logedinUserId, bool following, bool followers)
        {
            //var parent = _projectTwoUnitOfWork.User.Query(x => x.Id == logedinUserId).SingleOrDefault();

            var friends = new List<FriendsDTO> { };

            if (following) 
            {
                friends = _projectTwoUnitOfWork.Friends.Query(x => x.UserId == logedinUserId)
                                                       .Include("User")
                                                       .ToList()
                                                       .Select(x => x.DisplayFriendGridDTO)
                                                       .ToList();


            }


            if (followers)
            {
                friends = _projectTwoUnitOfWork.Friends.Query(x => x.ParentId == logedinUserId)
                                                       .Include("User")
                                                       .ToList()
                                                       .Select(x => x.DisplayFriendGridDTO)
                                                       .ToList();
            }


            return friends;

        }
        
        public List<FriendsDTO> GetMyFriend(int logedinUserId)
        {
            //var parent = _projectTwoUnitOfWork.User.Query(x => x.Id == logedinUserId).SingleOrDefault();

            var friends = _projectTwoUnitOfWork.Friends.Query(x => x.ParentId == logedinUserId && x.IsActive)
                                                     .Include("User")
                                                     .ToList()
                                                     .Select(x => x.DisplayGetFriendDTO)
                                                     .ToList();



            return friends;

        }
    }

}
