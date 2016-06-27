using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.PedidoVentasContext;

namespace RepositoryWebServiceTRH
{
   public  class RepositoryPedidosVenta : RespositoryBase,IRepository<Pedidos,string>
    {
        public RepositoryPedidosVenta(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref Pedidos entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Pedidos entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<Pedidos> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Pedidos> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pedidos> Find(Expression<Func<Pedidos, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Pedidos Get(string id)
        {
            try
            {

                return Context.contextPedidosVenta.Read(id);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo Get] [id] ", ex.Message), ex.InnerException);

            }
        }

        public List<Pedidos> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Pedidos> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(Pedidos entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Pedidos> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref Pedidos entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref Pedidos[] entitties)
        {
            throw new NotImplementedException();
        }
    }
}
