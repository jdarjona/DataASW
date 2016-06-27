using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySqlTRH
{
    public class RepositoryMonitorizacionCarga : IRepository<vListadoPedidosMonitorizacion, string>
    {

        private entityNavision db;


        public RepositoryMonitorizacionCarga() {

            db = new entityNavision();
          
        }

        public vListadoPedidosMonitorizacion Get(string id)
        {
            try
            {
                return db.vListadoPedidosMonitorizacion.Find(id);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'id' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Get] [id] ", ex.Message), ex.InnerException);

            }
        }

        public List<vListadoPedidosMonitorizacion> GetAll()
        {
            try
            {
                return db.vListadoPedidosMonitorizacion.OrderBy(c => c.indice).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo GetAll] ", ex.Message), ex.InnerException);

            }
        }
    }
}
