using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Database;
using ModelDataTRH.Proyectos.Trazabilidad_Generico;

namespace AlmacenRepuestosXamarin.Clases
{
    public class SlidingTabsFragment : Android.Support.V4.App.Fragment
    {
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        private View view;
        
        public AdapterSinoptico adapterSinoptico { get;  set; }
        public LinearLayout progressLayout;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            HasOptionsMenu = true;
            view = inflater.Inflate(Resource.Layout.fragment_sample, container, false);
            ImageView estado = new ImageView(Context);
            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarLista);
            progressLayout.Visibility = ViewStates.Visible;
            return view;
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            mSlidingTabScrollView = view.FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            mViewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            mViewPager.Adapter = new SamplePagerAdapter(this.Activity, mViewPager);
            mSlidingTabScrollView.ViewPager = mViewPager;
            progressLayout.Visibility = ViewStates.Gone;
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            inflater.Inflate(Resource.Menu.actionbarSinoptico, menu);
        }

        public class SamplePagerAdapter : PagerAdapter, ViewPager.IOnPageChangeListener
        {
            private string sevilla = " TRH Sevilla ";
            private string liege = " TRH Liege ";
            private const string urlSevilla = @"Maquinas/TRH";
            private const string urlLieja = @"Maquinas/TRH Liege";
            private ViewPager _mViewPager;
            private string url;
            private List<MaquinaFirebase> listSinoptico = new List<MaquinaFirebase>();
            private List<string> items = new List<string>();
            private Activity context;
            public AdapterSinoptico adapterSinoptico { get; set; }
            private ListView sinopticoListView;
            private LinearLayout progressLayout;
            public SamplePagerAdapter(Activity context, ViewPager mViewPager) : base()
            {
                items.Add(sevilla);
                items.Add(liege);
                _mViewPager = mViewPager;
                this.context = context;
                this._mViewPager.AddOnPageChangeListener(this);
            }

            public override int Count
            {
                get { return items.Count; }
            }

            private async Task initFirebase()
            {
                
                Func<Task<string>> authToken = async delegate ()
                {
                    return "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B";
                };

                var firebase = new FirebaseClient(@"https://flickering-fire-4088.firebaseio.com/", authToken);
                var itemsFirebase = await firebase
                 .Child(url)
                 .OnceAsync<MaquinaFirebase>();
                listSinoptico.Clear();
                foreach (var item in itemsFirebase)
                {
                    listSinoptico.Add(item.Object);
                }
                 adapterSinoptico.NotifyDataSetChanged();
                 //.Subscribe(OnItemMessage);

                AppCompatActivity activity = (AppCompatActivity)this.context;
                activity.SupportActionBar.Title = "SINÓPTICO FÁBRICA";
                activity.SupportActionBar.Subtitle = "";

            }

            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                
                SlidingTabsFragment stf = new SlidingTabsFragment();
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.pager_item, container, false);
                sinopticoListView = view.FindViewById<ListView>(Resource.Id.listViewSinopticoFragment);
                progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);
                
                adapterSinoptico = new AdapterSinoptico(this.context, listSinoptico);
                sinopticoListView.Adapter = adapterSinoptico;
                if (position == 0) {
                    url = urlSevilla;
                    initFirebase();
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
                if (position == 0)
                {
                    progressLayout.Visibility = ViewStates.Visible;
                    url = urlSevilla;
                    await initFirebase();
                    progressLayout.Visibility = ViewStates.Gone;

                }
                else if (position == 1)
                {
                    progressLayout.Visibility = ViewStates.Visible;
                    url = urlLieja;
                    await initFirebase();

                    progressLayout.Visibility = ViewStates.Gone;
                }
                else
                {

                }
            }

            private void ordenarListado(IMenuItem order)
            {
                //switch (order.ToString())
                //{
                //    case "Ordenar por Operario":
                //        listSinopticoSevilla = listSinopticoSevilla.OrderBy(o => o.inicialesComercial).ToList();
                //        listSinopticoLiege = listSinopticoLiege.OrderBy(o => o.inicialesComercial).ToList();
                //        break;
                //    case "Ordenar por Estado":
                //        listSinopticoSevilla = listSinopticoSevilla.OrderBy(o => o.Estado).ToList();
                //        listSinopticoLiege = listSinopticoLiege.OrderBy(o => o.Estado).ToList();
                //        break;
                //    case "Ordenar por Pedido":
                //        listSinopticoSevilla = listSinopticoSevilla.OrderBy(o => o.codigoPedido).ToList();
                //        listSinopticoLiege = listSinopticoLiege.OrderBy(o => o.codigoPedido).ToList();
                //        break;
                //    case "Ordenar por Fecha":
                //        listSinopticoSevilla = listSinopticoSevilla.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                //        listSinopticoLiege = listSinopticoLiege.OrderBy(o => o.Fecha_Carga_Requerida).ToList();
                //        break;
                //    default:
                //        break;
                //}
            }
        }
    }
    }