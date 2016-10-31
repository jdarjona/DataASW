using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataTRH.Proyectos.Ventas
{
    public class Almacen
    {
        public string keyField{ get; set; }

        public int idAlmacenField{ get; set; }

        public bool idAlmacenFieldSpecified{ get; set; }

        public string nombreAlmacenField{ get; set; }

        public string idClienteField{ get; set; }

        public string idComercialField{ get; set; }

        public int activoField{ get; set; }

        public bool activoFieldSpecified{ get; set; }

        public int compradorField{ get; set; }

        public bool compradorFieldSpecified{ get; set; }

        public decimal objAnualField{ get; set; }

        public bool objAnualFieldSpecified{ get; set; }

        public decimal ventasRealesField{ get; set; }

        public bool ventasRealesFieldSpecified{ get; set; }
    }
}
