using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raketti.Client.Services
{
	public class BrowserService
	{
		private readonly IJSRuntime _js;

		public BrowserService(IJSRuntime js)
		{
			_js = js;
		}

		public async Task<BrowserDimensions> GetDimensions()
		{
			return await _js.InvokeAsync<BrowserDimensions>("getDimensions");
		}
	}

	public class BrowserDimensions
	{
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
