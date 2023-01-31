using System;
using Jira;
using Jira.Models.DbContexts;
using JiraTests.Fake;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace JiraTests
{
	public abstract class BaseInitTest
	{
		private IServiceCollection _service;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void InitServiceProvider()
        {
            _service = new ServiceCollection();
            _service.Init();
            //_service.RemoveService<RealJiraDbContext>();
            _service.AddScoped<JiraDbContext, FakeJiraDbContext>();
            _service.AddDbContext<FakeJiraDbContext>();
            _serviceProvider = _service.BuildServiceProvider();
        }

        public T GetService<T>()
		{
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}