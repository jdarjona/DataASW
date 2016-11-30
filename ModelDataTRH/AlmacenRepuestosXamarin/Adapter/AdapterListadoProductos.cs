using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
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
            public TextView tv5;
            public TextView tv6;
            public TextView tv7;

            //public CheckBox cb1;
            public Button btn1;
            public Button btn2;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var shape = new ShapeDrawable(new RectShape());
            shape.Paint.Color = Color.DarkGray;
            shape.Paint.StrokeWidth = 1;
            shape.Paint.SetStyle(Paint.Style.Stroke);
            
            
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
                holder.tv4 = view.FindViewById<TextView>(Resource.Id.text4);
                holder.tv5 = view.FindViewById<TextView>(Resource.Id.text5);
                holder.tv6 = view.FindViewById<TextView>(Resource.Id.text6);
                holder.tv7 = view.FindViewById<TextView>(Resource.Id.textNumPaquetes);
                holder.tv7.SetBackgroundDrawable(shape);
                //holder.cb1 = view.FindViewById<CheckBox>(Resource.Id.checkBox1);
                //holder.cb1.Click += (sender, e) => CheckItemClick(ref view);
                holder.btn1 = view.FindViewById<Button>(Resource.Id.btn1);
                holder.btn1.Click += (sender, e) => minItemClick(ref view,position);
                holder.btn2 = view.FindViewById<Button>(Resource.Id.btn2);
                holder.btn2.Click += (sender, e) => sumItemClick(ref view,position);

                // view.Tag = holder;
            }
            else {

               // holder.cb1.Checked = holder.cb1.Checked;
                holder.tv7.Text = holder.tv7.Text;
            }

            Producto item = this[position];

            holder.tv1.Text = item.search_DescriptionField;
            holder.tv2.Text = "PAQ. Disp.: " + item.stockDisponibleField.ToString();
            holder.tv3.Text = "PAQ. Camión: " + item.paquetes_por_CamiónField.ToString();
            holder.tv4.Text = (item.cantidadSeleccionada * item.kgs_PaqueteField).ToString() + " KG";
            holder.tv5.Text = (item.cantidadSeleccionada * item.paños_x_PaqueteField).ToString() + " PAÑOS";
            holder.tv6.Text = (item.cantidadSeleccionada * item.m2_PaqueteField).ToString() + " M2";
            holder.tv7.OnFocusChangeListener = this;
            //holder.et1.Click += (sender, e) => CheckEditClick(holder);
          


            view.Tag = holder;

            return view;
            
        }

        private void minItemClick(ref View view,int position)
        {
            ViewHolder holder = view.Tag as ViewHolder;
            int.TryParse(holder.tv7.Text, out numero);
            if (numero > 1) {
                numero--;
                holder.tv7.Text = numero.ToString();
               
                list[position].seleccionado = true;
                list[position].cantidadSeleccionada = numero;
                list[position].altura_PaqueteField = 30;
                holder.tv4.Text = (list[position].cantidadSeleccionada * list[position].kgs_PaqueteField).ToString() + " KG";
                holder.tv5.Text = (list[position].cantidadSeleccionada * list[position].paños_x_PaqueteField).ToString() + " PAÑOS";
                holder.tv6.Text = (list[position].cantidadSeleccionada * list[position].m2_PaqueteField).ToString() + " M2";

            } else if (numero <= 1) {
                holder.tv7.Text = string.Empty;
                holder.tv4.Text = "0 KG";
                holder.tv5.Text = "0 PAÑOS";
                holder.tv6.Text = "0 M2";
                list[position].seleccionado = false;
                list[position].cantidadSeleccionada = 0;
                list[position].altura_PaqueteField = 0;
            }           
        }

        private void sumItemClick(ref View view,int position)
        {
            ViewHolder holder = view.Tag as ViewHolder;
            int.TryParse(holder.tv7.Text,out numero);
            numero++;
            holder.tv7.Text = numero.ToString();
            
            list[position].seleccionado = true;
            list[position].cantidadSeleccionada = numero;
            list[position].altura_PaqueteField = 15;
            holder.tv4.Text = (list[position].cantidadSeleccionada * list[position].kgs_PaqueteField).ToString() + " KG";
            holder.tv5.Text = (list[position].cantidadSeleccionada * list[position].paños_x_PaqueteField).ToString() + " PAÑOS";
            holder.tv6.Text = (list[position].cantidadSeleccionada * list[position].m2_PaqueteField).ToString() + " M2";
        }

        public void OnFocusChange(View v, bool hasFocus)
        {
            Toast.MakeText(this.context, "No me toques", ToastLength.Long);
        }

    }
}