using Android.App;
using Android.OS;
using Android.Widget;
using BroadCast.DataProviders;

namespace BroadCast.Activities
{
    [Activity(Label = "AlbumViewActivity")]
    public class AlbumViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.album_view);

            GridView gridView = FindViewById<GridView>(Resource.Id.album_grid);
            gridView.Adapter = new Adapters.AlbumViewAdapter(this);
            gridView.ItemClick += GridView_ItemClick;
        }

        void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            DataContainer.SetSelectedAlbum(e.Position);
            StartActivity(typeof(Activities.ImageViewActivity));
        }

        public override void OnBackPressed()
        {
            var activity = (Activity)this;
            activity.FinishAffinity();
        }
    }
}