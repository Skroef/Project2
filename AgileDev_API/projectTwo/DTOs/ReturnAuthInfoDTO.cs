using projectTwo.Models;
using System;

namespace projectTwo.DTOs
{
    public class ReturnAuthInfoDTO
    {
        public AuthInfoDTO AuthInfo { get; set; }
        public string mtoken { get; set; }
        public int UserId { get; set; }

    }
}
