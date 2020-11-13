using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Raketti.Server.Data;
using Raketti.Shared;

namespace Raketti.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly SqlContext _sql;

		public TestController(SqlContext context) {
			_sql = context;
		}

		[HttpGet("users")]
		public async Task<IActionResult> GetUsers()
		{
			var newUser = new User() {
				UserId = 5,
				UserName = "newuser",
				NormalizedUserName = "NEWUSER",
				FirstName = "New",
				LastName = "User",
				Provider = "Rick Sanchez",
				CreatedTimeStamp = DateTime.Now,
				ModifiedTimeStamp = DateTime.Now
			};

			/*await _sql.AddAsync<User>(newUser);
			await _sql.SaveChangesAsync();
			
			// AsNoTracking disabled tracking database changes. This increases performance on read-only operations.
			var users = await _sql.Users.AsNoTracking().ToListAsync();*/

			// messing with LINQ
			var users = await (from u in _sql.Users select u).ToListAsync();
			return Ok(users);
		}

		[HttpGet("roles")]
		public async Task<IActionResult> GetRoles()
		{
			// AsNoTracking disabled tracking database changes. This increases performance on read-only operations.
			var roles = await _sql.UserRoles.AsNoTracking().ToListAsync();
			return Ok(roles);
		}

		[HttpGet("students")]
		public async Task<IActionResult> GetStudents() {
			// AsNoTracking disabled tracking database changes. This increases performance on read-only operations.
			var students = await _sql.Students.AsNoTracking().ToListAsync();
			return Ok(students);
		}
	}
}
