using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH.EmpleadoContext;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
using RepositoryWebServiceTRH.AlmacenRepuestosContext;
using RepositoryWebServiceTRH.ItemContext;
using RepositoryWebServiceTRH.DatosEntreEmpresasContext;
using RepositoryWebServiceTRH.PedidoVentasContext;


namespace RepositoryWebServiceTRH
{
    class Context
    {
        public static Empleados_PortClient contextEmpleado { get; private set; }
        public static EntregaAlmacen_PortClient contextEntregaAlmacenEpis { get; private set; }
        public static AlmacenRepuestos_PortClient contextAlmacenesRepuestos { get; private set; }
        public static NuevaListaProductos_PortClient contextItem { get; private set; }
        public static DatosEntreEmpresas_PortClient contextDatosEntreEmpresas { get; private set; }
       // public static DatosEntreEmpresas_PortClient contextDatosEntreEmpresas { get; private set; }

            public static Pedidos_PortClient contextPedidosVenta { get; private set; }

        public static string usuario=@"TRHSEVILLA0\administrador";

        public static string pass="Paulagallardo2014";

        #region INICIALIZACION DE WS
        public  static void CreateContext(HostWebService hostWS)
        {
            BasicHttpBinding navisionWSBinding = new BasicHttpBinding();
            navisionWSBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            navisionWSBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            navisionWSBinding.MaxReceivedMessageSize = 2000971520;

            initEmpleadosPortCliente(navisionWSBinding, hostWS);
            initEntregaAlmacenEpisPortCliente(navisionWSBinding, hostWS);
            hostWS.tipoServicioWeb = HostWebService.tipoWebService.Codeunit;
            initAlmacenesClientesPortCliente(navisionWSBinding, hostWS);
            hostWS.tipoServicioWeb = HostWebService.tipoWebService.Page;
            initItemPortCliente(navisionWSBinding, hostWS);
            initPedidoVentasPortCliente(navisionWSBinding, hostWS);
        }
        private static BasicHttpBinding CreateBasicHttp()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            return binding;
        }
        #endregion
        
        #region INICIALIZACION DE PORT_CLIENTS
        private static void initEmpleadosPortCliente(BasicHttpBinding navisionWSBinding, HostWebService hostWs)
        {
           
            
            contextEmpleado = new Empleados_PortClient(navisionWSBinding, new EndpointAddress(string.Format(hostWs.urlHost, "Empleados", HostWebService.tipoWebService.Page.ToString())));
            contextEmpleado.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            contextEmpleado.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(hostWs.user, hostWs.password);
          


        }

        private static void initEntregaAlmacenEpisPortCliente(BasicHttpBinding navisionWSBinding, HostWebService hostWs)
        {
            contextEntregaAlmacenEpis = new EntregaAlmacen_PortClient(navisionWSBinding, new EndpointAddress(string.Format(hostWs.urlHost, "EntregaAlmacen", HostWebService.tipoWebService.Page.ToString())));
            contextEntregaAlmacenEpis.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            contextEntregaAlmacenEpis.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(hostWs.user, hostWs.password);
        }

        private static void initAlmacenesClientesPortCliente(BasicHttpBinding navisionWSBinding, HostWebService hostWs)
        {
            contextAlmacenesRepuestos = new AlmacenRepuestos_PortClient(navisionWSBinding, new EndpointAddress(string.Format(hostWs.urlHost, "AlmacenRepuestos", HostWebService.tipoWebService.Codeunit.ToString())));
            contextAlmacenesRepuestos.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            contextAlmacenesRepuestos.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(hostWs.user, hostWs.password);
        }
        private static void initItemPortCliente(BasicHttpBinding navisionWSBinding, HostWebService hostWs)
        {
            contextItem = new NuevaListaProductos_PortClient(navisionWSBinding, new EndpointAddress(string.Format(hostWs.urlHost, "NuevaListaProductos", HostWebService.tipoWebService.Page.ToString())));
            contextItem.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            contextItem.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(hostWs.user, hostWs.password);
        }

        private static void initPedidoVentasPortCliente(BasicHttpBinding navisionWSBinding, HostWebService hostWs)
        {
            contextPedidosVenta = new Pedidos_PortClient(navisionWSBinding, new EndpointAddress(string.Format(hostWs.urlHost, "Pedidos", HostWebService.tipoWebService.Page.ToString())));
            contextPedidosVenta.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            contextPedidosVenta.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(hostWs.user, hostWs.password);
        }

        private static void DatosEntreEmpresas_PortClient(BasicHttpBinding navisionWSBinding, HostWebService hostWs)
        {
            contextDatosEntreEmpresas = new DatosEntreEmpresas_PortClient(navisionWSBinding, new EndpointAddress(string.Format(hostWs.urlHost, "DatosEntreEmpresas", HostWebService.tipoWebService.Codeunit.ToString())));
            contextDatosEntreEmpresas.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            contextDatosEntreEmpresas.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential(hostWs.user, hostWs.password);
        }

        #endregion
    }

        #region OBTENCION DE URL
    public class HostWebService {

        public enum tipoWebService
        {
            Page
           , Codeunit
        }
        public enum tipoIp {
            publica,
            local
        }
        public enum empresaWS {
              TRHSevilla,
              TRHLieja
        }

        public tipoWebService tipoServicioWeb { get; set; }

        public string password { get; }
        public string user { get; }

        public string urlHost { get; }
        private string IpHost;
        private string empresaServicio;
        private string tipoWS = string.Empty;
         

        public HostWebService(tipoIp tipoIPHost, empresaWS empresaServicio, tipoWebService tipoWS, string entidadWS, string user, string password) {

            this.user = user;
            this.password = password;
            

            switch (tipoWS)
            {
                case tipoWebService.Page:
                  
                    this.tipoWS = "Page";
                    break;
                case tipoWebService.Codeunit:
                   
                    this.tipoWS = "Codeunit";
                    break;
               
            }

            switch (empresaServicio)
            {
                case empresaWS.TRHSevilla:
                    this.empresaServicio = "T.R.H.";
                   
                    break;
                case empresaWS.TRHLieja:
                    this.empresaServicio = "TRH Liege";
                    break;
                default:
                    break;
            }

            switch (tipoIPHost)
            {
                case tipoIp.publica:
                    this.IpHost = empresaServicio == empresaWS.TRHSevilla ? "intranet.trh-es.com" : "intranet.trh-be.com";
                    break;
                case tipoIp.local:
                    this.IpHost = "192.168.1.2";
                    break;
                default:
                    break;
            }
            urlHost = string.Format(@"http://{0}:7047/DynamicsNAV/WS/{1}/{2}/{3}",this.IpHost,this.empresaServicio,@"{1}",@"{0}");

           

        }


    }
    #endregion

}
