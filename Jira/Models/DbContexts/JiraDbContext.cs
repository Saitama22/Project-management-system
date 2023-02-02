using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Jira.Models.DbContexts
{
	public class JiraDbContext: DbContext, IJiraDbContext
	{
		public DbSet<Project> Projects { get; set; }		
		public DbSet<TaskJira> Tasks { get; set; }
		public DbSet<TaskState> TaskStates { get; set; }
				
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=JiraDb;Trusted_Connection=True;");
		}

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
		//	modelBuilder.Entity<Project>()
		//		.HasMany<TaskState>(e => e.TaskStates)
		//		.
		//}
	}
}
