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
using ModelDataTRH.Proyectos.Ventas;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "NuevaOfertaDireccionEnvioActivity")]
    public class NuevaOfertaDireccionEnvioActivity : BaseActivity
    {
        private ListView listDireccionEnvioOferta;
        private AdapterDireccionEnvioNuevaOferta adapter { get; set; }
        LinearLayout progressLayout;
        SearchViewExpandListener sve;
        IMenuItem item;
        public static string nombreAlmacenField = "";
        private Android.Support.V7.Widget.SearchView _searchView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            String value = Intent.GetStringExtra("key");
            listDireccionEnvioOferta = FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);

            listDireccionEnvioOferta.Adapter = getAdapterListadoDireccionesEnvio(value);
            listDireccionEnvioOferta.ItemClick += OnListItemClick;
            sve = new SearchViewExpandListener(adapter);
            SupportActionBar.Title = "Direcciones de Envío";
            // Create your application here
        }

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.pagerItemOferta;
            }
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //DireccionEnvio c = adapter.list[e.Position];
            nombreAlmacenField = string.Empty;
            Intent myIntent = new Intent(this, typeof(NuevaOfertaActivity));
            myIntent.PutExtra("key", nombreAlmacenField);
            StartActivity(myIntent);

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

        public IListAdapter getAdapterListadoDireccionesEnvio(string nombreAlmacen)
        {
            List<DireccionEnvio> listaPrueba = AccesoDatos.listaDireccionesEnvio;
            //List<DireccionEnvio> l = listaPrueba;
            List<DireccionEnvio> l = listaPrueba.Where(p => p.customer_NoField == NuevaOfertaClientesActivity.codCliente && !p.nameField.Equals(string.Empty)).ToList();

            adapter = new AdapterDireccionEnvioNuevaOferta(this, l);
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