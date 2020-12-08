using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raketti.Client.Shared;

namespace Raketti.Client.Services
{
	public interface IFlyoutService
	{
		public bool IsOpen { get; set; }
		public event Action OnChange;
		public void Open();
		public void Close();
	}
}
