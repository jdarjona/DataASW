using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Activities;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Database;
using ModelDataTRH.Proyectos.Trazabilidad_Generico;
using Newtonsoft.Json;

namespace AlmacenRepuestosXamarin.Clases
{
    public class SlidingTabsFragment : Android.Support.V4.App.Fragment, ViewPager.IOnPageChangeListener
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        private ImageView estado;
        private View view;
        private static int count = -1;
        public static int tabSeleccionado = 7;
        private List<MaquinaFirebase> listaAuxiliar1 = new List<MaquinaFirebase>();
        public AdapterSinoptico adapterSinoptico { get;  set; }
        public LinearLayout progressLayout;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            view = inflater.Inflate(Resource.Layout.fragment_sample, container, false);
            estado = new ImageView(Context);
            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarLista);
            progressLayout.Visibility = ViewStates.Visible;
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {

            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            this.mViewPager.AddOnPageChangeListener(this);
            mViewPager.Adapter = new SamplePagerAdapter(this.Activity, mViewPager);
           
            ((SamplePagerAdapter)mViewPager.Adapter).database = ((HomeView)this.Activity).database;

            mSlidingTabScrollView.ViewPager = mViewPager;
            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarLista);
            progressLayout.Visibility = ViewStates.Gone;
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            inflater.Inflate(Resource.Menu.actionbarSinoptico, menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //mViewPager.Adapter = new SamplePagerAdapter(this.Activity, mViewPager, item);
            SamplePagerAdapter adapter = (SamplePagerAdapter)mViewPager.Adapter;
            
            if (count==-1) {
                 listaAuxiliar1 = adapter.listSinoptico;
            }
            List<MaquinaFirebase> listaAuxiliar2 = listaAuxiliar1;
            switch (item.ToString())
            {
                case "Ver Todo":
                    adapter.listSinoptico = listaAuxiliar1.OrderByDescending(o => o.SeccionMaquina).ToList();
                    count = 0;
                    break;
                case "En Producción":
                    adapter.listSinoptico = (listaAuxiliar2.Where(o => o.Conexion == true && o.Operario1 != null).ToList()).OrderByDescending(o => o.SeccionMaquina).ToList();
                    count = 0;
                    break;

                default:
                    
                    break;
            }

            //((SamplePagerAdapter)mViewPager.Adapter).database = ((HomeView)this.Activity).database;
            mViewPager.Adapter.NotifyDataSetChanged();
            return base.OnOptionsItemSelected(item);
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
        public override void OnResume()
        {
            base.OnResume();
        }

        public class SamplePagerAdapter : PagerAdapter, ViewPager.IOnPageChangeListener , IChildEventListener
        {
            private string sevilla = " TRH Sevilla ";
            private string liege = " TRH Liege ";
            private const string urlSevilla = @"Maquinas/TRH";
            private const string urlLieja = @"Maquinas/TRH Liege";
            private ViewPager _mViewPager;
            private bool  vertodo = false;
            private string url;
            public List<MaquinaFirebase> listSinoptico = new List<MaquinaFirebase>();
            private List<string> items = new List<string>();
            private Activity context;
            public AdapterSinoptico adapterSinoptico { get; set; }
            public AdapterSinoptico adapterSinopticoSevilla { get; set; }
            public AdapterSinoptico adapterSinopticoLiege { get; set; }
            private ListView sinopticoListView;
            private List<ListView> listSinopticoListView = new List<ListView>();
            private List<LinearLayout> listProgressLayout = new List<LinearLayout>();
            private LinearLayout progressLayout;
            private IMenuItem item;
            public Firebase.Database.FirebaseDatabase database; 
            public SamplePagerAdapter(Activity context, ViewPager mViewPager) : base()
            {
                items.Add(sevilla);
                items.Add(liege);
                _mViewPager = mViewPager;
                this.context = context;
                this._mViewPager.AddOnPageChangeListener(this);
            }

            public SamplePagerAdapter(Activity context, ViewPager mViewPager, IMenuItem item) : this(context, mViewPager)
            {
                this.item = item;
                tipoListado(item);
            }

            public override int Count
            {
                get { return items.Count; }
            }

            private async Task initFirebase()
            {
                listSinoptico.Clear();
                Func<Task<string>> authToken = async delegate ()
                {
                    return "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B";
                };

                listSinoptico.Clear();
                database.GetReference(url).AddChildEventListener(this);
                adapterSinoptico.NotifyDataSetChanged();
                 

                AppCompatActivity activity = (AppCompatActivity)this.context;
                activity.SupportActionBar.Title = "SINÓPTICO FÁBRICA";
                activity.SupportActionBar.Subtitle = "";

            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }
            public override void NotifyDataSetChanged()

            {
                adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);
                this.context.RunOnUiThread(() => adapterSinoptico.NotifyDataSetChanged());
                sinopticoListView.Adapter = adapterSinoptico;
                //switch (tabSeleccionado)
                //{

                //    case 0:
                //        adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);
                //        this.context.RunOnUiThread(() => adapterSinoptico.NotifyDataSetChanged());
                //        sinopticoListView.Adapter = adapterSinoptico;
                //        break;
                //    case 1:
                //        adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);
                //        this.context.RunOnUiThread(() => adapterSinoptico.NotifyDataSetChanged());
                //        sinopticoListView.Adapter = adapterSinoptico;

                //        break;

                //    default:
                //        break;
                //}




                base.NotifyDataSetChanged();
            }
            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                
                SlidingTabsFragment stf = new SlidingTabsFragment();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                switch (position)
                {
                    case 0:
                        
                        sinopticoListView = view.FindViewById<ListView>(Resource.Id.listViewSinopticoFragment);
                        progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);
                        adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);
                        sinopticoListView.Adapter = adapterSinoptico;

                        listSinopticoListView.Add(sinopticoListView);
                        listProgressLayout.Add(progressLayout);
                        url = urlSevilla;
                        initFirebase();

                        break;

                    case 1:
                        listSinopticoListView.Add(view.FindViewById<ListView>(Resource.Id.listViewSinopticoFragment));
                        listProgressLayout.Add(view.FindViewById<LinearLayout>(Resource.Id.progressBar));

                        break;
                    default:
                        break;
                }
                
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
                database.GetReference(url).RemoveEventListener(this);
                sinopticoListView = listSinopticoListView[position];
                progressLayout = listProgressLayout[position];
                adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);
                sinopticoListView.Adapter = adapterSinoptico;
                progressLayout.Visibility = ViewStates.Visible;
                SlidingTabsFragment.count = -1;
                if (position == 0)
                {
                    url = urlSevilla; 
                }
                else if (position == 1)
                {
                    url = urlLieja;
                    progressLayout.Visibility = ViewStates.Gone;
                }

                await initFirebase();
                progressLayout.Visibility = ViewStates.Gone;
            }

            private void tipoListado(IMenuItem order)
            {
                switch (order.ToString())
                {
                    case "Ver Todo":
                        vertodo = true;
                        break;
                    case "En Producción":
                        vertodo = false;
                        break;

                    default:
                        vertodo = false;
                        break;
                }
            }

            public void OnCancelled(DatabaseError error)
            {
                
            }

           

            public void OnChildAdded(DataSnapshot snapshot, string previousChildName)
            {
                string json = JsonConvert.SerializeObject(snapshot.Value);
                MaquinaFirebase maquina = JsonConvert.DeserializeObject<MaquinaFirebase>(json);
                listSinoptico.Add(maquina);
                adapterSinoptico.NotifyDataSetChanged();
            }

            public void OnChildChanged(DataSnapshot snapshot, string previousChildName)
            {
                string json = JsonConvert.SerializeObject(snapshot.Value);
                MaquinaFirebase maquina = JsonConvert.DeserializeObject<MaquinaFirebase>(json);
                var maquinaListado = listSinoptico.Where(q => q.IdMaquina.Equals(maquina.IdMaquina)).FirstOrDefault();

                if (maquinaListado != null) {
                    maquinaListado = maquina;
                    this.context.RunOnUiThread(() => Toast.MakeText(this.context, maquinaListado.IdMaquina, ToastLength.Short).Show());
                    
                    adapterSinoptico.NotifyDataSetChanged();
                }

                
            }

            public void OnChildMoved(DataSnapshot snapshot, string previousChildName)
            {
               
            }

            public void OnChildRemoved(DataSnapshot snapshot)
            {
                
            }
        }
    }
    }