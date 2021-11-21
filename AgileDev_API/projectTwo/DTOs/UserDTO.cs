using System;

namespace projectTwo.DTOs
{
	public class UserInfoDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string HashPassword { get; set; }
        public string ImageUrl { get; set; }
    }
}
