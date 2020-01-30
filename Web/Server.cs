using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BroadCast.Models;
using EmbedIO;
using EmbedIO.WebSockets;
using BroadCast.DataProviders;

namespace BroadCast.Web
{
    class Server
    {
        public static string port = ":8080";
        public WebSocketsServer wss = new WebSocketsServer("/chat");
        public Server()
        {
            Task.Factory.StartNew(async () =>
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using var server = new WebServer("http://*" + port);
                server.WithLocalSessionManager();
                server.WithModule(wss);
                foreach (AlbumCover albumCover in DataContainer.GetAlbumCovers())
                {
                    string folderPath = String.Join("/", albumCover.imagePath.Split('/').SkipLast(1).ToArray());
                    server.WithStaticFolder("/" + albumCover.albumId, folderPath, true);
                }                
                server.WithEmbeddedResources("/", assembly, "BroadCast.Web.Static");
                await server.RunAsync();
            });
        }

        public class WebSocketsServer : WebSocketModule
        {
            public WebSocketsServer(string urlPath)
                : base(urlPath, true)
            {
                // placeholder
            }

            IWebSocketContext wssContext;

            public void sendMessage(string message)
            {
                SendAsync(wssContext, message);
            }

            /// <inheritdoc />
            protected override Task OnMessageReceivedAsync(
                IWebSocketContext context,
                byte[] rxBuffer,
                IWebSocketReceiveResult rxResult)
                => SendToOthersAsync(context, Encoding.GetString(rxBuffer));

            /// <inheritdoc />
            protected override Task OnClientConnectedAsync(IWebSocketContext context)
            {
                wssContext = context;
                return SendAsync(context, "Welcome to the chat room!");
            }

            /// <inheritdoc />
            protected override Task OnClientDisconnectedAsync(IWebSocketContext context)
                => SendToOthersAsync(context, "Someone left the chat room.");

            private Task SendToOthersAsync(IWebSocketContext context, string payload)
                => BroadcastAsync(payload, c => c != context);
        }
    }
}