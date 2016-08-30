using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AlmacenRepuestosXamarin.Clases
{
    public class SinopticoFabrica
    {
        public string maquina { get; set; }
        public string EstadoMaquina { get; set; }
        public string RecursoMaquina { get; set; }
        public string RendimientoMaquina { get; set; }
        public string ProductoMaquina { get; set; }
    }
}