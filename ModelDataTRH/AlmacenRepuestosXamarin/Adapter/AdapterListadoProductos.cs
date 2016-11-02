using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using Java.Util;
using ModelDataTRH.Proyectos.Ventas;
using static Android.Provider.Settings;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterListadoProductos : BaseAdapter<Producto>, IFilterable 
    {

        Activity context;
        public List<Producto> _originalData;
        public List<Producto> list;
        private bool seleccionado = false;

        public AdapterListadoProductos(Activity _context, List<Producto> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;

            Filter = new ProductosFilter(this);
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

        public class ProductosFilter : Filter
        {

            private readonly AdapterListadoProductos _adapter;
            public ProductosFilter(AdapterListadoProductos adapter)
            {
                _adapter = adapter;
            }       

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Producto>();
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
                            list => list.search_DescriptionField.ToLower().RemoveDiacritics().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.list = values.ToArray<Java.Lang.Object>().Select(r => r.ToNetObject<Producto>()).ToList();

                _adapter.NotifyDataSetChanged();

            }
        }

        public override Producto this[int position]
        {
            get { return list[position]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }


        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            seleccionado = false;
            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListEleccionProductos, parent, false);

            Producto item = this[position];
            
            view.FindViewById<TextView>(Resource.Id.text1).Text = item.search_DescriptionField;
            view.FindViewById<TextView>(Resource.Id.text2).Text = item.stockDisponibleField.ToString();
            view.FindViewById<TextView>(Resource.Id.text3).Text = item.paquetes_por_CamiónField.ToString();
            view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text = "";
            view.FindViewById<CheckBox>(Resource.Id.checkBox1).Checked = false;
            
            view.FindViewById<CheckBox>(Resource.Id.checkBox1).Click += delegate {
                accionClick(view);
            };

            return view;
        }

        private void accionClick(View view)
        {
            bool seleccionado = view.FindViewById<CheckBox>(Resource.Id.checkBox1).Checked;
            string numPaquetes = view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text;
            if (seleccionado && numPaquetes.Equals(""))
            {
                view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text = "1";
            }
            else if(!seleccionado)
            {
                view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text = "";
            }
            
            
        }
    }
}