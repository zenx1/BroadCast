using System;
using System.Net;
using Newtonsoft.Json.Linq;
using WebSocket4Net;

namespace BroadCast.Web
{
    static class WebApp
    {
        public static void Connect(string uuid, string wsUrl, Action callback)
        {
            SimpleSocketClient simpleSocket = new SimpleSocketClient(wsUrl);
            simpleSocket.onConnected(callback);
            simpleSocket.send(uuid, "http://" + GetLocalIPAddress());
        }

        static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.ToString().StartsWith("192.168."))
                {
                    return ip.ToString() + ":8080";
                }
            }
            return null;
        }
    }
}