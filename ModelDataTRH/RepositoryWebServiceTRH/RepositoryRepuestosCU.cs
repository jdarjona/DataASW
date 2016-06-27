using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.AlmacenRepuestosContext;

namespace RepositoryWebServiceTRH
{
    class RepositoryRepuestosCU : RespositoryBase,IRepository<RegistrarEntrega, String>
    {
        public RepositoryRepuestosCU(HostWebService hostWs) : base(hostWs)
        {

        }

        public void Add(ref RegistrarEntrega entity)
        {
            throw new NotImplementedException();
        }

        public void Add(RegistrarEntrega entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<RegistrarEntrega> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<RegistrarEntrega> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegistrarEntrega> Find(Expression<Func<RegistrarEntrega, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public RegistrarEntrega Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<RegistrarEntrega> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<RegistrarEntrega> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<RegistrarEntrega> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(RegistrarEntrega entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ref RegistrarEntrega entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref RegistrarEntrega[] entitties)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }
    }
}
