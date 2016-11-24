using System.Collections.Generic;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Data;
using Android.Graphics;
using ModelDataTRH;
using static AlmacenRepuestosXamarin.Resource;

namespace AlmacenRepuestosXamarin.Model
{
    public static class Monitorizacion
    {
        public static  string empresaSevilla = " TRH Sevilla ";
        public static  string empresaLiege = " TRH Liege ";

        public static List<vListadoPedidosMonitorizacion> listMonitorizacion = new List<vListadoPedidosMonitorizacion>();
        public static List<vListadoPedidosMonitorizacion> listMonitorizacionSevilla = new List<vListadoPedidosMonitorizacion>();
        public static List<vListadoPedidosMonitorizacion> listMonitorizacionLieja = new List<vListadoPedidosMonitorizacion>();

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

        public static async Task<List<vListadoPedidosMonitorizacion>> getListMonitorizacion(string empresa)
        {
            datos = new AccesoDatos();
            listMonitorizacion = await datos.getListadoMonitorCarga(empresa);


            if (empresa == empresaSevilla)
            {
                listMonitorizacionSevilla.Clear();
                listMonitorizacionSevilla.AddRange(listMonitorizacion);
            } else if (empresa == empresaLiege) { 
                listMonitorizacionLieja.Clear();
                listMonitorizacionLieja.AddRange(listMonitorizacion);
            }
           

            return listMonitorizacion;
        }
        
        public static async Task<bool> upLoadImage(string codPedido, string empresa, Java.IO.File Fichero, bool small)
        {

            
            datos = new AccesoDatos();
            return await datos.UploadBitmapAsync(empresa,Fichero, codPedido,small);

        }
    }
}