using System;
using System.Net;
using System.Threading;
using System.Collections.Specialized;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace VPet.Plugin.API {
    public class API : MainPlugin {
        public override string PluginName => "API";
        private HttpListener listener;
        private VPET_API api;
        public API(IMainWindow mainwin) : base(mainwin) {
            MW = mainwin;
        }
        public override void LoadPlugin() {
            api = new VPET_API(MW);
            Thread th = new Thread(mainloop);
            th.Start();
        }
        public void mainloop() {
            Main main = MW.Main;
            WorkTimer timer = main.WorkTimer;
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:9898/");
            listener.Start();
            var funcmap = api.GetAPI();
            TextWriterTraceListener fileListener = new TextWriterTraceListener("vpet_api.log");
            Debug.Listeners.Add(fileListener);

            while(true) {
                var context = listener.GetContext();
                var request = GetRequest(context);
                Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+" "+request["RequestPath"]+" "+request["param_log"]);
                Debug.Flush();
                
                string responseMessage;
                try {
                    responseMessage = funcmap[request["RequestPath"]](request);
                } catch (Exception e) {
                    responseMessage = e.ToString()+request["RequestPath"];
                }
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseMessage);
                context.Response.ContentType = "text/plain";
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.Close();
            }
        }
        private Dictionary<string,string> GetRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var requestPath = request.Url.AbsolutePath.Substring(1);
            Dictionary<string,string> dict = new Dictionary<string,string>();
            dict["RequestPath"] = requestPath;
            NameValueCollection queryParameters = request.QueryString;
            string param_log = "";
            var parameters = ConvertToKeyValuePairs(queryParameters);
            foreach (KeyValuePair<string, string> kv in parameters) {
                dict[kv.Key] = kv.Value;
                param_log += kv.Key+"="+kv.Value;
            }
            dict["param_log"] = param_log;
            return dict;
        }
        static KeyValuePair<string, string>[] ConvertToKeyValuePairs(NameValueCollection collection)
        {
            var pairs = new KeyValuePair<string, string>[collection.Count];

            int i = 0;
            foreach (string key in collection.AllKeys)
            {
                pairs[i] = new KeyValuePair<string, string>(key, collection[key]);
                i++;
            }

            return pairs;
        }

        public override void Setting()
        {
            base.Setting();
        }
    }
    
}