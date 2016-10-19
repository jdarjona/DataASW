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
using ModelDataTRH.Ventas;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterNuevaOferta : BaseAdapter<Cliente>
    {
        Activity context;
        public List<Cliente> list;

        public AdapterNuevaOferta(Activity _context, List<Cliente> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override Cliente this[int position]
        {
            get { return list[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListNuevaOferta, parent, false);

            Cliente item = this[position];
            view.FindViewById<TextView>(Resource.Id.txt1).Text = " ";
            view.FindViewById<TextView>(Resource.Id.txt2).Text = item.nameField.ToString();
            view.FindViewById<TextView>(Resource.Id.txt3).Text = " ";

            return view;
        }
    }
}