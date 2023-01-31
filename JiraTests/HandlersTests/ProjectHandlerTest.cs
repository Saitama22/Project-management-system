using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.Handlers;
using Jira.Models.Intarfaces.Repositories;
using JiraTests.RepoTests;
using NUnit.Framework;

namespace JiraTests.HandlersTests
{
	class ProjectHandlerTest : BaseInitTest
	{
		IProjectHandler _projectHandler;
		
		ProjectRepoTests _projectRepoTests;

		[SetUp]
		public void Init()
		{
			_projectHandler = GetService<IProjectHandler>();
			_projectRepoTests = new();
			_projectRepoTests.Init(GetService<IProjectRepo>());
		}

		[Test]
		public async Task OperationsTests()
		{
			await GetAllProjectTest();

			await CreateTest();
		}

		public async Task CreateTest()
		{
			var projectName = Guid.NewGuid().ToString();
			await _projectHandler.CreateOrUpdateAsync(new Project() 
			{ 
				Name = projectName
			});
			var projectId = _projectHandler.GetAllProjects().Last().Id;
			var project = await _projectHandler.GetProjectAsync(projectId);
			Assert.AreEqual(project.Name, projectName);
			Assert.AreEqual(project.TaskStates.Count(), 3);
			project.TaskStates = new();
			project.Name = "";
			await _projectHandler.CreateOrUpdateAsync(project);
			var projectUpdate = await _projectHandler.GetProjectAsync(projectId);
			Assert.True(projectUpdate.TaskStates.Count() == 0);
			Assert.AreEqual(projectUpdate.Name, string.Empty);
		}

		public async Task GetAllProjectTest()
		{
			var projects = _projectHandler.GetAllProjects();
			Assert.NotNull(projects);
			foreach (var project in projects)
			{
				await _projectHandler.DeleteProject(project);
			}
			var projectsAfterDelete = _projectHandler.GetAllProjects();
			Assert.NotNull(projectsAfterDelete);
			Assert.False(projectsAfterDelete.Any());

			await _projectRepoTests.Create();
			await _projectRepoTests.Create();
			var projectsAfterInsert = _projectHandler.GetAllProjects();
			Assert.AreEqual(projectsAfterInsert.Count(), 2);
			
		}
	}
}
