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
using static Android.Support.V7.Widget.RecyclerView;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterListadoProductos : BaseAdapter<Producto>, IFilterable, View.IOnFocusChangeListener
    {

        Activity context;
        public List<Producto> _originalData;
        public List<Producto> list;
        private bool seleccionado = false;
        private CheckBox cbChoice;
        private EditText numPaquetes;
        private int numero =0 ;
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

        private class ViewHolder : Java.Lang.Object
        {
            public TextView tv1;
            public TextView tv2;
            public TextView tv3;
            public TextView tv4;
            public CheckBox cb1;
            public Button btn1;
            public Button btn2;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
         
            ViewHolder holder = null;
            var view = convertView;

            if (view != null)
                holder = view.Tag as ViewHolder;

            if (holder == null)
            {
                holder = new ViewHolder();
                view = context.LayoutInflater.Inflate(Resource.Layout.RowListEleccionProductos, null);
                holder.tv1 = view.FindViewById<TextView>(Resource.Id.text1);
                holder.tv2 = view.FindViewById<TextView>(Resource.Id.text2);
                holder.tv3 = view.FindViewById<TextView>(Resource.Id.text3);
                holder.tv4 = view.FindViewById<TextView>(Resource.Id.textNumPaquetes);
                holder.cb1 = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
                holder.cb1.Click += (sender, e) => CheckItemClick(ref view);
                holder.btn1 = view.FindViewById<Button>(Resource.Id.btn1);
                holder.btn1.Click += (sender, e) => minItemClick(ref view);
                holder.btn2 = view.FindViewById<Button>(Resource.Id.btn2);
                holder.btn2.Click += (sender, e) => sumItemClick(ref view);

                // view.Tag = holder;
            }
            else {

                holder.cb1.Checked = holder.cb1.Checked;
                holder.tv4.Text = holder.tv4.Text;
            }

            Producto item = this[position];

            holder.tv1.Text = item.search_DescriptionField;
            holder.tv2.Text = item.stockDisponibleField.ToString();
            holder.tv3.Text = item.paquetes_por_CamiónField.ToString();
            holder.tv4.OnFocusChangeListener = this;
            //holder.et1.Click += (sender, e) => CheckEditClick(holder);
          


            view.Tag = holder;

            return view;
            
        }

        private void minItemClick(ref View view)
        {
            ViewHolder holder = view.Tag as ViewHolder;
            int.TryParse(holder.tv4.Text, out numero);
            if (numero != 0) {
                numero--;
                holder.tv4.Text = numero.ToString();
            }
            
        }

        private void sumItemClick(ref View view)
        {
            ViewHolder holder = view.Tag as ViewHolder;
            int.TryParse(holder.tv4.Text,out numero);
            numero++;
            holder.tv4.Text = numero.ToString(); 
        }

        private void CheckEditClick(View holder)
        {
            
        }

        private void CheckItemClick(ref View view)
        {
           ViewHolder holder = view.Tag as ViewHolder;
            if (holder.cb1.Checked)
            {
                if (holder.tv4.Text.Equals(string.Empty)) { 
                    holder.tv4.Text = "1";
                    holder.tv4.Enabled = true;
                    holder.btn1.Enabled = true;
                    holder.btn2.Enabled = true;
                }
            }
            else 
            {
                holder.tv4.Text = "";
                holder.tv4.Enabled = false;
                holder.btn1.Enabled = false;
                holder.btn2.Enabled = false;                    
            }
            holder.tv4.ClearFocus();
            view.Tag = holder;
        }

        public void OnFocusChange(View v, bool hasFocus)
        {
            Toast.MakeText(this.context, "No me toques", ToastLength.Long);
        }



        //private void accionClick(View view,int position)
        //{
        //    bool seleccionado = view.FindViewById<CheckBox>(Resource.Id.checkBox1).Checked;
        //    string numPaquetes = view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text;


        //    if (seleccionado && numPaquetes.Equals(""))
        //    {
        //        view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text = "1";
        //    }
        //    else if(!seleccionado)
        //    {
        //        view.FindViewById<EditText>(Resource.Id.editTextNumPaquete).Text = "";
        //    }        
        //}
    }
}