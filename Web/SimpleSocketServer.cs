using System;
using Newtonsoft.Json.Linq;

namespace BroadCast.Web
{
    class SimpleSocketServer
    {
        Server.WebSocketsServer webSocketsServer;
        public SimpleSocketServer(Server.WebSocketsServer webSocketsServer)
        {
            this.webSocketsServer = webSocketsServer;
        }

        public void Send(string name, string body)
        {
            JObject data = new JObject();
            data["name"] = "image";
            data["body"] = body;
            webSocketsServer.sendMessage(data.ToString());
        }
        public void Send(string name)
        {
            JObject data = new JObject();
            data["name"] = "albums";
            webSocketsServer.sendMessage(data.ToString());
        }

        public void onClose(Action callback)
        {
            webSocketsServer.onClientDisconnected = callback;
        }
    }
}