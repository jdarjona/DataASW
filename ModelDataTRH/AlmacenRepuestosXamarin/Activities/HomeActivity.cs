using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using System.Linq;
using AlmacenRepuestosXamarin.Fragments;
using AlmacenRepuestosXamarin.Helpers;
using System.Collections.Generic;

using Android.Content;
using AlmacenRepuestosXamarin.Model;
using AlmacenRepuestosXamarin.Data;
using System.Threading.Tasks;

using Firebase.Xamarin.Database.Streaming;
using Firebase.Xamarin.Database;
using System;
using AlmacenRepuestosXamarin.Clases;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "TRH", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/TRH", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HomeView : BaseActivity 
    {
       

        private MyActionBarDrawerToggle drawerToggle;        
        private string drawerTitle;
        private string title;
        private AccesoDatos datos ;
        private DrawerLayout drawerLayout;
        private ListView drawerListView;
        Spinner spinner;
        IMenu _imenu;

        private static readonly string[] Sections = new[] {
            "App Almacen", "Monitor Carga", "Sinóptico" //, "Configuracion"
        };

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.page_home_view;
            }
        }
        protected class PedidoFireBase
        {
            public string codPedido { get; set; }
            public int estado { get; set; }
            public string descripcion { get; set; }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.title = this.drawerTitle = this.Title;

            Helpers.Preferencias preferencias = new Preferencias(this);

            this.drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            this.drawerListView = this.FindViewById<ListView>(Resource.Id.left_drawer_Menu);

            //Create Adapter for drawer List
            this.drawerListView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.item_menu, Sections);

            //Set click handler when item is selected
            this.drawerListView.ItemClick += (sender, args) => ListItemClicked(args.Position);

            //Set Drawer Shadow
            this.drawerLayout.SetDrawerShadow(Resource.Drawable.drawer_shadow_dark, (int)GravityFlags.Start);


            //DrawerToggle is the animation that happens with the indicator next to the actionbar
            this.drawerToggle = new MyActionBarDrawerToggle(this, this.drawerLayout,
                this.Toolbar,
                Resource.String.drawer_open,
                Resource.String.drawer_close);

            //Display the current fragments title and update the options menu
            this.drawerToggle.DrawerClosed += (o, args) => {
                //this.SupportActionBar.Title = this.title;
                this.InvalidateOptionsMenu();
            };

            //Display the drawer title and update the options menu
            this.drawerToggle.DrawerOpened += (o, args) => {
                //this.SupportActionBar.Title = this.drawerTitle;
                this.InvalidateOptionsMenu();
            };

            //Set the drawer lister to be the toggle.
            this.drawerLayout.SetDrawerListener(this.drawerToggle);

            string action = this.Intent.GetStringExtra("idFragment");

            if (!string.IsNullOrEmpty(action))
            {

                int idFragment;

                int.TryParse(action, out idFragment);

                ListItemClicked(idFragment);

            }
            else {
                ListItemClicked(0);
            }

            
             initFirebase();
        }



        private async void initFirebase() {

           await Monitorizacion.getListMonitorizacion(Monitorizacion.empresaLiege);
            await Monitorizacion.getListMonitorizacion(Monitorizacion.empresaSevilla);
            Func<Task<string>> authToken = async delegate()
            {
                return  "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B";
            };


           

            var firebase = new FirebaseClient(@"https://flickering-fire-4088.firebaseio.com/", authToken);

            //var consulta = await firebase.Child(@"Pedidos")
            //    .OnceAsync<Dictionary<string,PedidoFireBase>>();

            var items =  firebase
             .Child(@"Pedidos")//.OnceAsync<PedidoFireBase>();
           
            .AsObservable<Dictionary<string, PedidoFireBase>>()
            
             .Subscribe(OnItemMessage);
            

        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        public override void OnBackPressed()
        {
            int count = SupportFragmentManager.BackStackEntryCount;

            if (count == 0)
            {
                base.OnBackPressed();
            }
            else
            {
                SupportFragmentManager.PopBackStack();
            }
        }

        private void ListItemClicked(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;

            SupportActionBar.Subtitle = "";
            
            switch (position)
            {
                case 0:
                    SupportActionBar.Title = this.title = Sections[position];
                    fragment = new BuscadorEmpleados();
                    SupportFragmentManager.BeginTransaction()
                       .Replace(Resource.Id.content_frame, fragment)
                       .Commit();
                    break;
                case 1:
                    //MenuInflater.Inflate(Resource.Menu.menu, _imenu);
                    fragment = new ListadoMonitorizacion();
                    SupportFragmentManager.BeginTransaction()
                           .Replace(Resource.Id.content_frame, fragment)
                           .Commit();
                    break;
                case 2:

                    fragment = new SlidingTabsFragment();
                    SupportFragmentManager.BeginTransaction()
                          .Replace(Resource.Id.content_frame, fragment)
                          .Commit();

                    //var sinoptico = new Intent(this, typeof(SinopticoActivity));
                    //StartActivity(sinoptico);

                    break;

                case 3://actityConfiguracion
                    var actityConfiguracion = new Intent(this, typeof(OpcionesActivity));
                    StartActivity(actityConfiguracion);

                    break;
            }
            this.drawerListView.SetItemChecked(position, true);
            this.drawerLayout.CloseDrawers();
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
             _imenu = menu;
            //var drawerOpen = this.drawerLayout.IsDrawerOpen((int)GravityFlags.Left);
            ////when open don't show anything
            //for (int i = 0; i < menu.Size(); i++)
            //    menu.GetItem(i).SetVisible(!drawerOpen);

            return base.OnPrepareOptionsMenu(menu);
        }

        protected async override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            this.drawerToggle.SyncState();
            await Monitorizacion.updateListMonitorizacion();            
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            this.drawerToggle.OnConfigurationChanged(newConfig);
        }

        // Pass the event to ActionBarDrawerToggle, if it returns
        // true, then it has handled the app icon touch event
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (this.drawerToggle.OnOptionsItemSelected(item))
                return true;

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnSaveInstanceState(Bundle savedInstanceState)
        {
            savedInstanceState.PutInt("idFrameOpen", 1);
            base.OnSaveInstanceState(savedInstanceState);
        }

        private async Task notificar(PedidoFireBase message) {

            Intent notificationIntent = this.PackageManager.GetLaunchIntentForPackage(this.PackageName);
            notificationIntent.SetFlags(ActivityFlags.ClearTop);

            notificationIntent.PutExtra("idFragment", "1");
            int pendingIntentId = (int)(System.DateTime.Now.Millisecond & 0xfffffff);
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, pendingIntentId, notificationIntent, PendingIntentFlags.UpdateCurrent);
            this.RunOnUiThread(() => {
              

                    Toast.MakeText(this, message.descripcion, ToastLength.Short).Show();

                    Notification.Builder builder = new Notification.Builder(this)
                        .SetTicker(message.codPedido)
                        .SetContentIntent(pendingIntent)
                        .SetContentTitle(message.codPedido)
                        .SetContentText(message.descripcion)
                        .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                        .SetSmallIcon(Resource.Drawable.campana32)
                     
                         .SetAutoCancel(true);

                    // Build the notification:
                    Notification notification = builder.Build();
                    
                    notification.Defaults =  NotificationDefaults.All; //NotificationFlags.AutoCancel |
                notification.Extras.PutString("idFragment", "1");

                    // Get the notification manager:
                    NotificationManager notificationManager =
                        GetSystemService(Context.NotificationService) as NotificationManager;

                    notificationManager.Notify(message.codPedido, 0, notification);

            });
        }

        //Comprobamos que la notificacion de cambio del estado del pedido es diferente a la inicial y así mandar la notificación
        private async  void OnItemMessage(FirebaseEvent<Dictionary<string, PedidoFireBase>> message)
        {
            foreach (KeyValuePair<string, PedidoFireBase> item in message.Object)
            {
                string pedMessage = item.Value.codPedido.ToString();
                string estadoMessage = item.Value.descripcion.ToString();


                if (Monitorizacion.listMonitorizacionLieja != null || Monitorizacion.listMonitorizacionSevilla != null)
                {
                    var pedido = Monitorizacion.listMonitorizacionLieja.Where(q => q.codigoPedido.Equals(item.Value.codPedido)).FirstOrDefault();
                    if (pedido== null)
                        pedido = Monitorizacion.listMonitorizacionSevilla.Where(q => q.codigoPedido.Equals(item.Value.codPedido)).FirstOrDefault();

                    if (pedido != null)
                    {

                        if (pedido.Estado != item.Value.estado)
                        {
                          
                                notificar(item.Value);
                            

                        }

                    }
                    else
                    {
                        notificar(item.Value);
                    }
                }   
                //if (Monitorizacion.listMonitorizacion.Count > 0)
                //{
                //    for (int i = 0; i < Monitorizacion.listMonitorizacion.Count; i++)
                //    {
                //        string pedidoListado = Monitorizacion.listMonitorizacion[i].codigoPedido.ToString();
                //        if (item.Value.codPedido.Equals(Monitorizacion.listMonitorizacion[i].codigoPedido))
                //        {
                //            string estadoListado = Monitorizacion.listMonitorizacion[i].Estado.ToString();
                //            if (!Monitorizacion.listMonitorizacion[i].Estado.Equals(item.Value.descripcion) && (message.EventType == FirebaseEventType.InsertOrUpdate))
                //            {
                //                notificar(item.Value);
                //            }
                //        }
                //    }
                //}
            }
           
            
        }
    }
}