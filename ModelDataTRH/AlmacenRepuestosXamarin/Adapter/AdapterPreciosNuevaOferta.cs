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
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ModelDataTRH.Proyectos.Ventas;
using static AlmacenRepuestosXamarin.Adapter.AdapterListadoProductos;

namespace AlmacenRepuestosXamarin.Adapter
{
    public class AdapterPreciosNuevaOferta : BaseAdapter<Producto>, IFilterable, View.IOnFocusChangeListener , ITextWatcher //, View.IOnClickListener
    {
        Activity context;
        public List<Producto> _originalData;
        public List<Producto> list;
        private bool seleccionado = false;
        private CheckBox cbChoice;
        private EditText numPaquetes;
        private int numero = 0;

        public AdapterPreciosNuevaOferta(Activity _context, List<Producto> _list) : base()
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

            private readonly AdapterPreciosNuevaOferta _adapter;
            public ProductosFilter(AdapterPreciosNuevaOferta adapter)
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
                view = context.LayoutInflater.Inflate(Resource.Layout.RowPreciosOfertaLayout, null);
                holder.tv1 = view.FindViewById<TextView>(Resource.Id.textProducto);
                holder.tv2 = view.FindViewById<TextView>(Resource.Id.textDescuento);
                holder.tv3 = view.FindViewById<TextView>(Resource.Id.textEurosMcuadrados);
                holder.tv4 = view.FindViewById<TextView>(Resource.Id.textEurosToneladas);
                holder.tv5 = view.FindViewById<TextView>(Resource.Id.textImporte);
                holder.tv2.Click += (sender, e) => OnClick(view, holder.tv2.Tag);
                holder.tv3.Click += (sender, e) => OnClick(view, holder.tv3.Tag);
                holder.tv4.Click += (sender, e) => OnClick(view, holder.tv4.Tag);
                holder.tv5.Click += (sender, e) => OnClick(view, holder.tv5.Tag);

                holder.tv2.AfterTextChanged += (sender, args) => AfterTextChanged(args.Editable);
                holder.tv3.AfterTextChanged += (sender, args) => AfterTextChanged(args.Editable);
                holder.tv4.AfterTextChanged += (sender, args) => AfterTextChanged(args.Editable);
                holder.tv5.AfterTextChanged += (sender, args) => AfterTextChanged(args.Editable);




            }
            
            
            Producto item = this[position];

            holder.tv1.Text = item.search_DescriptionField;
            holder.tv2.Text = "37,5";
            holder.tv3.Text = "0,657";
            holder.tv4.Text = "468,951";
            holder.tv5.Text = "780,52";

            view.Tag = holder;

            return view;

        }

        private void OnClick(View view, Java.Lang.Object tag)
        {
            TextView tv = view as TextView;
            //tv.Text = 
            
        }

        public void OnFocusChange(View v, bool hasFocus)
        {
            Toast.MakeText(this.context, "No me toques", ToastLength.Long);
        }

        public void AfterTextChanged(IEditable s)
        {
           
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
           // throw new NotImplementedException();
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
           // throw new NotImplementedException();
        }

    }

}