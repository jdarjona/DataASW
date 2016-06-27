using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AlmacenRepuestosXamarin.Fragments
{
    public class DetallePedidoVenta : Android.Support.V4.App.Fragment
    {

        private View view;
        private LinearLayout progressLayout;
        private AdapterDetallePedidoVenta adapterDetallePedidoVenta;
        private AccesoDatos datos = new AccesoDatos();
        private ListView listViewDetallePedidoVenta;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.HasOptionsMenu = true;
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var olderView= base.OnCreateView(inflater, container, savedInstanceState);
            view = inflater.Inflate(Resource.Layout.DetallePedidoVentaLayout, null);

            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressDetallePedidoVenta);
            progressLayout.Visibility = ViewStates.Gone;

            fillDetallePedidoVenta();

            return view;
        }

        private async void fillDetallePedidoVenta()
        {


            progressLayout.Visibility = ViewStates.Visible;
            string codPedido = string.Empty;
            Bundle bundle = this.Arguments;
            if (bundle != null)
            {
                codPedido = bundle.GetString("codPedido");
            }
            var pedidoVenta = await datos.getPedidoVenta(codPedido);

            listViewDetallePedidoVenta = (ListView)view.FindViewById(Resource.Id.listViewDetallePedidoVenta);
            adapterDetallePedidoVenta = new AdapterDetallePedidoVenta(this.Activity, pedidoVenta.SalesLines.ToList());

            listViewDetallePedidoVenta.Adapter = adapterDetallePedidoVenta;

            progressLayout.Visibility = ViewStates.Gone;


        }

        public void PopBackStack()
        {
        }
    }
}