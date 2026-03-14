using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repository
{
    public class GenericRepository<TEntity, Tkey>(CoreveraDbContext _coreveraDbContext) :IGenericRepository<TEntity , Tkey> where TEntity :BaseEntity<Tkey>
    {
     

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _coreveraDbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification)
        {
           return  await  SpecificationEvaluator.CreateQuery(_coreveraDbContext.Set<TEntity>(), specification).ToListAsync();
        }



        public async Task<TEntity> GetProductById(Tkey id) => await  _coreveraDbContext.Set<TEntity>().FindAsync(id);
        public async Task<TEntity> GetProductById(ISpecification<TEntity, Tkey> specification)
        {
           return await  SpecificationEvaluator.CreateQuery(_coreveraDbContext.Set<TEntity>(), specification).FirstOrDefaultAsync();
        }




        public void Update(TEntity entity)
        {
            _coreveraDbContext.Set<TEntity>().Update(entity);
        }



        public async Task AddAsync(TEntity entity)
        {
           await _coreveraDbContext.Set<TEntity>().AddAsync(entity);
        }



        public void delete(TEntity entity)
        {
            _coreveraDbContext.Set<TEntity>().Remove(entity);
        }

      

   
    }
}
