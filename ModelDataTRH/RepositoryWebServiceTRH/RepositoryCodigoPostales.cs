using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.CodigoPostalesContext;
namespace RepositoryWebServiceTRH
{
    public class RepositoryCodigoPostales : RespositoryBase, IRepository<CodigoPostales, String>
    {
        public RepositoryCodigoPostales(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref CodigoPostales entity)
        {
            throw new NotImplementedException();
        }

        public void Add(CodigoPostales entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<CodigoPostales> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<CodigoPostales> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CodigoPostales> Find(Expression<Func<CodigoPostales, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public CodigoPostales Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<CodigoPostales> GetAll()
        {
            try
            {
                CodigoPostales_Filter[] filtro = new CodigoPostales_Filter[] { new CodigoPostales_Filter() { Field = CodigoPostales_Fields.Code, Criteria = "*" } };
                return Context.contextCodigoPostales.ReadMultiple(filtro, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<CodigoPostales> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(CodigoPostales entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<CodigoPostales> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref CodigoPostales entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref CodigoPostales[] entitties)
        {
            throw new NotImplementedException();
        }
    }
}
