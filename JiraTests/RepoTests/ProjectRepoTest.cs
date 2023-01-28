using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.Repositories;
using NUnit.Framework;

namespace JiraTests.RepoTests
{
	class ProjectRepoTest : BaseInitTest
	{
		[Test]
		public async Task CreateTestAsync()
		{
			var projectRepo = GetService<IProjectRepo>();
			await projectRepo.CreateOrUpdateAsync(new Project()
			{
				Name = "Test"
			});

		}
	}
}
