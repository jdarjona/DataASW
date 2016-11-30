using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataTRH.Proyectos.Ventas
{
    public class Producto
    {
     public string keyField { get; set; } //  sample string 1 ,
     public string noField { get; set; } //  sample string 2 ,
     public string descriptionField { get; set; } //  sample string 3 ,
     public string search_DescriptionField { get; set; } //  sample string 4 ,
     public double long_BarraField { get; set; } // 5.0,
     public bool long_BarraFieldSpecified { get; set; } // true,
     public double trans_BarraField { get; set; } // 7.0,
     public bool trans_BarraFieldSpecified { get; set; } // true,
     public double nº_Barras_TransField { get; set; } // 9.0,
     public bool nº_Barras_TransFieldSpecified { get; set; } // true,
     public double nº_Barras_LongField { get; set; } // 11.0,
     public bool nº_Barras_LongFieldSpecified { get; set; } // true,
     public double kGxM2Field { get; set; } // 13.0,
     public bool kGxM2FieldSpecified { get; set; } // true,
     public double paños_x_PaqueteField { get; set; } // 15.0,
     public bool paños_x_PaqueteFieldSpecified { get; set; } // true,
     public double kgs_PaqueteField { get; set; } // 17.0,
     public bool kgs_PaqueteFieldSpecified { get; set; } // true,
     public double m2_PaqueteField { get; set; } // 19.0,
     public bool m2_PaqueteFieldSpecified { get; set; } // true,
     public string item_Category_CodeField { get; set; } //  sample string 21 ,
     public string product_Group_CodeField { get; set; } //  sample string 22 ,
     public DateTime last_Date_ModifiedField { get; set; } //  2016-10-26T11{ get; set; } //59{ get; set; } //05.1893639+02{ get; set; } //00 ,
     public bool last_Date_ModifiedFieldSpecified { get; set; } // true,
     public string sales_Unit_of_MeasureField { get; set; } //  sample string 25 ,
     public string grupoProductoDescripcionField { get; set; } //  sample string 26 ,
     public int replenishment_SystemField { get; set; } // 0,
     public bool replenishment_SystemFieldSpecified { get; set; } // true,
     public int paquetes_por_CamiónField { get; set; } // 28,
     public bool paquetes_por_CamiónFieldSpecified { get; set; } // true,
     public double stockDisponibleField { get; set; } // 30.0,
     public bool stockDisponibleFieldSpecified { get; set; } // true,
     public string paisField { get; set; } //  sample string 32 ,
     public string almacenField { get; set; } //  sample string 33 ,
     public double altura_PaqueteField { get; set; }
     public bool seleccionado { get; set; }
     public double cantidadSeleccionada { get; set; }
    }
}
