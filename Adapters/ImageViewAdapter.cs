using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BroadCast.CustomControls;
using Com.Bumptech.Glide;
using BroadCast.DataProviders;

namespace BroadCast.Adapters
{
    class ImageViewAdapter : BaseAdapter
    {

        Context context;
        private JavaList<string> images;

        public ImageViewAdapter(Context context)
        {
            this.context = context;
            this.images = DataContainer.GetSelectedAlbumImages();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return images.Get(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ImageViewAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ImageViewAdapterViewHolder;

            if (holder == null)
            {
                holder = new ImageViewAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.image_view_model, parent, false);
                holder.thumbnail = view.FindViewById<SquareImageView>(Resource.Id.thumbnail);
                view.Tag = holder;
            }

            //fill in your items
            global::Android.Net.Uri uri = global::Android.Net.Uri.FromFile(new Java.IO.File(images[position]));
            Glide.With(context).Load(uri).Into(holder.thumbnail);

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return images.Count();
            }
        }

    }

    class ImageViewAdapterViewHolder : Java.Lang.Object
    {
        public SquareImageView thumbnail { get; set; }
    }
}