using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raketti.Shared
{
	public class User
	{
		public int UserId { get; set; }
		public string EmployeeId { get; set; }
		public string UserName { get; set; }
		public string NormalizedUserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string NormalizedEmail { get; set; }
		public string PasswordHash { get; set; }
		public string PhoneNumber { get; set; }
		public string LocationCode { get; set; }
		public string Provider { get; set; }
		public DateTime LastSeen { get; set; }
		public string DisabledQueues { get; set; }
		public DateTime CreatedTimeStamp { get; set; }
		public int CreatedUserId { get; set; }
		public DateTime ModifiedTimeStamp { get; set; }
		public int ModifiedUserId { get; set; }
		public DateTime ArchivedTimeStamp { get; set; }
		public int ArchivedUserId { get; set; }
	}
}
