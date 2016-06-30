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

        public static async void updateListMonitorizacion()
        {
            datos = new AccesoDatos();
            listMonitorizacion = await datos.getListadoMonitorCarga();
        }
    }
}