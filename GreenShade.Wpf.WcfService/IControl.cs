using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GreenShade.Wpf.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IControl”。
    [ServiceContract]
    public interface IControl
    {
        [OperationContract,WebGet(UriTemplate = "Files?aid={filename}", ResponseFormat = WebMessageFormat.Json)]
        string DoWork(string filename);
    }
}
