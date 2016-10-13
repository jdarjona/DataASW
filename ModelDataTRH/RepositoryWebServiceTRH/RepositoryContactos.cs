using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.ContactosContext;
namespace RepositoryWebServiceTRH
{
    public class RepositoryContactos : RespositoryBase, IRepository<Contactos, String>
    {
        public RepositoryContactos(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public void Add(ref Contactos entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Contactos entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<Contactos> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Contactos> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contactos> Find(Expression<Func<Contactos, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Contactos Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<Contactos> GetAll()
        {
            try
            {
                Contactos_Filter[] filtro = new Contactos_Filter[] { new Contactos_Filter() { Field =  Contactos_Fields.Name, Criteria = "*" } };
                return Context.contextContactos.ReadMultiple(filtro, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<Contactos> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(Contactos entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Contactos> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(ref Contactos entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref Contactos[] entitties)
        {
            throw new NotImplementedException();
        }
    }
}
