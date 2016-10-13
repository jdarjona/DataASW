using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.OfertaListadoContext;

namespace RepositoryWebServiceTRH
{
   public class RepositoryOfertaListado : RespositoryBase, IRepository<OfertaListado, String>
    {


        public RepositoryOfertaListado(HostWebService hostWs) : base(hostWs)
        {

        }
        
        public void Add(ref OfertaListado entity)
        {
            throw new NotImplementedException();
        }

        public void Add(OfertaListado entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<OfertaListado> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<OfertaListado> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OfertaListado> Find(Expression<Func<OfertaListado, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public OfertaListado Get(string id)
        {
            try
            {

                return Context.contextOfertaListado.Read(id);
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

        public List<OfertaListado> GetAll()
        {
            try
            {
                DateTime fecha = DateTime.Today.AddDays(-15);
                String fechaString = fecha.ToString("d");
                OfertaListado_Filter[] filtro = new OfertaListado_Filter[] { new OfertaListado_Filter { Field = OfertaListado_Fields.Posting_Date, Criteria = ">" + fechaString } };
                return Context.contextOfertaListado.ReadMultiple(null, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<OfertaListado> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<OfertaListado> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(OfertaListado entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ref OfertaListado entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref OfertaListado[] entitties)
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

