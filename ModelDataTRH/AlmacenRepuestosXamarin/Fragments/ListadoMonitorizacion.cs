using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using AlmacenRepuestosXamarin.Data;
using AlmacenRepuestosXamarin.Adapter;
using ModelDataTRH;


using Android.Support.V7.App;
using AlmacenRepuestosXamarin.Model;
using System;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;

using AlmacenRepuestosXamarin.Activities;

using Android.Support.V4.View;
using Java.Lang;
using AlmacenRepuestosXamarin.Clases;
using System.Threading.Tasks;
using static Android.Widget.AdapterView;
using Android.Content;
using ZXing.Mobile;
using AlmacenRepuestosXamarin.Activities;


namespace AlmacenRepuestosXamarin.Fragments
{


    public class ListadoMonitorizacion : Android.Support.V4.App.Fragment, ViewPager.IOnPageChangeListener
    {
        protected class PedidoFireBase
        {
            public string codPedido { get; set; }
            public int estado { get; set; }
            public string descripcion { get; set; }
        }

        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        public static int tabSeleccionado = 7;
        private View view;
        private int ordenSeleccionado = -1;
        //private ListView listViewMonitorizacion;
        //private LinearLayout progressLayout;
        //private AccesoDatos datos;
        //private AdapterMonitoriaion adapterMonitorizacion;
        //AdapterSpinner<Empresas> adapterEmpresas;
        //private List<vListadoPedidosMonitorizacion> listMonitorizacion;
        //FirebaseClient _client;
        //FirebaseCache<PedidoFireBase> cacheFireBase;
        //private Spinner spinnerEmpresa ;
        LinearLayout progressLayout;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            view = inflater.Inflate(Resource.Layout.fragment_sample, null);

            MobileBarcodeScanner.Initialize(this.Activity.Application);
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            this.mViewPager.AddOnPageChangeListener(this);
            mViewPager.Adapter = new SamplePagerAdapter(this.Activity,mViewPager);
            mSlidingTabScrollView.ViewPager = mViewPager;
            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarLista);
            progressLayout.Visibility = ViewStates.Gone;
        }
        

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            inflater.Inflate(Resource.Menu.actionbar_main, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //mViewPager.Adapter = new SamplePagerAdapter(this.Activity, mViewPager, item,tabSeleccionado);

            SamplePagerAdapter adapter = (SamplePagerAdapter)mViewPager.Adapter;
            switch (item.ToString())
            {
                case "Ordenar por Empleado":
                    if (ordenSeleccionado == item.ItemId)
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderBy(o => o.inicialesComercial).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderBy(o => o.inicialesComercial).ToList();
                      //  adapter.listMonitorizacion = adapter.listMonitorizacion.OrderBy(o => o.inicialesComercial).ToList();
                        ordenSeleccionado = -1;

                    }
                    else
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderByDescending(o => o.inicialesComercial).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderByDescending(o => o.inicialesComercial).ToList();
                    //    adapter.listMonitorizacion = adapter.listMonitorizacion.OrderByDescending(o => o.inicialesComercial).ToList();
                        ordenSeleccionado = item.ItemId;
                    }

                    break;
                case "Ordenar por Estado":
                    if (ordenSeleccionado == item.ItemId)
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderBy(o => o.Estado).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderBy(o => o.Estado).ToList();
                   //     adapter.listMonitorizacion = adapter.listMonitorizacion.OrderBy(o => o.Estado).ToList();
                        ordenSeleccionado = -1;
                    }
                    else
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderByDescending(o => o.Estado).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderByDescending(o => o.Estado).ToList();
                    //    adapter.listMonitorizacion = adapter.listMonitorizacion.OrderByDescending(o => o.Estado).ToList();
                        ordenSeleccionado = item.ItemId;
                    }

                    break;
                case "Ordenar por Pedido":
                    if (ordenSeleccionado == item.ItemId)
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderBy(o => o.codigoPedido).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderBy(o => o.codigoPedido).ToList();
                      //  adapter.listMonitorizacion = adapter.listMonitorizacion.OrderBy(o => o.codigoPedido).ToList();
                        ordenSeleccionado = -1;
                    }
                    else
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderByDescending(o => o.codigoPedido).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderByDescending(o => o.codigoPedido).ToList();
                       // adapter.listMonitorizacion = adapter.listMonitorizacion.OrderByDescending(o => o.codigoPedido).ToList();
                        ordenSeleccionado = item.ItemId;
                    }

                    break;
                case "Ordenar por Fecha":
                    if (ordenSeleccionado == item.ItemId)
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                     //   adapter.listMonitorizacion = adapter.listMonitorizacion.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                        ordenSeleccionado = -1;
                    }
                    else
                    {
                        adapter.listMonitorizacionSevilla = adapter.listMonitorizacionSevilla.OrderByDescending(o => o.Fecha_Carga_Requerida).ToList();
                        adapter.listMonitorizacionLiege = adapter.listMonitorizacionLiege.OrderByDescending(o => o.Fecha_Carga_Requerida).ToList();
                       // adapter.listMonitorizacion = adapter.listMonitorizacion.OrderByDescending(o => o.Fecha_Carga_Requerida).ToList();
                        ordenSeleccionado = item.ItemId;
                    }

                    break;
                case "Foto":

                    launchScaner();
                    break;
                default:
                    break;
            }
            mViewPager.Adapter.NotifyDataSetChanged();
            return base.OnOptionsItemSelected(item);
        }

        //private void OnItemMessage(FirebaseEvent<PedidoFireBase> message)
        //{

        //    if (message.EventType == FirebaseEventType.InsertOrUpdate)
        //    {
        //        this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, message.Object.descripcion, ToastLength.Short).Show());
        //    }
        //}

        public override void OnResume()
        {
            base.OnResume();
        }

        public void OnPageScrollStateChanged(int state)
        {

        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            tabSeleccionado = position;
        }

        public void OnPageSelected(int position)
        {
            tabSeleccionado = position;
        }

        private async Task launchScaner()
        {


            var scanner = new MobileBarcodeScanner();
            Button flashButton;
            View zxingOverlay;

            scanner.UseCustomOverlay = true;

            //Inflate our custom overlay from a resource layout
            zxingOverlay = LayoutInflater.FromContext(this.Activity).Inflate(Resource.Layout.OverlayReadBarCode, null);

            //Find the button from our resource layout and wire up the click event
            flashButton = zxingOverlay.FindViewById<Button>(Resource.Id.buttonZxingFlash);
            flashButton.Click += (sender, e) => scanner.ToggleTorch();

            //Set our custom overlay
            scanner.CustomOverlay = zxingOverlay;

            //Start scanning!
            var result = await scanner.Scan();


            if (result != null)
            {

                Toast.MakeText(this.Activity, result.Text, ToastLength.Long);

                var activityFotosPedidos = new Intent(this.Activity, typeof(TakeFotoActivity));
                Bundle bundle = activityFotosPedidos.Extras;
                activityFotosPedidos.PutExtra("codPedido", result.Text);
                var empresa = SamplePagerAdapter.pagSeleccionada == 0 ? Monitorizacion.empresaLiege : Monitorizacion.empresaLiege;
                activityFotosPedidos.PutExtra("empresa", empresa);

                this.StartActivity(activityFotosPedidos);


            }
            else
            {

                this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, "Cancelado por el usuario", ToastLength.Short).Show());
            }




        }


        //NUEVO TAB

        public class SamplePagerAdapter : PagerAdapter, ViewPager.IOnPageChangeListener
        {
            
            public  List<vListadoPedidosMonitorizacion> listMonitorizacionSevilla { get; set; }
            public List<vListadoPedidosMonitorizacion> listMonitorizacionLiege { get; set; }
           // public List<vListadoPedidosMonitorizacion> listMonitorizacion { get; set; }

            private AdapterMonitoriaion adapterMonitorizacionLieja { get; set; }
            private AdapterMonitoriaion adapterMonitorizacionSevilla { get; set; }
            private ListView monitorizacionListViewLieja;
            private ListView monitorizacionListViewSevilla;
            private List<string> items = new List<string>();
            private ViewPager _mViewPager;
            private AppCompatActivity context;
            private  List<vListadoPedidosMonitorizacion> list;
            
            public static int pagSeleccionada;
            //private const string sevilla = " TRH Sevilla ";
            //private cont string liege = " TRH Liege ";
            private int _tabSeleccionado= 7;
            
            LinearLayout progressLayout;


            public SamplePagerAdapter(Activity context, ViewPager mViewPager) : base()
            {
                items.Add(Monitorizacion.empresaSevilla);
                items.Add(Monitorizacion.empresaLiege);
                this.context = (AppCompatActivity) context;
                updateListadosMonitorizacion();
                _mViewPager = mViewPager;
                this._mViewPager.AddOnPageChangeListener(this);
                
            }

            public SamplePagerAdapter(Activity context, ViewPager mViewPager, IMenuItem order, int tabSeleccionado) : this(context, mViewPager)
            {
                updateListadosMonitorizacion();
                //items.Add(sevilla);
                //items.Add(liege);
                this.context = (AppCompatActivity)context;
                _tabSeleccionado = tabSeleccionado;
                //_mViewPager = mViewPager;
                //this._mViewPager.AddOnPageChangeListener(this);
                
               // this.order = order;
               // ordenarListados(order);
            }

          

         

            public override int Count
            {
                get { return items.Count; }
            }

            
            public AdapterMonitoriaion getListaMonitorizacion(string empresa)
            {

                AdapterMonitoriaion adapterMonitorizacion= null;
               
                if (empresa.Equals(Monitorizacion.empresaSevilla))
                {

                    listMonitorizacionSevilla = listMonitorizacionSevilla.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                     adapterMonitorizacion = new AdapterMonitoriaion(this.context, listMonitorizacionSevilla);
                   

                }

                else if (empresa.Equals(Monitorizacion.empresaLiege))
                {

                    listMonitorizacionLiege = listMonitorizacionLiege.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                    adapterMonitorizacion = new AdapterMonitoriaion(this.context, listMonitorizacionLiege);
              
                }
                
                //this.context.RunOnUiThread(() => adapterMonitorizacion.NotifyDataSetChanged());
               

                return adapterMonitorizacion;

            }


            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                
                
                return view == obj;
                
            }
            public override int GetItemPosition(Java.Lang.Object objectValue)
            {
                return base.GetItemPosition(objectValue);
            }
            public override void NotifyDataSetChanged()
            {
                switch (pagSeleccionada)
                {

                    case 0:
                        adapterMonitorizacionSevilla = new AdapterMonitoriaion(this.context, listMonitorizacionSevilla);
                        this.context.RunOnUiThread(() => adapterMonitorizacionSevilla.NotifyDataSetChanged());
                        monitorizacionListViewSevilla.Adapter = adapterMonitorizacionSevilla;
                        break;
                    case 1:
                        adapterMonitorizacionLieja = new AdapterMonitoriaion(this.context, listMonitorizacionLiege);
                        this.context.RunOnUiThread(() => adapterMonitorizacionLieja.NotifyDataSetChanged());
                        monitorizacionListViewLieja.Adapter = adapterMonitorizacionLieja;

                        break;

                    default:
                        break;
                }
                
               
                
               
                base.NotifyDataSetChanged();
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                int tabSeleccionado = position;
                SlidingTabsFragment stf = new SlidingTabsFragment();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.ListaMonitorizacionLayout, container, false);
               
                switch (position)
                {

                    case 0:
                        monitorizacionListViewSevilla = view.FindViewById<ListView>(Resource.Id.listViewMonitorizacion);

                        monitorizacionListViewSevilla.Adapter = getListaMonitorizacion(items[position].ToString());
                        monitorizacionListViewSevilla.ItemClick += OnClickItemList;

                        string numCamionesRuta = string.Empty;
                        string numTmRuta = string.Empty;
                        string numTmPrevistas = string.Empty;
                        string numTmCarga = string.Empty;
                        numCamionesRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Count().ToString("N0");
                        numTmRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");
                        numTmCarga = listMonitorizacionSevilla.Where(q => q.Estado == 4 || q.Estado == 3 || q.Estado == 5).Sum(q => q.pesoKg / 1000).ToString("N0");
                        numTmPrevistas = listMonitorizacionSevilla.Where(q => q.Fecha_Carga_Requerida.Value.ToLocalTime() <= DateTime.Today).Sum(q => q.pesoKg / 1000).ToString("N0");

                        AppCompatActivity activity = (AppCompatActivity)this.context;
                        activity.SupportActionBar.Title = string.Format(@"{0} Tm enviadas de {1} hoy", numTmRuta, numTmPrevistas);
                        activity.SupportActionBar.Subtitle = string.Format(@"{0} Tm en carga", numTmCarga);
                        break;
                    case 1:
                        monitorizacionListViewLieja = view.FindViewById<ListView>(Resource.Id.listViewMonitorizacion);

                        monitorizacionListViewLieja.Adapter = getListaMonitorizacion(items[position].ToString());
                        monitorizacionListViewLieja.ItemClick += OnClickItemList;

                        break;

                    default:
                        break;
                }
                container.AddView(view);
                
                return view;
            }

            private void OnClickItemList(object sender, ItemClickEventArgs e)
            {
                Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.DetallePedidoVenta();

                string codPedido = ((AdapterMonitoriaion)((ListView)sender).Adapter).list[e.Position].Cod__Agrupacion_Pedido;
                Bundle bundle = new Bundle();
                bundle.PutString("codPedido", codPedido);
                fragment.Arguments = bundle;

                context.SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .AddToBackStack("ListadoMonitorizacion")
                        .Commit();
            }

            

        private async void  getListadoLiege()
            {
                listMonitorizacionLiege = await Monitorizacion.getListMonitorizacion(Monitorizacion.empresaLiege);

                
            }

            private async void getListadoSevilla()
            {
                listMonitorizacionSevilla = await Monitorizacion.getListMonitorizacion(Monitorizacion.empresaSevilla);
            }

            public string GetHeaderTitle(int position)
            {
       
                return items[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }



            public void OnPageScrollStateChanged(int state)
            {
            }

            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
               // pagSeleccionada = position;
            }

            public void OnPageSelected(int position)
            {
                string numCamionesRuta = string.Empty;
                string numTmRuta = string.Empty;
                string numTmPrevistas= string.Empty;
                string numTmCarga = string.Empty;
                pagSeleccionada = position;
                AppCompatActivity activity = (AppCompatActivity)this.context;
                if (position == 0)
                {
                    

                    numCamionesRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Count().ToString("N0");
                    numTmRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");
                    numTmCarga = listMonitorizacionSevilla.Where(q => q.Estado == 4 || q.Estado == 3 || q.Estado == 5).Sum(q => q.pesoKg / 1000).ToString("N0");
                    numTmPrevistas = listMonitorizacionSevilla.Where(q => q.Fecha_Carga_Requerida.Value.ToLocalTime()<=DateTime.Today).Sum(q => q.pesoKg / 1000).ToString("N0");

                }
                else if (position == 1)
                {
                    
                    numCamionesRuta = listMonitorizacionLiege.Where(q => q.Estado == 6).Count().ToString("N0");
                    numTmRuta = listMonitorizacionLiege.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");
                    numTmCarga = listMonitorizacionLiege.Where(q => q.Estado ==4 || q.Estado==3 || q.Estado==5).Sum(q => q.pesoKg / 1000).ToString("N0");
                    numTmPrevistas = listMonitorizacionLiege.Where(q => (q.Fecha_Carga_Requerida.HasValue?q.Fecha_Carga_Requerida.Value.ToLocalTime():DateTime.Today.AddDays(1)) <= DateTime.Today).Sum(q => q.pesoKg / 1000).ToString("N0");

                }
                else
                {

                }
                activity.SupportActionBar.Title = string.Format(@"{0} Tm enviadas de {1} hoy", numTmRuta, numTmPrevistas);
                activity.SupportActionBar.Subtitle = string.Format(@"{0} Tm en carga", numTmCarga);
            }

            public  void updateListadosMonitorizacion() {
                getListadoLiege();
                getListadoSevilla(); 
            }
        }

      

    }
}