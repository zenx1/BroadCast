using System.Collections.Generic;
using Android.App;
using Android.Provider;
using Android.Runtime;
using BroadCast.Models;

namespace BroadCast.DataProviders
{
    class ImageAlbums
    {
        public JavaList<AlbumCover> Covers { get; set; }
        public Dictionary<long, JavaList<string>> Images { get; set; }

        public ImageAlbums()
        {
            var projection = new[] { MediaStore.Images.Media.InterfaceConsts.BucketId, MediaStore.Images.Media.InterfaceConsts.BucketDisplayName, MediaStore.Images.Media.InterfaceConsts.Data };
            var cursor = Application.Context.ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projection, null, null, MediaStore.Images.Media.InterfaceConsts.DateModified);
            Covers = new JavaList<AlbumCover>();
            Images = new Dictionary<long, JavaList<string>>();
            if (cursor.MoveToLast())
            {
                do
                {
                    long albumID = cursor.GetLong(cursor.GetColumnIndex(projection[0]));
                    string imagePath = cursor.GetString(cursor.GetColumnIndex(projection[2]));

                    if (!Images.ContainsKey(albumID))
                    {
                        Images.Add(albumID, new JavaList<string>());
                        var cover = new AlbumCover
                        {
                            albumId = albumID,
                            itemName = cursor.GetString(cursor.GetColumnIndex(projection[1])),
                            imagePath = imagePath
                        };
                        Covers.Add(cover);
                    }

                    Images[albumID].Add(imagePath);

                } while (cursor.MoveToPrevious());
            }
        }
    }
}