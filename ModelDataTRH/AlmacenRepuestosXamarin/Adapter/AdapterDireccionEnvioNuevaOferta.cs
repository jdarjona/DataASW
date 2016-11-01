using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ModelDataTRH.Proyectos.Ventas;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterDireccionEnvioNuevaOferta : BaseAdapter<DireccionEnvio>, IFilterable
    {
        Activity context;
        public List<DireccionEnvio> _originalData;
        public List<DireccionEnvio> list;

        public AdapterDireccionEnvioNuevaOferta(Activity _context, List<DireccionEnvio> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
            Filter = new DireccionEnvioFilter(this);
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

        public Filter Filter
        {
            get; set;
        }

        public class DireccionEnvioFilter : Filter
        {

            private readonly AdapterDireccionEnvioNuevaOferta _adapter;
            public DireccionEnvioFilter(AdapterDireccionEnvioNuevaOferta adapter)
            {
                _adapter = adapter;
            }



            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<DireccionEnvio>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter.list;

                var textoFiltro = (Java.Lang.Object)constraint;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.


                    results.AddRange(
                        _adapter._originalData.Where(
                            list => list.post_CodeField.ToLower().RemoveDiacritics().StartsWith(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.list = values.ToArray<Java.Lang.Object>().Select(r => r.ToNetObject<DireccionEnvio>()).ToList();

                _adapter.NotifyDataSetChanged();

            }
        }

        public override DireccionEnvio this[int position]
        {
            get { return list[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListNuevaOferta, parent, false);

            DireccionEnvio item = this[position];

            view.FindViewById<TextView>(Resource.Id.txt1).Text = item.nameField;
            view.FindViewById<TextView>(Resource.Id.txt2).Text = item.addressField;
            view.FindViewById<TextView>(Resource.Id.txt3).Text = item.countyField + @"/"+ item.cityField+ @"/" + item.post_CodeField;

            return view;
        }

    }
}