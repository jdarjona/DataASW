using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDataTRH.Proyectos.Trazabilidad_Generico
{
    public class MaquinaFirebase
    {
        public enum Seccion
        {
            Trefileria,
            Trefirezado,
            Fino,
            Grueso
        }

        public Decimal CantidadObjectivo { get; set; }
        public Decimal CantidadProducidad { get; set; }
        public String CodOperario { get; set; }
        public String CodProducto { get; set; }
        public bool Conexion { get; set; }
        public string IdMaquina { get; set; }
        public bool Marcha { get; set; }
        public string Operario1 { get; set; }
        public string Operario2 { get; set; }
        public Decimal PesoObjetivo { get; set; }
        public Decimal PesoProducido { get; set; }
        public int Rendimiento { get; set; }
        public Seccion SeccionMaquina { get; set; }
        public string UnidadMedida { get; set; }


















    }
}
