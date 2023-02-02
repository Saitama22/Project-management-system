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
	public class TaskStateRepo : BaseIdRepo<TaskState>, ITaskStateRepo
	{
		public TaskStateRepo(JiraDbContext context) : base(context)
		{
		}

		protected override DbSet<TaskState> MainDbSet => Context.TaskStates;

		public async Task AddTaskAsync(TaskJira taskJira, int taskStateId)
		{
			var taskState = await GetByIdAsync(taskStateId);
			taskState.TasksJira ??= new();
			taskState.TasksJira.Add(taskJira);
			await Context.SaveChangesAsync(); 
		}

		public async override Task<TaskState> GetByIdAsync(int id)
		{
			return await MainDbSet.Include(r => r.TasksJira).FirstOrDefaultAsync(r => r.Id == id);
		}

		protected override bool WithIncludeEntity(TaskState entity)
		{
			if (entity.TasksJira == null)
				return false;
			return true;
		}
	}
}
