using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raketti.Client.Shared;

namespace Raketti.Client.Services
{
	public class FlyoutService : IFlyoutService
	{
		public bool IsOpen { get; set; } = false;

		public void Open() {
			Console.WriteLine("open flyout");
			IsOpen = true;
		}

		public void Close() {
			Console.WriteLine("close flyout");
			IsOpen = false;
		}
	}
}
