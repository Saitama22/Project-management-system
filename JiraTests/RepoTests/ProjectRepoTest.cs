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
	class ProjectRepoTest : BaseInitTest
	{
		IProjectRepo _projectRepo;
		[SetUp]
		public async Task Init()
		{
			_projectRepo = GetService<IProjectRepo>();
		}
		[Test]
		public async Task CreateAndDeleteProjectAsync()
		{
			//Created
			var projectNameOld = Guid.NewGuid().ToString();
			await _projectRepo.CreateOrUpdateAsync(new Project()
			{
				Name = projectNameOld,
			});
			var resultRecord = await _projectRepo.Records.FirstOrDefaultAsync(r => r.Name == projectNameOld);
			Assert.NotNull(resultRecord);
			Assert.AreEqual(resultRecord.Name, projectNameOld);

			//GetByID
			var resultRecordById = await _projectRepo.GetByIdAsync(resultRecord.Id);
			Assert.NotNull(resultRecordById);
			Assert.AreEqual(resultRecordById.Name, projectNameOld);

			//Update
			var projectNameUpdated = Guid.NewGuid().ToString();
			resultRecordById.Name = projectNameUpdated;
			Assert.AreNotEqual(projectNameUpdated, projectNameOld);
			await _projectRepo.CreateOrUpdateAsync(resultRecordById);

			//GetByID Old
			var resultRecordOldName = await _projectRepo.Records.FirstOrDefaultAsync(r => r.Name == projectNameOld);
			Assert.Null(resultRecordOldName);
			var resultRecordNewName = await _projectRepo.Records.FirstOrDefaultAsync(r => r.Name == projectNameUpdated);
			Assert.NotNull(resultRecordNewName);
			Assert.AreEqual(resultRecordNewName.Id, resultRecord.Id);

			//Delete
			await _projectRepo.DeleteByIdAsync(resultRecordNewName.Id);
			Assert.Null(await _projectRepo.GetByIdAsync(resultRecordNewName.Id));
		}
	}
}
