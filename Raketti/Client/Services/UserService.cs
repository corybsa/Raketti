using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public class UserService : IUserService
	{
		private readonly HttpClient _http;

		public UserService(HttpClient http) {
			_http = http;
		}

		public async Task<DbResponse<User>> GetUsers() {
			return await _http.GetFromJsonAsync<DbResponse<User>>("api/test/users");
		}
	}
}
