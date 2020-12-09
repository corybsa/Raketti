using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Novell.Directory.Ldap;
using Raketti.Server.Data;
using Raketti.Shared;

namespace Raketti.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly Helper _helper;
		private readonly IConfiguration _configuration;

		public AuthController(SqlConfiguration sql, IConfiguration configuration)
		{
			_helper = new Helper(sql);
			_configuration = configuration;
		}

		[HttpPost]
		public async Task<IActionResult> Login(AuthInfo auth)
		{
			var response = new AuthResponse<string>();

			if (auth.Username == string.Empty)
			{
				response.Success = false;
				response.Message = "Username cannot be empty.";
				return BadRequest(response);
			}

			if (auth.Password == string.Empty)
			{
				response.Success = false;
				response.Message = "Password cannot be empty.";
				return BadRequest(response);
			}

			using (var cn = new LdapConnection())
			{
				try
				{
					cn.Connect("ds05", 389);
					cn.Bind($"LCSD\\{auth.Username}", auth.Password);
					cn.Disconnect();
				}
				catch (LdapException e)
				{
					response.Success = false;
					response.Message = e.Message;
					return BadRequest(response);
				}
				finally
				{
					cn.Disconnect();
					cn.Dispose();
				}

				var parameters = new DynamicParameters();
				parameters.Add("Username", auth.Username);

				try
				{
					var user = (await _helper.ExecStoredProcedure<User>("GetUser", parameters)).Data.First();
					response.Data = CreateToken(user);
					Client.Services.UserService.user = user;
				}
				catch (Exception e)
				{
					response.Success = false;
					response.Message = e.Message;
					return StatusCode(StatusCodes.Status500InternalServerError, response);
				}

				return Ok(response);
			}
		}

		private string CreateToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Name, user.Username)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
	}
}
