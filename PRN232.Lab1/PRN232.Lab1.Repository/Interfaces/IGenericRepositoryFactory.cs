using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.Lab1.Repository.Interfaces
{
	public interface IGenericRepositoryFactory
	{
		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}
