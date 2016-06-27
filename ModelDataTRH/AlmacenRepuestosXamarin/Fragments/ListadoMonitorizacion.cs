using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AlmacenRepuestosXamarin.Data;
using AlmacenRepuestosXamarin.Adapter;
using ModelDataTRH;
using Firebase.Xamarin;
using Firebase.Xamarin.Streaming;
using OkHttp;
using Firebase.Xamarin.Query;

namespace AlmacenRepuestosXamarin.Fragments
{
   

    public class ListadoMonitorizacion : Android.Support.V4.App.Fragment
    {
        protected class PedidoFireBase
        {

            public string codPedido { get; set; }
            public int estado { get; set; }
            public string descripcion { get; set; }
        }

        private View view;
        private ListView listViewMonitorizacion;
        private LinearLayout progressLayout;
        private AccesoDatos datos=new AccesoDatos();
        private AdapterMonitoriaion adapterMonitorizacion;
        private List<vListadoPedidosMonitorizacion> listMonitorizacion;
        FirebaseClient<PedidoFireBase> _client;
        FirebaseCache<PedidoFireBase> cacheFireBase;

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

            var olderView =base.OnCreateView(inflater, container, savedInstanceState);
            view = inflater.Inflate(Resource.Layout.ListaMonitorizacionLayout, null);

            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressListaMonitorizacion);
            progressLayout.Visibility = ViewStates.Gone;

            listViewMonitorizacion = (ListView)view.FindViewById(Resource.Id.listViewMonitorizacion);

            fillListaMonitorizacion();

            listViewMonitorizacion.ItemClick += (sender, e) =>
            {
                //var activityDetalleRepuestoActivity = new Intent(this.Activity, typeof(detalleRepuestoActivity));
                //activityDetalleRepuestoActivity.PutExtra("idEntregaAlmacen", ManagerRepuestos.getRepuestos()[e.Position].Key);
                //StartActivity(activityDetalleRepuestoActivity);

                Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.DetallePedidoVenta();

                Bundle bundle = new Bundle();
                bundle.PutString("codPedido", listMonitorizacion[e.Position].Cod__Agrupacion_Pedido);
                fragment.Arguments = bundle;

                FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, fragment)
                    .AddToBackStack("ListadoMonitorizacion")
                   .Commit();
            };

            //_client = new FirebaseClient<PedidoFireBase>(@"https://flickering-fire-4088.firebaseio.com/", "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B");
           
            //StreamToken<PedidoFireBase> _token = _client.GetStreamToken(@"Pedidos/TRH Liege");
            //_token
            //    .Where(q=>q.EventType== FirebaseEventType.InsertOrUpdate)
            //    .Subscribe(OnItemMessage);

         
            //getFireBase();

            return view;
        }


        private async void getFireBase() {

            var items = await _client
                              .Child("Pedidos/TRH Liege")
                              .OrderByKey()
                              .LimitToFirst(2)
                              .OnceAsync<PedidoFireBase>();



        }

        private void OnItemMessage(FirebaseEvent<PedidoFireBase> message)
        {
            //this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, message.Object.descripcion, ToastLength.Short).Show());

           
           
                if (message.EventType == FirebaseEventType.InsertOrUpdate)
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, message.Object.descripcion, ToastLength.Short).Show());
                }
                else
                {
                    //Do Something else
                }
            
        }
        public void PopBackStack()
        {
        }
        private async void fillListaMonitorizacion() {


            progressLayout.Visibility = ViewStates.Visible;

            listMonitorizacion = await datos.getListadoMonitorCarga();

            
            adapterMonitorizacion = new AdapterMonitoriaion(this.Activity, listMonitorizacion);

            listViewMonitorizacion.Adapter = adapterMonitorizacion;

            progressLayout.Visibility = ViewStates.Gone;

            
        }
    }
}