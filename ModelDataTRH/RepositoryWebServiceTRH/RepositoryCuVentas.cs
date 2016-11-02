using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryWebServiceTRH
{
   public class RepositoryCuVentas : RespositoryBase
    {
        public RepositoryCuVentas(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public string getDocumentoInidenciaDireccion(string codDocumento,string destino) {

            try
            {
                return Context.contextCuVentas.PdfIncidenciaDireccion(codDocumento, destino);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("id", "El parametro 'codDocumento' no puede vernir vacio");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo getDocumentoInidenciaDireccion] ", ex.Message), ex.InnerException);
            }
        }
    }
}
