using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Clases;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Database;
using ModelDataTRH.Proyectos.Trazabilidad_Generico;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "Sinóptico",  Icon = "@drawable/carretilla" , LaunchMode = LaunchMode.SingleTop , ScreenOrientation = ScreenOrientation.Portrait)] //"Sliding Tab Layout"
    public class SinopticoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.sinoptico);

            //FragmentTransaction transaction = FragmentManager.BeginTransaction();
            //SlidingTabsFragment fragment = new SlidingTabsFragment();
            //transaction.Replace(Resource.Id.sample_content_fragment, fragment);
            //transaction.Commit();
            //initFirebase();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.actionbar_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        //private async void initFirebase()
        //{

        //    Func<Task<string>> authToken = async delegate ()
        //    {
        //        return "XQsDE173GieFhbMUUs2t2OD5eUwZFjjrEsAYbq6B";
        //    };

        //    var firebase = new FirebaseClient(@"https://flickering-fire-4088.firebaseio.com/", authToken);
        //    var items = firebase
        //     .Child(@"Pedidos/TRH Liege")//.OnceAsync<PedidoFireBase>();

        //    .AsObservable<MaquinaFirebase>();

        //     //.Subscribe(OnItemMessage);


        //}
    }
}