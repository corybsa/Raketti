using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public interface IUserService
	{
		Task<IEnumerable<User>> GetUsers();
	}
}
