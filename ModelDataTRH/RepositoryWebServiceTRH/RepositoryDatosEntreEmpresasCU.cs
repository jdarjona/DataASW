using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryWebServiceTRH
{
    class RepositoryDatosEntreEmpresasCU : RespositoryBase
    {
        public RepositoryDatosEntreEmpresasCU(HostWebService hostRespositorio) : base(hostRespositorio)
        {
        }

        public bool getDatosPCInterno(string codProducto,string almacen, out decimal precio, out decimal inventario) {
            try
            {
                precio = 0;
                inventario = 0;
                
                if (Context.contextDatosEntreEmpresas.GetInventarioCosteUnitario(codProducto, almacen, ref precio, ref inventario))
                {
                    return true;
                }
                else
                {                   
                    throw new Exception("Metodo getDatosPCInterno: Devuelve false al consultar el metodo GetInventarioCosteUnitario con parametros: " +Environment.NewLine
                        + "CodProducto: " +codProducto + Environment.NewLine 
                        + "Almacen: " +almacen+ Environment.NewLine
                        + "precio: "+ precio + Environment.NewLine
                        + "inventario: " + inventario + Environment.NewLine
                        );
                }
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException(@"codProducto/almacen", "El parametro 'codProducto/almacen' no pueden vernir vacios");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("{0} mensaje: {1}", "[Metodo getDatosPCInterno]", ex.Message), ex.InnerException);
            }
        }
    }
}
