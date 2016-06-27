using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySqlTRH
{
    public interface IRepository<TEntity, Tid> where TEntity : class 
    {

        List<TEntity> GetAll();
        TEntity Get(Tid id);

    }
}
