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

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var olderView= base.OnCreateView(inflater, container, savedInstanceState);
            view = inflater.Inflate(Resource.Layout.DetallePedidoVentaLayout, null);

            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressDetallePedidoVenta);
            progressLayout.Visibility = ViewStates.Gone;

            fillDetallePedidoVenta();

            return view;
        }

        private async void fillDetallePedidoVenta()
        {
            if (AccesoDatos.estadoConexion())
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

                    activity.SupportActionBar.Title = pedidoVenta.sell_to_Customer_NameField;
                    var tmTotal = pedidoVenta.salesLinesField.Sum(q => q.cantidad_KGField / 1000).ToString("N0");
                    var importeTotal = pedidoVenta.salesLinesField.Sum(q => q.precioLineaTotalField).ToString("N2");

                    activity.SupportActionBar.Subtitle = string.Format("{0} Tm - {1} €", tmTotal, importeTotal);


                    listViewDetallePedidoVenta = (ListView)view.FindViewById(Resource.Id.listViewDetallePedidoVenta);
                    adapterDetallePedidoVenta = new AdapterDetallePedidoVenta(this.Activity, pedidoVenta.salesLinesField.ToList());
                    listViewDetallePedidoVenta.Adapter = adapterDetallePedidoVenta;
                    progressLayout.Visibility = ViewStates.Gone;
                }
                else
                {
                    progressLayout.Visibility = ViewStates.Gone;
                    Toast.MakeText(this.Activity, "Sin información del pedido seleccionado.", ToastLength.Short).Show();
                    FragmentManager.PopBackStack();
                }

            }
            else {
                Toast.MakeText(Application.Context, "SIN CONEXION", ToastLength.Long).Show();
            }

        }

        public void PopBackStack()
        {
        }
    }
}