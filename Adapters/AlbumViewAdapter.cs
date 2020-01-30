using System.Linq;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BroadCast.CustomControls;
using BroadCast.Models;
using Com.Bumptech.Glide;
using BroadCast.DataProviders;

namespace BroadCast.Adapters
{
    class AlbumViewAdapter : BaseAdapter
    {
        readonly Context context;

        private JavaList<AlbumCover> albumCovers;

        public AlbumViewAdapter(Context context)
        {
            this.context = context;
            albumCovers = DataContainer.GetAlbumCovers();
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return albumCovers.Get(position);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            AlbumViewAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as AlbumViewAdapterViewHolder;

            if (holder == null)
            {
                holder = new AlbumViewAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.album_view_model, parent, false);
                holder.Cover = view.FindViewById<SquareImageView>(Resource.Id.cover);
                holder.Name = view.FindViewById<TextView>(Resource.Id.name);
                view.Tag = holder;
            }

            global::Android.Net.Uri uri = global::Android.Net.Uri.FromFile(new Java.IO.File(albumCovers[position].imagePath));
            Glide.With(context).Load(uri).Into(holder.Cover);
            holder.Name.Text = albumCovers[position].itemName;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return albumCovers.Count();
            }
        }

    }

    class AlbumViewAdapterViewHolder : Java.Lang.Object
    {
        public SquareImageView Cover { get; set; }
        public TextView Name { get; set; }
    }
}