using System;
using System.Collections.Generic;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace AlmacenRepuestosXamarin.Clases
{
    public class SlidingTabsFragment : Android.Support.V4.App.Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        
        private View view;
        private AccesoDatos datos;
        private LinearLayout progressLayout;
        private ListView listViewSinoptico;
        public List<SinopticoFabrica> listSinoptico { get; set; }
        public AdapterSinoptico adapterSinoptico { get;  set; }
        

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            view = inflater.Inflate(Resource.Layout.fragment_sample, container, false);
            ImageView estado = new ImageView(Context);
          
            return view;// inflater.Inflate(Resource.Layout.fragment_sample, container, false);
        }

        //public override void OnResume()
        //{
        //    listSinoptico = fillListaSinoptico();
        //    //adapterSinoptico = new SamplePagerAdapter(this.Activity, listSinoptico);
        //    //mViewPager.Adapter = listSinoptico;
        //    this.Activity.RunOnUiThread(() => adapterSinoptico.NotifyDataSetChanged());

        //    base.OnResume();

        //}

        //public AdapterSinoptico fillListaSinoptico()
        //{
            
        //    listSinoptico = new List<SinopticoFabrica>();
        //    SinopticoFabrica sf = new SinopticoFabrica();
        //    sf.maquina = "M1";
        //    sf.EstadoMaquina = "OK";
        //    sf.ProductoMaquina = "MM00126";
        //    sf.RecursoMaquina = "Mani";
        //    sf.RendimientoMaquina = @"99%";
        //    listSinoptico.Add(sf);
        //    sf = new SinopticoFabrica();
        //    sf.maquina = "M2";
        //    sf.EstadoMaquina = "Error";
        //    sf.ProductoMaquina = "MM00127";
        //    sf.RecursoMaquina = "Mani";
        //    sf.RendimientoMaquina = @"95%";
        //    listSinoptico.Add(sf);
        //    adapterSinoptico = new AdapterSinoptico(this.Activity,listSinoptico);
        //    return adapterSinoptico;

        //}

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter(this.Activity);
            

            mSlidingTabScrollView.ViewPager = mViewPager;
        }

        public class SamplePagerAdapter : PagerAdapter
        {
            private string sevilla = " TRH Sevilla ";
            private string liege = " TRH Liege ";
            public List<SinopticoFabrica> listSinoptico { get; set; }
            List<string> items = new List<string>();
            private Activity context;
            public List<SinopticoFabrica> list;
            public AdapterSinoptico adapterSinoptico { get; set; }

            public SamplePagerAdapter(Activity context) : base()
            {
                items.Add(sevilla);
                items.Add(liege);
                this.context = context;
            }
            //public SamplePagerAdapter(Activity _context, List<SinopticoFabrica> _list)
            //: base()
            //{
            //    items.Add(" TRH Sevilla ");
            //    items.Add(" TRH Liege ");
            //    this.context = _context;
            //    this.list = _list;
            //}

            public override int Count
            {
                get { return items.Count; }
            }

            public AdapterSinoptico fillListaSinoptico(string empresa)
            {

               
                listSinoptico = new List<SinopticoFabrica>();
                SinopticoFabrica sf;
                if (empresa.Equals(sevilla))
                {
                    sf = new SinopticoFabrica();
                    sf.maquina = "MÁQUINA: M1";
                    sf.EstadoMaquina = "ON";
                    sf.ProductoMaquina = "PRODUCTO: MM00126";
                    sf.RecursoMaquina = "OPERARIO: Manuel";
                    sf.RendimientoMaquina = "100";
                    listSinoptico.Add(sf);
                    sf = new SinopticoFabrica();
                    sf.maquina = "MÁQUINA: M2";
                    sf.EstadoMaquina = "OFF";
                    sf.ProductoMaquina = "PRODUCTO: MM00127";
                    sf.RecursoMaquina = "OPERARIO: Rafael";
                    sf.RendimientoMaquina = "70";
                    listSinoptico.Add(sf);

                }
                else if (empresa.Equals(liege))
                {
                    sf = new SinopticoFabrica();
                    sf.maquina = "MÁQUINA: M3";
                    sf.EstadoMaquina = "OFF";
                    sf.ProductoMaquina = "PRODUCTO: MM00126";
                    sf.RecursoMaquina = "OPERARIO: Daniel";
                    sf.RendimientoMaquina = "20";
                    listSinoptico.Add(sf);
                    sf = new SinopticoFabrica();
                    sf.maquina = "MÁQUINA: M4";
                    sf.EstadoMaquina = "ON";
                    sf.ProductoMaquina = "PRODUCTO: MM00127";
                    sf.RecursoMaquina = "OPERARIO: Jesus";
                    sf.RendimientoMaquina = "60";
                    listSinoptico.Add(sf);
                    sf = new SinopticoFabrica();
                    sf.maquina = "MÁQUINA: M6";
                    sf.EstadoMaquina = "OFF";
                    sf.ProductoMaquina = "PRODUCTO: MM00128";
                    sf.RecursoMaquina = "OPERARIO: Samuel";
                    sf.RendimientoMaquina = "90";
                    listSinoptico.Add(sf);
                    sf = new SinopticoFabrica();
                    sf.maquina = "MÁQUINA: M7";
                    sf.EstadoMaquina = "OFF";
                    sf.ProductoMaquina = "PRODUCTO: MM00129";
                    sf.RecursoMaquina = "OPERARIO: Esteban";
                    sf.RendimientoMaquina = "40";
                    listSinoptico.Add(sf);
                }
                else {
                    Toast.MakeText(context, "Error al cargar empresa", ToastLength.Long).Show();
                }

                adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);

                AppCompatActivity activity = (AppCompatActivity)this.context;
                activity.SupportActionBar.Title = "SINÓPTICO FÁBRICA";
                activity.SupportActionBar.Subtitle = "";

                return adapterSinoptico;

            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                SlidingTabsFragment stf = new SlidingTabsFragment();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                ListView sinopticoListView = view.FindViewById<ListView>(Resource.Id.listViewSinopticoFragment);
                if (position == 0) {
                    sinopticoListView.Adapter = fillListaSinoptico(items[position].ToString());
                } else if (position == 1) {
                    sinopticoListView.Adapter = fillListaSinoptico(items[position].ToString());
                }

                container.AddView(view);
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
        }
    }
    }