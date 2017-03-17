using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;

namespace RepositoryWebServiceTRH
{
    public class RepositoryEntragaAlmacenEpis : RespositoryBase, IRepository<EntregaAlmacen, String>
    {
        public RepositoryEntragaAlmacenEpis(HostWebService hostWs) : base(hostWs)
        {
            
        }

       

        public void insertRepuesto(string codEmpleado,string codRepuesto,decimal cantidad,int maquina,int destino) {
            try
            {
                CultureInfo culture = new CultureInfo("es-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                Context.contextAlmacenesRepuestos.InsertRepuesto(codEmpleado, codRepuesto,cantidad,maquina,destino);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Error insertRepuesto campos nulos", "Comuníquelo al departamento de informática, gracias.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo insertRepuesto] [codEmpleado] ", ex.Message), ex.InnerException);

            }
        }

        public void updateRepuesto(string codEmpleado, string codRepuesto, decimal cantidad, int maquina, int destino)
        {
            try
            {
                CultureInfo culture = new CultureInfo("es-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                Context.contextAlmacenesRepuestos.UpdateRepuesto(codEmpleado, codRepuesto, cantidad, maquina, destino);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Error updateRepuesto campos nulos", "Comuníquelo al departamento de informática, gracias.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo updateRepuesto] [codEmpleado] ", ex.Message), ex.InnerException);

            }
        }

        public void deleteRepuesto(string codProducto,string codEmpleado)
        {
            try
            {
                CultureInfo culture = new CultureInfo("es-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                Context.contextAlmacenesRepuestos.DeleteRepuesto(codProducto, codEmpleado);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("Error deleteRepuesto campos nulos", "Comuníquelo al departamento de informática, gracias.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo deleteRepuesto] [codEmpleado] ", ex.Message), ex.InnerException);

            }
        }





        public string register(string codEmpleado) {

            try
            {
                CultureInfo culture = new CultureInfo("es-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;

                return Context.contextAlmacenesRepuestos.RegistrarEntrega(codEmpleado);
                 
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("codEmpleado", "El parametro 'codEmpleado' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo register] [codEmpleado] ", ex.Message), ex.InnerException);

            }
            
        }

        public void Add(EntregaAlmacen entity)
        {

            try
            {
                
                Context.contextEntregaAlmacenEpis.Create(ref entity);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("entity", "El parametro 'entity' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo Add] [entity] ", ex.Message), ex.InnerException);
                
            }
           
        }

        public void Add(ref EntregaAlmacen entity)
        {
            try
            {
                Context.contextEntregaAlmacenEpis.Create(ref entity);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("entity", "El parametro 'entity' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo Add] [entity] ", ex.Message), ex.InnerException);

            }
        }

        public void AddRange(IEnumerable<EntregaAlmacen> entitties)
        {

            try
            {
                EntregaAlmacen[] entregaAlmacenArray = entitties.ToArray<EntregaAlmacen>();
                Context.contextEntregaAlmacenEpis.CreateMultiple(ref entregaAlmacenArray);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("entitties", "El parametro 'entitties' no puede vernir vacio");
            }
            catch (Exception  ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo AddRange] [entitties] ", ex.Message), ex.InnerException);
                
            }
           
        }

        public void AddRange(ref IEnumerable<EntregaAlmacen> entitties)
        {
            try
            {
                EntregaAlmacen[] entregaAlmacenArray = entitties.ToArray<EntregaAlmacen>();
                Context.contextEntregaAlmacenEpis.CreateMultiple(ref entregaAlmacenArray);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("entitties", "El parametro 'entitties' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo AddRange] [entitties] ", ex.Message), ex.InnerException);

            }
        }

        public IEnumerable<EntregaAlmacen> Find(Expression<Func<EntregaAlmacen, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<EntregaAlmacen> GetbyIdKey(string id)
        {
            
            try
            {
                EntregaAlmacen_Filter[] filtro = new EntregaAlmacen_Filter[2];

                //Filtro Empleado
                filtro[0] = new EntregaAlmacen_Filter();
                filtro[0].Field = EntregaAlmacen_Fields.Cod_Empleado;
                filtro[0].Criteria = id;
                //Filtro Empleado
                filtro[1] = new EntregaAlmacen_Filter();
                filtro[1].Field = EntregaAlmacen_Fields.Entregado;
                filtro[1].Criteria = "No";

                return Context.contextEntregaAlmacenEpis.ReadMultiple(filtro, null, 0).ToList();
              
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo GetbyIdKey] [id] ", ex.Message), ex.InnerException);
            }
        }

        public List<EntregaAlmacen> GetAll()
        {
            try
            {
                return Context.contextEntregaAlmacenEpis.ReadMultiple(null, null, 0).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error no controlado: ", ex.InnerException);
            }
        }

        public void RemoveRange(IEnumerable<EntregaAlmacen> entities)
        {
            throw new NotImplementedException();
        }

        public void Remove(EntregaAlmacen entity)
        {
            throw new NotImplementedException();
        }

        public EntregaAlmacen Get(string id)
        {
            try
            {
                EntregaAlmacen_Filter[] filtro = new EntregaAlmacen_Filter[1];

                //Filtro Empleado
                filtro[0] = new EntregaAlmacen_Filter();
                filtro[0].Field = EntregaAlmacen_Fields.Cod_Producto;
                filtro[0].Criteria = id;
              

                
                return Context.contextEntregaAlmacenEpis.ReadMultiple(filtro, null, 0).First();

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo GetbyIdKey] [id] ", ex.Message), ex.InnerException);
            }
        }

        public void Update(ref EntregaAlmacen entity)
        {
            try
            {
                Context.contextEntregaAlmacenEpis.Update(ref entity);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("entity", "El parametro 'entity' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo Add] [entity] ", ex.Message), ex.InnerException);

            }
        }

        public void UpdateRange(ref EntregaAlmacen[] entitties)
        {
            try
            {
              
                Context.contextEntregaAlmacenEpis.UpdateMultiple(ref entitties);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("entitties", "El parametro 'entitties' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo AddRange] [entitties] ", ex.Message), ex.InnerException);

            }
        }

        public void Remove(string key)
        {
            try
            {
                Context.contextEntregaAlmacenEpis.Delete(key);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo Remove] [entitties] ", ex.Message), ex.InnerException);

            }
            
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public string GetAlbaran(string codDocumento) {

            try
            {
                return  Context.contextAlmacenesRepuestos.GetAlbaran(codDocumento);

            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("codDocumento", "El parametro 'codDocumento' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo getAlbaran] [codDocumento] ", ex.Message), ex.InnerException);

            }

        }
    }
}
