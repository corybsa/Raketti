using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using Raketti.Server.Data;
using Raketti.Shared;

namespace Raketti.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly Helper helper;

		public TestController(SqlConnectionConfiguration sql)
		{
			helper = new Helper(sql);
		}

		[HttpGet("users")]
		public async Task<IActionResult> GetUsers()
		{
			IEnumerable<User> users = await helper.ExecStoredProcedure<User>("sp_admin_users_getUsers");

			return Ok(users);
		}

		[HttpGet("user")]
		public async Task<IActionResult> GetUser()
		{
			var parameters = new DynamicParameters();
			parameters.Add("UserId", 3);
			var users = await helper.ExecStoredProcedure<User>("sp_admin_users_getUser", parameters);

			return Ok(users);
		}
	}
}
