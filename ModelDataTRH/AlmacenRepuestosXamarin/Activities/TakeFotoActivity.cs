namespace AlmacenRepuestosXamarin.Activities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Provider;
    using Android.Widget;
    using AlmacenRepuestosXamarin.Clases;
    using AlmacenRepuestosXamarin.Activities;
    using Java.IO;
    using AlmacenRepuestosXamarin.Adapter;
    using Environment = Android.OS.Environment;
    using Uri = Android.Net.Uri;
    using Android.Views;
    using Model;
    [Activity(Label = "Camera App Demo")]
    public class TakeFotoActivity :  BaseActivity
    {
        private File _dir;
        private File _file;
        private ImageView _imageView;
        private string codPedido = string.Empty;
        private string codPedidoFormateado = string.Empty;
        private List<Fotos> listFotos = new List<Fotos>();
        private ListView listViewFotos;
        private AdapterFotos adapterFotos;
        private String empresa;
        private Dialog dialog;
        public LinearLayout progressLayout;
        public enum TiposFotos
        {
            Cabeza ,
            Remolque,
            LadoDerecho,
            LadoIzquierdo
        }

        public class Fotos{

            public String Descripcion { get; set; }
            private TiposFotos tipo;
            public TiposFotos Tipo
            {
                get { return tipo; }
                set
                {
                    switch (value)
                    {
                        case TiposFotos.Cabeza:
                            Descripcion = "Foto cabeza tractora";
                            break;
                        case TiposFotos.Remolque:
                            Descripcion = "Foto remolque camión";
                            break;
                        case TiposFotos.LadoDerecho:
                            Descripcion = "Foto lado derecho";
                            break;
                        case TiposFotos.LadoIzquierdo:
                            Descripcion = "Foto lado izquierdo";
                            break;
                        default:
                            break;
                    }
                }  }

            public Bitmap Image { get; set; }
           // public Bitmap Image128 { get; set; }
            public File Fichero { get; set; }
        }
        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.TakeFoto;
            }
        }
        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                progressLayout.Visibility = ViewStates.Visible;
                // make it available in the gallery
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Uri contentUri = Uri.FromFile(_file);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);

                // display in ImageView. We will resize the bitmap to fit the display
                // Loading the full sized image will consume to much memory 
                // and cause the application to crash.
                TiposFotos tipoFoto = (TiposFotos)Enum.Parse(typeof(TiposFotos), _file.Name.Replace(".jpg", string.Empty).Replace(codPedidoFormateado, string.Empty).Replace("_", string.Empty));


                using (Bitmap imgFoto = await _file.Path.getBitmapFile() ) {

                    Fotos foto = new Fotos();

                    foto.Tipo = tipoFoto;//(TiposFotos)Enum.Parse(typeof(TiposFotos), _file.Name.Replace(".jpg",string.Empty).Replace(codPedidoFormateado,string.Empty).Replace("_",string.Empty)) ;
                    foto.Image = imgFoto;
                    foto.Fichero = _file;

                    listFotos.Add(foto);
                    this.RunOnUiThread(() => { adapterFotos.NotifyDataSetChanged(); progressLayout.Visibility = ViewStates.Gone; });
                    
                }



            }

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //SetContentView(Resource.Layout.TakeFoto);
            codPedido = this.Intent.Extras.GetString("codPedido");
            codPedidoFormateado = codPedido.Replace("/", "_").Replace("-", "_");
            empresa = this.Intent.Extras.GetString("empresa");
            listViewFotos = FindViewById<ListView>(Resource.Id.listViewFotos);
            adapterFotos = new AdapterFotos(this, listFotos);
            listViewFotos.Adapter = adapterFotos;

            progressLayout = FindViewById<LinearLayout>(Resource.Id.progressBarFoto);
            progressLayout.Visibility = ViewStates.Gone;

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
                
            }

            var fab = FindViewById<com.refractored.fab.FloatingActionButton>(Resource.Id.addFotosfloating);
            fab.AttachToListView(listViewFotos);
            // Button btoScan = FindViewById<Button>(Resource.Id.btoScanear);
            fab.Click += OnClinckAddFotos;

        }

        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_fotos, menu);
            return base.OnCreateOptionsMenu(menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {

                case Resource.Id.home:
                    foreach (var foto in listFotos)
                    {
                        foto.Fichero.DeleteOnExit();
                        foto.Image.Dispose();

                    }
                    Finish();
                    Toast.MakeText(this, "Cancelado por el usuario", ToastLength.Long).Show();
                    return true;
                    break;
                case Resource.Id.foto:
                    
                    try
                    {


                        dialog = new Dialog(this);
                        dialog.SetContentView(Resource.Layout.DialogTipoFoto);
                        dialog.SetTitle("Seleccione que tipo de foto");
                        dialog.SetCancelable(true);

                        RadioButton radioCabeza = (RadioButton)dialog.FindViewById(Resource.Id.radioCabeza);
                        RadioButton radioDerecho = (RadioButton)dialog.FindViewById(Resource.Id.radioLadoDerecho);
                        RadioButton radioIzquierdo = (RadioButton)dialog.FindViewById(Resource.Id.radioLadoIzquierdo);
                        RadioButton radioTrasera = (RadioButton)dialog.FindViewById(Resource.Id.radioTrasera);
                        radioCabeza.Tag = 0;
                        radioTrasera.Tag = 1;
                        radioDerecho.Tag = 2;
                        radioIzquierdo.Tag = 3;


                        radioCabeza.Click += RadioButtonClick;
                        radioDerecho.Click += RadioButtonClick;
                        radioIzquierdo.Click += RadioButtonClick;
                        radioTrasera.Click += RadioButtonClick;

                        dialog.Show();
                        
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    return true;
                    break;
                default:
                    return base.OnOptionsItemSelected(item);
                    break;
            }
        }

        public async void OnClinckAddFotos(object sender, EventArgs e)
        {

            if (listFotos.Count == 4)
            {
                progressLayout.Visibility = ViewStates.Visible;
                foreach (var foto in listFotos)
                {

                    await Monitorizacion.upLoadImage(codPedido, empresa, foto.Fichero, false);
                    await Monitorizacion.upLoadImage(codPedido, empresa, foto.Fichero, true);
                    foto.Fichero.DeleteOnExit();
                    foto.Image.Dispose();

                }
                listFotos.Clear();
                progressLayout.Visibility = ViewStates.Gone;
                
                this.Finish();
            }
            else {
                Toast.MakeText(this, "Imagenes guardadas con exito", ToastLength.Long).Show();
            }
           

        }
        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            TiposFotos tipo =(TiposFotos) ((int)rb.Tag);
            Toast.MakeText(this, rb.Text, ToastLength.Short).Show();

            Intent intent = new Intent(MediaStore.ActionImageCapture);
            Fotos foto = listFotos.Where(q => q.Tipo == tipo).FirstOrDefault();
            if (foto != null) listFotos.Remove(foto);

            _file = new File(_dir, String.Format("{0}_{1}.jpg",tipo.ToString(),codPedidoFormateado));
            _file.DeleteOnExit();
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));

            StartActivityForResult(intent, 0);

           
            dialog.Dismiss();
            
        }
        private void CreateDirectoryForPictures()
        {
            _dir = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "CameraAppDemo");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);

            _file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));

            StartActivityForResult(intent, 0);
        }
    }
}
