using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Data;
using AlmacenRepuestosXamarin.Helpers;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Xamarin.Auth;
using Java.Interop;
using Newtonsoft.Json;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "Login", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/TRH", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : Activity
    {
        private Spinner spinner;
        private Button button;
        private View view;
        private EditText usuario;
        private EditText password;
        private string empresaSeleccionada;
        public static readonly string[] empresas = { "Sevilla", "Liege" };
        public static string comercial = "V005";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Helpers.Preferencias preferencias = new Preferencias(this);
            SetContentView(Resource.Layout.LoginLayout);
            AccesoDatos ad = new AccesoDatos();
            button = FindViewById<Button>(Resource.Id.btnAceptarLogin);
            usuario = FindViewById<EditText>(Resource.Id.editTextUser);
            password = FindViewById<EditText>(Resource.Id.editTextPass);
            spinner = FindViewById<Spinner>(Resource.Id.spinnerLoginEmpresa);

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, empresas);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spinner.Adapter = adapter;
            button.Click += delegate {
                btnOneClick(this.view);
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            empresaSeleccionada = spinner.GetItemAtPosition(e.Position).ToString();
            string toast = string.Format("Selected text is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }


        public void btnOneClick(View v)
        {
            string user = usuario.Text;
            string pass = password.Text;

            if (usuario.Equals(string.Empty) || pass.Equals(string.Empty))
            {
                Toast.MakeText(this, "Introduzca Usuario y Contraseña!!", ToastLength.Long).Show();
            }
            else
            {
                getDatosLogin(user, pass, empresaSeleccionada);
            }    
        }

        private async void getDatosLogin(string user, string pass, string empresaSeleccionada)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AccesoDatos.UrlToken);
            HttpResponseMessage response =
              client.PostAsync("Token",
                new StringContent(string.Format("grant_type=password&username={0}&password={1}",
                  user,
                  pass), Encoding.UTF8,
                  "application/x-www-form-urlencoded")).Result;

            string resultJSON = response.Content.ReadAsStringAsync().Result;
            if (!resultJSON.Contains("Error de inicio de sesion"))
            {
                LoginTokenResult result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);
                var homeView = new Intent(this, typeof(HomeView));
                StartActivity(homeView);
               
            }
            else
            {

                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("Error en usuario o contraseña, intentelo de nuevo!!");
                builder.SetPositiveButton("OK", (s, e) => { });
                builder.Create().Show();
                
            }
        }

        public class LoginTokenResult
        {
            public override string ToString()
            {
                return AccessToken;
            }

            [JsonProperty(PropertyName = "access_token")]
            public string AccessToken { get; set; }

            [JsonProperty(PropertyName = "error")]
            public string Error { get; set; }

            [JsonProperty(PropertyName = "error_description")]
            public string ErrorDescription { get; set; }

        }
    }

}