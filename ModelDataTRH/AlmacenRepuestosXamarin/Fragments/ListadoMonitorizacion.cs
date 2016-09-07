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
using Android.Support.V4.View;
using Java.Lang;
using AlmacenRepuestosXamarin.Clases;
using System.Threading.Tasks;
using static Android.Widget.AdapterView;
using Android.Content;

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
            mViewPager.Adapter = new SamplePagerAdapter(this.Activity, mViewPager, item,tabSeleccionado);
            return base.OnOptionsItemSelected(item);
        }

        private void OnItemMessage(FirebaseEvent<PedidoFireBase> message)
        {

            if (message.EventType == FirebaseEventType.InsertOrUpdate)
            {
                this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, message.Object.descripcion, ToastLength.Short).Show());
            }
        }

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

        //NUEVO TAB

        public class SamplePagerAdapter : PagerAdapter, ViewPager.IOnPageChangeListener
        {
            
            private  List<vListadoPedidosMonitorizacion> listMonitorizacionSevilla { get; set; }
            private List<vListadoPedidosMonitorizacion> listMonitorizacionLiege { get; set; }
            private List<vListadoPedidosMonitorizacion> listMonitorizacion { get; set; }

            private AdapterMonitoriaion adapterMonitorizacion { get; set; }
            private ListView monitorizacionListView;
            private List<string> items = new List<string>();
            private ViewPager _mViewPager;
            private AppCompatActivity context;
            private  List<vListadoPedidosMonitorizacion> list;
            private IMenuItem order;
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
                
                this.order = order;
                ordenarListados(order);
            }

            private void ordenarListados(IMenuItem order)
            {
                switch (order.ToString())
                {
                    case "Ordenar por Empleado":
                        listMonitorizacionSevilla = listMonitorizacionSevilla.OrderBy(o => o.inicialesComercial).ToList();
                        listMonitorizacionLiege = listMonitorizacionLiege.OrderBy(o => o.inicialesComercial).ToList();
                        break;
                    case "Ordenar por Estado":
                        listMonitorizacionSevilla = listMonitorizacionSevilla.OrderBy(o => o.Estado).ToList();
                        listMonitorizacionLiege = listMonitorizacionLiege.OrderBy(o => o.Estado).ToList();
                        break;
                    case "Ordenar por Pedido":
                        listMonitorizacionSevilla = listMonitorizacionSevilla.OrderBy(o => o.codigoPedido).ToList();
                        listMonitorizacionLiege = listMonitorizacionLiege.OrderBy(o => o.codigoPedido).ToList();
                        break;
                    case "Ordenar por Fecha":
                        listMonitorizacionSevilla = listMonitorizacionSevilla.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                        listMonitorizacionLiege = listMonitorizacionLiege.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                        break;
                    default:
                        break;
                }
                
            }

           

            public override int Count
            {
                get { return items.Count; }
            }

            
            public AdapterMonitoriaion getListaMonitorizacion(string empresa)
            {
                

                if (empresa.Equals(Monitorizacion.empresaSevilla))
                {
                    
                    listMonitorizacion = listMonitorizacionSevilla;
  
                }

                else if (empresa.Equals(Monitorizacion.empresaLiege))
                {
                    
                    listMonitorizacion = listMonitorizacionLiege;
                }
               

                
                adapterMonitorizacion = new AdapterMonitoriaion(this.context, listMonitorizacion);
                this.context.RunOnUiThread(() => adapterMonitorizacion.NotifyDataSetChanged());
                AppCompatActivity activity = (AppCompatActivity)this.context;
                var numCamionesRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Count().ToString("N0");
                var numTmRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");

                activity.SupportActionBar.Title = string.Format(@"Camiones Ruta:{0}", numCamionesRuta);
                activity.SupportActionBar.Subtitle = string.Format(@"Tm envidas: {0}", numTmRuta);


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

                base.NotifyDataSetChanged();
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                int tabSeleccionado = position;
                SlidingTabsFragment stf = new SlidingTabsFragment();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.ListaMonitorizacionLayout, container, false);
                monitorizacionListView = view.FindViewById<ListView>(Resource.Id.listViewMonitorizacion);

                monitorizacionListView.Adapter = getListaMonitorizacion(items[position].ToString());
                monitorizacionListView.ItemClick += (sender, e) =>
                {

                    Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.DetallePedidoVenta();
                   
                    string codPedido = ((AdapterMonitoriaion)((ListView)sender).Adapter).list[e.Position].Cod__Agrupacion_Pedido;
                    Bundle bundle = new Bundle();
                    bundle.PutString("codPedido", codPedido);
                    fragment.Arguments = bundle;

                    //verDetallePedido();

                    //fragment = new SlidingTabsFragment();
                    //SupportFragmentManager.BeginTransaction()
                    //      .Replace(Resource.Id.content_frame, fragment)
                    //      .Commit();
                    //Android.Support.V4.App.Fragment _fragment = new Android.Support.V4.App.Fragment();
                    

                    context.SupportFragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .AddToBackStack("ListadoMonitorizacion")
                       .Commit();
                };
                
                container.AddView(view);
                
                return view;
            }

            private void verDetallePedido()
            {
                Fragment _fragment = new Fragment();
                Intent detalleMonitorizacion = new Intent(this.context, typeof(DetalleMonitorizacionActivity));
                _fragment.StartActivity(detalleMonitorizacion);
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
                pagSeleccionada = position;
            }

            public void OnPageSelected(int position)
            {
                string numCamionesRuta = string.Empty;
                string numTmRuta = string.Empty;
                AppCompatActivity activity = (AppCompatActivity)this.context;
                if (position == 0)
                {
                    

                    numCamionesRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Count().ToString("N0");
                    numTmRuta = listMonitorizacionSevilla.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");

                }
                else if (position == 1)
                {
                    
                    numCamionesRuta = listMonitorizacionLiege.Where(q => q.Estado == 6).Count().ToString("N0");
                    numTmRuta = listMonitorizacionLiege.Where(q => q.Estado == 6).Sum(q => q.pesoKg / 1000).ToString("N0");

                }
                else
                {

                }
                activity.SupportActionBar.Title = string.Format(@"Camiones Ruta:{0}", numCamionesRuta);
                activity.SupportActionBar.Subtitle = string.Format(@"Tm envidas: {0}", numTmRuta);
            }

            public  void updateListadosMonitorizacion() {
                getListadoLiege();
                getListadoSevilla(); 
            }
        }

            //FIN NUEVO TAB

        //public override void OnResume()
        //{
        //    listMonitorizacion = Monitorizacion.getListMonitorizacion();

        //    this.adapterMonitorizacion.list = listMonitorizacion;
        //    this.Activity.RunOnUiThread(() => adapterMonitorizacion.NotifyDataSetChanged());

        //    base.OnResume();

        //}
      
    }
}