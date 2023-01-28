using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jira.Models.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace JiraTests.Fake
{
	class FakeJiraDbContext : JiraDbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=JiraTestDb;Trusted_Connection=True;");
		}
	}
}
