using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using ModelDataTRH.Ventas;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "ListadoOfertasActivity")]
    public class ListadoOfertasActivity : BaseActivity
    {
        private ListView listOfertas;
        public static string comercial = "V001";
        private AdapterListadoOfertas adapter { get; set; }
        LinearLayout progressLayout;
        SearchViewExpandListener sve;
        IMenuItem item;
        public static string codCliente = string.Empty;
        public static string nombreCliente = string.Empty;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.pagerItemOferta;
            }
        }

        //private SearchView _searchView;
        private Android.Support.V7.Widget.SearchView _searchView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            listOfertas = FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);

            listOfertas.Adapter = getAdapterListadoOfertas(comercial);
            listOfertas.ItemClick += OnListItemClick;
            sve = new SearchViewExpandListener(adapter);
            SupportActionBar.Title = "Listado de Ofertas";

        }
        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {




            Oferta c = adapter.list[e.Position];
            //codCliente = c.noField;
            //nombreCliente = c.nameField;
            //Intent myIntent = new Intent(this, typeof(NuevaOfertaAlmacenesActivity));
            //myIntent.PutExtra("key", codCliente);
            //StartActivity(myIntent);

            //StartActivity(nuevaOfertaAlmacenesActivity);
            // var intent = new Intent(codCliente);

            //intent = codCliente;

            //IR A NUEVA ACTIVIDAD PARA SELECCIONAR EL ALMACEN 


            // this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, adapter.list[e.Position].FullName, ToastLength.Short).Show());

            //var activityRepuestosEPIS = new Intent(this.Activity, typeof(ListEPISRepuestos));

            //StartActivity(activityRepuestosEPIS);

            // ManagerRepuestos.getRepuestos().Clear();
            //Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.ListaRepuestosEntrega();

            //FragmentManager.BeginTransaction()
            //    .Replace(Resource.Id.content_frame, fragment)
            //     .AddToBackStack(null)
            //   .Commit();


        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.buscador, menu);

            item = menu.FindItem(Resource.Id.action_search);

            var searchView = MenuItemCompat.GetActionView(item);
            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();

            _searchView.QueryTextChange += (s, e) => {


                this.RunOnUiThread(() => {

                    adapter.Filter.InvokeFilter(new Java.Lang.String(e.NewText.ToString()));
                    adapter.NotifyDataSetChanged();

                });


            };

            _searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                Toast.MakeText(this, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
            //AppCompatActivity activity = (AppCompatActivity)this.Activity;

            MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(adapter));

            return base.OnCreateOptionsMenu(menu);
        }
        protected override void OnResume()
        {
            base.OnResume();
        }

        public IListAdapter getAdapterListadoOfertas(string comercial)
        {
            List<Oferta> l = AccesoDatos.listaOfertas.Where(p => p.salesperson_CodeField == comercial ).ToList();

            adapter = new AdapterListadoOfertas(this, l);
            return adapter;
        }


        public bool OnMenuItemActionCollapse(IMenuItem item)
        {

            // _adapter.Filter.InvokeFilter("*");
            //_adapter.Filter.InvokeFilter();
            return true;
        }

        public bool OnMenuItemActionExpand(IMenuItem item)
        {
            return true;
        }
        //------------------------------------------------------------------------------//

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