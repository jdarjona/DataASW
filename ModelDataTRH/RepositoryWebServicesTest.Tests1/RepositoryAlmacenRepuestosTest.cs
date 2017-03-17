using NUnit.Framework;
using RepositoryWebServiceTRH;

namespace RepositoryWebServicesTest.Tests1
{
    [TestFixture]
    public class RepositoryAlmacenRepuestosTest
    {

        private HostWebService hostWs;
        [SetUp]
        public void initContactosContext()
        {
            hostWs = new HostWebService(HostWebService.tipoIp.local, HostWebService.empresaWS.TRHLieja, HostWebService.tipoWebService.Page, "Empleados", @"TRH.LIEJA\administrador", "Paulagallardo2014");
        }

        [Test]
        public void insertarRepuesto(string codEmpleado, string codRepuesto, decimal cantidad, int maquina, int destino)
        {
            RepositoryWebServiceTRH.RepositoryEntragaAlmacenEpis almacenRepuestos = new RepositoryEntragaAlmacenEpis(hostWs) ;
            almacenRepuestos.insertRepuesto(codEmpleado, codRepuesto, cantidad, maquina, destino);
            Assert.Pass();

        }
        public void actualizarRepuesto(string codEmpleado, string codRepuesto, decimal cantidad, int maquina, int destino)
        {
            RepositoryWebServiceTRH.RepositoryEntragaAlmacenEpis almacenRepuestos = new RepositoryEntragaAlmacenEpis(hostWs);
            almacenRepuestos.updateRepuesto(codEmpleado, codRepuesto, cantidad, maquina, destino);
            Assert.Pass();
        }

        public void eliminarRepuesto(string codProducto, string codEmpleado)
        {
            RepositoryWebServiceTRH.RepositoryEntragaAlmacenEpis almacenRepuestos = new RepositoryEntragaAlmacenEpis(hostWs);
            almacenRepuestos.deleteRepuesto(codProducto, codEmpleado);
            Assert.Pass();
        }
        

    }
}
