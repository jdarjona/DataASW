using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
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

            ////OBTENER CONEXION WIFI O LOCAL
            //WifiManager wifiManager = (WifiManager)this.Context.GetSystemService(Service.WifiService);
            //int ip = wifiManager.ConnectionInfo.IpAddress;
            //string SSID = wifiManager.ConnectionInfo.SSID;
            //string conexionWifi = string.Empty;

            //if (wifiManager.IsWifiEnabled)
            //{
            //    conexionWifi = "Activada";
            //}
            //else
            //{
            //    conexionWifi = "Desactivada";
            //}
            //Toast.MakeText(this.Activity, "Conexión Wifi: "+conexionWifi+" IP: " + ip + " SSID: " + SSID, ToastLength.Short).Show();
            ////FIN OBTENER WIFI O LACAL
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

            if (pedidoVenta != null)
            {
                AppCompatActivity activity = (AppCompatActivity)this.Activity;

                activity.SupportActionBar.Title = pedidoVenta.Sell_to_Customer_Name;
                var tmTotal = pedidoVenta.SalesLines.Sum(q => q.Cantidad_KG / 1000).ToString("N0");
                var importeTotal = pedidoVenta.SalesLines.Sum(q => q.PrecioLineaTotal).ToString("N2");

                activity.SupportActionBar.Subtitle = string.Format("{0} Tm - {1} €",tmTotal,importeTotal);


                listViewDetallePedidoVenta = (ListView)view.FindViewById(Resource.Id.listViewDetallePedidoVenta);
                adapterDetallePedidoVenta = new AdapterDetallePedidoVenta(this.Activity, pedidoVenta.SalesLines.ToList());
                listViewDetallePedidoVenta.Adapter = adapterDetallePedidoVenta;
                progressLayout.Visibility = ViewStates.Gone;
            }
            else {
                progressLayout.Visibility = ViewStates.Gone;
                Toast.MakeText(this.Activity, "Sin información del pedido seleccionado.", ToastLength.Short).Show();
                FragmentManager.PopBackStack();
            }

            

        }

        public void PopBackStack()
        {
        }
    }
}