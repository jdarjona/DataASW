using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ModelDataTRH.Ventas;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "Ventas", LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/TRH", ScreenOrientation = ScreenOrientation.Portrait)]
    public class VentasActivity : Activity
    {
        private ImageButton btnNuevaOferta;
        private ImageButton btnListadoOfertas;
        private ImageButton btnListadoPedidos;
        private ImageButton btnListadoProductos;
        private View view;
        public static Oferta nuevaOferta = new Oferta();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MenuVentasLayout);

            btnNuevaOferta = FindViewById<ImageButton>(Resource.Id.nuevaOferta);
            btnListadoOfertas = FindViewById<ImageButton>(Resource.Id.listadoOfertas);
            btnListadoPedidos = FindViewById<ImageButton>(Resource.Id.listadoPedidos);
            btnListadoProductos = FindViewById<ImageButton>(Resource.Id.listadoProductos);

            btnNuevaOferta.Click += delegate {
                InicioNuevaOferta(this.view, LoginActivity.comercial);
            };
            btnListadoOfertas.Click += delegate {
                getListadoOfertas(this.view, LoginActivity.comercial);
            };
            btnListadoPedidos.Click += delegate {
                getListadoPedidos(this.view, LoginActivity.comercial);
            };
            btnListadoProductos.Click += delegate {
                getListadoProductos(this.view);
            };
            
        }


        private void InicioNuevaOferta(object view, string comercial)
        {
            var nuevaOferta = new Intent(this, typeof(NuevaOfertaActivity));
            StartActivity(nuevaOferta);
            //Toast.MakeText(this, "InicioNuevaOferta", ToastLength.Long).Show();
        }

        private void getListadoOfertas(View view, string comercial)
        {
            Toast.MakeText(this, "getListadoOfertas", ToastLength.Long).Show();
        }

        private void getListadoPedidos(View view, string comercial)
        {
            Toast.MakeText(this, "getListadoPedidos", ToastLength.Long).Show();
        }

        private void getListadoProductos(View view)
        {
            Toast.MakeText(this, "getListadoProductos", ToastLength.Long).Show();
        }


    }
}