using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GreenShade.Wpf.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Control”。
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Control : IControl
    {
        public Stream ControlPage(string filename)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            if (filename == "startPage.html")
            {
                string ipAdress = null;
                string name = Dns.GetHostName();
                IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
                foreach (IPAddress ipa in ipadrlist)
                {
                    if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (ipa.ToString().StartsWith("192.168.1"))
                        {
                            ipAdress =ipa.ToString();
                        }
                    }
                }
                if (!String.IsNullOrWhiteSpace(ipAdress))
                {

                    String url = "{ \"rootUrl\": \"http://" + ipAdress + ":8000\"}";
                    String ipJson = System.IO.Path.Combine(Environment.CurrentDirectory, "ControlPage/ip.json").Replace('\\', '/');
                    System.IO.File.WriteAllText(ipJson, url);
                }
            }

            String filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "ControlPage/" + filename).Replace('\\', '/');
            Stream stream = (Stream)new FileStream(filePath, FileMode.Open);
            //Set the correct context type for the file requested.
            int extIndex = filename.LastIndexOf(".");
            string extension = filename.Substring(extIndex, filename.Length - extIndex);
            switch (extension)
            {
                case ".html":
                case ".htm":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
                    break;
                case ".png":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "image/png";
                    break;
                case ".js":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/javascript";
                    break;
                case ".json":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                    break;
                case ".css":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/css";
                    break;
                case ".scss":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/css";
                    break;
                case ".map":
                    WebOperationContext.Current.OutgoingResponse.ContentType = "text/css";
                    break;
                default:
                    throw (new ApplicationException("File type not supported"));
            }
            

            return stream;
        }

        //SerialPort myPort = null;
        public string DoWork(string id)
        {
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin","*");

            switch (id)
            {
                case "up":             
                    MainWindow.Instance.myPort.Write("w");
                    break;
                case "left":
                    MainWindow.Instance.myPort.Write("a");
                    break;
                case "right":
                    MainWindow.Instance.myPort.Write("d");
                    break;
                case "down":
                    MainWindow.Instance.myPort.Write("s");
                    break;
                case "circle":
                    MainWindow.Instance.myPort.Write("z");
                    break;
                case "stop":
                    MainWindow.Instance.myPort.Write("0");
                    break;
                default:
                    MainWindow.Instance.myPort.Write("0");
                    break;
            }           
            return id;
        }
      
    }
}
