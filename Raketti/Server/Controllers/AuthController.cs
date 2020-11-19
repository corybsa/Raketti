using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		public AuthController(SqlConfiguration sql)
		{
			_helper = new Helper(sql);
		}

		[HttpPost]
		public async Task<IActionResult> Login(AuthInfo auth)
		{
			using (var cn = new LdapConnection())
			{
				try
				{
					cn.Connect("ds05", 389);
					cn.Bind(auth.Username, auth.Password);
					cn.Disconnect();
				}
				catch (LdapException e)
				{
					return BadRequest(e.Message);
				}
				finally
				{
					cn.Disconnect();
					cn.Dispose();
				}

				return Ok();
			}
		}
	}
}
