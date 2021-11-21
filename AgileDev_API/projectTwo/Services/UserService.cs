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
    public class UserService : IUserService
    {
        private readonly ProjectTwoContext _projectTwoContext;
        private readonly IProjectTwoUnitOfWork _projectTwoUnitOfWork;

		public UserService(ProjectTwoContext projectTwoContext, IProjectTwoUnitOfWork projectTwoUnitOfWork)
        {
            _projectTwoContext = projectTwoContext;
            _projectTwoUnitOfWork = projectTwoUnitOfWork;
        }

        public UserInfoDTO getAllUsersInfo()
		{
            var user = _projectTwoContext.User.Select(x => new UserInfoDTO
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Password = x.Password,
            }).SingleOrDefault();

            return user;
        }

        public void registerUser(RegisterUserDTO registerUser)
        {
            var userToSave = new User
            {
                Name = registerUser.Name,
                Surname = registerUser.Surname,
                Email = registerUser.Email,
                Password = registerUser.Password,
                MobileNumber = registerUser.MobileNumber,
                //About = "I do not have an About",
                //StatusId = 1,
                UserRoleId = 1
            };

            _projectTwoUnitOfWork.User.Add(userToSave);

            _projectTwoUnitOfWork.Save();
        }

        public AuthInfoDTO getUserInfo(string email)
        {
            return _projectTwoUnitOfWork.User.Query(x => x.Email == email)
                    //.Include("Status")
                    .Include("UserRole")
                    .ToList()
                    .Select(x => x.AuthInfoDto)
                    .SingleOrDefault();
        }

        public UserProfileDTO GetCurrentUser(int id)
        {
            return _projectTwoUnitOfWork.User.Query(x => x.Id == id)
                .ToList()
                .Select(x => x.DisplayDTO)
                .FirstOrDefault();

        }

        public void editUser(UserProfileDTO pack) 
        {
            var modUser = _projectTwoUnitOfWork.User.Query(x => x.Id == pack.Id).SingleOrDefault();

            modUser.Name = pack.Name;
            modUser.Surname = pack.Surname;
            modUser.Email = pack.Email;
            modUser.MobileNumber = pack.MobileNumber;
            modUser.ImageUrl = pack.ImageUrl;
            
            _projectTwoUnitOfWork.Save();
        }

        public UserProfileDTO GetUserProfileImg(int logedinUserId)
        {
            var j = _projectTwoUnitOfWork.User.Query(x => x.Id == logedinUserId)
                .ToList()
                .Select(x => x.DisplayPfpDTO)
                .FirstOrDefault();
            return j;
        }

        public UserProfileDTO GetUserProfile(int logedinUserId)
        {
            var j = _projectTwoUnitOfWork.User.Query(x => x.Id == logedinUserId)
                .ToList()
                .Select(x => x.DisplayPfpDTO)
                .FirstOrDefault();
            return j;
        }

        public UserProfileDTO FindFriend(string email)
        {
            var j = _projectTwoUnitOfWork.User.Query(x => x.Email == email)
                .ToList()
                .Select(x => x.DisplayPfpDTO)
                .FirstOrDefault();
            return j;
        }

        //public List<UserProfileDTO> GetgetFriendRequest()
        //{
        //    var parent = _projectTwoUnitOfWork.User.Query(x => x.Email == "Hennie@mweb.co.za").SingleOrDefault();

        //    var friends = _projectTwoUnitOfWork.Friends.Query(x => x.ParentId == parent.Id && x.IsWaiting)
        //                                               .Include("User")
        //                                               .ToList()
        //                                               .Select(x => x.User.DisplayPfpDTO)
        //                                               .ToList();



        //    return friends;

        //}


    }

}
