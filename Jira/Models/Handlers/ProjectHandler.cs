using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.Handlers;
using Jira.Models.Intarfaces.Repositories;

namespace Jira.Models.Handlers
{
	public class ProjectHandler : IProjectHandler
	{
		private readonly IProjectRepo _projectRepo;
		private readonly ITaskStateRepo _taskStateRepo;

		public ProjectHandler(IProjectRepo projectRepo, ITaskStateRepo taskStateRepo)
		{
			_projectRepo = projectRepo;
			_taskStateRepo = taskStateRepo;
		}

		public async Task CreateOrUpdateAsync(Project project)
		{
			if (project.Id == 0)
				project.TaskStates = new List<TaskState>()
				{
					new TaskState() { Name = "Надо сделать" },
					new TaskState() { Name = "В работе" },
					new TaskState() { Name = "Сделано" },
				};
			await _projectRepo.CreateOrUpdateAsync(project);
		}

		public IQueryable<Project> GetAllProjects()
		{
			return _projectRepo.Records ?? new List<Project>().AsQueryable();
		}

		public async Task<Project> GetProjectAsync(int projectId)
		{
			var project = await _projectRepo.GetByIdAsync(projectId);
			project.TaskStates ??= new();
			foreach (var taskStates in project.TaskStates)
			{
				taskStates.TasksJira ??= new();
			}
			return project;
		}

		public async Task CreateTaskAsync(TaskJira taskJira, int taskStateId)
		{
			await _taskStateRepo.AddTaskAsync(taskJira, taskStateId);
		}

		public async Task<int> GetProjectIdByTaskStateIdAsync(int taskStateId)
		{
			var taskState = await _taskStateRepo.GetByIdAsync(taskStateId);
			return (await _projectRepo.GetByIdAsync(taskState.Project.Id)).Id;
		}
	}
}
