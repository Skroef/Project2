using System.Collections.Generic;
using projectTwo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using projectTwo.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;


namespace projectTwo.Controllers
{       
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        [Route("~/api/Auth/AuthUser")]
        public ActionResult<string> authUser(string password, string email)
        {
            return new JsonResult(_authService.TheTokenSignIn(password, email));
        }
    }
}
