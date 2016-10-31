using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ModernHttpClient;
using Newtonsoft.Json;
using RepositoryWebServiceTRH.EmpleadoContext;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
using ModelDataTRH;
using RepositoryWebServiceTRH.PedidoVentasContext;
using Android.Net.Wifi;
using AlmacenRepuestosXamarin.Activities;
using AlmacenRepuestosXamarin.Helpers;
using ModelDataTRH.Proyectos.Trazabilidad_Generico;
using static AlmacenRepuestosXamarin.Clases.SlidingTabsFragment;
using AlmacenRepuestosXamarin.Fragments;
using ModelDataTRH.Proyectos.Ventas;
using System.Threading;
using Newtonsoft.Json.Linq;
using ModelDataTRH.Ventas;

namespace AlmacenRepuestosXamarin.Data
{
    public  class AccesoDatos
    {
        static int ip = 0;
        static string SSID = string.Empty;
        static bool conexionWifi = false;
        public static string UrlToken = string.Empty;
        public static List<Cliente> listaClientes;
        public static List<Almacen> listaAlmacenes;
        public static List<Contacto> listaContactos;
        public static List<DireccionEnvio> listaDireccionesEnvio;
        public static List<Pedido> listaPedidos;
        public static List<Oferta> listaOfertas;
        public static List<Producto> listaProductos =  new List<Producto>();
        // public static List<Producto> listaProductos;

        // private const string webBase = @"http://intranet.trh-be.com/WSTRH/";
        // private const string webBase = @"http://192.168.1.2/WSTRH/";
        public  static string empre=string.Empty;
        private  HttpClient client = new HttpClient(new NativeMessageHandler());
        public static Oferta nuevaOferta = new Oferta();

        public AccesoDatos()
        {
            getDatosRedWifi();
            client = initClient();
            

        }

        public async Task initGetListados(string empresa)
        {
            listaClientes =await  getClientes();
            listaAlmacenes = await getAlmacenes();
            listaContactos = await getContactos();
            listaDireccionesEnvio = await getDireccionEnvio();
            listaPedidos = await getListadoPedidos();
            //listaProductos = await getListadoProductos();
            Producto p = new Producto();
            p.search_DescriptionField = "Q2012 Q2";
            p.stockDisponibleField = 30;
            p.paquetes_por_CamiónField = 15;
            listaProductos.Add(p);
            p = new Producto();
            p.search_DescriptionField = "Q2010 Q2";
            p.stockDisponibleField = 24;
            p.paquetes_por_CamiónField = 14;
            listaProductos.Add(p);
            //listaOfertas = await getListadoOfertas();
            //getProductos();
        }


        #region VENTAS
        public async Task<List<Cliente>> getClientes()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/Clientes");

                if (response.IsSuccessStatusCode)
                {
                   var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Cliente>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getClientes() ", ex.InnerException);
            }

            return null;
        } 

        public async Task<List<Almacen>> getAlmacenes()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/Almacenes");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Almacen>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getAlmacenes() ", ex.InnerException);
            }

            return null;
        }


        public async Task<List<Contacto>> getContactos()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/Contactos");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Contacto>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getContactos() ", ex.InnerException);
            }

            return null;
        }

        public async Task<List<DireccionEnvio>> getDireccionEnvio()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/DireccionesEnvio");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<DireccionEnvio>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getDireccionEnvio() ", ex.InnerException);
            }

            return null;
        }

        public async Task<List<Pedido>> getListadoPedidos()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/Pedidos");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Pedido>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getListadoPedidos() ", ex.InnerException);
            }

            return null;
        }

        public async Task<List<Producto>> getListadoProductos()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/Productos");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Producto>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getListadoPedidos() ", ex.InnerException);
            }

            return null;
        }
        public async Task<List<Oferta>> getListadoOfertas()
        {
            try
            {
                client = initClient();
                client.DefaultRequestHeaders.Add("Authorization", LoginActivity.token.ToString());
                var response = await client.GetAsync(@"api/Ofertas");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Oferta>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada en metodo getListadoOfertas() ", ex.InnerException);
            }

            return null;
        }
        #endregion
        public HttpClient initClient() {
            client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(getDatosConexionEmpresa(Preferencias.getEmpresaSevilla()))
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

            return client;
        }

        public void getDatosRedWifi()
        {
            //OBTENER DATOS WIFI O LOCAL
            WifiManager wifiManager = (WifiManager)Application.Context.GetSystemService(Service.WifiService);
            ip = wifiManager.ConnectionInfo.IpAddress;
            SSID = wifiManager.ConnectionInfo.SSID.Replace('"', ' ').Trim(); 
            conexionWifi = wifiManager.IsWifiEnabled;
            //  Toast.MakeText(this, "Conexión Wifi: " + conexionWifi + " IP: " + ip + " SSID: " + SSID, ToastLength.Short).Show();
            //FIN OBTENER WIFI O LACAL
        }

        public  string getDatosConexionEmpresa(string empresa)
        {
            string resultado = "no se puede establecer conexión";

            var lista = Preferencias.listSSIDLiege;


            if (empresa.Equals("Liege"))
            {
                if (conexionWifi && Preferencias.listSSIDLiege.Contains(SSID))
                {
                    resultado = Helpers.Preferencias.getUrlLocalLieja();//= @"http://192.168.1.2/WSTRH/";
                    UrlToken = @"http://192.168.1.2/WSTRH/Token";
                }
                else
                {
                    resultado = Helpers.Preferencias.getUrlPublicaLieja();
                    UrlToken = @"http://intranet.trh-be.com/WSTRH/Token";
                }
            }

            if (empresa.Equals("Sevilla"))
            {
                
                if (conexionWifi && Preferencias.listSSIDSevilla.ToArray().Contains(SSID))
                {
                    resultado = Helpers.Preferencias.getUrlLocalSevilla();
                    UrlToken = @"http://192.168.1.2/WSTRH/Token";
                }
                else
                {
                    resultado = Helpers.Preferencias.getUrlPublicaSevilla();
                    UrlToken = @"http://intranet.trh-es.com/WSTRH/Token";
                }
            }

            return resultado;
        }


        public  async Task<List<Empleados>> getEmpleados()
        {
            try
            {
                client = initClient();

                var response = await client.GetAsync(@"api/Empleado");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<Empleados>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);
            }
            
            return null;
        }

        

        public async Task<EntregaAlmacen> addRepuesto(string codEmpleado,string codRepuesto) {

            try
            {
                client = initClient();
                //var json = JsonConvert.SerializeObject(item);
                var contentPost = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                string url = string.Format(@"api/EntregaAlmacen?codRepusto={0}&codEmpleado={1}", codRepuesto, codEmpleado);
                var response = await client.PostAsync(url, contentPost);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<EntregaAlmacen>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excepcion no controlada", ex.InnerException);
            }

            return null;

        }

        public async Task<EntregaAlmacen> updateRepuesto( EntregaAlmacen repuesto) {


            try
            {

                client = initClient();
                var contentPost = new StringContent("", Encoding.UTF8, "application/json");
                string url = string.Format(@"api/EntregaAlmacen?key={0}&cantidad={1}&destino={2}&maquina={3}"
                                                      , repuesto.Cod_Producto,repuesto.Cantidad,repuesto.Destino,repuesto.Maquina);
                var response = await client.PutAsync(url, contentPost);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    repuesto = JsonConvert.DeserializeObject<EntregaAlmacen>(content);

                    return repuesto;
                  
                }
               
            }
            catch (Exception ex)
            {

                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);


            }

            return null;
        }

        public async Task<bool> deleteRepuesto(string key) {

            client = initClient();
            string url = string.Format(@"api/EntregaAlmacen?key={0}", key);
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Empleados>>(content);
                return true;
            }
            return false;
        }

        public async Task<string> registerEntrega(string codEmpleado) {


            string codDocumento = string.Empty;
            try
            {

                client = initClient();
                var contentPost = new StringContent("", Encoding.UTF8, "application/json");
                string url = string.Format(@"api/EntregaAlmacen?codEmpleado={0}",  codEmpleado);
                var response = await client.PutAsync(url, contentPost);
                if (response.IsSuccessStatusCode)
                {
                   var content = await response.Content.ReadAsStringAsync();

                    codDocumento = JsonConvert.DeserializeObject<string>(content);

                }
                return codDocumento;
            }
            catch (Exception ex)
            {

                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);
               
            }
           

        }

        public async Task<string> getAlbaranEntreEmpresas(string codDocumento) {


            try
            {


                client = initClient();
                string url = string.Format(@"api/EntregaAlmacen?codDocumento={0}", codDocumento);
                string nombreFichero=string.Empty;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    nombreFichero = JsonConvert.DeserializeObject<string>(content);
                    

                }

                url = string.Format(@"PDF/{0}", nombreFichero);
                byte[] bytes =  await client.GetByteArrayAsync(url);

                var localPath =string.Format(@"{0}/{1}", global::Android.OS.Environment.ExternalStorageDirectory.Path , nombreFichero);

                File.WriteAllBytes(localPath, bytes);

                return localPath;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);
            }

            return string.Empty;

        }

        
            public async Task<List<vListadoPedidosMonitorizacion>> getListadoMonitorCarga()
        {
            try
            {
                client = initClient();
                var response = await client.GetAsync(@"api/ListadoMonitorizacion");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<vListadoPedidosMonitorizacion>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);
            }

            return null;
        }

        

        public async Task<List<vListadoPedidosMonitorizacion>> getListadoMonitorCarga(String empresa)
        {
            empre = empresa;
            try
            {
                if (empresa.Equals(" TRH Liege "))
                {
                    client = new HttpClient(new NativeMessageHandler())
                    {
                        // BaseAddress = new Uri(webBase)
                        BaseAddress = new Uri(getDatosConexionEmpresa(Preferencias.getEmpresaLiege()))
                    };

                }
                else {
                    client = new HttpClient(new NativeMessageHandler())
                    {
                        // BaseAddress = new Uri(webBase)
                        BaseAddress = new Uri(getDatosConexionEmpresa(Preferencias.getEmpresaSevilla()))
                    };

                }
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                //client = initClient();
                var response = client.GetAsync(@"api/ListadoMonitorizacion");
                if (response.Result.IsSuccessStatusCode)
                {
                    var content = await response.Result.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<vListadoPedidosMonitorizacion>>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);
            }

            return null;
        }


        public HttpClient getCliente(string empresa) {
           
            if (empresa.Equals("Liege"))
            {
                client = new HttpClient(new NativeMessageHandler())
                {
                    // BaseAddress = new Uri(webBase)
                    BaseAddress = new Uri(getDatosConexionEmpresa(Preferencias.getEmpresaLiege()))
                };

            }
            else
            {
                client = new HttpClient(new NativeMessageHandler())
                {
                    // BaseAddress = new Uri(webBase)
                    BaseAddress = new Uri(getDatosConexionEmpresa(Preferencias.getEmpresaSevilla()))
                };

            }
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
            return client;
        }

        public async Task<Pedido> getPedidoVenta(string codPedido)
        {
            string empresa = String.Empty;
            try
            {
                int pag=ListadoMonitorizacion.SamplePagerAdapter.pagSeleccionada;
                if (pag==1) {
                    empresa = "Liege";
                }
                client = getCliente(empresa);
                string url = string.Format(@"api/Pedidos?codPedido={0}", codPedido);
                var response = await client.GetAsync(url);
                

                if (response.IsSuccessStatusCode && response!=null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Pedido>(content);
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido una excipcion no controlada", ex.InnerException);
            }

            return null;
        }
    }
}