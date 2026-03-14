using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity :BaseEntity<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity,Tkey> specification);

        Task<TEntity> GetProductById(Tkey id);
        Task<TEntity> GetProductById(ISpecification<TEntity,Tkey> specification);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void delete(TEntity entity);



    }
}
