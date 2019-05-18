using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZXing;
using ZXing.Common;

namespace GreenShade.Wpf.WcfService
{
    /// <summary>
    /// QrcodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QrcodeWindow : Window
    {
        public QrcodeWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QrCodeImg.Source = null;
            try
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
                            ipAdress = "http://" + ipa.ToString() + ":8000/Service/page?name=startPage.html";
                        }
                    }
                    // Console.Writeline(ipa.ToString());
                }
                IpUrl.Text = ipAdress;
                GeneratorQRCode(ipAdress);

            }
            catch (Exception ex)
            {
                IpUrl.Text = ex.Message;//异常
            }

        }

        // 二维码生成函数    
        private System.Drawing.Image GeneratorQRCode(string txt)
        {
            //BarcodeWriter一个智能类来编码一些内容的二维码、条形码图像 
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE//设置二维码的格式
            };
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8"); // 编码格式    
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
            int codeSizeInPixels = 260;      // 二维码长宽       
            writer.Options.Height = codeSizeInPixels;
            writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;       // 设置边框         
            BitMatrix bm = writer.Encode(txt);
            Bitmap img = writer.Write(bm);//对指定内容进行编码，并返回该码的呈现实例(渲染属性、实例使用，在方法之前调用)
            QrCodeImg.Source = BitmapToBitmapImage(img);//将图片控件的数据源设为生成后的二维码
            return img;
        }
        // Bitmap --> BitmapImage       

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Position = 0; BitmapImage result = new BitmapImage();
                result.BeginInit(); result.CacheOption = BitmapCacheOption.OnLoad; result.StreamSource = stream; result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
