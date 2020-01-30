using Android.App;
using Android.OS;
using Android.Widget;
using BroadCast.DataProviders;

namespace BroadCast.Activities
{
    [Activity(Label = "ImageViewActivity")]
    public class ImageViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.image_view);

            GridView gridView = FindViewById<GridView>(Resource.Id.image_grid);
            gridView.Adapter = new Adapters.ImageViewAdapter(this);
            gridView.ItemClick += GridView_ItemClick;
        }

        void GridView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            DataContainer.SetSelectedImageRemotePath(e.Position);
        }
    }
}