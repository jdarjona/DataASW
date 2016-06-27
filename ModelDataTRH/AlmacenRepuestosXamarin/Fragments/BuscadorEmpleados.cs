using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Model;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using RepositoryWebServiceTRH.EmpleadoContext;

namespace AlmacenRepuestosXamarin.Fragments
{
    [Activity(Label = "Almacen Repuestos",  Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class BuscadorEmpleados : Android.Support.V4.App.Fragment
    {
        List<Empleados> empleados = new List<Empleados>();
        Data.AccesoDatos restService = new Data.AccesoDatos();
        ListView listViewEmpleados;
        Adapter.AdapterEmpleados adaptadorEmpleados;
        private Android.Support.V7.Widget.SearchView _searchView;
        LinearLayout progressLayout;
        IMenuItem item;
        View view;

        public BuscadorEmpleados() {

            this.RetainInstance = true;

        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.HasOptionsMenu = true;
            // Create your fragment here
        }



        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var ignored = base.OnCreateView(inflater, container, savedInstanceState);

             view = inflater.Inflate(Resource.Layout.Main, null);

            

            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarMain);
             listViewEmpleados = (ListView)view.FindViewById(Resource.Id.listEmpleados);

            
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) {

            inflater.Inflate(Resource.Menu.buscador, menu);

            item = menu.FindItem(Resource.Id.action_search);

            var searchView = MenuItemCompat.GetActionView(item);
            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();


            _searchView.QueryTextChange += (s, e) => {

                adaptadorEmpleados.Filter.InvokeFilter(e.NewText);
                adaptadorEmpleados.NotifyDataSetChanged();


                };

            _searchView.QueryTextSubmit += (s, e) =>
            {
                // Handle enter/search button on keyboard here
                Toast.MakeText(this.Activity, "Searched for: " + e.Query, ToastLength.Short).Show();
                e.Handled = true;
            };
            AppCompatActivity activity = (AppCompatActivity)this.Activity;
           
            MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(adaptadorEmpleados));

            fillListView();

            base.OnCreateOptionsMenu(menu, inflater);

        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            //  

            //var listView = (Android.Widget.AbsListView)sender;
            var listView = (Android.Widget.ListView)e.Parent;
            var adapter = (AdapterEmpleados)listView.Adapter;




            //var lista = new Empleados();

            ManagerRepuestos.addEmpleado(adapter.list[e.Position]);

            //var l = listView.Adapter.GetItem(e.Position).JavaCast<Empleados>();


            this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, adapter.list[e.Position].FullName, ToastLength.Short).Show());

            //var activityRepuestosEPIS = new Intent(this.Activity, typeof(ListEPISRepuestos));

            //StartActivity(activityRepuestosEPIS);

            ManagerRepuestos.getRepuestos().Clear();
            Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.ListaRepuestosEntrega();

            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                 .AddToBackStack(null)
               .Commit();
            

        }

        private async Task<List<Empleados>> getEmpleados()
        {

            var query = await restService.getEmpleados();
            return query;

        }
        private async void fillListView()
        {
            try
            {
                progressLayout.Visibility = ViewStates.Visible;
                empleados = await getEmpleados();
                
                adaptadorEmpleados = new AdapterEmpleados(this.Activity, empleados);
                listViewEmpleados.ItemClick += OnListItemClick;
                listViewEmpleados.Adapter = adaptadorEmpleados;

                MenuItemCompat.SetOnActionExpandListener(item, new SearchViewExpandListener(adaptadorEmpleados));
                progressLayout.Visibility = ViewStates.Gone;
            }
            catch (Exception e)
            {
                progressLayout.Visibility = ViewStates.Gone;
            }
            finally
            {
                progressLayout.Visibility = ViewStates.Gone;
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

                _adapter.Filter.InvokeFilter("");
                
                return true;
            }

            public bool OnMenuItemActionExpand(IMenuItem item)
            {
                return true;
            }
        }
    }
}