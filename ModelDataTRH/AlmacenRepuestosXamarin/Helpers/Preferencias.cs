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
        const string urlLocalLieja= "urlLocalLieja";
        const string urlLocalSevilla = "urlLocalSevilla";
        const string urlPublicaLieja = "urlPublicaLieja";
        const string ipPublicaSevilla = "urlPublicaSevilla";
        const string empresaSevilla = "Sevilla";
        const string empresaLiege = "Liege";
        public static string[] listSSIDLiege { get; set; }
        public static string[] listSSIDSevilla { get; set; }

        public Preferencias(Activity context) {

            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editPreference = prefs.Edit();

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


    }
}