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

            ThreadPool.QueueUserWorkItem(o =>
            {
                ImageAlbums albums = new ImageAlbums();
                DataContainer.SetAlbums(albums);
                Server server = new Server();
                DataContainer.NewImageSelected += new EventHandler((object sender, EventArgs e) =>
                {
                    JObject imageData = new JObject();
                    imageData["fromMobileApp"] = true;
                    imageData["path"] = ((DataContainer.NewImageSelectedEventArgs)e).remotePath;                    
                    server.wss.sendMessage(imageData.ToString());
                });
                Connect();
            });
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
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}