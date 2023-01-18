using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.DbContexts;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jira.Models.Repositories
{
	public class TaskJiraRepo : BaseIdRepo<TaskJira>, ITaskJiraRepo
	{
		public TaskJiraRepo(JiraDbContext context) : base(context)
		{
		}

		protected override DbSet<TaskJira> MainDbSet => Context.Tasks;
	}
}
