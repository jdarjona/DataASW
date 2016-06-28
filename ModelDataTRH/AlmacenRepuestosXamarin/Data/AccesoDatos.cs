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

namespace AlmacenRepuestosXamarin.Data
{
    public  class AccesoDatos
    {
       private const string webBase = @"http://intranet.trh-be.com/WSTRH/";
      // private const string webBase = @"http://192.168.1.2/WSTRH/";
        private  HttpClient client = new HttpClient(new NativeMessageHandler());


        public AccesoDatos()
        {
             client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(webBase)
            };


            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));

        }

        public  async Task<List<Empleados>> getEmpleados()
        {
            try
            {
                // Uri uri = new Uri(string.Format("{0}/{1}", webBase, @"api/Empleado"));
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                
              //  var respustosJson = JsonConvert.SerializeObject(repuestos);
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
               


                string url = string.Format(@"api/EntregaAlmacen?codDocumento={0}", codDocumento);
                string nombreFichero=string.Empty;
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    nombreFichero = JsonConvert.DeserializeObject<string>(content);
                    

                }

                url = string.Format(@"pdf/{0}", nombreFichero);
                byte[] bytes =  await client.GetByteArrayAsync(url);

                var localPath =string.Format(@"{0}/{1}", global::Android.OS.Environment.ExternalStorageDirectory.Path , nombreFichero);
                //byte[] bytes = new byte[response.Length];
                //response.Read(bytes, 0, (int)response.Length);


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
                // Uri uri = new Uri(string.Format("{0}/{1}", webBase, @"api/Empleado"));
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

        public async Task<Pedidos> getPedidoVenta(string codPedido)
        {
            try
            {
                // Uri uri = new Uri(string.Format("{0}/{1}", webBase, @"api/Empleado"));
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = string.Format(@"api/Pedidos?codPedido={0}", codPedido);
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<Pedidos>(content);
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