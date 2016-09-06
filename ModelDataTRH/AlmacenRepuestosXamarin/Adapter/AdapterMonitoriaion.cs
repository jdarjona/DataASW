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
using ModelDataTRH;
namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterMonitoriaion : BaseAdapter<vListadoPedidosMonitorizacion>
    {
        Activity context;
        public List<vListadoPedidosMonitorizacion> list;

        public AdapterMonitoriaion(Activity _context, List<vListadoPedidosMonitorizacion> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

       

        public override vListadoPedidosMonitorizacion this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListMonitorizacion, parent, false);

                vListadoPedidosMonitorizacion item = this[position];
                view.FindViewById<TextView>(Resource.Id.CodPedido).Text = string.Format(@"{0}-{1}", item.Cod__Agrupacion_Pedido, item.Cod__Pedido_Transporte);
                view.FindViewById<TextView>(Resource.Id.Cliente).Text = item.Nombre_Agencia;
                view.FindViewById<TextView>(Resource.Id.Fecha).Text = item.Fecha_Carga_Requerida.HasValue ? item.Fecha_Carga_Requerida.Value.ToString("d") : string.Empty;
                view.FindViewById<TextView>(Resource.Id.Estado).Text = item.estadoDescripcion;
                view.FindViewById<TextView>(Resource.Id.Comercial).Text = item.inicialesComercial;

                view.FindViewById<TextView>(Resource.Id.Estado).SetBackgroundResource(ColorEstado(item.Estado));
            
            return view;
        }

        private int ColorEstado(int estado) {

            switch (estado)
            {
                case 0:
                    return Android.Resource.Color.HoloRedLight; //'gridEstadoRojoE'
                    break;
                case 1:
                    return Android.Resource.Color.HoloRedLight; //'gridEstadoRojoE'
                    break;
                case 2:
                    return Android.Resource.Color.HoloOrangeLight; // 'gridEstadoNaranjaE'
                    break;
                case 3:
                    return Android.Resource.Color.White; //'gridFilaE'
                    break;
                case 4:
                    return Android.Resource.Color.White; //'gridFilaE'
                    break;
                case 5:
                    return Android.Resource.Color.HoloGreenLight; //'gridEstadoVerdeE'
                    break;
                case 6:
                    return Android.Resource.Color.HoloGreenLight; //'gridEstadoVerdeE'
                    break;

                default:
                    return Android.Resource.Color.White; //'gridFilaE'
                    break;
            }

        }
    }
}