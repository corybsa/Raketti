using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raketti.Client.Shared;

namespace Raketti.Client.Services
{
	public class FlyoutService : IFlyoutService
	{
		public event Action OnChange;
		public bool IsOpen { get; set; } = false;

		private void NotifyDataChanged() => OnChange?.Invoke();

		public void Open() {
			IsOpen = true;
			NotifyDataChanged();
		}

		public void Close() {
			IsOpen = false;
			NotifyDataChanged();
		}
	}
}
