using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using System.Threading;
using BroadCast.DataProviders;
using BroadCast.Web;
using ZXing.Mobile;
using Newtonsoft.Json.Linq;
using System;
using Android.Support.V4.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using System.Collections.Generic;
using Android.Widget;

namespace BroadCast
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            EnsurePermissions();
        }

        private void init()
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                ImageAlbums albums = new ImageAlbums();
                DataContainer.SetAlbums(albums);
                Server server = new Server();
                DataContainer.NewImageSelected += new EventHandler((object sender, EventArgs e) =>
                {
                    JObject imageData = new JObject();
                    imageData["title"] = "image";
                    imageData["body"] = ((DataContainer.NewImageSelectedEventArgs)e).remotePath;
                    server.wss.sendMessage(imageData.ToString());
                });
                DataContainer.ImageViewActvitiyClosed += new EventHandler((object sender, EventArgs e) =>
                {
                    JObject imageData = new JObject();
                    imageData["title"] = "albums";
                    server.wss.sendMessage(imageData.ToString());
                });
                Connect();
            });
        }

        private void DisplayMissingPermissionsMessage()
        {
            TextView message = FindViewById<TextView>(Resource.Id.message);
            message.Text = GetString(Resource.String.permissions_denied);
        }
        private void HideMissingPermissionsMessage()
        {
            TextView message = FindViewById<TextView>(Resource.Id.message);
            message.Text = "";
        }
        private List<string> missingPermissions;
        private void EnsurePermissions()
        {
            missingPermissions = new List<string> { };

            foreach (string permission in Helpers.Constants.REQUIRED_PERMISSIONS)
            {
                if (ContextCompat.CheckSelfPermission(this, permission) != (int)Permission.Granted)
                {
                    missingPermissions.Add(permission);
                }
            }

            if (missingPermissions.Count > 0)
            {
                DisplayMissingPermissionsMessage();
                ActivityCompat.RequestPermissions(this, missingPermissions.ToArray(), Helpers.Constants.PERMISSION_REQUEST);
            }
            else
            {
                init();
            }
        }

        public void Connect()
        {
            ThreadPool.QueueUserWorkItem(async o =>
            {
                MobileBarcodeScanner.Initialize(Application);
                var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                var result = await scanner.Scan();
                if (result == null)
                {
                    return;
                }
                JObject qr = JObject.Parse(result.Text);

                WebApp.Connect((string)qr["uuid"], (string)qr["wsUrl"], () =>
                {
                    RunOnUiThread(() =>
                    {
                        StartActivity(typeof(Activities.AlbumViewActivity));
                    });
                });
            });
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if (requestCode == Helpers.Constants.PERMISSION_REQUEST)
            {
                if (grantResults.Length == missingPermissions.Count)
                {
                    bool permissionsGranted = true;
                    for (int grantIndex = 0; grantIndex < grantResults.Length - 1; grantIndex++)
                    {
                        if (grantResults[grantIndex] != Permission.Granted)
                        {
                            permissionsGranted = false;
                            break;
                        }
                    }
                    if (permissionsGranted)
                    {
                        HideMissingPermissionsMessage();
                        init();
                    }
                    else
                    {
                        DisplayMissingPermissionsMessage();
                    }
                }
            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }
    }
}