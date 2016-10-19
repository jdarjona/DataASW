using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Clases;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using ModelDataTRH.Proyectos.Ventas;
using static AlmacenRepuestosXamarin.Clases.SlidingTabsFragment;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "NuevaOfertaActivity")]
    public class NuevaOfertaActivity : Activity, ViewPager.IOnPageChangeListener
    {

        LinearLayout progressLayout;
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.fragment_sample);
            mSlidingTabScrollView = FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            
            mViewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            this.mViewPager.AddOnPageChangeListener(this);
            mViewPager.Adapter = new SPagerAdapter(this, mViewPager);
            mSlidingTabScrollView.ViewPager = mViewPager;
            progressLayout = FindViewById<LinearLayout>(Resource.Id.progressBarLista);
            progressLayout.Visibility = ViewStates.Gone;
            // Create your application here
        }

        public void OnPageScrollStateChanged(int state)
        {
           // throw new NotImplementedException();
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            //throw new NotImplementedException();
        }

        public void OnPageSelected(int position)
        {
            //throw new NotImplementedException();
        }



        public class SPagerAdapter : PagerAdapter, ViewPager.IOnPageChangeListener
        {
            private string cliente = " Cliente ";
            private string almacen = " Almacen ";
            private string contacto = " Contacto ";
            private string direccionEnvio = " Dirección de Envio ";
            private string productos = " Productos ";

            private ViewPager _mViewPager;

            private AdapterNuevaOferta adapterNuevaOferta;
            private List<string> items = new List<string>();
            private Activity context;
            private ListView nuevaOfertaListview;
            private List<Cliente> listClientes = new List<Cliente>();
            private LinearLayout progressLayout;
            private IMenuItem item;
            private int tabSeleccionado=0;

            public SPagerAdapter(Activity context, ViewPager mViewPager) : base()
            {
                items.Add(cliente);
                items.Add(almacen);
                items.Add(contacto);
                items.Add(direccionEnvio);
                items.Add(productos);
                _mViewPager = mViewPager;
                this.context = context;
                this._mViewPager.AddOnPageChangeListener(this);
            }

            public SPagerAdapter(Activity context, ViewPager mViewPager, IMenuItem item) : this(context, mViewPager)
            {
                this.item = item;
                
            }

            public override int Count
            {
                get { return items.Count; }
            }

           

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                string title = GetHeaderTitle(tabSeleccionado);
                SlidingTabsFragment stf = new SlidingTabsFragment();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pagerItemOferta, container, false);
                nuevaOfertaListview = view.FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);
                progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);

                ////listSinoptico.OrderBy(o => o.CodOperario != null && o.Conexion);
                listClientes.Clear();
                Cliente c = new Cliente();
                c.nameField = "Hierros S.A";
                listClientes.Add(c);
                adapterNuevaOferta = new AdapterNuevaOferta(this.context, listClientes);
                nuevaOfertaListview.Adapter = adapterNuevaOferta;
                //if (position == 0)
                //{
                //    url = urlSevilla;
                //    initFirebase();
                //}
                container.AddView(view);
                progressLayout.Visibility = ViewStates.Gone;
                return view;
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

            }

            public async void OnPageSelected(int position)
            {
                tabSeleccionado = position;
            }
        }
    }
}