using System;
using System.Linq;
using Jira.Models.DbContexts;
using Jira.Models.Handlers;
using Jira.Models.Intarfaces.DbContexts;
using Jira.Models.Intarfaces.Handlers;
using Jira.Models.Intarfaces.Repositories;
using Jira.Models.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Jira
{
	public static class DI
	{
		public static IServiceCollection Init(this IServiceCollection services)
		{
			services.AddMvc();
			services.AddControllersWithViews();
			services.AddDbContext();
			services.AddRepos();
			services.AddHandlers();
			return services;
		}

		private static IServiceCollection AddDbContext(this IServiceCollection services)
		{
			services.AddDbContext<JiraDbContext>();
		//	services.AddScoped<JiraDbContext, RealJiraDbContext>();
			return services;
		}

		private static IServiceCollection AddRepos(this IServiceCollection services)
		{
			services.AddScoped<IProjectRepo, ProjectRepo>();
			services.AddScoped<ITaskStateRepo, TaskStateRepo>();
			services.AddScoped<ITaskJiraRepo, TaskJiraRepo>();
			return services;
		}

		private static IServiceCollection AddHandlers(this IServiceCollection services)
		{
			services.AddScoped<IProjectHandler, ProjectHandler>();
			return services;
		}
		public static IServiceCollection RemoveService<T>(this IServiceCollection services)
		{
			if (services.IsReadOnly)
			{
				throw new Exception($"{nameof(services)} is read only");
			}

			var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
			if (serviceDescriptor != null) services.Remove(serviceDescriptor);

			return services;
		}		
	}
}
