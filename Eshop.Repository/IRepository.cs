using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Eshop.Domain.DomainModels;
using Microsoft.EntityFrameworkCore.Query;

namespace Eshop.Repository
{
    
    public interface IRepository<T> where T:  BaseEntity {

        T Insert(T entity);
        T Update(T entity);
        T Delete(T id);

        E Get<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);


    }

}
