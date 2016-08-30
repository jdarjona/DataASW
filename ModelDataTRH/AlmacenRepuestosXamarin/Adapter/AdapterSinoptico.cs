using System.Collections.Generic;
using AlmacenRepuestosXamarin.Clases;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using static AlmacenRepuestosXamarin.Resource;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterSinoptico : BaseAdapter<SinopticoFabrica>
    {
        Activity context;
        Fragment fragment;
        public List<SinopticoFabrica> list;
        private Context context1;
        private List<SinopticoFabrica> listSinoptico;
        ImageView imagen;

        public AdapterSinoptico(Activity _context, List<SinopticoFabrica> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
            imagen = new ImageView(_context);
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
            int rendimiento = 0;
            if (view == null)
                view = context.LayoutInflater.Inflate(Layout.RowListSinoptico, parent, false);

            
            ImageView imagenEstado =(ImageView) view.FindViewById<ImageView>(Resource.Id.EstadoMaquina);
            ProgressBar barraRendimiento = (ProgressBar)view.FindViewById<ProgressBar>(Resource.Id.RendimientoMaquina);
            SinopticoFabrica item = this[position];

            view.FindViewById<TextView>(Resource.Id.Maquina).Text = item.maquina;
            view.FindViewById<TextView>(Resource.Id.Maquina).SetTypeface(null, TypefaceStyle.Bold);

            string estadoMaquina = item.EstadoMaquina;
            if (estadoMaquina.Equals("ON") ) {
                imagenEstado.SetBackgroundResource(Resource.Drawable.on);
            } else if (estadoMaquina.Equals("OFF") ) {
                imagenEstado.SetBackgroundResource(Resource.Drawable.off);
            }
               
            view.FindViewById<TextView>(Resource.Id.RecursoMaquina).Text = item.RecursoMaquina;
            view.FindViewById<TextView>(Resource.Id.RecursoMaquina).SetTypeface(null, TypefaceStyle.Bold);

            int.TryParse(item.RendimientoMaquina,out rendimiento);
            barraRendimiento.Progress = rendimiento;
            view.FindViewById<TextView>(Resource.Id.textoRendimiento).Text = item.RendimientoMaquina + @" %";
            if (rendimiento > 76)
            {
                view.FindViewById<TextView>(Resource.Id.textoRendimiento).SetTextColor(Android.Graphics.Color.Green);
            }
            else {
                view.FindViewById<TextView>(Resource.Id.textoRendimiento).SetTextColor(Android.Graphics.Color.Red);
            }
            
            view.FindViewById<TextView>(Resource.Id.textoRendimiento).SetTypeface(null, TypefaceStyle.Bold);

            view.FindViewById<TextView>(Resource.Id.ProductoMaquina).Text = item.ProductoMaquina;
            view.FindViewById<TextView>(Resource.Id.ProductoMaquina).SetTypeface(null, TypefaceStyle.Bold);


            return view;
        }
      
    }
}