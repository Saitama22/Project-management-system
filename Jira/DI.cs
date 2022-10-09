using Jira.Models.DbContexts;
using Microsoft.Extensions.DependencyInjection;

namespace Jira
{
	public static class DI
	{
		public static IServiceCollection Init(this IServiceCollection services)
		{
			services.AddDbContext();
			return services;
		}

		private static IServiceCollection AddDbContext(this IServiceCollection services)
		{
			services.AddDbContext<JiraDbContext>();
			return services;
		}
	}
}
