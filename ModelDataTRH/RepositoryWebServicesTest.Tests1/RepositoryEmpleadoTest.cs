using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryWebServiceTRH;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
//using AlmacenRepuestosXamarin.Model;

namespace RepositoryWebServicesTest.Tests1
{
    [TestFixture]
    public class RepositoryEmpleadoTest
    {
        private HostWebService hostWs;

        [SetUp]
        public void initEmpeladoContext() {

            hostWs = new HostWebService(HostWebService.tipoIp.local, HostWebService.empresaWS.TRHLieja, HostWebService.tipoWebService.Page, "Empleados", @"TRH.LIEJA\administrador", "Paulagallardo2014");


        }

        [Test]
        public void Get_All_ReturnEmpleados()
        {


            RepositoryWebServiceTRH.RepositoryEmpleado repoEmpleado = new RepositoryEmpleado(hostWs);

            var empleados = repoEmpleado.GetAll();

            Assert.AreNotEqual(null, empleados);
            // TODO: Add your test code here

        }
        [Test]
        public void Get_ById_ReturnEmpleado()
        {

            
            RepositoryWebServiceTRH.RepositoryEmpleado repoEmpleado = new RepositoryEmpleado(hostWs) ;
            string id = "E0001";
            var empleado=repoEmpleado.Get(id);

            Assert.AreNotEqual(null, empleado, string.Format(@"No hemos encontrado el empleado {0}", id));
            // TODO: Add your test code here
          
        }

        [Test]
        public void Get_ParametroNull_ReturnException() {

            RepositoryWebServiceTRH.RepositoryEmpleado repoEmpleado = new RepositoryEmpleado(hostWs);
            string id = null;
            //var empleado = repoEmpleado.Get(id);

            Assert.Throws<ArgumentNullException>(() => repoEmpleado.Get(id));
           // Assert.AreNotEqual(null, empleado, string.Format(@"Hemos encontrado el empleado {0}", id));

        }

        [Test]
        public void UpdateRange_ReturnOk()
        {

            RepositoryWebServiceTRH.RepositoryEntragaAlmacenEpis repoEntregaEPI = new RepositoryEntragaAlmacenEpis(hostWs);
            List<RepositoryWebServiceTRH.EntregaAlmacenEpisContext.EntregaAlmacen> listEntrega = new List<RepositoryWebServiceTRH.EntregaAlmacenEpisContext.EntregaAlmacen>();

            RepositoryWebServiceTRH.EntregaAlmacenEpisContext.EntregaAlmacen item = new RepositoryWebServiceTRH.EntregaAlmacenEpisContext.EntregaAlmacen();

            item.Cod_Empleado = "E0007";
            item.Cod_Producto = "REP002143";
            repoEntregaEPI.Add(ref item);

            item.Cantidad = 1;

            listEntrega.Add(item);

            EntregaAlmacen[] repuestos = listEntrega.ToArray<EntregaAlmacen>();

            repoEntregaEPI.UpdateRange(ref repuestos);



            Assert.Pass();

        }


        [Test]
        public void createUpdateDeleteRepuesto() {
           
                RepositoryEntragaAlmacenEpis repoEntregaEPI = new RepositoryEntragaAlmacenEpis(hostWs);
                EntregaAlmacen nuevoItem = new EntregaAlmacen();
                nuevoItem.Cod_Almacen = "PATIO";
                nuevoItem.Cod_Empleado = "E0006";
                nuevoItem.Cod_Producto = "REP000001";

                //CREATE
                repoEntregaEPI.Add(ref nuevoItem);

                //UPDATE
                nuevoItem.Cantidad = 2;
                nuevoItem.Destino = Destino.Otros;
                repoEntregaEPI.Update(ref nuevoItem);

                //DELETE
                repoEntregaEPI.Remove(nuevoItem.Key);

                Assert.Pass();          

        }

        //[Test]
        //public void Register_ReturnOk()
        //{

        //    RepositoryWebServiceTRH.RepositoryEntragaAlmacenEpis repoEntregaEPI = new RepositoryEntragaAlmacenEpis(hostWs);
         

        //    repoEntregaEPI.register("E0021");



        //    Assert.Pass();

        //}

        //[Test]
        //public void Find_Querry_ReturnAny()
        //{

        //    RepositoryWebServiceTRH.RepositoryEmpleado repoEmpleado = new RepositoryEmpleado(hostWs);

        //    System.Linq.Expressions.Expression<Func<RepositoryWebServiceTRH.EmpleadoContext.Empleados, bool>> expr =q=>q.Name.StartsWith("Manuel");
        //    var empleado = repoEmpleado.Find(expr);


        //    Assert.AreNotEqual(null, empleado.ToList());

        //}
    }
}
