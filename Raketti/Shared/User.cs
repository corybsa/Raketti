using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raketti.Shared
{
	[Table("usersORM")]
	public class User
	{
		[Key]
		[Required]
		public int UserId { get; set; }
		public string EmployeeId { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public string NormalizedUserName { get; set; }
		[Required]
		public string FirstName { get; set; }
		public string LastName { get; set; }
		// Value generated on add or update
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string NormalizedEmail { get; set; }
		public string PasswordHash { get; set; }
		public string PhoneNumber { get; set; }
		public string LocationCode { get; set; }
		[Required]
		public string Provider { get; set; }
		public DateTime LastSeen { get; set; }
		public string DisabledQueues { get; set; }
		[Required]
		public DateTime CreatedTimeStamp { get; set; }
		public int CreatedUserId { get; set; }
		[Required]
		public DateTime ModifiedTimeStamp { get; set; }
		public int ModifiedUserId { get; set; }
		public DateTime ArchivedTimeStamp { get; set; }
		public int ArchivedUserId { get; set; }
	}
}
