using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.ClientesContext;

namespace RepositoryWebServiceTRH
{
    public class RepositoryClientes : RespositoryBase, IRepository<Clientes, String>
    {
        public RepositoryClientes(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref Clientes entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Clientes entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<Clientes> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Clientes> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Clientes> Find(Expression<Func<Clientes, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Clientes Get(string id)
        {

            try
            {

                return Context.contextClientes.Read(id);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo Get] ", ex.Message), ex.InnerException);
            }
        }

        public List<Clientes> GetAll()
        {
            try
            {
                Clientes_Filter[] filtro = new Clientes_Filter[] { new Clientes_Filter() { Field = Clientes_Fields.No, Criteria = "*" } };
                return Context.contextClientes.ReadMultiple(filtro, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<Clientes> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(Clientes entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Clientes> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref Clientes entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref Clientes[] entitties)
        {
            throw new NotImplementedException();
        }
    }


}
