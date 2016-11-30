using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Clases;
using AlmacenRepuestosXamarin.Data;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Lang;
using ModelDataTRH.Proyectos.Ventas;
using ModelDataTRH.Ventas;
using static AlmacenRepuestosXamarin.Clases.SlidingTabsFragment;

namespace AlmacenRepuestosXamarin.Activities
{
    [Activity(Label = "Nueva Oferta", LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/TRH", ScreenOrientation = ScreenOrientation.Portrait)]
    public class NuevaOfertaActivity : BaseActivity , ViewPager.IOnPageChangeListener , Android.Widget.NumberPicker.IOnValueChangeListener
    {
        LinearLayout progressLayout;
        private SlidingTabScrollView mSlidingTabScrollView;
        private ViewPager mViewPager;
        List<string> data;
        private int tabSeleccionado;
        public NumberPicker np;
        public TextView cantidadProductoSeleccionado;
        public string[] nombreProductosSeleccionados;
        public Button btnMaxPila1;
        public Button btnMinPila1;
        public Button btnMaxPila2;
        public Button btnMinPila2;
        public LinearLayout pila1;
        public LinearLayout pila2;
        //static SPagerAdapter adapter;
        bool flag = false;

        IMenuItem item;
        IMenu _menu;
        private Android.Support.V7.Widget.SearchView _searchView;
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.OpcionesNuevaOfertaCabeceraLayout;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {

            
            base.OnCreate(savedInstanceState);
            mSlidingTabScrollView = FindViewById<SlidingTabScrollView>(Resource.Id.sliding_tabs);
            
            mViewPager = FindViewById<ViewPager>(Resource.Id.viewpager);            
            this.mViewPager.AddOnPageChangeListener(this);
            data = new List<string>();
            data.Add(" Opciones ");
            data.Add(" Productos ");
            data.Add(" Carga ");            
            data.Add(" Precios ");
            data.Add(" Incidencias ");
            mViewPager.OffscreenPageLimit = data.Count()-1;
            mViewPager.Adapter = new SPagerAdapter(this, mViewPager,data);
            
            mSlidingTabScrollView.ViewPager = mViewPager;

        }

        #region Metodos Menú
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _menu = menu;
            SupportActionBar.SetTitle(Resource.String.Opciones_Oferta);
            MenuInflater.Inflate(Resource.Menu.soloMenuCambioDestino, _menu);
           
            return base.OnCreateOptionsMenu(_menu);
        }

        

        public override bool OnOptionsItemSelected(IMenuItem item)
        {


            switch (item.ItemId)
            {
                case Resource.Id.cambiarDestino:
                    break;
 
                default:
                    //Finish();

                    break;
            }
            return base.OnOptionsItemSelected(item);

        }
        public void addItem(IMenuItem item)
        {
            SPagerAdapter adapter = (SPagerAdapter)(mViewPager.Adapter);
            var searchView = MenuItemCompat.GetActionView(item);
            _searchView = searchView.JavaCast<Android.Support.V7.Widget.SearchView>();
            _searchView.QueryTextChange += (s, e) => {
                this.RunOnUiThread(() => {

                    switch (tabSeleccionado)
                    {
                        case 1:
                            adapter.adapterListadoProductos.Filter.InvokeFilter(new Java.Lang.String(e.NewText.ToString()));
                            adapter.NotifyDataSetChanged();
                            break;
                        default:
                            break;
                    }
                    adapter.NotifyDataSetChanged();
                });
            };

            _searchView.QueryTextSubmit += (s, e) =>
            {
                e.Handled = true;
            };
        }
        #endregion

        #region Metodos Implementados Interfaz
        public void OnPageScrollStateChanged(int state)
        {
            if (state == 0)
            {
                switch (tabSeleccionado)
                {
                    case 0:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Opciones_Oferta);
                        MenuInflater.Inflate(Resource.Menu.soloMenuCambioDestino, _menu);

                        break;
                    case 1:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Listado_Productos);
                        MenuInflater.Inflate(Resource.Menu.menuNuevaOfertaViewpagerLayout, _menu);
                        item = _menu.FindItem(Resource.Id.action_search_viewpager);
                        addItem(item);


                        break;
                    case 2:
                        
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Carga);
                        SPagerAdapter adapter = (SPagerAdapter)(mViewPager.Adapter);
                        np = new NumberPicker(this);
                        np = this.FindViewById<NumberPicker>(Resource.Id.numberPickerProductos);
                        cantidadProductoSeleccionado = this.FindViewById<TextView>(Resource.Id.textView2);

                        btnMaxPila1 = this.FindViewById<Button>(Resource.Id.buttonMaxPila1);
                        btnMinPila1 = this.FindViewById<Button>(Resource.Id.buttonMinPila1);
                        btnMaxPila2 = this.FindViewById<Button>(Resource.Id.buttonMaxPila2);
                        btnMinPila2 = this.FindViewById<Button>(Resource.Id.buttonMinPila2);

                        if (!flag) { 
                            btnMaxPila1.Click += (sender, e) => maxMinBtnClickPilas("maxPila1", adapter);
                            btnMinPila1.Click += (sender, e) => maxMinBtnClickPilas("minPila1", adapter);
                            btnMaxPila2.Click += (sender, e) => maxMinBtnClickPilas("maxPila2", adapter);
                            btnMinPila2.Click += (sender, e) => maxMinBtnClickPilas("minPila2", adapter);
                        }
                        flag = true;
                        pila1 = this.FindViewById<LinearLayout>(Resource.Id.linearLayoutPila1);
                        pila2 = this.FindViewById<LinearLayout>(Resource.Id.linearLayoutPila2);

                        if (adapter.listProductosSeleccionados.Count() > 0)
                            {
                            np.WrapSelectorWheel = true;
                            np.SetOnValueChangedListener(this);
                            np.DescendantFocusability = DescendantFocusability.BlockDescendants;

                                nombreProductosSeleccionados = new string[adapter.listProductosSeleccionados.Count()];
                                int position = -1;
                                foreach (var item in adapter.listProductosSeleccionados)
                                {
                                    position++;
                                    nombreProductosSeleccionados[position] += item.search_DescriptionField;
                                    
                                }
                            np.MinValue = 0;
                            if (np.MaxValue > nombreProductosSeleccionados.Count()-1) {
                                np.MaxValue = nombreProductosSeleccionados.Count()-1;
                            }
                            np.SetDisplayedValues(nombreProductosSeleccionados);
                            np.MaxValue = nombreProductosSeleccionados.Count()-1;

                            string name = nombreProductosSeleccionados[np.Value].ToString();
                            var cantidad = (from p in adapter.listProductosSeleccionados where p.search_DescriptionField == name select p.cantidadSeleccionada).First();
                            cantidadProductoSeleccionado.Text = cantidad.ToString();

                        }
                        else
                            {

                            np.MinValue = 0;
                            if (np.MaxValue > 0)
                            {
                                np.MaxValue = 0;
                            }
                            np.SetDisplayedValues(new string[] { "" });
                            np.MaxValue = 0;
                            cantidadProductoSeleccionado.Text = "0";
                                //np.SetDisplayedValues(new string[] { "" });
                            }
                        
                        
                        break;
                    case 3:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Precios);
                        break;
                    case 4:
                        _menu.Clear();
                        SupportActionBar.SetTitle(Resource.String.Incidencias);
                        MenuInflater.Inflate(Resource.Menu.soloMenuCambioDestino, _menu);
                        break;
                    default:
                        break;
                }
            }
        }


        private void maxMinBtnClickPilas(string v, SPagerAdapter adapter)
        {
            double cantidad = 0;
            string name = nombreProductosSeleccionados[np.Value].ToString();
            var cantidadReal = (from p in adapter.listProductosSeleccionados where p.search_DescriptionField == name select p.cantidadSeleccionada).First();
            switch (v)
            {
                case "maxPila1":
                    
                    double.TryParse(cantidadProductoSeleccionado.Text,out cantidad);
                    if (cantidad > 0)
                    {
                        cantidad--;
                        cantidadProductoSeleccionado.Text = cantidad.ToString();
                        añadirTxtPila(1);
                    }
                    else {
                        Toast.MakeText(this, "NO DISPONE DE MAS PAQUETES DE ESTE PRODUCTO!!!", ToastLength.Long);
                    }
                    break;

                case "minPila1":
                    double.TryParse(cantidadProductoSeleccionado.Text, out cantidad);
                    string nombrePaquetePila1  = nombreProductosSeleccionados[np.Value].ToString();
                    int numeroPaquetesPila = pila1.ChildCount;
                    var paqueteABorrarPila1 = (TextView)pila1.GetChildAt(numeroPaquetesPila-1);
                    if (paqueteABorrarPila1 != null) { 
                        if (cantidad < cantidadReal && nombrePaquetePila1.Equals(paqueteABorrarPila1.Text))
                        {
                            cantidad++;
                            cantidadProductoSeleccionado.Text = cantidad.ToString();
                            pila1.RemoveView(paqueteABorrarPila1);
                        }
                    
                        else {
                            cantidad++;
                            pila1.RemoveView(paqueteABorrarPila1);
                        }
                    }
                    break;

                case "maxPila2":
                    double.TryParse(cantidadProductoSeleccionado.Text, out cantidad);
                    if (cantidad > 0)
                    {
                        cantidad--;
                        cantidadProductoSeleccionado.Text = cantidad.ToString();
                        añadirTxtPila(2);
                    }
                    else
                    {
                        Toast.MakeText(this, "NO DISPONE DE MAS PAQUETES DE ESTE PRODUCTO!!!", ToastLength.Long);
                    }
                    break;
                case "minPila2":
                    double.TryParse(cantidadProductoSeleccionado.Text, out cantidad);
                    string nombrePaquetePila2 = nombreProductosSeleccionados[np.Value].ToString();
                    int numeroPaquetesPila2 = pila2.ChildCount;
                    var paqueteABorrarPila2 = (TextView)pila2.GetChildAt(numeroPaquetesPila2 - 1);
                    if(paqueteABorrarPila2 != null) { 
                        if (cantidad < cantidadReal && nombrePaquetePila2.Equals(paqueteABorrarPila2.Text))
                        {
                            cantidad++;
                            cantidadProductoSeleccionado.Text = cantidad.ToString();
                            pila2.RemoveView(paqueteABorrarPila2);
                        }
                    
                        else
                        {
                            cantidad++;
                            pila2.RemoveView(paqueteABorrarPila2);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void añadirTxtPila(int v)
        {
            TextView addPaquete;
           
            switch (v)
            {
                
                case 1:
                    addPaquete = new TextView(this);                    
                    addPaquete.Text = nombreProductosSeleccionados[np.Value].ToString();
                    addPaquete.Gravity = GravityFlags.Center;
                    addPaquete.SetBackgroundColor(Android.Graphics.Color.Beige);
                    pila1.AddView(addPaquete);
                    break;

                case 2:
                    addPaquete = new TextView(this);
                    addPaquete.Text = nombreProductosSeleccionados[np.Value].ToString();
                    addPaquete.Gravity = GravityFlags.Center;
                    addPaquete.SetBackgroundColor(Android.Graphics.Color.Beige);
                    pila2.AddView(addPaquete);
                    break;

                default:
                    break;
            }


        }
        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {

        }
        public void OnPageSelected(int position)
        {
            tabSeleccionado = position;
        }

        public void OnValueChange(NumberPicker picker, int oldVal, int newVal)
        {
            int valor = 0;
            SPagerAdapter adapter = (SPagerAdapter)(mViewPager.Adapter);
            valor = np.Value;
            var a = np.GetHashCode();
            string name = nombreProductosSeleccionados[valor].ToString();
            var cantidad = (from p in adapter.listProductosSeleccionados where p.search_DescriptionField == name select p.cantidadSeleccionada).First();
            cantidadProductoSeleccionado.Text = cantidad.ToString();
        }
        #endregion

        #region Adaptador de activity NuevaOferta
        public class SPagerAdapter : PagerAdapter, ViewPager.IOnPageChangeListener 
        {
            private ViewPager _mViewPager;
            private Toolbar toolBar;
            public AdapterListadoProductos adapterListadoProductos;
            public AdapterPreciosNuevaOferta adapterPreciosNuevaOferta;
            private AdapterNuevaOferta adapterNuevaOferta;
            private List<string> items = new List<string>();
            private Activity context;
            public ListView nuevaOfertaListview;
            public ListView productosOferta;
            public List<Cliente> listClientes = new List<Cliente>();
            public List<Producto> listProductosSeleccionados = new List<Producto>();
            private LinearLayout progressLayout;
            private IMenuItem item;
            private int tabSeleccionado = 0;
            private RadioButton rbDobleDescargaSi;
            private RadioButton rbDobleDescargaNo;
            private RadioButton rbTipoTransporteSi;
            private RadioButton rbTipoTransporteNo;
            private RadioButton rbCamionGruaSi;
            private RadioButton rbCamionGruaNo;
            List<string> _data;


           
            public SPagerAdapter(Activity context, ViewPager mViewPager, List<string> data) : base()
            {
                _data = data;
                _mViewPager = mViewPager;
                this.context = context;
                this._mViewPager.AddOnPageChangeListener(this);
            }



            public override int Count
            {
                get { return _data != null ? _data.Count : 0; }
            }


            public override int GetItemPosition(Java.Lang.Object objectValue)
            {
                return base.GetItemPosition(objectValue);
            }
            public override bool IsViewFromObject(View view, Java.Lang.Object obj)
            {
                return view == obj;
            }

            public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
            {
                string title = string.Empty;
                SlidingTabsFragment stf;
                View view = LayoutInflater.From(container.Context).Inflate(Resource.Layout.nuevaOfertaOpcionesLayout, container, false);
                LayoutInflater inflater = LayoutInflater.From(container.Context);

                switch (position)
                {

                    case 0:
                        view = inflater.Inflate(Resource.Layout.nuevaOfertaOpcionesLayout, container, false);
                        break;

                    case 1:
                        view = inflater.Inflate(Resource.Layout.pagerItemOfertaSinHeader, container, false);
                        nuevaOfertaListview = view.FindViewById<ListView>(Resource.Id.listViewNuevaOfertaFragment);
                        progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);
                        adapterListadoProductos = new AdapterListadoProductos(this.context, AccesoDatos.listaProductos);
                        
                        nuevaOfertaListview.Adapter = adapterListadoProductos;
                        break;

                    case 2:
                        view = inflater.Inflate(Resource.Layout.cargaProductos, container, false);
                        
                        //calcularCamionesParaOferta(adapterListadoProductos.list);
                        
                        break;
                    case 3:
                        view = inflater.Inflate(Resource.Layout.layoutNuevaOfertaPrecios, container, false);
                        nuevaOfertaListview = view.FindViewById<ListView>(Resource.Id.listViewNuevaOfertaPrecios);
                        progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBar);
                        adapterPreciosNuevaOferta = new AdapterPreciosNuevaOferta(this.context, AccesoDatos.listaProductos);

                        nuevaOfertaListview.Adapter = adapterPreciosNuevaOferta;
                        break;

                    default:
                        break;
                }
                ((ViewPager)container).AddView(view);


                return view;
            }

            private void calcularCamionesParaOferta(List<Producto> list)
            {
                double peso = 0;
                double altura = 0;
                foreach (var item in list)
                {
                    if (item.seleccionado) {
                        peso += item.cantidadSeleccionada * item.kgs_PaqueteField;
                        altura += item.cantidadSeleccionada * item.altura_PaqueteField;
                    }
                }
            }

            public string GetHeaderTitle(int position)
            {
                return _data[position];
            }

            public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object obj)
            {
                container.RemoveView((View)obj);
            }

            public void OnPageScrollStateChanged(int state)
            {
                switch (tabSeleccionado)
                {
                    case 0:
                        break;
                    case 1:
                        listProductosSeleccionados.Clear();
                        foreach (var item in adapterListadoProductos.list)
                        {
                            if (item.seleccionado == true)
                            {
                                listProductosSeleccionados.Add(item);
                            }
                        }
                        break;
                    case 2:
                        //NumberPicker np = this.context.FindViewById<NumberPicker>(Resource.Id.numberPickerProductos);
                        

                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }

            public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {

            }

            public void OnPageSelected(int position)
            {
                tabSeleccionado = position;
            }
        }
       
        #endregion


        //private class SearchViewExpandListener
        //  : Java.Lang.Object, MenuItemCompat.IOnActionExpandListener
        //{
        //    private readonly IFilterable _adapter;


        //    public SearchViewExpandListener(IFilterable adapter)
        //    {
        //        _adapter = adapter;
        //    }

        //    public bool OnMenuItemActionCollapse(IMenuItem item)
        //    {

        //        _adapter.Filter.InvokeFilter(" ");
        //        //_adapter.Filter.InvokeFilter();
        //        return true;
        //    }

        //    public bool OnMenuItemActionExpand(IMenuItem item)
        //    {
        //        return true;
        //    }
        //}
    }
}