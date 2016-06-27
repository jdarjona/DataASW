using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace RepositoryWebServiceTRH
{
    public interface IRepository<TEntity,Tid> where TEntity:class
    {


        TEntity Get(Tid id);
        List<TEntity> GetbyIdKey(Tid id);
        List<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void Add(ref TEntity entity);
        void AddRange(IEnumerable<TEntity> entitties);
        void AddRange(ref IEnumerable<TEntity> entitties);

        void Update(ref TEntity entity);
        void UpdateRange(ref TEntity[] entitties);

        void Remove(TEntity entity);
        void Remove(string key);
        void RemoveAll();
        void RemoveRange(IEnumerable<TEntity> entities);
        

    }
}
