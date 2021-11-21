using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Mvc;
using projectTwo.DTOs;

namespace projectTwo.Interfaces
{
    public interface IAuthService
	{
		JsonResult TheTokenSignIn(string password, string email);


	}
}
