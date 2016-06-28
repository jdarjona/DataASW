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
        ISharedPreferences prefs;
        const string urlLocalLieja= "urlLocalLieja";
        const string urlLocalSevilla = "urlLocalSevilla";
        const string urlPublicaLieja = "urlPublicaLieja";
        const string ipPublicaSevilla = "urlPublicaSevilla";

        public Preferencias(Activity context) {

            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editPreference = prefs.Edit();

            if (getUrlLocalLieja().Equals(string.Empty)){

                editPreference.PutString(urlLocalLieja, @"http://192.168.1.2/WSTRH");
                editPreference.PutString(urlLocalSevilla, @"http://192.168.1.2/WSTRH");
                editPreference.PutString(urlPublicaLieja, @"http://intranet.trh-be.com/WSTRH");
                editPreference.PutString(ipPublicaSevilla, @"http://intranet.trh-es.com/WSTRH");
                editPreference.Apply();
                var a = getUrlLocalLieja();

            }
            
        }

        public string getUrlLocalLieja() {

            return prefs.GetString(urlLocalLieja, string.Empty);
        }

        public string getUrlLocalSevilla()
        {
           return prefs.GetString(urlLocalSevilla, string.Empty);
        }

        public string getUrlPublicaLieja()
        {
            return prefs.GetString(urlPublicaLieja, string.Empty);
        }

        public string getUrlPublicaSevilla()
        {
            return prefs.GetString(ipPublicaSevilla, string.Empty);
        }

    }
}