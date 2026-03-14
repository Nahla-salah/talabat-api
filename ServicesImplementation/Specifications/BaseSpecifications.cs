using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesImplementation.Specifications
{
    public class BaseSpecifications<TEntity,T> : ISpecification<TEntity,T> where TEntity : BaseEntity<T>
    {

            public BaseSpecifications(Expression<Func<TEntity, bool>> criteria)
            {
                Criteria = criteria;
            }
        public Expression<Func<TEntity, bool>> Criteria
        {
            get;
            private set;
        }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpression.Add(includeExpression);


        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
            => OrderBy = orderByExpression;



        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
         => OrderByDescending = orderByDescExpression;

    }
}
