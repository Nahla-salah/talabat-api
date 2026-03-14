using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repository
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecification<TEntity, Tkey> specification)
            where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);


            if (specification.IncludeExpression != null && specification.IncludeExpression.Any())
            {
                foreach (var include in specification.IncludeExpression)
                {
                    query = query.Include(include);
                }
            }



            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);

            else if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);


            return query;   

        }          
    }
}
