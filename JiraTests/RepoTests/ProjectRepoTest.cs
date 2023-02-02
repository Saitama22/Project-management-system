using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jira.Models.Entities;
using Jira.Models.Intarfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace JiraTests.RepoTests
{
	class ProjectRepoTests : BaseInitTest
	{
		public IProjectRepo ProjectRepo { get; set; }

		[SetUp]
		public void Init()
		{
			ProjectRepo = GetService<IProjectRepo>();
		}

		public void Init(IProjectRepo projectRepo)
		{
			ProjectRepo = projectRepo;
		}

		[Test]
		public async Task CreateAndDeleteProjectAsync()
		{			
			//Created
			var resultRecord = await Create();
			var resultRecordName = resultRecord.Name;

			//Read
			var resultRecordById = await Get(resultRecord.Id);
			Assert.NotNull(resultRecordById);
			Assert.AreEqual(resultRecordById.Name, resultRecordName);

			//Update
			await Update(resultRecordById);

			//Delete
			await Delete(resultRecordById);
		}

		public async Task<Project> Create(string name = null)
		{
			var projectName = name ?? Guid.NewGuid().ToString();
			await ProjectRepo.CreateOrUpdateAsync(new Project()
			{
				Name = projectName,
			});
			var resultRecord = ProjectRepo.Records.FirstOrDefault(r => r.Name == projectName);
			Assert.NotNull(resultRecord);
			Assert.AreEqual(resultRecord.Name, projectName);
			return resultRecord;
		}

		public async Task<Project> Get(int id)
		{
			var resultRecord = await ProjectRepo.GetByIdAsync(id);
			return resultRecord;
		}

		public async Task Update(Project project, string name = null)
		{
			var projectNameOld = project.Name;
			var projectNameUpdated = Guid.NewGuid().ToString();
			Assert.AreNotEqual(projectNameUpdated, project.Name);
			project.Name = projectNameUpdated;
			await ProjectRepo.CreateOrUpdateAsync(project);

			var resultRecordOldName = await ProjectRepo.Records.FirstOrDefaultAsync(r => r.Name == projectNameOld);
			Assert.Null(resultRecordOldName);

			var resultRecordNewName = await ProjectRepo.Records.FirstOrDefaultAsync(r => r.Name == projectNameUpdated);
			Assert.NotNull(resultRecordNewName);
			Assert.AreEqual(resultRecordNewName.Id, project.Id);
		}

		private async Task Delete(Project project)
		{
			await ProjectRepo.DeleteByIdAsync(project.Id);
			Assert.Null(await ProjectRepo.GetByIdAsync(project.Id));
		}
	}
}
