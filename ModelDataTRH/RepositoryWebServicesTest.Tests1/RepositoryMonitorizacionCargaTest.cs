using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryWebServicesTest.Tests1
{
    [TestFixture]
    public class RepositoryMonitorizacionCargaTest
    {
        [Test]
        public void Get_All_ReturnListadoMonitorizacion()
        {
            // TODO: Add your test code here

            RepositorySqlTRH.RepositoryMonitorizacionCarga rp = new RepositorySqlTRH.RepositoryMonitorizacionCarga();

            var listMonitorizacion = rp.GetAll();

           
           Assert.IsNotNull(listMonitorizacion);

            

            
        }
    }
}
