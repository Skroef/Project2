using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projectTwo.Database;
using projectTwo.Models;
using Swashbuckle.Swagger;
using projectTwo.DTOs;
using projectTwo.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace projectTwo.Services
{
    public class AuthService : IAuthService
    {
		private readonly ProjectTwoContext _projectTwoContext;
		private readonly HashThisService _hashThis;
		private readonly IUserService _userService;


		public AuthService(ProjectTwoContext context, HashThisService hashThis, IUserService userService)
		{
			_projectTwoContext = context;
			_hashThis = hashThis;
			_userService = userService;
		}

		public JsonResult TheTokenSignIn(string password, string email)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("NinnoKeyAuthor");
			//var hash = _hashThis.GenHashCode(password);
			var userExist = _projectTwoContext.User.Where(x => x.Password == password && x.Email == email).Any();


			if (userExist)
			{
				var user = _projectTwoContext.User.Where(x => x.Email == email).SingleOrDefault().Id;

				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(new Claim[]
					{
							new Claim(ClaimTypes.Name, email)
					}),

					Expires = DateTime.UtcNow.AddMinutes(120),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
				};
				var mtoken = GenerateJSONWebToken();
				if (mtoken == null) return new JsonResult("Not Valid User Login");
				var AuthInfo = _userService.getUserInfo(email);
				var itemsToReturn = new ReturnAuthInfoDTO
				{
					AuthInfo = AuthInfo,
					mtoken = mtoken,
					UserId = user
				};

				return new JsonResult(itemsToReturn);
			}
			
			throw new ValidationException("User Not Valid");
		}

		private string GenerateJSONWebToken()
		{
			//string key = "NinnoKeyAuthor";
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:NinnoKeyAuthor"));

			var issuer = "TestForAthentication";

			//var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(["jwt:key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(issuer,
				issuer,
				null,
				expires: DateTime.Now.AddMinutes(120),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		//public JsonResult TheTokenSignIn2(string password, string email)
		//{
		//	string key = "NinnoKeyAuthor";
		//	var issuer = "TestForAthentication";

		//	var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
		//	var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		//	var hash = _hashThis.GenHashCode(password);
		//	var userExist = _context.User.Where(x => x.Password == hash && x.EmployeeNumber == userId).Any();
		//	if (userExist)
		//	{
		//		var theClaim = new List<Claim>();
		//		theClaim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
		//		theClaim.Add(new Claim("userId", userId.ToString()));
		//		theClaim.Add(new Claim("password", password));

		//		var yourSpecialTokenJWT = new JwtSecurityToken(issuer,
		//						issuer,
		//						theClaim,
		//						expires: DateTime.Now.AddDays(2),
		//						signingCredentials: credentials);

		//		var finalJWTTokenGen = new JwtSecurityTokenHandler().WriteToken(yourSpecialTokenJWT);
		//		return new JsonResult(finalJWTTokenGen);
		//	}
		//	return new JsonResult("NotValidUser");
		//}


	}

}


