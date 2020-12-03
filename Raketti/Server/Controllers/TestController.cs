using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Raketti.Server.Data;
using Raketti.Shared;

namespace Raketti.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly Helper _helper;

		public TestController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[HttpGet("users")]
		public async Task<IActionResult> GetUsers()
		{
			DbResponse<User> response;
			try
			{
				var parameters = new DynamicParameters();
				parameters.Add("StatementType", 1);
				response = await _helper.ExecStoredProcedure<User>("sp_Users", parameters);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}

			return Ok(response);
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetUser(int userId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("StatementType", 1);
			parameters.Add("UserId", userId);
			var response = await _helper.ExecStoredProcedure<User>("sp_Users", parameters);

			return Ok(response);
		}
	}
}
