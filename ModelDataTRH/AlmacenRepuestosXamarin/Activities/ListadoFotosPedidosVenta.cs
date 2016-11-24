using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Android.OS;
using Android.Provider;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using AlmacenRepuestosXamarin.Clases;
using AlmacenRepuestosXamarin.Activities;

namespace AlmacenRepuestosXamarin.Fragments
{

    [Activity(Label = "ListadoFotosPedidosVenta", ScreenOrientation = ScreenOrientation.Portrait)]
    public class ListadoFotosPedidosVenta : BaseActivity
    {
        private ListView listViewFotosVentas;
        private string codPedido = string.Empty;
        private View view;
        private LinearLayout progressLayout;
        private ImageView _imageView;
        private File _dir;
        private File _file;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.ListFotosPedidoVenta;
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok) {
                // make it available in the gallery
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Uri contentUri = Uri.FromFile(_file);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);

                // display in ImageView. We will resize the bitmap to fit the display
                // Loading the full sized image will consume to much memory 
                // and cause the application to crash.
                int height = _imageView.Height;
                int width = Resources.DisplayMetrics.WidthPixels;
                using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
                {
                    _imageView.RecycleBitmap();
                    _imageView.SetImageBitmap(bitmap);
                }


            }
          
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //SetContentView(Resource.Layout.ListFotosPedidoVenta);
            //Bundle bundle = this.Arguments;
            //if (bundle != null)
            //{
                codPedido = this.Intent.Extras.GetString("codPedido");
            //}
            progressLayout = FindViewById<LinearLayout>(Resource.Id.progressBarListaFotos);
            progressLayout.Visibility = ViewStates.Gone;

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                //Button button = FindViewById<Button>(Resource.Id.myButton);
                //_imageView = FindViewById<ImageView>(Resource.Id.imageView1);
                //button.Click += TakeAPicture;
            }
            // Create your fragment here
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            
            MenuInflater.Inflate(Resource.Menu.menu_fotos, menu);
            return base.OnCreateOptionsMenu(menu);

        }
     
        private void CreateDirectoryForPictures()
        {
            _dir = new File(
                   Environment.GetExternalStoragePublicDirectory(
                   Environment.DirectoryPictures), "codPedido");
            if (_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }
       
        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Resource.Id.foto:

                  
                    try
                    {
                        Intent intent = new Intent(MediaStore.ActionImageCapture);
                        _file = new File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
                        intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(_file));
                        this.StartActivityForResult(intent, 0);
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

       
        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

    }
}