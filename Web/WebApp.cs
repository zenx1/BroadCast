using System;
using System.Net;
using Newtonsoft.Json.Linq;
using WebSocket4Net;

namespace BroadCast.Web
{
    static class WebApp
    {
        public static void Connect(string uuid, string wsUrl, Action Connected)
        {
            JObject registrationData = new JObject();
            registrationData["title"] = uuid;
            registrationData["body"] = "http://" + GetLocalIPAddress();
            WebSocket websocket = new WebSocket(wsUrl);
            websocket.Opened += new EventHandler(websocket_Opened);
            websocket.Open();

            void websocket_Opened(object sender, EventArgs e)
            {
                websocket.Send(registrationData.ToString());
                websocket.Close();
                websocket.Dispose();
                Connected();
            }
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