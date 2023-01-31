using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.Intarfaces.Entities;

namespace Jira.Models.Intarfaces.Repositories
{
	public interface IRepo<T>
	{
		public Task CreateOrUpdateAsync(T entity);

		public Task CreateOrUpdateAsync(IList<T> entity);

		public Task DeleteAsync(T entity);

	}
}
