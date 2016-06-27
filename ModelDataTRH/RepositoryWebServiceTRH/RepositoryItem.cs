using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
using RepositoryWebServiceTRH.ItemContext;

namespace RepositoryWebServiceTRH
{
    public class RepositoryItem : RespositoryBase, IRepository<NuevaListaProductos, String>
    {


        public RepositoryItem(HostWebService hostWs) : base(hostWs)
        {
           
        }

        public void Add(ref NuevaListaProductos entity)
        {
            throw new NotImplementedException();
        }

        public void Add(NuevaListaProductos entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<NuevaListaProductos> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<NuevaListaProductos> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NuevaListaProductos> Find(Expression<Func<NuevaListaProductos, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public NuevaListaProductos Get(string id)
        {
            try
            {

                return Context.contextItem.Read(id);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw  new Exception(string.Format("{0} mensaje: {1}","[Metodo Get] ",ex.Message),ex.InnerException);
            }

        }

        public List<NuevaListaProductos> GetAll()
        {
            try
            {
                return Context.contextItem.ReadMultiple(null, null, 0).ToList();
            }            
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }    
        }

        public List<NuevaListaProductos> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<NuevaListaProductos> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(NuevaListaProductos entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ref NuevaListaProductos entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref NuevaListaProductos[] entitties)
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
