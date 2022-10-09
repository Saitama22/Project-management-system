using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Jira.Models.Intarfaces.Entities;

namespace Jira.Models.Entities
{
	public class TaskState : IIdEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }
}
}
