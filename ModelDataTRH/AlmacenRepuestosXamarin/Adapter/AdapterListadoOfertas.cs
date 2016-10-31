using System;
using System.Collections.Generic;
using System.Globalization;
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
using ModelDataTRH.Ventas;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterListadoOfertas : BaseAdapter<Oferta>, IFilterable
    {

        Activity context;
        public List<Oferta> _originalData;
        public List<Oferta> list;

        public AdapterListadoOfertas(Activity _context, List<Oferta> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
            Filter = new OfertasFilter(this);
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

        public class OfertasFilter : Filter
        {

            private readonly AdapterListadoOfertas _adapter;
            public OfertasFilter(AdapterListadoOfertas adapter)
            {
                _adapter = adapter;
            }



            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Oferta>();
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
                            list => list.keyField.ToLower().RemoveDiacritics().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.list = values.ToArray<Java.Lang.Object>().Select(r => r.ToNetObject<Oferta>()).ToList();

                _adapter.NotifyDataSetChanged();

            }
        }

        public override Oferta this[int position]
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
            var cultureInfo = CultureInfo.GetCultureInfo("es-ES");
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListNuevaOferta, parent, false);

            Oferta item = this[position];
            view.FindViewById<TextView>(Resource.Id.txt1).Text = item.noField.ToString();
            view.FindViewById<TextView>(Resource.Id.txt2).Text = item.numero_IncidenciasField.ToString();
            view.FindViewById<TextView>(Resource.Id.txt3).Text = item.sell_to_Customer_NameField;

            return view;
        }
    }
}