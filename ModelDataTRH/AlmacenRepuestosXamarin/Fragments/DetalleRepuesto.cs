using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Data;
using AlmacenRepuestosXamarin.Model;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using RepositoryWebServiceTRH.EmpleadoContext;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;

namespace AlmacenRepuestosXamarin.Fragments
{
    public class DetalleRepuesto : Android.Support.V4.App.Fragment
    {
        private LinearLayout progressLayout;
        private AdapterSpinner<Destino> adapterDestinos;
        private AdapterSpinner<Maquina> adapterMaquinas;
        private EditText edittext;
        private Drawable warning;
        private Spinner spinnerDestino;
        private Spinner spinnerMaquina;
        private EntregaAlmacen repuesto;
        private View view;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.HasOptionsMenu = true;

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            view = inflater.Inflate(Resource.Layout.detalleRepuesto, null);


            string key=string.Empty;
            Bundle bundle = this.Arguments;
            if (bundle != null)
            {
                 key = bundle.GetString("idEntregaAlmacen");
            }
            repuesto = ManagerRepuestos.getRepuestoByKey(key);

            
            
            AppCompatActivity activity = (AppCompatActivity)this.Activity;
            activity.SupportActionBar.Title = string.Format(@"Producto: {0}", repuesto.Cod_Producto);
            activity.SupportActionBar.Subtitle = string.Format(@"Unidad Medida: {0}", repuesto.Unit_of_Measure_Code);
            edittext = view.FindViewById<EditText>(Resource.Id.textCantidad);



            edittext.TextChanged += Edittext_TextChanged;
            if (repuesto.Cantidad != 0)
            {
                edittext.Text = repuesto.Cantidad.ToString();
            }
            else
            {
                edittext.Text = string.Empty;
            }

            //spinnerDestino.SetSelection(repuesto.) = 0;
            edittext.FocusChange += (sender, args) =>
            {
                bool isFocused = args.HasFocus;
                if (!isFocused)
                {
                    spinner_OnClick(sender);
                }
            };

            spinnerDestino = (Spinner)view.FindViewById(Resource.Id.spinnerDestino);

            spinnerDestino.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerDestino_ItemSelected);
            //spinnerDestino.ItemClick += spinner_OnClick;


            spinnerMaquina = (Spinner)view.FindViewById(Resource.Id.spinnerMaquina);

            spinnerMaquina.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerMaquina_ItemSelected);
            spinnerMaquina.Visibility = ViewStates.Invisible;
            //spinnerMaquina.ItemClick += spinner_OnClick;


            var s = (Destino[])Enum.GetValues(typeof(Destino));

            adapterDestinos = new AdapterSpinner<Destino>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, s);
            // Specify the layout to use when the list of choices appears
            adapterDestinos.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            // Apply the adapter to the spinner
            spinnerDestino.Adapter = adapterDestinos;

            spinnerDestino.Focusable = true;
            spinnerDestino.FocusableInTouchMode = true;
            spinnerDestino.RequestFocus(FocusSearchDirection.Up);

            //string[] Maquinas = new String[] { "M1", "M2", "M3", "M4", "R1", "R2", "R3", "R4", "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8" };

            var arrayMaquinas = (Maquina[])Enum.GetValues(typeof(Maquina));
            adapterMaquinas = new AdapterSpinner<Maquina>(this.Activity, Android.Resource.Layout.SimpleSpinnerItem, arrayMaquinas);
            // Specify the layout to use when the list of choices appears
            adapterDestinos.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            // Apply the adapter to the spinner
            spinnerMaquina.Adapter = adapterMaquinas;
            spinnerMaquina.Focusable = true;
            spinnerMaquina.FocusableInTouchMode = true;
            spinnerMaquina.RequestFocus(FocusSearchDirection.Up);

            TextView textDescription = view.FindViewById<TextView>(Resource.Id.textDescription);
            textDescription.Text = repuesto.Descripcion_Producto;




            Resources.GetDrawable(Android.Resource.Drawable.AlertLightFrame);
            warning = (Drawable)Resources.GetDrawable(Android.Resource.Drawable.AlertLightFrame);


            //spinnerDestino.Click += (object sender, EventArgs e) =>
            //{
            //    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            //    imm.HideSoftInputFromWindow(edittext.WindowToken, 0);
            //};


            Button btoAceptar = view.FindViewById<Button>(Resource.Id.btoAceptar);

            btoAceptar.Click += OnClik_btoAceptar;

            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarDetalle);
            progressLayout.Visibility = ViewStates.Gone;

            if (repuesto.Cantidad != 0)
            {
                spinnerDestino.SetSelection((int)repuesto.Destino);
                spinnerMaquina.SetSelection((int)repuesto.Maquina);
            }

            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {

            inflater.Inflate(Resource.Menu.menuDetalle, menu);

            base.OnCreateOptionsMenu(menu, inflater);

        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (AccesoDatos.estadoConexion())
            {
                switch (item.ItemId)
                {
                    case Resource.Id.eliminar:

                        eliminarRepuesto();
                        //ManagerRepuestos.eliminarRepuesto(repuesto.Key);

                        //Toast.MakeText(this, "Se eliminó el repuesto " + repuesto.Cod_Producto + " de la lista", ToastLength.Short).Show();
                        //Finish();
                        break;

                    default:


                        break;
                }
            }
            else {
                Toast.MakeText(Application.Context, "SIN CONEXION", ToastLength.Long).Show();
            }
            return base.OnOptionsItemSelected(item);
        }
        public void PopBackStack() {
            if (AccesoDatos.estadoConexion())
            {
                validar();
            }
            else {
                Toast.MakeText(this.Activity, "SIN CONEXION", ToastLength.Long).Show();
            }
        }
        private async void OnClik_btoAceptar(object sender, EventArgs e)
        {

            var _btoAceptar = (Button)sender;
            if (AccesoDatos.estadoConexion())
            {
                if (validar())
                {
                    progressLayout.Visibility = ViewStates.Visible;
                    repuesto = await ManagerRepuestos.updateRepuesto(repuesto);
                    progressLayout.Visibility = ViewStates.Gone;



                    FragmentManager.PopBackStack();
                }
                else
                {

                    _btoAceptar.SetError("Introduza una cantidad antes de aceptar", warning);
                }
            }
            else {
                Toast.MakeText(this.Activity, "SIN CONEXION", ToastLength.Long).Show();
            }

        }
        private async Task<bool> eliminarRepuesto()
        {
            if (AccesoDatos.estadoConexion())
            {
                await ManagerRepuestos.eliminarRepuesto(repuesto.Key);

                Toast.MakeText(this.Activity, "Se eliminó el repuesto " + repuesto.Cod_Producto + " de la lista", ToastLength.Short).Show();
                //Finish();
                FragmentManager.PopBackStack();

                return true;
            }else
            {
                Toast.MakeText(this.Activity, "SIN CONEXION", ToastLength.Long).Show();
                return false;
            }

        }
        private void spinner_OnFocus() { }

        private void spinner_OnClick(object sender)
        {

            InputMethodManager inputMethodManager = (InputMethodManager)this.Activity.GetSystemService(Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(edittext.WindowToken, 0);

        }



        private void Edittext_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            var editTextCantidad = (EditText)sender;
            int cantidad;
            int.TryParse(editTextCantidad.Text, out cantidad);
            //GetResources().getDrawable(R.drawable.alert_icon);
            if (cantidad > repuesto.Inventory)
            {
                editTextCantidad.SetError(string.Format(@"La cantidad es mayor que las existencias disponibles, Existencias disponible {0}", this.repuesto.Inventory.ToString("N2")), warning);
                cantidad = 0;
                editTextCantidad.Text = string.Empty;

            }

            repuesto.Cantidad = cantidad;

        }

        #region "Implementacion Interfaz ItemSelectedListener "

        private void spinnerDestino_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;


            //this.RunOnUiThread(() => Toast.MakeText(this, spinner.SelectedItem.ToString(), ToastLength.Short).Show());
            //repuesto.destino = spinner.SelectedItem.ToString();

            repuesto.Destino = (Destino)adapterDestinos.arrayObjets[e.Position];
            this.spinnerMaquina.Visibility = ViewStates.Invisible;
            if (spinner.SelectedItem.ToString() == Destino.Máquina.ToString())
            {
                this.spinnerMaquina.Visibility = ViewStates.Visible;
            }


        }

        private void spinnerMaquina_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

            Spinner spinner = (Spinner)sender;
            repuesto.Maquina = (Maquina)adapterMaquinas.arrayObjets[e.Position];
            // repuesto.maquina=spinner.SelectedItem.ToString();

        }

        private bool validar()
        {
            StringBuilder error = new StringBuilder(); ;
            error.Append(string.Empty);
            bool valido = true;
            if (repuesto.Cantidad <= 0)
            {
                error.Append("Introduzca cantidad");
                error.Append("\n");
                valido = false;


            }
            if (repuesto.Destino == Destino._blank_)
            {
                error.Append("Debe seleccionar un destino");
                error.Append("\n");
                valido = false;


            }
            if (repuesto.Destino == Destino.Máquina && repuesto.Maquina == Maquina._blank_)
            {
                error.Append("Debe seleccionar una maquina");
                error.Append("\n");
                valido = false;


            }

            if (!valido)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this.Activity);
                alert.SetTitle("Registro");
                // alert.SetIcon(Android.Resource.Drawable.ButtonDefault);
                alert.SetMessage(error.ToString());
                alert.SetNeutralButton("Ok", (s, e) => { });
                alert.Show();

            }

            return valido;
        }





        #endregion
    }
}