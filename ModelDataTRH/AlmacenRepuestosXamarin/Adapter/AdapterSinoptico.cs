using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Clases;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ModelDataTRH;
namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterSinoptico : BaseAdapter<SinopticoFabrica>
    {
        Activity context;
        Fragment fragment;
        public List<SinopticoFabrica> list;
        private Context context1;
        private List<SinopticoFabrica> listSinoptico;

        public AdapterSinoptico(Activity _context, List<SinopticoFabrica> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }
        public AdapterSinoptico(Fragment fragment, List<SinopticoFabrica> _list)
            : base()
        {
            this.fragment = fragment;
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

        public override SinopticoFabrica this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListSinoptico, parent, false);

                SinopticoFabrica item = this[position];
                view.FindViewById<TextView>(Resource.Id.Maquina).Text = item.maquina;
                view.FindViewById<TextView>(Resource.Id.EstadoMaquina).Text = item.EstadoMaquina;
                view.FindViewById<TextView>(Resource.Id.RecursoMaquina).Text = item.RecursoMaquina;
                view.FindViewById<TextView>(Resource.Id.RendimientoMaquina).Text = item.RendimientoMaquina;
                view.FindViewById<TextView>(Resource.Id.ProductoMaquina).Text = item.ProductoMaquina;


            return view;
        }
      
    }
}