using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ModelDataTRH.Proyectos.Trazabilidad_Generico;

namespace AlmacenRepuestosXamarin.Model
{
    public static class TrazabilidadModelFirebase
    {
        public static List<MaquinaFirebase> listMaquinaFirebase = new List<MaquinaFirebase>();
        private static AccesoDatos datos;

        public static List<MaquinaFirebase> getListMaquinaFirebase()
        {
            return listMaquinaFirebase;
        }

        public static async Task updateListMaquinaFirebase()
        {
            datos = new AccesoDatos();
            listMaquinaFirebase = await datos.getListadoMaquinaFirebase();
        }
        public static async Task updateListMaquinaFirebase(string empresa)
        {
            datos = new AccesoDatos();
            listMaquinaFirebase = await datos.getListadoMaquinaFirebase(empresa);
        }

        public static async Task<List<MaquinaFirebase>> getListMaquinaFirebase(string empresa)
        {
            datos = new AccesoDatos();
            listMaquinaFirebase = await datos.getListadoMaquinaFirebase(empresa);

            return listMaquinaFirebase;
        }

    }
}