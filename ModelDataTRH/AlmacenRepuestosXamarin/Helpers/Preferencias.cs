using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AlmacenRepuestosXamarin.Helpers
{
    public  class Preferencias
    {
        static ISharedPreferences prefs;
        ISharedPreferencesEditor editPreference;
        const string urlLocalLieja= "urlLocalLieja";
        const string urlLocalSevilla = "urlLocalSevilla";
        const string urlPublicaLieja = "urlPublicaLieja";
        const string ipPublicaSevilla = "urlPublicaSevilla";
        const string empresaSevilla = "Sevilla";
        const string empresaLiege = "Liege";
        const string usuario = "usuarioApp";
        const string password = "passwordApp";
        const string empresaLogin = "empresaLgoinApp";
        public static string[] listSSIDLiege { get; set; }
        public static string[] listSSIDSevilla { get; set; }


        
        public Preferencias(Activity context) {

            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            editPreference = prefs.Edit();

            listSSIDLiege = context.Resources.GetStringArray(Resource.Array.ListSSIDLiege);
            listSSIDSevilla = context.Resources.GetStringArray(Resource.Array.ListSSIDSevilla);

            if (getUrlLocalLieja().Equals(string.Empty)){

                editPreference.PutString(urlLocalLieja, @"http://192.168.1.2/WSTRH/");
                editPreference.PutString(urlLocalSevilla, @"http://192.168.1.2/WSTRH/");
                editPreference.PutString(urlPublicaLieja, @"http://intranet.trh-be.com/WSTRH/");
                editPreference.PutString(ipPublicaSevilla, @"http://intranet.trh-es.com/WSTRH/");
                editPreference.PutString(empresaSevilla,"Sevilla");
                editPreference.PutString(empresaLiege, "Liege");
                editPreference.Apply();
                var a = getUrlLocalLieja();

            }
            
        }


        #region Metodos Get Preferencias

        public static String getUsuarioApp()
        {
            return prefs.GetString(usuario, string.Empty);
        }
        public static String getPasswordApp()
        {
            return prefs.GetString(password, string.Empty);
        }
        public static String getEmpresaLoginApp()
        {
            return prefs.GetString(empresaLogin, string.Empty);
        }


        public static string getUrlLocalLieja() {

            return prefs.GetString(urlLocalLieja, string.Empty);
        }

        public static string getUrlLocalSevilla()
        {
           return prefs.GetString(urlLocalSevilla, string.Empty);
        }

        public static string getUrlPublicaLieja()
        {
            return prefs.GetString(urlPublicaLieja, string.Empty);
        }

        public static string getUrlPublicaSevilla()
        {
            return prefs.GetString(ipPublicaSevilla, string.Empty);
        }

        public static string getEmpresaSevilla()
        {
            return prefs.GetString(empresaSevilla, string.Empty);
        }

        public static string getEmpresaLiege()
        {
            return prefs.GetString(empresaLiege, string.Empty);
        }

        #endregion

        #region Metodoos Put Preferencias
        public  void putUsuarioApp(string _usuario)
        {
            editPreference.PutString(usuario,_usuario);
            editPreference.Apply();
        }
        public  void putPasswordApp(string _password)
        {
            editPreference.PutString(password, _password);
            editPreference.Apply();
        }

        public void putEmpresaLoginApp(string _empresaLogin)
        {
            editPreference.PutString(empresaLogin, _empresaLogin);
            editPreference.Apply();
        }

        #endregion

    }
}