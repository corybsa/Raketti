using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raketti.Shared
{
	[Table("userRolesORM")]
	public class UserRoles
	{
		[Key]
		public int UserRoleId { get; set; }
		[ForeignKey("User")]
		public int UserId { get; set; }
		public int RoleId { get; set; }
		public string LocationCode { get; set; }
		public int? QueueId { get; set; }
		public bool? QueueAssignable { get; set; }

		public User User { get; set; }
	}
}
