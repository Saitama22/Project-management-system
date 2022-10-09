using Jira.Models.Intarfaces.Entities;

namespace Jira.Models.Entities
{
	public class TaskJira : IIdEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public TaskState State { get; set; }

		public Project Project { get; set; }
	}
}
