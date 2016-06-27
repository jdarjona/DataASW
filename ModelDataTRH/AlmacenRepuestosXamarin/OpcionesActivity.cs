using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AlmacenRepuestosXamarin
{
    [Activity(Label = "OpcionesActivity")]
   
    public class OpcionesActivity : PreferenceActivity
    {


        public OpcionesActivity()
        {
        }

        protected override void OnCreate(Bundle savedInstance)
        {
            base.OnCreate(savedInstance);

            if (HasHeaders)
            {
                var button = new Button(this);
                button.Text = "Some action";
                this.SetListFooter(button);
            }
        }


        public override void OnBuildHeaders(IList<PreferenceActivity.Header> target)
        {

            LoadHeadersFromResource(Resource.Xml.preferenceHeader, target);
        }
        [Register("AlmacenRepuestos.AlmacenRepuestosXamarin.Prefs1Fragment")]
        public class Prefs1Fragment : PreferenceFragment
        {
            public Prefs1Fragment():base() {

            }
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                // Load the preferences from an XML resource
                AddPreferencesFromResource(Resource.Xml.preferences);
            }
        }
        public class Prefs1FragmentInner : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                // Can retrieve arguments from preference XML.
                Log.Info("args", "Arguments: " + Arguments);

                // Load the preferences from an XML resource
                AddPreferencesFromResource(Resource.Xml.preferences);
            }
        }

        public class Prefs2Fragment : PreferenceFragment
        {
            public override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                // Can retrieve arguments from preference XML.
                Log.Info("args", "Arguments: " + Arguments);

                // Load the preferences from an XML resource
                AddPreferencesFromResource(Resource.Xml.preferences);
            }
        }
    }
}