using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Views;
using Android.Widget;
using RepositoryWebServiceTRH.EmpleadoContext;
using Java.Lang;
using Object = Java.Lang.Object;
using System.Globalization;
using System;
using AlmacenRepuestosXamarin.Adapter;
using System.Text;

namespace AlmacenRepuestosXamarin.Adapter
{
    public static class ExtensionString {
        public static string RemoveDiacritics(this string text)
        {
            return string.Concat(
                text.Normalize(NormalizationForm.FormD)
                .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                              UnicodeCategory.NonSpacingMark)
              ).Normalize(NormalizationForm.FormC);
        }
    }
    public class AdapterEmpleados : BaseAdapter<Empleados>, IFilterable
    {
        private List<Empleados> _originalData;
        public Activity context;
        public List<Empleados> list;

        public AdapterEmpleados(Activity _context, List<Empleados> _list):base()
            
        {
            this.context = _context;
            this.list = _list;

            Filter = new EmpleadosFilter(this);
        }

        public override void NotifyDataSetChanged()
        {
            // If you are using cool stuff like sections
            // remember to update the indices here!

            
            base.NotifyDataSetChanged();
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public Filter Filter { get;  set; }

       

        public class EmpleadosFilter : Filter
        {
            private readonly AdapterEmpleados _adapter;
            public EmpleadosFilter(AdapterEmpleados adapter)
            {
                _adapter = adapter;
            }

           

            protected  override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Empleados>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter.list;

                var textoFiltro=(Java.Lang.Object)constraint;
                //if (constraint == null || constraint.Length() > 0)
                //var textoFiltro = ObjectExtensions.ToJavaObject<Java.Lang.ICharSequence>(constraint);//ObjectExtensions.ToNetObject<System.String>();

               

                if (textoFiltro==null)
                {
                    constraint = new Java.Lang.String(" "); 
                }
                if (_adapter._originalData != null && _adapter._originalData.Any() && constraint.Length() > 0)
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.

                    
                    results.AddRange(
                        _adapter._originalData.Where(
                            list => list.FullName.ToLower().RemoveDiacritics().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values =  FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                //constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.list = values.ToArray<Object>().Select(r => r.ToNetObject<Empleados>()).ToList();

               _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                //constraint.Dispose();
                //results.Dispose();
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Empleados this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.spinnerLayout, parent, false);

            Empleados item = this[position];
            string text= CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.FullName.ToLower());
            view.FindViewById<TextView>(Resource.Id.textoSpinner).Text = text;




            return view;
        }
    }
}
