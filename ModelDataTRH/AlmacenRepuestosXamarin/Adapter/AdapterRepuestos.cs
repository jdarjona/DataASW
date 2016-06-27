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
using AlmacenRepuestosXamarin.Model;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
using FortySevenDeg.SwipeListView;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterRepuestos : BaseAdapter<EntregaAlmacen>
    {
        Activity context;
        public List<EntregaAlmacen> list;

        public AdapterRepuestos(Activity _context, List<EntregaAlmacen> _list)
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

        public override EntregaAlmacen this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.listRowRepuestos, parent, false);

            EntregaAlmacen item = this[position];
            view.FindViewById<TextView>(Resource.Id.idRepuesto).Text = item.Cod_Producto.ToString();
            view.FindViewById<TextView>(Resource.Id.Description).Text = item.Descripcion_Producto;
            view.FindViewById<TextView>(Resource.Id.Cantidad).Text = item.Cantidad.ToString("N0");

            return view;
        }
    }
}