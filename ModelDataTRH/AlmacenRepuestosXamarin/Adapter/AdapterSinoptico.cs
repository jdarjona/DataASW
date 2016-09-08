using System;
using System.Collections.Generic;
using AlmacenRepuestosXamarin.Clases;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ModelDataTRH.Proyectos.Trazabilidad_Generico;
using static AlmacenRepuestosXamarin.Resource;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterSinoptico : BaseAdapter<MaquinaFirebase>
    {
        Activity context;
        Fragment fragment;
        public List<MaquinaFirebase> list;
        ImageView imagen;

        public AdapterSinoptico(Activity _context, List<MaquinaFirebase> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
            imagen = new ImageView(_context);
        }
        public AdapterSinoptico(Fragment fragment, List<MaquinaFirebase> _list)
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

        public override MaquinaFirebase this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

             int cantidadObjetivo=0;
            int cantidadProducida = 0;
            View view = convertView;
            int rendimiento = 0;
            if (view == null)
                view = context.LayoutInflater.Inflate(Layout.RowListSinoptico, parent, false);

            
            ImageView imagenEstado =(ImageView) view.FindViewById<ImageView>(Resource.Id.EstadoMaquina);
            ImageView ImagenMaquinaMarcha = (ImageView)view.FindViewById<ImageView>(Resource.Id.ImagenMaquinaMarcha);
            ImageView ImagenMaquina = (ImageView)view.FindViewById<ImageView>(Resource.Id.ImagenMaquina);

            ProgressBar barraRendimiento = (ProgressBar)view.FindViewById<ProgressBar>(Resource.Id.RendimientoMaquina);
            MaquinaFirebase item = this[position];

            view.FindViewById<TextView>(Resource.Id.Maquina).Text = item.IdMaquina;
            view.FindViewById<TextView>(Resource.Id.Maquina).SetTypeface(null, TypefaceStyle.Bold);

            bool estadoMaquina = item.Marcha;
            bool conexion = item.Conexion;

            switch (item.IdMaquina)
            {
                case "M1":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.Fino);
                    break;
                case "M2":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.Fino);
                    break;
                case "M3":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.grueso);
                    break;
                case "M4":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.grueso);
                    break;
                case "M6":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.grueso);
                    break;
                case "M7":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.Fino);
                    break;

                case "T1":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t1);
                    break;
                case "T2":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t2);
                    break;
                case "T3":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t3);
                    break;
                case "T4":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t4);
                    break;
                case "T5":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t5);
                    break;
                case "T6":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t6);
                    break;
                case "T7":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t7);
                    break;
                case "T8":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t8);
                    break;
                case "T9":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t9);
                    break;
                case "T10":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t10);
                    break;
                case "T11":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t11);
                    break;
                case "T12":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t12);
                    break;
                case "T13":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t1);
                    break;
                case "T14":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t2);
                    break;
                case "T15":
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.t3);
                    break;
                
                default:
                    ImagenMaquina.SetBackgroundResource(Resource.Drawable.enderezado);
                    break;
            }

            if (estadoMaquina)
            {
                imagenEstado.SetBackgroundResource(Resource.Drawable.marcha);
            }
            else 
            {
                imagenEstado.SetBackgroundResource(Resource.Drawable.marchaNo);
            }
            if (conexion)
            {
                ImagenMaquinaMarcha.SetBackgroundResource(Resource.Drawable.conexionOk);
            }
            else
            {
                ImagenMaquinaMarcha.SetBackgroundResource(Resource.Drawable.conectadaNo);
            }
            view.FindViewById<TextView>(Resource.Id.RecursoMaquina).Text = item.Operario1;
            view.FindViewById<TextView>(Resource.Id.RecursoMaquina).SetTypeface(null, TypefaceStyle.Bold);

            barraRendimiento.Progress = item.Rendimiento;
            view.FindViewById<TextView>(Resource.Id.textoRendimiento).Text = item.Rendimiento + @" %";
            if (item.Rendimiento > 76)
            {
                view.FindViewById<TextView>(Resource.Id.textoRendimiento).SetTextColor(Android.Graphics.Color.Green);
            }
            else
            {
                view.FindViewById<TextView>(Resource.Id.textoRendimiento).SetTextColor(Android.Graphics.Color.Red);
            }
            
            view.FindViewById<TextView>(Resource.Id.textoRendimiento).SetTypeface(null, TypefaceStyle.Bold);

            view.FindViewById<TextView>(Resource.Id.SeccionMaquina).Text = item.SeccionMaquina.ToString();
            view.FindViewById<TextView>(Resource.Id.ProductoMaquina).Text = item.CodProducto;
            view.FindViewById<TextView>(Resource.Id.ProductoMaquina).SetTypeface(null, TypefaceStyle.Bold);
            view.FindViewById<TextView>(Resource.Id.CantidadProducida).Text = (Convert.ToInt32(item.CantidadProducidad)).ToString("#,##0");

            view.FindViewById<TextView>(Resource.Id.CantidadObjetivo).Text = (Convert.ToInt32(item.CantidadObjectivo)).ToString("#,##0");
            view.FindViewById<TextView>(Resource.Id.UnidadMedida).Text = item.UnidadMedida.ToString() + @"/";
            view.FindViewById<TextView>(Resource.Id.UnidadMedidaObjetivo).Text = item.UnidadMedida.ToString() + " Objetivo";
            
            return view;
        }
      
    }
}