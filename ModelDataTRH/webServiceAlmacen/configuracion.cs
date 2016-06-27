using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
namespace wsTRH
{

    public class webServiceEmpresa {


        public string id;
        public bool activo;
        public string empresa;
        public string dominio;
        public string url;
        public string ipDominio;
        public string codMallaEstandar;
        public string codMallaEspecial;
        public string codMallaSegunda;
        public string codMallaCerramiento;
        public string codMallaSemiEstandar;
    }

    public static class configuracion
    {


        
        public static List<webServiceEmpresa> getDatosEmpresa() {

            string pathFileJson = HttpContext.Current.Server.MapPath("~/config.json");
            var fileJson = File.OpenText(pathFileJson);
            string jsonString = fileJson.ReadToEnd();

            
            List<webServiceEmpresa> empresa = JsonConvert.DeserializeObject<List<webServiceEmpresa>>(jsonString);

            return empresa.Where(c => c.activo == true).ToList();
          
            

        }
    }
}