using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.EmpleadoContext;

namespace RepositoryWebServiceTRH
{
    public class RepositoryEmpleado : RespositoryBase,IRepository<EmpleadoContext.Empleados, String>
    {

        

        public RepositoryEmpleado(HostWebService hostWs):base(hostWs) {

           
        }

        public void Add(ref Empleados entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Empleados entity)
        {

           
            throw new NotImplementedException();
        }

        public void AddRange(ref IEnumerable<Empleados> entitties)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Empleados> entitties)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Empleados> Find(Expression<Func<Empleados,bool>> predicate)
       // public IEnumerable<Empleados> Find( Predicate<Empleados> predicate)
        {
            Empleados_Filter[] filter = new Empleados_Filter[] { new Empleados_Filter { Field = Empleados_Fields.No, Criteria = "*" } };

            try
            {
                IQueryable<Empleados> query = Context.contextEmpleado.ReadMultiple(filter, null, 0).AsQueryable();
               // predicate(query)
               var result= query.Where(predicate).ToList();
                return result;
               // var expresion = delegate(q => q.FullName.StartsWith("Manuel"))
            
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("predicate", "El parametro 'predicate' no puede vernir vacio"); 
            }
            catch (Exception ex) {

                throw new Exception("Se ha producido una excipcion no controlada",ex.InnerException); 
            }
        }

        public Empleados Get(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {               
                return Context.contextEmpleado.Read(id);
            }
            else {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
        }

        public List<Empleados> GetAll()
        {
            try
            {
                Empleados_Filter[] filter = new Empleados_Filter[] { new Empleados_Filter { Field = Empleados_Fields.No, Criteria = "*" } };
                return Context.contextEmpleado.ReadMultiple(filter, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public List<Empleados> GetbyIdKey(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Empleados> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(Empleados entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ref Empleados entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(ref Empleados[] entitties)
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
