namespace AlmacenRepuestosXamarin.Clases
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.Media;
    using Android.Widget;
    using Java;
    using Clases;
    public static class BitmapHelpers
    {
        /// <summary>
        /// This method will recyle the memory help by a bitmap in an ImageView
        /// </summary>
        /// <param name="imageView">Image view.</param>
        public static void RecycleBitmap(this ImageView imageView)
        {
            if (imageView == null)
            {
                return;
            }

            Drawable toRecycle = imageView.Drawable;
            
            if ((toRecycle != null) && ((BitmapDrawable)toRecycle).Bitmap!=null)
            {
                ((BitmapDrawable)toRecycle).Bitmap.Recycle();
            }
        }


        /// <summary>
        /// Load the image from the device, and resize it to the specified dimensions.
        /// </summary>
        /// <returns>The and resize bitmap.</returns>
        /// <param name="fileName">File name.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InPurgeable = true,
                InJustDecodeBounds = true
            };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
            // Images are being saved in landscape, so rotate them back to portrait if they were taken in portrait
            //Matrix mtx = new Matrix();
            //ExifInterface exif = new ExifInterface(fileName);
            //string orientation = exif.GetAttribute(ExifInterface.TagOrientation);

            //switch (orientation)
            //{
            //    case "6": // portrait
            //        mtx.PreRotate(90);
            //        resizedBitmap = Bitmap.CreateBitmap(resizedBitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height, mtx, false);
            //        mtx.Dispose();
            //        mtx = null;
            //        break;
            //    case "1": // landscape
            //        break;
            //    default:
            //        mtx.PreRotate(90);
            //        resizedBitmap = Bitmap.CreateBitmap(resizedBitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height, mtx, false);
            //        mtx.Dispose();
            //        mtx = null;
            //        break;
            //}

            return resizedBitmap;
        }
        public static async Task<Bitmap> getBitmapFile(this string fileName) {
           
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InJustDecodeBounds = false
            };

            // The result will be null because InJustDecodeBounds == true.
            Bitmap resizedBitmap = await BitmapFactory.DecodeFileAsync(fileName, options);
            //Matrix mtx = new Matrix();
            //ExifInterface exif = new ExifInterface(fileName);
            //string orientation = exif.GetAttribute(ExifInterface.TagOrientation);

            //switch (orientation)
            //{
            //    case "6": // portrait
            //        mtx.PreRotate(90);
            //        resizedBitmap = Bitmap.CreateBitmap(resizedBitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height, mtx, false);
            //        mtx.Dispose();
            //        mtx = null;
            //        break;
            //    case "1": // landscape
            //        break;
            //    default:
            //        mtx.PreRotate(90);
            //        resizedBitmap = Bitmap.CreateBitmap(resizedBitmap, 0, 0, resizedBitmap.Width, resizedBitmap.Height, mtx, false);
            //        mtx.Dispose();
            //        mtx = null;
            //        break;
            //}

            return resizedBitmap;
        }
    }

    //public static class JavaUtils
    //{
    //    public static Dictionary<K, V> ToDictionary<K, V>(this Java.Util.HashMap map)
    //    {
    //        var dict = new Dictionary<K, V>();
    //        var iterator = map.KeySet().GetEnumerator();

    //        var enumeracion = map.Values().GetEnumerator();


    //        foreach (var item in enumeracion)
    //        {

    //        }
    //        foreach (var item in iterator)
    //        {
    //            var key = (K)item;
    //            dict.Add(key, (V)map.Get(key));
    //        }

    //        while (iterator.MoveNext())
    //        {
    //            var key = (K)iterator.Current;
    //            dict.Add(key, (V)map.Get(iterator.Current.));
    //        }
    //        return dict;
    //    }
    //}
}