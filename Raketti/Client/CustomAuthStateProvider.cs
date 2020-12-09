using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Raketti.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Raketti.Client
{
	public class CustomAuthStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService _localStorageService;
		private readonly HttpClient _http;

		public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
		{
			_localStorageService = localStorageService;
			_http = http;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

			var identity = new ClaimsIdentity();
			_http.DefaultRequestHeaders.Authorization = null;

			if (!string.IsNullOrEmpty(authToken))
			{
				try
				{
					identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
					_http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

					/*foreach (var c in identity.Claims)
					{
						if (c.Type.Equals("exp"))
						{
							var expireTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(int.Parse(c.Value));

							if (DateTime.UtcNow > expireTime)
							{
								throw new Exception("Token expired");
							}
						}
					}*/
				}
				catch (Exception)
				{
					await _localStorageService.RemoveItemAsync("authToken");
					identity = new ClaimsIdentity();
				}
			}

			var user = new ClaimsPrincipal(identity);
			var state = new AuthenticationState(user);

			NotifyAuthenticationStateChanged(Task.FromResult(state));

			return state;
		}

		private byte[] ParseBase64WithoutPadding(string base64)
		{
			switch (base64.Length % 4)
			{
				case 2: base64 += "=="; break;
				case 3: base64 += "="; break;
			}
			return Convert.FromBase64String(base64);
		}

		public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
		{
			var payload = jwt.Split('.')[1];
			var jsonBytes = ParseBase64WithoutPadding(payload);
			var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
			var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

			return claims;
		}
	}
}
