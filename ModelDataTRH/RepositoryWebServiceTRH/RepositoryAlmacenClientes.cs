using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.AlmacenesClientesContext;
namespace RepositoryWebServiceTRH
{
    public class RepositoryAlmacenClientes : RespositoryBase, IRepository<AlmacenesClientes, String>
    {
        public RepositoryAlmacenClientes(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref AlmacenesClientes entity)
        {
            throw new NotImplementedException();
        }

        public void Add(AlmacenesClientes entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<AlmacenesClientes> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<AlmacenesClientes> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AlmacenesClientes> Find(Expression<Func<AlmacenesClientes, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public AlmacenesClientes Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<AlmacenesClientes> GetAll()
        {
            try
            {
                AlmacenesClientes_Filter[] filtro = new AlmacenesClientes_Filter[] { new AlmacenesClientes_Filter() { Field = AlmacenesClientes_Fields.NombreAlmacen, Criteria = "*" } };
                return Context.contextAlmacenesClientes.ReadMultiple(filtro, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<AlmacenesClientes> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(AlmacenesClientes entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<AlmacenesClientes> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref AlmacenesClientes entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref AlmacenesClientes[] entitties)
        {
            throw new NotImplementedException();
        }
    }
}
