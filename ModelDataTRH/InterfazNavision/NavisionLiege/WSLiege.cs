using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH;

namespace InterfazNavision.NavisionLiege
{
    public  class WSLiege
    {

        // public static HostWebService hostWS = new HostWebService(HostWebService.tipoIp.publica, HostWebService.empresaWS.TRHLieja, HostWebService.tipoWebService.Page, "NuevaListaProductos", @"TRH.LIEJA\administrador", "Paulagallardo2014");
        public  HostWebService hostWS;

        public WSLiege()
        {
            //obtenerDatosConexionLiege();
            hostWS = new HostWebService(HostWebService.tipoIp.local, HostWebService.empresaWS.TRHSevilla, HostWebService.tipoWebService.Page, "NuevaListaProductos", @"TRHSEVILLA0\administrador", "Paulagallardo2014");

        }

        public  void obtenerDatosConexionLiege()
        {
            //Lectura de datos liege del archivo config
        }


        #region ITEM-PRODUCTOS        

        public  void getPrecioandInventarioByItem(string id,ref decimal price, ref decimal inventory) {
            
            RepositoryWebServiceTRH.RepositoryItem producto = new RepositoryItem(hostWS);
            var query= producto.Get(id);

            price = query.Unit_Cost;
            inventory = 15;
           
        }

        public   RepositoryItem getAllItem()
        {

            RepositoryWebServiceTRH.RepositoryItem producto = new RepositoryItem(hostWS);
            producto.GetAll();

            return producto;
        }
        #endregion

        #region EMPLEADOS
        public  RepositoryEmpleado getEmpleados(string id)
        {

            RepositoryWebServiceTRH.RepositoryEmpleado producto = new RepositoryEmpleado(hostWS);
            producto.Get(id);

            return producto;
        }

        public  RepositoryEmpleado getAllEmpleados()
        {

            RepositoryWebServiceTRH.RepositoryEmpleado producto = new RepositoryEmpleado(hostWS);
            producto.GetAll();

            return producto;
        }
        #endregion
    }
}
