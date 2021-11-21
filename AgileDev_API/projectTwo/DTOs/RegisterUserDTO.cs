using System;

namespace projectTwo.DTOs
{
	public class RegisterUserDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
    }
}
