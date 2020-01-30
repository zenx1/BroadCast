using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BroadCast.Helpers
{
    class Constants
    {
        public static readonly int PERMISSION_REQUEST = 1000;
        public static readonly string[] REQUIRED_PERMISSIONS = new string[]{
            Manifest.Permission.Camera,
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.Internet
            };
    }
}