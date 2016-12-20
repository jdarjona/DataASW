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

using Java.Interop;
using Dalvik;
using Newtonsoft.Json;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "TRH", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/TRH", ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : Activity
    {
        public static string token = string.Empty;
        private Spinner spinner;
        private Button button;
        private View view;
        private EditText usuario;
        private EditText password;
        private LinearLayout progressLayout;
        public static string empresaSeleccionada;
        public static readonly string[] empresas = { "Sevilla", "Liege" };
        public static string comercial = "V005";
        AccesoDatos ad;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
            Helpers.Preferencias preferencias = new Preferencias(this);
            SetContentView(Resource.Layout.LoginLayout);
            button = FindViewById<Button>(Resource.Id.btnAceptarLogin);
            usuario = FindViewById<EditText>(Resource.Id.editTextUser);
            password = FindViewById<EditText>(Resource.Id.editTextPass);
            spinner = FindViewById<Spinner>(Resource.Id.spinnerLoginEmpresa);
            progressLayout = FindViewById<LinearLayout>(Resource.Id.progressBarLogin);
            progressLayout.SetGravity(GravityFlags.Center);
            usuario.Text = Preferencias.getUsuarioApp().ToString();
            password.Text = Preferencias.getPasswordApp().ToString();           

            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, empresas);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            spinner.Adapter = adapter;
            spinner.SetSelection(comprobarEmpresaLogin());
            button.Click += delegate {
                btnOneClick(this.view);
            };
            progressLayout.Visibility = ViewStates.Gone;
            //btnOneClick(this.view);
        }

        private int comprobarEmpresaLogin()
        {
            string emp = Preferencias.getEmpresaLoginApp();
            if (emp.Equals("Sevilla"))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            empresaSeleccionada = spinner.GetItemAtPosition(e.Position).ToString();
            

        }


        public void btnOneClick(View v)
        {
            ad = new AccesoDatos();
            button.Enabled = false;
            usuario.Enabled = false;
            password.Enabled = false;
            spinner.Enabled = false;
            string user =  usuario.Text;
            string pass =  password.Text;
            
            if (usuario.Equals(string.Empty) || pass.Equals(string.Empty))
            {
                button.Enabled = true;
                usuario.Enabled = true;
                password.Enabled = true;
                spinner.Enabled = true;
                Toast.MakeText(this, "Introduzca Usuario y Contraseña!!", ToastLength.Long).Show();
            }
            else
            {
                progressLayout.Visibility = ViewStates.Visible;
                getDatosLogin(user, pass, empresaSeleccionada);                
            }    
        }

        private async void getDatosLogin(string user, string pass, string empresaSeleccionada)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AccesoDatos.UrlToken);
            string contenido = string.Format("grant_type=password&username={0}&password={1}", user, pass);
            HttpResponseMessage response =
              client.PostAsync("Token", new StringContent(contenido, Encoding.UTF8,
                  "application/x-www-form-urlencoded")).Result;

            string resultJSON = response.Content.ReadAsStringAsync().Result;
            if (!resultJSON.Contains("Error de inicio de sesion"))
            {
                
                LoginTokenResult result = JsonConvert.DeserializeObject<LoginTokenResult>(resultJSON);
                token = "Bearer " + result.ToString();
                await ad.initGetListados(empresaSeleccionada);
                if (Preferencias.getUsuarioApp().Equals("") && Preferencias.getPasswordApp().Equals("")){
                    Preferencias pre = new Preferencias(this);
                    pre.putUsuarioApp(user);
                    pre.putPasswordApp(pass);
                    pre.putEmpresaLoginApp(empresaSeleccionada);
                    
                }                
                progressLayout.Visibility = ViewStates.Gone;
                var homeView = new Intent(this, typeof(HomeView));
                StartActivity(homeView);
                this.Finish();
            }
            else
            {
                progressLayout.Visibility = ViewStates.Gone;
                button.Enabled = true;
                usuario.Enabled = true;
                password.Enabled = true;
                spinner.Enabled = true;
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