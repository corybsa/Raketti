using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raketti.Shared;

namespace Raketti.Server.Data
{
	public class SqlContext : DbContext
	{
		public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

		// Entities
		public DbSet<User> Users { get; set; }
		public DbSet<UserRoles> UserRoles { get; set; }
		public DbSet<Student> Students { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// computed column
			modelBuilder.Entity<User>()
				.Property(b => b.DisplayName)
				.HasComputedColumnSql("(Trim(([FirstName]+' ')+isnull([LastName],'')))");
		}
	}
}
