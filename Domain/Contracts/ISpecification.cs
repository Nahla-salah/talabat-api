using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity,T> where TEntity :BaseEntity<T>
    {
        //Criteria
        Expression<Func<TEntity, bool>> Criteria { get; } 
        //Include 
        List<Expression<Func<TEntity, Object>>> IncludeExpression { get; }

       //sorting
       Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }



    }
}
