using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ModelDataTRH.Proyectos.Ventas;
using ModelDataTRH.Ventas;


namespace AlmacenRepuestosXamarin.Adapter


{
    


    public class AdapterNuevaOferta : BaseAdapter<Cliente>, IFilterable
    {
        Activity context;
        public List<Cliente> _originalData;
        public List<Cliente> list;

        public AdapterNuevaOferta(Activity _context, List<Cliente> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
            Filter = new ClientesFilter(this);
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

        public class ClientesFilter : Filter
        {

            private readonly AdapterNuevaOferta _adapter;
            public ClientesFilter(AdapterNuevaOferta adapter)
            {
                _adapter = adapter;
            }



            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Cliente>();
                if (_adapter._originalData == null)
                    _adapter._originalData = _adapter.list;

                var textoFiltro = (Java.Lang.Object)constraint;

                if (constraint == null) return returnObj;

                if (_adapter._originalData != null && _adapter._originalData.Any() )
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.


                    results.AddRange(
                        _adapter._originalData.Where(
                            list => list.nameField.ToLower().RemoveDiacritics().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.list = values.ToArray<Java.Lang.Object>().Select(r => r.ToNetObject<Cliente>()).ToList();

                _adapter.NotifyDataSetChanged();

            }
        }

        public override Cliente this[int position]
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

            Cliente item = this[position];
            view.FindViewById<TextView>(Resource.Id.txt1).Text = item.nameField.ToString();
            if (item.saldoField <= 0)
            {
                view.FindViewById<TextView>(Resource.Id.txt2).SetTextColor(Color.Red);
            }
            else {
                view.FindViewById<TextView>(Resource.Id.txt2).SetTextColor(Color.Green);
            }
            
            view.FindViewById<TextView>(Resource.Id.txt2).Text = "Saldo: " + string.Format(cultureInfo, "{0:C}", item.saldoField);
            view.FindViewById<TextView>(Resource.Id.txt3).Text = "Cr�dito Max.: " + string.Format(cultureInfo, "{0:C}", item.cred_Max_CESCEField);

            return view;
        }
    }
}