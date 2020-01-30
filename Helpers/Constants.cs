using System;
using System.Collections.Generic;
using Android;

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