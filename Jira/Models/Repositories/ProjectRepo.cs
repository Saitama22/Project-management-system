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
	public class ProjectRepo : BaseIdRepo<Project>, IProjectRepo
	{
		public ProjectRepo(JiraDbContext context) : base(context)
		{
		}

		protected override DbSet<Project> MainDbSet => Context.Projects;

		public override async Task<Project> GetByIdAsync(int id)
		{
			return await MainDbSet.Include(r => r.TaskStates).ThenInclude(r => r.TasksJira).FirstOrDefaultAsync(r => r.Id == id);
		}

		protected override bool WithIncludeEntity(Project entity)
		{
			if (entity.TaskStates == null)
				return false;
			return true;
		}
	}
}
