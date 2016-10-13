using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.DireccionesEnvioContext;
namespace RepositoryWebServiceTRH
{
    public class RepositoryDireccionEnvio : RespositoryBase, IRepository<DireccionesEnvio, String>
    {
        public RepositoryDireccionEnvio(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref DireccionesEnvio entity)
        {
            throw new NotImplementedException();
        }

        public void Add(DireccionesEnvio entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<DireccionesEnvio> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<DireccionesEnvio> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DireccionesEnvio> Find(Expression<Func<DireccionesEnvio, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DireccionesEnvio Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<DireccionesEnvio> GetAll()
        {
            try
            {
                DireccionesEnvio_Filter[] filtro = new DireccionesEnvio_Filter[] { new DireccionesEnvio_Filter { Field = DireccionesEnvio_Fields.Code, Criteria = "*" } };
                return Context.contextDireccionesEnvio.ReadMultiple(filtro, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<DireccionesEnvio> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(DireccionesEnvio entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<DireccionesEnvio> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref DireccionesEnvio entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref DireccionesEnvio[] entitties)
        {
            throw new NotImplementedException();
        }
    }
}
