using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using InterfazNavision.NavisionLiege;

namespace RepositoryWebServicesTest.Tests1
{
    [TestFixture]
    class InterfazNavisionTest
    {

        private InterfazNavision.NavisionLiege.WSLiege wsNavision;
        [SetUp]
        public void inicializarInstacia() {
            wsNavision = new InterfazNavision.NavisionLiege.WSLiege();
        }

        [Test]
        public void getPrecioItemById() {

            string id="REP000001";
            decimal price = 0; 
            decimal inventory=0;
            wsNavision.getPrecioandInventarioByItem(id, ref price, ref inventory);

            Assert.Pass("Your first passing test");
        }
    }
}
