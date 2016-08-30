using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ZXing.Mobile;

namespace AlmacenRepuestosXamarin
{
    //public class fragmentTabEmpleados : Fragment
    //{
    //    Button buttonScanCustomView;
    //    MobileBarcodeScanner scanner;
    //    Context context;
    //    public override void OnCreate(Bundle savedInstanceState)
    //    {
    //        base.OnCreate(savedInstanceState);

    //        // Create your fragment here
    //    }

    //    public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
    //    {
    //        // Use this to return your custom view for this Fragment
    //        // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

    //        base.OnCreateView(inflater, container, savedInstanceState);

    //        View view = inflater.Inflate(Resource.Layout.TabListaRepuestos, container, false);

    //        context = (this).Activity.ApplicationContext;


    //        Button flashButton;
    //        View zxingOverlay;

    //        buttonScanCustomView = view.FindViewById<Button>(Resource.Id.ButtonScan);
    //        buttonScanCustomView.Click += async delegate {

    //            //Tell our scanner we want to use a custom overlay instead of the default
    //            scanner.UseCustomOverlay = true;

    //            //Inflate our custom overlay from a resource layout
    //            zxingOverlay = LayoutInflater.FromContext(context).Inflate(Resource.Layout.OverlayReadBarCode, null);

    //            //Find the button from our resource layout and wire up the click event
    //            flashButton = zxingOverlay.FindViewById<Button>(Resource.Id.buttonZxingFlash);
    //            flashButton.Click += (sender, e) => scanner.ToggleTorch();

    //            //Set our custom overlay
    //            scanner.CustomOverlay = zxingOverlay;

    //            //Start scanning!
    //            var result = await scanner.Scan();

    //            HandleScanResult(result);
    //        };

    //        return view;
    //    }


    //    void HandleScanResult(ZXing.Result result)
    //    {
    //        string msg = "";

    //        if (result != null && !string.IsNullOrEmpty(result.Text))
    //            msg = "Found Barcode: " + result.Text;
    //        else
    //            msg = "Scanning Canceled!";

    //        (this).Activity.RunOnUiThread(() => Toast.MakeText(context, msg, ToastLength.Short).Show());
    //    }
    //}
}