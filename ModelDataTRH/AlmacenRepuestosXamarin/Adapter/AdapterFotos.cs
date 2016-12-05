using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AlmacenRepuestosXamarin.Activities;
using static AlmacenRepuestosXamarin.Resource;
using Android.Graphics;
using AlmacenRepuestosXamarin.Clases;

namespace AlmacenRepuestosXamarin.Adapter
{
   
    public class AdapterFotos : BaseAdapter<TakeFotoActivity.Fotos>
    {
        Activity context;
        
        public List<TakeFotoActivity.Fotos> list;
        ImageView imagen;

        public AdapterFotos(Activity _context, List<TakeFotoActivity.Fotos> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
            imagen = new ImageView(_context);
        }
       
        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override TakeFotoActivity.Fotos this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            
            View view = convertView;
            Android.Content.Res.Resources res = context.Resources;
            if (view == null)
                view = context.LayoutInflater.Inflate(Layout.RowListFotosPedidoVenta, parent, false);


            ImageView ImagenCamion = (ImageView)view.FindViewById<ImageView>(Resource.Id.ImagenCamion);
           

          
            TakeFotoActivity.Fotos item = this[position];

            view.FindViewById<TextView>(Resource.Id.DescripcionFoto).Text = item.Descripcion;
            view.FindViewById<TextView>(Resource.Id.DescripcionFoto).SetTypeface(null, TypefaceStyle.Bold);

            
            int height =(int) res.GetDimension(Resource.Dimension.imgCamion_height);//ImagenCamion.he;
            int width = context.Resources.DisplayMetrics.WidthPixels;
            
            ImagenCamion.RecycleBitmap();
            using (Bitmap bitmap = item.Fichero.Path.LoadAndResizeBitmap(width, height))
            {
                
                ImagenCamion.SetImageBitmap(bitmap);
            }
          

           

            return view;
        }

    }
}