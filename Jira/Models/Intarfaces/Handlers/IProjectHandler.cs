using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.Entities;

namespace Jira.Models.Intarfaces.Handlers
{
	public interface IProjectHandler
	{
		Task CreateOrUpdateAsync(Project project);
		Task CreateTaskAsync(TaskJira taskJira, int projectId);
		IQueryable<Project> GetAllProjects();
		Task<Project> GetProjectAsync(int projectId);
		Task<int> GetProjectIdByTaskStateIdAsync(int taskStateId);
	}
}
