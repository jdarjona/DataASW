using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AlmacenRepuestosXamarin.Data;
using AlmacenRepuestosXamarin.Adapter;
using ModelDataTRH;
using Firebase.Xamarin;

using Android.Support.V7.App;
using AlmacenRepuestosXamarin.Model;
using System;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
using Firebase.Xamarin.Database.Streaming;
using AlmacenRepuestosXamarin.Activities;
using Firebase.Xamarin.Database;

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
        private AccesoDatos datos;
        private AdapterMonitoriaion adapterMonitorizacion;
        AdapterSpinner<Empresas> adapterEmpresas;
        private List<vListadoPedidosMonitorizacion> listMonitorizacion;
        FirebaseClient _client;
        FirebaseCache<PedidoFireBase> cacheFireBase;
        private Spinner spinnerEmpresa ;


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.HasOptionsMenu = true;

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment

            spinnerEmpresa = new Spinner(container.Context);
            var olderView =base.OnCreateView(inflater, container, savedInstanceState);
            view = inflater.Inflate(Resource.Layout.ListaMonitorizacionLayout, null);

            datos = new AccesoDatos();
            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressListaMonitorizacion);
            progressLayout.Visibility = ViewStates.Gone;

            listViewMonitorizacion = (ListView)view.FindViewById(Resource.Id.listViewMonitorizacion);

            fillListaMonitorizacion();

            listViewMonitorizacion.ItemClick += (sender, e) =>
            {

                Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.DetallePedidoVenta();

                Bundle bundle = new Bundle();
                bundle.PutString("codPedido", listMonitorizacion[e.Position].Cod__Agrupacion_Pedido);
                fragment.Arguments = bundle;

                FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, fragment)
                    .AddToBackStack("ListadoMonitorizacion")
                   .Commit();
            };

            

            return view;
        }

        private async void spinnerEmpresa_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            spinner.SetSelection(e.Position);
            string empresa = "Sevilla";
            if (e.Position == 1) {
                empresa = "Liege";
            }
            await Monitorizacion.updateListMonitorizacion(empresa);
            listMonitorizacion = Monitorizacion.getListMonitorizacion();
            adapterMonitorizacion = new AdapterMonitoriaion(this.Activity, listMonitorizacion);

            listViewMonitorizacion.Adapter = adapterMonitorizacion;
        }


        //private async void getFireBase() {

        //    var items = await _client
        //                      .Child("Pedidos/TRH Liege")
        //                      .OrderByKey()
        //                      .LimitToFirst(2)
        //                      .OnceAsync<PedidoFireBase>();



        //}

        private void OnItemMessage(FirebaseEvent<PedidoFireBase> message)
        {
           
                if (message.EventType == FirebaseEventType.InsertOrUpdate)
                {
                    this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, message.Object.descripcion, ToastLength.Short).Show());
                }
                else
                {
                    //Do Something else
                }
            
        }

        public override void OnResume()
        {
            listMonitorizacion = Monitorizacion.getListMonitorizacion();
            
             this.adapterMonitorizacion.list= listMonitorizacion;
            this.Activity.RunOnUiThread(() => adapterMonitorizacion.NotifyDataSetChanged());
           
            base.OnResume();
            
        }
        private  void fillListaMonitorizacion() {


            progressLayout.Visibility = ViewStates.Visible;
            var s = (Empresas[])Enum.GetValues(typeof(Empresas));

            adapterEmpresas = new AdapterSpinner<Empresas>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, s);
            adapterEmpresas.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinnerEmpresa = (Spinner) view.FindViewById(Resource.Id.Empresas);
            spinnerEmpresa.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerEmpresa_ItemSelected);
            spinnerEmpresa.Adapter = adapterEmpresas;
            spinnerEmpresa.Focusable = true;
            spinnerEmpresa.FocusableInTouchMode = true;
            spinnerEmpresa.RequestFocus(FocusSearchDirection.Up);
            //listMonitorizacion = await datos.getListadoMonitorCarga();

            // Monitorizacion.updateListMonitorizacion();
            listMonitorizacion = Monitorizacion.getListMonitorizacion();
            adapterMonitorizacion = new AdapterMonitoriaion(this.Activity, listMonitorizacion);

            listViewMonitorizacion.Adapter = adapterMonitorizacion;

            AppCompatActivity activity = (AppCompatActivity)this.Activity;
            var numCamionesRuta = listMonitorizacion.Where(q => q.Estado == 6).Count().ToString("N0");
            var numTmRuta = listMonitorizacion.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");
           
            activity.SupportActionBar.Title = string.Format(@"Camiones Ruta:{0}", numCamionesRuta); 
            activity.SupportActionBar.Subtitle = string.Format(@"Tm envidas: {0}", numTmRuta);

            progressLayout.Visibility = ViewStates.Gone;

            
        }
    }
}