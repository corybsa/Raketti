using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Raketti.Client.Shared;

namespace Raketti.Client.Services
{
	public interface IFlyoutService
	{
		public bool IsOpen { get; set; }
		public string Title { get; set; }
		public Type Content { get; set; }
		public event Action OnChange;

		public void Open(string title, Type content);
		public void Close();
	}
}
