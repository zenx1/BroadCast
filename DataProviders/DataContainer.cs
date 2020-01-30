using Android.Runtime;
using BroadCast.Models;
using BroadCast.DataProviders;
using System;

namespace BroadCast.DataProviders
{
    class DataContainer
    {
        private static ImageAlbums albums;

        private static long selectedAlbumID;

        public static void SetAlbums(ImageAlbums albums)
        {
            DataContainer.albums = albums;
        }
        public static JavaList<AlbumCover> GetAlbumCovers()
        {
            return albums.Covers;
        }
        public static void SetSelectedAlbum(int position)
        {
            selectedAlbumID = albums.Covers[position].albumId;
        }
        public static JavaList<string> GetSelectedAlbumImages()
        {
            return albums.Images[selectedAlbumID];
        }
        public static string GetSelectedImage(int position)
        {
            return albums.Images[selectedAlbumID][position];
        }

        public static event EventHandler NewImageSelected;

        public class NewImageSelectedEventArgs : EventArgs
        {
            public string remotePath { get; set; }
        }

        public static void SetSelectedImageRemotePath(int position)
        {
            string[] pathParts = albums.Images[selectedAlbumID][position].Split("/");
            NewImageSelected?.Invoke(null, new NewImageSelectedEventArgs
            {
                remotePath = "/" + selectedAlbumID + "/" + pathParts[pathParts.Length - 1]
            });
        }

        public static event EventHandler ImageViewActvitiyClosed;

        public static void InformImageViewActvitiyClosed()
        {
            ImageViewActvitiyClosed?.Invoke(null, new NewImageSelectedEventArgs
            {
                remotePath = "/"
            });
        }
    }
}