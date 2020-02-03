using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using WebSocket4Net;

namespace BroadCast.Web
{
    class SimpleSocketClient
    {
        WebSocket webSocket;
        bool webSocketOpened = false;
        public SimpleSocketClient(string wsUrl)
        {
            webSocket = new WebSocket(wsUrl);
            webSocket.Open();
        }

        public void onConnected(Action callback)
        {
            callback();
        }

        public void send(string name, string body)
        {
            JObject data = new JObject();
            data["name"] = name;
            data["body"] = body;
            if (webSocketOpened)
            {
                webSocket.Send(data.ToString());
            }
            else
            {
                void webSocket_Opened(object sender, EventArgs e)
                {
                    webSocketOpened = true;
                    webSocket.Opened -= webSocket_Opened;
                    webSocket.Send(data.ToString());
                }
                webSocket.Opened += new EventHandler(webSocket_Opened);
            }
        }

        public void close()
        {
            webSocket.Close();
            webSocket.Dispose();
        }
    }
}