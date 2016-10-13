using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH;

namespace RepositoryWebServicesTest.Tests1
{
    [TestFixture]
    public class RepositoryContactosTest
    {
        private HostWebService hostWs;
        [SetUp]
        public void initContactosContext()
        {

            hostWs = new HostWebService(HostWebService.tipoIp.local, HostWebService.empresaWS.TRHLieja, HostWebService.tipoWebService.Page, "Empleados", @"TRH.LIEJA\administrador", "Paulagallardo2014");


        }

        [Test]
        public void Get_All_Contactos()
        {
            // TODO: Add your test code here

            RepositoryWebServiceTRH.RepositoryContactos repoContactos = new RepositoryContactos(hostWs);
            var contactos = repoContactos.GetAll();

            Assert.IsNotNull(contactos);
            
        }

        [Test]
        public void Get_All_Clientes()
        {
            // TODO: Add your test code here

            RepositoryWebServiceTRH.RepositoryClientes repoClientes = new RepositoryClientes(hostWs);
            var clientes = repoClientes.GetAll();

            Assert.IsNotNull(clientes);
        }

        [Test]
        public void Get_All_CodigoPostales()
        {
            // TODO: Add your test code here
            RepositoryWebServiceTRH.RepositoryCodigoPostales repo = new RepositoryCodigoPostales(hostWs);

            var codigoPostales = repo.GetAll();
            Assert.IsNotNull(codigoPostales);
        }

        [Test]
        public void Get_All_AlmacenCliente()
        {
            // TODO: Add your test code here
            RepositoryWebServiceTRH.RepositoryAlmacenClientes repo = new RepositoryAlmacenClientes(hostWs);

            var almacenesClientes = repo.GetAll();
            Assert.IsNotNull(almacenesClientes);
        }

        [Test]
        public void Get_All_DireccionEnvio()
        {
            // TODO: Add your test code here
            RepositoryWebServiceTRH.RepositoryDireccionEnvio repo = new RepositoryDireccionEnvio(hostWs);
            var direccionesEnvio = repo.GetAll();
            Assert.IsNotNull(direccionesEnvio);
        }

        [Test]
        public void Get_All_PedidoListado()
        {
            // TODO: Add your test code here
            RepositoryWebServiceTRH.RepositoryPedidoListado repo = new RepositoryPedidoListado(hostWs);
            var pedidoListado = repo.GetAll();
            Assert.IsNotNull(pedidoListado);
        }
    }
}
