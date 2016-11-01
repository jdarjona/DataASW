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
    [Activity(Label = "NuevaOfertaAlmacenActivity")]
    public class NuevaOfertaAlmacenesActivity : BaseActivity
    {
        private ListView listAlmacenesOferta;
        private AdapterAlmacenNuevaOferta adapter { get; set; }
        LinearLayout progressLayout;
        SearchViewExpandListener sve;
        IMenuItem item;
        public static string nombreAlmacenField = "";
        private Android.Support.V7.Widget.SearchView _searchView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           // Intent intent = this.getIntent();
            String value = Intent.GetStringExtra("key");
            // Create your application here
            listAlmacenesOferta = FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);

            listAlmacenesOferta.Adapter = getAdapterListadoAlmacenes(value);
            listAlmacenesOferta.ItemClick += OnListItemClick;
            sve = new SearchViewExpandListener(adapter);
            SupportActionBar.Title = "Listado Almacenes";
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
            Almacen c = adapter.list[e.Position];
            nombreAlmacenField = c.nombreAlmacenField;
            Intent myIntent = new Intent(this, typeof(NuevaOfertaContactoActivity));
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

        public IListAdapter getAdapterListadoAlmacenes(string codAlmacen)
        {
            List<Almacen> l =  AccesoDatos.listaAlmacenes.Where(p => p.idClienteField == codAlmacen && p.idComercialField.Equals(NuevaOfertaClientesActivity.comercial)).ToList();

            adapter = new AdapterAlmacenNuevaOferta(this, l);
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