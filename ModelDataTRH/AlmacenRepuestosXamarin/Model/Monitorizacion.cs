using System.Collections.Generic;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Data;
using ModelDataTRH;

namespace AlmacenRepuestosXamarin.Model
{
    public static class Monitorizacion
    {
        private static List<vListadoPedidosMonitorizacion> listMonitorizacion = new List<vListadoPedidosMonitorizacion>();
        private static AccesoDatos datos;
        public static List<vListadoPedidosMonitorizacion> getListMonitorizacion()
        {
            return listMonitorizacion;
        }

        public static async Task updateListMonitorizacion()
        {
            datos = new AccesoDatos();
            listMonitorizacion = await datos.getListadoMonitorCarga();
        }
        public static async Task updateListMonitorizacion(string empresa)
        {
            datos = new AccesoDatos();
            listMonitorizacion = await datos.getListadoMonitorCarga(empresa);
        }
    }
}