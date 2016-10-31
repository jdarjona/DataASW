using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Clases;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ModelDataTRH.Proyectos.Ventas;
using ModelDataTRH.Ventas;
using static AlmacenRepuestosXamarin.Clases.SlidingTabsFragment;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "Nueva Oferta", LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/TRH", ScreenOrientation = ScreenOrientation.Portrait)]
    public class NuevaOfertaActivity : BaseActivity , ViewPager.IOnPageChangeListener
    {
        LinearLayout progressLayout;
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        List<string> data;
        private int tabSeleccionado;
        IMenuItem item;
        IMenu _menu;
        private Android.Support.V7.Widget.SearchView _searchView;
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.OpcionesNuevaOfertaCabeceraLayout;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            
            base.OnCreate(savedInstanceState);
            mSlidingTabScrollView = FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            
            mViewPager = FindViewById<ViewPager>(Resource.Id.viewpager);            
            this.mViewPager.AddOnPageChangeListener(this);
            data = new List<string>();
            data.Add(" Opciones ");
            data.Add(" Productos ");
            data.Add(" Carga ");            
            data.Add(" Precios ");
            data.Add(" Incidencias ");
            mViewPager.OffscreenPageLimit = data.Count()-1;
            mViewPager.Adapter = new SPagerAdapter(this, mViewPager,data);
            
            mSlidingTabScrollView.ViewPager = mViewPager;

        }

        public void OnPageScrollStateChanged(int state)
        {
            if (state == 0) {
                switch (tabSeleccionado)
                {
                    case 0:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Opciones_Oferta);
                        MenuInflater.Inflate(Resource.Menu.soloMenuCambioDestino, _menu);
                        item = _menu.FindItem(Resource.Id.cambiarDestino);
                        //addItem(item);
                        break;
                    case 1:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Listado_Productos);
                        MenuInflater.Inflate(Resource.Menu.menuNuevaOfertaViewpagerLayout, _menu);
                        item = _menu.FindItem(Resource.Id.action_search_viewpager);
                        addItem(item);


                        break;
                    case 2:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Carga);
                        MenuInflater.Inflate(Resource.Menu.buscador, _menu);
                        item = _menu.FindItem(Resource.Id.action_search);
                        addItem(item);
                        break;
                    case 3:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Precios);
                        break;
                    case 4:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Incidencias);
                        MenuInflater.Inflate(Resource.Menu.buscador, _menu);
                        item = _menu.FindItem(Resource.Id.action_search);
                        addItem(item);
                        break;
                    default:
                        break;
                }
            }
        }


        public void addItem(IMenuItem item) {
            SPagerAdapter adapter = (SPagerAdapter)(mViewPager.Adapter);
            var searchView = MenuItemCompat.GetActionView(item);
            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();
            _searchView.QueryTextChange += (s, e) => {
                this.RunOnUiThread(() => {

                    switch (tabSeleccionado)
                    {
                        case 1:
                            adapter.adapterListadoProductos.Filter.InvokeFilter(new Java.Lang.String(e.NewText.ToString()));
                            mViewPager.Adapter.NotifyDataSetChanged();
                            break;
                        default:
                            break;
                    }
                    //mViewPager.Adapter.Filter.InvokeFilter(new Java.Lang.String(e.NewText.ToString()));
                    mViewPager.Adapter.NotifyDataSetChanged();
                });
            };

            _searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _menu = menu;
            SupportActionBar.SetTitle(Resource.String.Opciones_Oferta);
            MenuInflater.Inflate(Resource.Menu.soloMenuCambioDestino, _menu);
            item = _menu.FindItem(Resource.Id.cambiarDestino);
            //MenuInflater.Inflate(Resource.Menu.buscador, _menu);

            //item = _menu.FindItem(Resource.Id.action_search);

            //var searchView = MenuItemCompat.GetActionView(item);
            //_searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();

            //_searchView.QueryTextChange += (s, e) => {


            //    this.RunOnUiThread(() => {

            //       // mViewPager.Adapter.Filter.InvokeFilter(new Java.Lang.String(e.NewText.ToString()));
            //        mViewPager.Adapter.NotifyDataSetChanged();

            //    });


            //};

            //_searchView.QueryTextSubmit += (s, e) =>
            //{
            //    // Handle enter/search button on keyboard here
            //    Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
            //    e.Handled = true;
            //};
            ////AppCompatActivity activity = (AppCompatActivity)this.Activity;

            ////MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(mViewPager.Adapter));

            return base.OnCreateOptionsMenu(_menu);
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {

        }

        public void OnPageSelected(int position)
        {
            tabSeleccionado = position;
        }



        public class SPagerAdapter : PagerAdapter , ViewPager.IOnPageChangeListener
        {
            private ViewPager _mViewPager;
            private Toolbar toolBar;
            public AdapterListadoProductos adapterListadoProductos;
            private AdapterNuevaOferta adapterNuevaOferta;
            private List<string> items = new List<string>();
            private Activity context;
            private ListView nuevaOfertaListview;
            private List<Cliente> listClientes = new List<Cliente>();
            private LinearLayout progressLayout;
            private IMenuItem item;
            private int tabSeleccionado=0;
            private RadioButton rbDobleDescargaSi;
            private RadioButton rbDobleDescargaNo;
            private RadioButton rbTipoTransporteSi;
            private RadioButton rbTipoTransporteNo;
            private RadioButton rbCamionGruaSi;
            private RadioButton rbCamionGruaNo;
            List<string> _data;
            public SPagerAdapter(Activity context, ViewPager mViewPager,List<string> data) : base()
            {
                _data = data;
                _mViewPager = mViewPager;
                this.context = context;               
                this._mViewPager.AddOnPageChangeListener(this);
            }



            public override int Count
            {
                get { return _data != null ? _data.Count : 0; }
            }


            public override int GetItemPosition(Java.Lang.Object objectValue)
            {
                return base.GetItemPosition(objectValue);
            }
            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                string title = string.Empty;
                SlidingTabsFragment stf;
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.nuevaOfertaOpcionesLayout, container, false);



                LayoutInflater inflater = LayoutInflater.From(container.Context);

                switch (position)
                {

                    case 0:
                        view = inflater.Inflate(Resource.Layout.nuevaOfertaOpcionesLayout, container, false);
                        break;

                    case 1:
                        view = inflater.Inflate(Resource.Layout.pagerItemOfertaSinHeader, container, false);

                        nuevaOfertaListview = view.FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);
                        progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);
                        
                        adapterListadoProductos = new AdapterListadoProductos(this.context, AccesoDatos.listaProductos);
                        nuevaOfertaListview.Adapter = adapterListadoProductos;
                        break;
                    case 2:
                        view = inflater.Inflate(Resource.Layout.pagerItemOfertaSinHeader, container, false);
                        nuevaOfertaListview = view.FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);
                        progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);
                        listClientes.Clear();
                        Cliente ca = new Cliente();
                        ca.nameField = "Hierros Manuel S.A";
                        listClientes.Add(ca);
                        adapterNuevaOferta = new AdapterNuevaOferta(this.context, listClientes);
                        nuevaOfertaListview.Adapter = adapterNuevaOferta;
                        break;
                    default:
                        break;
                }
                ((ViewPager)container).AddView(view);
               

                    return view;
            }

            public string GetHeaderTitle(int position)
            {
                return _data[position];
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

            }

            public void OnPageSelected(int position)
            {
                //actionBar.SetTitle(Resource.String.Listado_Productos);// ActionBar.setTitle(titles[pos]);

                tabSeleccionado = position;
            }
        }

        private class SearchViewExpandListener
          : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        {
            private readonly IFilterable _adapter;


            public SearchViewExpandListener(IFilterable adapter)
            {
                _adapter = adapter;
            }

            public bool OnMenuItemActionCollapse(IMenuItem item)
            {

                _adapter.Filter.InvokeFilter(" ");
                //_adapter.Filter.InvokeFilter();
                return true;
            }

            public bool OnMenuItemActionExpand(IMenuItem item)
            {
                return true;
            }
        }
    }
}