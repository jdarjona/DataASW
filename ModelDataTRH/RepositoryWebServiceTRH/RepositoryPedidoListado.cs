using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.PedidoListadoContext;
namespace RepositoryWebServiceTRH
{
    public class RepositoryPedidoListado : RespositoryBase, IRepository<PedidoListado, string>
    {
        public RepositoryPedidoListado(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref PedidoListado entity)
        {
            throw new NotImplementedException();
        }

        public void Add(PedidoListado entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<PedidoListado> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<PedidoListado> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoListado> Find(Expression<Func<PedidoListado, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public PedidoListado Get(string id)
        {
            try
            {

                return Context.contextPedidoListado.ReadByRecId(id);
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

        public List<PedidoListado> GetAll()
        {
            try
            {

                PedidoListado_Filter[] filtro = new PedidoListado_Filter[] { new PedidoListado_Filter { Field = PedidoListado_Fields.Sell_to_Customer_Name, Criteria = "*" } };
                return Context.contextPedidoListado.ReadMultiple(filtro, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<PedidoListado> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(PedidoListado entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<PedidoListado> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref PedidoListado entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref PedidoListado[] entitties)
        {
            throw new NotImplementedException();
        }
    }
}
