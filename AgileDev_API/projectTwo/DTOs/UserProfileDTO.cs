using System;
using System.Collections.Generic;

namespace projectTwo.DTOs
{
	public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string ImageUrl { get; set; }
        public string FullName { get; set; }
        public int? UserRoleId { get; set; }

    }
}
