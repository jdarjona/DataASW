using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmacenRepuestosXamarin.Adapter;
using AlmacenRepuestosXamarin.Model;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Wdullaer.Swipeactionadapter;
using RepositoryWebServiceTRH.EmpleadoContext;
using RepositoryWebServiceTRH.EntregaAlmacenEpisContext;
using ZXing.Mobile;

namespace AlmacenRepuestosXamarin.Fragments
{
    [Activity(Label = "ListEPISRepuestos", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ListaRepuestosEntrega : Android.Support.V4.App.Fragment, SwipeActionAdapter.ISwipeActionListener
    {

        private List<EntregaAlmacen> listRepuestosEpis;
        private Empleados empleado;
        private AdapterRepuestos adapterRepuestos;
        private SwipeActionAdapter adaptarSwipe;
        private ListView listViewEmpleados;
        private string numeroDocumento = string.Empty;
        private LinearLayout progressLayout;
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
            view = inflater.Inflate(Resource.Layout.ListEPISRepuestos, null);

            var count = ManagerRepuestos.getRepuestos();

            // this.Activity.Title = empleado.FullName;
            empleado = ManagerRepuestos.getEmpleado();
            MobileBarcodeScanner.Initialize(this.Activity.Application);

            listRepuestosEpis = ManagerRepuestos.getRepuestos();
           // listRepuestosEpis.Clear();
            listViewEmpleados = (ListView)view.FindViewById(Resource.Id.listProductos);
            adapterRepuestos = new AdapterRepuestos(this.Activity, listRepuestosEpis);//new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1, listRepuestosEpis);

            // adapterRepuestos;

            adaptarSwipe = new SwipeActionAdapter(adapterRepuestos);

            adaptarSwipe.SetSwipeActionListener(this)
                   .SetDimBackgrounds(true)
                   .SetListView(this.listViewEmpleados);

            listViewEmpleados.Adapter = adaptarSwipe;
            adaptarSwipe.AddBackground(SwipeDirection.DirectionFarLeft, Resource.Menu.row_bg_left_far);
            adaptarSwipe.AddBackground(SwipeDirection.DirectionNormalLeft, Resource.Menu.row_bg_left);
            adaptarSwipe.AddBackground(SwipeDirection.DirectionFarRight, Resource.Menu.row_bg_right_far);
            adaptarSwipe.AddBackground(SwipeDirection.DirectionNormalRight, Resource.Menu.row_bg_right);





            //listViewEmpleados.ItemClick += OnListItemClick;

            var fab = view.FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.floating);
            fab.AttachToListView(listViewEmpleados);
            // Button btoScan = FindViewById<Button>(Resource.Id.btoScanear);
            fab.Click += (object sender, EventArgs e) =>
            {
                var code = launchScaner();
            };

            listViewEmpleados.ItemClick += (sender, e) =>
            {
                //var activityDetalleRepuestoActivity = new Intent(this.Activity, typeof(detalleRepuestoActivity));
                //activityDetalleRepuestoActivity.PutExtra("idEntregaAlmacen", ManagerRepuestos.getRepuestos()[e.Position].Key);
                //StartActivity(activityDetalleRepuestoActivity);

                Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.DetalleRepuesto();

                Bundle bundle = new Bundle();
                bundle.PutString("idEntregaAlmacen", ManagerRepuestos.getRepuestos()[e.Position].Key);
                fragment.Arguments = bundle;
                
                FragmentManager.BeginTransaction()
                    .Replace(Resource.Id.content_frame, fragment)
                    .AddToBackStack("ListaRepuestosEntrega")
                   .Commit();
            };


            progressLayout = view.FindViewById<LinearLayout>(Resource.Id.progressBarListaEntrega);
            progressLayout.Visibility = ViewStates.Gone;



        

            return view;
        }

        private async Task<bool> abrirPdf(string codDocumento)
        {

            string localPath = await ManagerRepuestos.getAlbaran(codDocumento);
            var localImage = new Java.IO.File(localPath);
            if (!localPath.Equals(string.Empty))
            {

                if (localImage.Exists())
                {

                    global::Android.Net.Uri uri = global::Android.Net.Uri.FromFile(localImage);

                    var intent = new Intent(Intent.ActionView, uri);
                    // intent.SetType ("application/pdf");

                    intent.SetDataAndType(global::Android.Net.Uri.FromFile(localImage), "application/pdf");

                    //this.StartActivity(intent);
                    ((Activity)view.Context).StartActivity(intent);
                }
            }
            else {

                Toast.MakeText(this.Activity, "No se ha encontrado ninguna Ruta.", ToastLength.Long);
                return false;
            }

            return true;
        }

     

        
        public  override void OnResume()
        {
            base.OnResume();

            //this.adapterRepuestos.list = ManagerRepuestos.getRepuestos();
            this.adapterRepuestos.NotifyDataSetChanged();

        }

        public  void PopBackStack()
        {
            if (ManagerRepuestos.getRepuestos().Count != 0)
            {


                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this.Activity);
                alert.SetTitle("¿Estas seguro de cerrar la lista?");
                alert.SetMessage("Si cierras la lista, estos datos serán eliminados.");
                alert.SetPositiveButton("SÍ", (s, e) =>
                {
                    ManagerRepuestos.limpiarRepuestos();
                   
                });
                alert.SetNegativeButton("NO", (s, e) =>
                {

                });

                Dialog dialog = alert.Create();
                dialog.Show();
            }
            else
            {

                
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {

            inflater.Inflate(Resource.Menu.menu, menu);

            base.OnCreateOptionsMenu(menu, inflater);

        }




        public override bool OnOptionsItemSelected(IMenuItem item)
        {


            switch (item.ItemId)
            {
                case Resource.Id.registrar:

                    if (ManagerRepuestos.getRepuestos().Count() > 0)
                    {
                        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this.Activity);
                        alert.SetTitle("¿Estas seguro de registrar la lista?");
                        alert.SetMessage("Si la registras esos datos no podrán ser modificados");
                        alert.SetPositiveButton("SÍ", (s, e) =>
                        {

                            RegistrarSalida();
                        });
                        alert.SetNegativeButton("NO", (s, e) =>
                        {

                        });

                        Dialog dialog = alert.Create();
                        dialog.Show();
                    }
                    else
                    {

                        Toast.MakeText(this.Activity, "No hay repuestos para registrar!!!", ToastLength.Short).Show();
                    }
                    break;

                default:
                   this.Activity.Finish();

                    break;
            }
            return base.OnOptionsItemSelected(item);

        }

        private async Task<bool> RegistrarSalida()
        {

            progressLayout.Visibility = ViewStates.Visible;
            numeroDocumento = string.Empty;

            Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this.Activity);
            alert.SetTitle("Registro");
            alert.SetIcon(Android.Resource.Drawable.ButtonStar);


            numeroDocumento = await ManagerRepuestos.registrarLista(empleado.No);
            if (!string.IsNullOrEmpty(numeroDocumento))
            {

                if (ManagerRepuestos.getRepuestos()[0].Destino.Equals(Destino.Liege) || ManagerRepuestos.getRepuestos()[0].Destino.Equals(Destino.Sevilla))
                {
                    alert.SetMessage("¿Desea descargar el albarán?");
                    alert.SetPositiveButton("SÍ", alertAlbaran_Ok);
                    alert.SetNegativeButton("NO", (s, e) =>
                    {
                        PopBackStack();
                    });
                    alert.Show();


                }
                else
                {
                    alert.SetMessage("Registro correcto");
                    alert.SetNeutralButton("Ok", (s, e) => { PopBackStack(); });
                    alert.Show();

                }

                ManagerRepuestos.clearRepuestos();
                progressLayout.Visibility = ViewStates.Gone;
                FragmentManager.PopBackStack();
                return true;

            }
            else
            {
                alert.SetMessage("Error al realizar el registro, pongase en contacto con el departamento informática");
                alert.SetNeutralButton("Ok", (s, e) => { });
                alert.Show();
                progressLayout.Visibility = ViewStates.Gone;
                return false;
            }




        }

        private async void alertAlbaran_Ok(object s, DialogClickEventArgs e)
        {

            if (!string.IsNullOrEmpty(numeroDocumento))
            {
                bool ok = await abrirPdf(numeroDocumento);
                if (ok)
                {
                    PopBackStack();
                }
                else
                {
                    Toast.MakeText(this.Activity, "No se encuentra albarán!!!", ToastLength.Long).Show();
                }
                progressLayout.Visibility = ViewStates.Gone;
            }
            else {

                Toast.MakeText(this.Activity, "No existe numero documento, nada que imprimir", ToastLength.Long).Show();
            }
        }

        private async Task launchScaner()
        {


            var scanner = new MobileBarcodeScanner();
            Button flashButton;
            View zxingOverlay;

            scanner.UseCustomOverlay = true;

            //Inflate our custom overlay from a resource layout
            zxingOverlay = LayoutInflater.FromContext(this.Activity).Inflate(Resource.Layout.OverlayReadBarCode, null);

            //Find the button from our resource layout and wire up the click event
            flashButton = zxingOverlay.FindViewById<Button>(Resource.Id.buttonZxingFlash);
            flashButton.Click += (sender, e) => scanner.ToggleTorch();

            //Set our custom overlay
            scanner.CustomOverlay = zxingOverlay;

            //Start scanning!
            var result = await scanner.Scan();

            

            Toast.MakeText(this.Activity, result.Text, ToastLength.Long);
            //HandleScanResult(result);

            string msg = string.Empty;
            if (!ManagerRepuestos.existeRepuestoEnLista(empleado.No, result.Text))
             
            {
                if (result != null && !string.IsNullOrEmpty(result.Text))
                {
                    EntregaAlmacen rep = await ManagerRepuestos.addRepuesto(empleado.No, result.Text);

                    if (rep == null)
                    {

                        Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this.Activity);
                        alert.SetTitle("Error en lectura de Código de Barras");
                        alert.SetMessage("Código leido: " + result);
                        alert.SetPositiveButton("Ok", (s, e) =>
                        {
                            rep = null;
                            //msg = "Código leido: " + result;
                            //this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, msg, ToastLength.Short).Show());
                        });
                        alert.Show();

                       
                    }
                    else
                    {
                        //var rep = await ManagerRepuestos.addRepuesto(empleado.No, result.Text);
                        //if (rep != null)
                        //{
                            adaptarSwipe.NotifyDataSetChanged();

                            //var activityDetalleRepuestoActivity = new Intent(this.Activity, typeof(detalleRepuestoActivity));
                            //activityDetalleRepuestoActivity.PutExtra("idEntregaAlmacen", rep.Key);
                            //StartActivity(activityDetalleRepuestoActivity);


                            Android.Support.V4.App.Fragment fragment = new AlmacenRepuestosXamarin.Fragments.DetalleRepuesto();

                            Bundle bundle = new Bundle();
                            bundle.PutString("idEntregaAlmacen", rep.Key);
                            fragment.Arguments = bundle;




                            FragmentManager.BeginTransaction()
                                .Replace(Resource.Id.content_frame, fragment)
                                .AddToBackStack("ListaRepuestosEntrega")
                               .Commit();
                        //}
                    
                    }

                }
                else
                { 
                    msg = "Scaneo Cancelado";
                   
                }
            }
            else
            {
                msg = "Ya fue escaneado ese producto!!";

            }

            if (!string.IsNullOrEmpty(msg)) this.Activity.RunOnUiThread(() => Toast.MakeText(this.Activity, msg, ToastLength.Short).Show());

        }

        public bool HasActions(int p0, SwipeDirection direction)
        {
            if (direction.IsLeft) return true; // Change this to false to disable left swipes
            if (direction.IsRight) return true;
            return false;
        }

        public void OnSwipe(int[] positionList, SwipeDirection[] directionList)
        {
            for (int i = 0; i < positionList.Length; i++)
            {
                SwipeDirection direction = directionList[i];
                int position = positionList[i];
                String dir = "";

                if (direction == SwipeDirection.DirectionFarLeft)
                {
                    dir = "Far left";
                }
                else if (direction == SwipeDirection.DirectionNormalLeft)
                {
                    dir = "Left";
                }
                else if (direction == SwipeDirection.DirectionFarRight)
                {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this.Activity);
                    alert.SetTitle("¿Estas seguro de eliminar el repuesto?");
                    //alert.SetMessage("Si ");
                    alert.SetPositiveButton("SÍ", (s, e) =>
                    {
                        var item = adapterRepuestos.list[position];

                        eliminarRepuesto(item);

                    });
                    alert.SetNegativeButton("NO", (s, e) =>
                    {

                    });

                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                else if (direction == SwipeDirection.DirectionNormalRight)
                {


                }



            }
        }
        public bool ShouldDismiss(int p0, SwipeDirection direction)
        {
            return direction == SwipeDirection.DirectionNormalLeft;
        }

        private async void eliminarRepuesto(EntregaAlmacen item)
        {

            await ManagerRepuestos.eliminarRepuesto(item.Key);
            this.adaptarSwipe.NotifyDataSetChanged();

        }
    }
}