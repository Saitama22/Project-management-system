using System.Collections.Generic;
using Jira.Models.Intarfaces.Entities;

namespace Jira.Models.Entities
{
	public class Project: IIdEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public List<TaskState> TaskStates { get; set; }
	}
}
