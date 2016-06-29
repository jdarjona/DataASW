using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using AlmacenRepuestosXamarin.Fragments;
using AlmacenRepuestosXamarin.Helpers;
using Android.Support.V7.App;
using Firebase.Xamarin;
using Firebase.Xamarin.Streaming;
using Android.Content;
using Android.Net.Wifi;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "Almacen Repuestos", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/carretilla", ScreenOrientation = ScreenOrientation.Portrait)]
    // [Activity(Label = "Almacen Repuestos", MainLauncher = true, Icon = "@drawable/carretilla", Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HomeView : BaseActivity
    {
       

        private MyActionBarDrawerToggle drawerToggle;
        private string drawerTitle;
        private string title;

        private DrawerLayout drawerLayout;
        private ListView drawerListView;
        FirebaseClient<PedidoFireBase> _client;
        private static readonly string[] Sections = new[] {
            "App Almacen", "Monitor Carga", "Configuracion"
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
            // SetContentView(Resource.Layout.page_home_view);
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
                this.SupportActionBar.Title = this.title;
                this.InvalidateOptionsMenu();
            };

            //Display the drawer title and update the options menu
            this.drawerToggle.DrawerOpened += (o, args) => {
                this.SupportActionBar.Title = this.drawerTitle;
                this.InvalidateOptionsMenu();
            };

            //Set the drawer lister to be the toggle.
            this.drawerLayout.SetDrawerListener(this.drawerToggle);



            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                ListItemClicked(0);
            }

            _client = new FirebaseClient<PedidoFireBase>(@"https://flickering-fire-4088.firebaseio.com/", "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B");

            StreamToken<PedidoFireBase> _token = _client.GetStreamToken(@"Pedidos/TRH Liege");




            _token
                // .Where(q => q.EventType == FirebaseEventType.InsertOrUpdate)
                .Subscribe(OnItemMessage);

            
        }

        

        

        public override void OnBackPressed()
        {
            

            int count = SupportFragmentManager.BackStackEntryCount;

            if (count == 0)
            {
                base.OnBackPressed();
                //additional code
            }
            else
            {
                SupportFragmentManager.PopBackStack();
            }


           
        }
        private void ListItemClicked(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = new BuscadorEmpleados();
                    SupportFragmentManager.BeginTransaction()
                       .Replace(Resource.Id.content_frame, fragment)
                       .Commit();
                    break;
                case 1:
                   fragment = new ListadoMonitorizacion();
                    SupportFragmentManager.BeginTransaction()
                           .Replace(Resource.Id.content_frame, fragment)
                           .Commit();
                    break;
                case 2:
                    var actityConfiguracion = new Intent(this, typeof(OpcionesActivity));
                    StartActivity(actityConfiguracion);
                    
                    break;
            }

           

            this.drawerListView.SetItemChecked(position, true);
            SupportActionBar.Title = this.title = Sections[position];
            this.drawerLayout.CloseDrawers();
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {

            var drawerOpen = this.drawerLayout.IsDrawerOpen((int)GravityFlags.Left);
            //when open don't show anything
            for (int i = 0; i < menu.Size(); i++)
                menu.GetItem(i).SetVisible(!drawerOpen);


            return base.OnPrepareOptionsMenu(menu);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            this.drawerToggle.SyncState();
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
            // Save the user's current game state
            savedInstanceState.PutInt("idFrameOpen", 1);
           

            // Always call the superclass so it can save the view hierarchy state
            base.OnSaveInstanceState(savedInstanceState);
        }

        private void OnItemMessage(FirebaseEvent<PedidoFireBase> message)
        {




            Intent intent = new Intent(this,typeof(HomeView));

            //intent.SetAction("OPEN_TAB_1");
            //intent.SetComponent.set
            //Intent intent = new Intent();

            //TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);

            //// Add all parents of SecondActivity to the stack: 
            //stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(ListadoMonitorizacion)));

            //// Push the intent that starts SecondActivity onto the stack:
            //stackBuilder.AddNextIntent(intent);




            const int pendingIntentId = 0;
            //this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, message.Object.descripcion, ToastLength.Short).Show());
            PendingIntent pendingIntent = PendingIntent.GetActivity(this, pendingIntentId, intent, PendingIntentFlags.UpdateCurrent);
            if (message.EventType == FirebaseEventType.InsertOrUpdate)
                this.RunOnUiThread(() => {
                if (message.EventType == FirebaseEventType.InsertOrUpdate)
                {
                    this.RunOnUiThread(() => Toast.MakeText(this, message.Object.descripcion, ToastLength.Short).Show());
                        Notification.Builder builder = new Notification.Builder(this)
                        
                            .SetContentIntent(pendingIntent)
                            .SetContentTitle(message.Object.codPedido)
                            .SetContentText(message.Object.descripcion)
                            .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                            .SetSmallIcon(Resource.Drawable.carretilla);
                            

                    // Build the notification:
                    Notification notification = builder.Build();

                    // Get the notification manager:
                    NotificationManager notificationManager =
                        GetSystemService(Context.NotificationService) as NotificationManager;

                    // Publish the notification:
                   // const int notificationId = 0;
                    notificationManager.Notify(message.Object.codPedido,0, notification);
                      //  ListItemClicked(1);
                }
              
            });
            
            
        }
    }
}