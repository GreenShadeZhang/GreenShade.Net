using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GreenShade.Wpf.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Control”。
    public class Control : IControl
    {
        //SerialPort myPort = null;
        public string DoWork(string filename)
        {
            //if(myPort==null)
            //{
            //    myPort = new SerialPort();
            //    myPort.BaudRate = 9600;
            //    myPort.DataBits = 8;
            //    myPort.PortName = "COM6";
            //    myPort.NewLine = "\r\n";
            //    myPort.Open();
            //}
             MainWindow.Instance.myPort.Write(filename);
             //= myPort.ReadLine();
           // myPort.Close();
            //myPort.DataReceived += MyPort_DataReceived;
            return filename;
        }
      
    }
}
