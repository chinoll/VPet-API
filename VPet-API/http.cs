using System;
using System.Net;
using System.Threading;
using System.Collections.Specialized;
using VPet_Simulator.Core;
using VPet_Simulator.Windows.Interface;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
// class Program
// {
//     static void Main(string[] args)
//     {
//         // 创建HttpListener实例
//         HttpListener listener = new HttpListener();

//         // 监听的URL地址
//         listener.Prefixes.Add("http://localhost:9898/");

//         // 启动监听器
//         listener.Start();

//         Console.WriteLine("服务器已启动，正在监听 http://localhost:9898/");

//         while (true)
//         {
//             // 接受传入的HTTP请求
//             HttpListenerContext context = listener.GetContext();
//         // 获取请求对象
//             HttpListenerRequest request = context.Request;

//             // 读取请求的消息体
//             string requestBody;
//             using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
//             {
//                 requestBody = reader.ReadToEnd();
//             }

//             Console.WriteLine("收到请求：");
//             Console.WriteLine("方法：" + request.HttpMethod);
//             Console.WriteLine("URL：" + request.Url);
//             Console.WriteLine("消息体：" + requestBody);
//             NameValueCollection queryParameters = request.QueryString;
//             foreach (string key in queryParameters.AllKeys)
//             {
//                 string value = queryParameters[key];
//                 Console.WriteLine("查询字符串参数：" + key + "=" + value);
//             }
//             string requestPath = request.Url.AbsolutePath;
//             Console.WriteLine("请求路径：" + requestPath);
//             var parameters = ConvertToKeyValuePairs(queryParameters);

//             // 输出参数
//             Console.WriteLine("请求参数：");
//             foreach (var parameter in parameters)
//             {
//                 Console.WriteLine(parameter.Key + " = " + parameter.Value);
//             }
//             // 响应消息
//             string responseMessage = "Hello, World!";
//             byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseMessage);

//             // 设置响应头信息
//             context.Response.ContentType = "text/plain";
//             context.Response.ContentLength64 = buffer.Length;

//             // 写入响应数据
//             context.Response.OutputStream.Write(buffer, 0, buffer.Length);

//             // 关闭响应
//             context.Response.Close();
//         }
    //     static KeyValuePair<string, string>[] ConvertToKeyValuePairs(NameValueCollection collection)
    //         {
    //             var pairs = new KeyValuePair<string, string>[collection.Count];

    //             int i = 0;
    //             foreach (string key in collection.AllKeys)
    //             {
    //                 pairs[i] = new KeyValuePair<string, string>(key, collection[key]);
    //                 i++;
    //             }

    //             return pairs;
    //         }
    // }
// }
namespace VPet.Plugin.API {
    public class API : MainPlugin {
        public override string PluginName => "API";
        private HttpListener listener;
        private VPET_API api;
        public API(IMainWindow mainwin) : base(mainwin) {
            //此处主窗体玩家,Core等信息均为空,请不要加载游戏和玩家数据
            MW = mainwin;
        }
        public override void LoadPlugin() {
            api = new VPET_API(MW);
            Thread th = new Thread(mainloop);
            th.Start();
            // Main main = MW.Main;
            // WorkTimer timer = main.WorkTimer;
            // StreamWriter sw = new StreamWriter("test.txt");
            // sw.WriteLine(timer.GetCount);
            // sw.Close();
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

        // private int APIGetCount()
        // {
        //     return MW.WorkTimer.GetCount;
        // }
        // private int APIGetCount()
        // {
        //     return MW.WorkTimer.GetCount;
        // }
        public override void Setting()
        {
            base.Setting();
        }
    }
    
}