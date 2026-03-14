using Domain.Contracts;
using Domain.Models;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repository
{
    public class UnitOfWork(CoreveraDbContext _coreveraDbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = new Dictionary<string, object>();
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
            {
                return (IGenericRepository<TEntity, Tkey>)_repositories[typeName];
            }
            else
            {
                //Create object of repository
                var repo = new GenericRepository<TEntity, Tkey>(_coreveraDbContext);
                //Add object to dictionary
                _repositories.Add(typeName, repo);
                //Return object
                return repo;
            }

        }

        public async Task<int> SaveChangesAsync()
        {
       return   await  _coreveraDbContext.SaveChangesAsync();
        }
    }
}
