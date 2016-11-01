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
using ModelDataTRH.Proyectos.Ventas;


namespace AlmacenRepuestosXamarin.Adapter
{
    
     public class AdapterDetallePedidoVenta : BaseAdapter<Sales_Order_Subform_ws>
    {
        Activity context;
        public List<Sales_Order_Subform_ws> list;

        public AdapterDetallePedidoVenta(Activity _context, List<Sales_Order_Subform_ws> _list)
            : base()
        {
            this.context = _context;
            this.list = _list.Where(q=>!q.aliasField.Equals("PT")).ToList();
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Sales_Order_Subform_ws this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowDetallePedidoVenta, parent, false);

            Sales_Order_Subform_ws item = this[position];
            view.FindViewById<TextView>(Resource.Id.CodProducto).Text = item.aliasField;
            view.FindViewById<TextView>(Resource.Id.Descripcion).Text = item.descriptionField;
            view.FindViewById<TextView>(Resource.Id.Peso).Text = string.Format(@"{0} {1}",item.cantidad_KGField.ToString("N2"),"Kg");
            view.FindViewById<TextView>(Resource.Id.Paquetes).Text = item.cantidad_PAQField.ToString("N0");


            return view;
        }

     
    }
}