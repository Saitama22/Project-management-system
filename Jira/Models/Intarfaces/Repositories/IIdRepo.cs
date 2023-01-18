using System.Linq;
using System.Threading.Tasks;
using Jira.Models.Intarfaces.Entities;

namespace Jira.Models.Intarfaces.Repositories
{
	public interface IIdRepo<T> : IRepo<T> where T: IIdEntity 
	{
		IQueryable<T> Records { get; }

		Task<T> GetByIdAsync(int id);

		Task DeleteAsync(int id);
	}
}
