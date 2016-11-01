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
    public class VentasActivity : BaseActivity
    {
        private ImageButton btnNuevaOferta;
        private ImageButton btnListadoOfertas;
        private ImageButton btnListadoPedidos;
        private ImageButton btnListadoProductos;
        private View view;
        public static Oferta nuevaOferta = new Oferta();

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.MenuVentasLayout;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.MenuVentasLayout);

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
            var nuevaOferta = new Intent(this, typeof(NuevaOfertaClientesActivity));
            StartActivity(nuevaOferta);

            //Toast.MakeText(this, "InicioNuevaOferta", ToastLength.Long).Show();
        }

        private void getListadoOfertas(View view, string comercial)
        {
            Intent myIntent = new Intent(this, typeof(ListadoOfertasActivity));
            myIntent.PutExtra("key", LoginActivity.comercial);
            StartActivity(myIntent);
        }

        private void getListadoPedidos(View view, string comercial)
        {
            Intent myIntent = new Intent(this, typeof(ListadoPedidosActivity));
            myIntent.PutExtra("key", LoginActivity.comercial);
            StartActivity(myIntent);
        }

        private void getListadoProductos(View view)
        {
            Toast.MakeText(this, "getListadoProductos", ToastLength.Long).Show();
        }


    }
}