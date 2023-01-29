using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models.DbContexts;
using Jira.Models.Intarfaces.DbContexts;
using Jira.Models.Intarfaces.Entities;
using Jira.Models.Intarfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Jira.Models.Repositories
{
	public abstract class BaseIdRepo<T> : IIdRepo<T> where T : class, IIdEntity
	{
		protected BaseIdRepo(JiraDbContext context)
		{
			Context = context;
		}

		protected JiraDbContext Context { get; private set; }
		protected abstract DbSet<T> MainDbSet { get; }

		public IQueryable<T> Records => MainDbSet;

		public async Task CreateOrUpdateAsync(IList<T> entities)
		{
			foreach (var entity in entities)
			{
				await CreateOrUpdateAsync(entity);
			}
		}

		public virtual async Task CreateOrUpdateAsync(T entity)
		{
			if (entity.Id == 0)
			{
				await MainDbSet.AddAsync(entity);
			}
			else
			{
				if (MainDbSet.Any(r => r.Id == entity.Id))
					MainDbSet.Update(entity);
			}
			await Context.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			await DeleteAsync(entity);
		}

		public async Task DeleteAsync(T entity)
		{
			await Task.Run(() => MainDbSet.Remove(entity));
			await Context.SaveChangesAsync();
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			return await MainDbSet.FindAsync(id);
		}
	}
}
