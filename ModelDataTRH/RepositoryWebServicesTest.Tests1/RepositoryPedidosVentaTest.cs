using NUnit.Framework;
using System;
using RepositoryWebServiceTRH;

namespace RepositoryWebServicesTest.Tests1
{
    [TestFixture]
    public class RepositoryPedidosVentaTest
    {
        private HostWebService hostWs;

        [SetUp]
        public void initEmpeladoContext()
        {

            hostWs = new HostWebService(HostWebService.tipoIp.local, HostWebService.empresaWS.TRHLieja, HostWebService.tipoWebService.Page, "Empleados", @"TRH.LIEJA\administrador", "Paulagallardo2014");


        }

        [Test]
        public void Get_Id_ReturnPedido()
        {


            RepositoryWebServiceTRH.RepositoryPedidosVenta repoPedidoVenta = new RepositoryPedidosVenta(hostWs);

            var pedido = repoPedidoVenta.Get(@"CDE-V16/0392");

            Assert.AreNotEqual(null, pedido);
            // TODO: Add your test code here

        }
    }
}