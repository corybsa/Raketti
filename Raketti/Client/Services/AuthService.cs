﻿using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _http;

		public AuthService(HttpClient http)
		{
			_http = http;
		}

		public async Task<object> Login(AuthInfo auth)
		{
			var result = await _http.PostAsJsonAsync("api/auth", auth);

			if (result.IsSuccessStatusCode)
			{
				return await result.Content.ReadFromJsonAsync<object>();
			}

			return null;
		}
	}
}
