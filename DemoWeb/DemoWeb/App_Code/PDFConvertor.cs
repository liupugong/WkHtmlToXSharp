using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WkHtmlToXSharp;


/// <summary>
/// Summary description for SingtonWK
/// </summary>
namespace DemoWeb
{
    public class PDFConvertor
    {
        private static readonly Lazy<PDFConvertor> instance = new Lazy<PDFConvertor>(() => new PDFConvertor());


        public static PDFConvertor Instance
        {
            get
            {
                return instance.Value;
            }
        }
        //
        // TODO: Add constructor logic here
        //
        /// <summary>
        /// 设置Cache使用的管理器,此管理器必须在配置中进行定义
        /// </summary>
        private PDFConvertor()
        {
            TryRegisterLibraryBundles();
        }

        private void TryRegisterLibraryBundles()
        {
            var ignore = Environment.GetEnvironmentVariable("WKHTMLTOXSHARP_NOBUNDLES");

            if (ignore == null || ignore.ToLower() != "true")
            {
                // Register all available bundles..
                //WkHtmlToXLibrariesManager.Register(new Linux32NativeBundle());
                //WkHtmlToXLibrariesManager.Register(new Linux64NativeBundle());
                WkHtmlToXLibrariesManager.Register(new Win32NativeBundle());
                WkHtmlToXLibrariesManager.Register(new Win64NativeBundle());
            }
        }

        public byte[] Convert(string url)
        {
            using (var wk = new MultiplexingConverter())
            {
                wk.GlobalSettings.Margin.Top = "0cm";
                wk.GlobalSettings.Margin.Bottom = "0cm";
                wk.GlobalSettings.Margin.Left = "0cm";
                wk.GlobalSettings.Margin.Right = "0cm";


                wk.ObjectSettings.Web.EnablePlugins = false;
                wk.ObjectSettings.Web.EnableJavascript = false;
                wk.ObjectSettings.Page = url;

                wk.ObjectSettings.Load.Proxy = "none";

                var tmp = wk.Convert();
                return tmp;
            }
        }

        public byte[] TryConvert(string url)
        {
            int errorcount = 0;
            Exception e = null;
            while (errorcount < 3)
            {
                try
                {
                    return Convert(url);
                }
                catch (Exception ex)
                {
                    errorcount += 1;
                    //try
                    //{
                    //    AppDomain.Unload(AppDomain.CurrentDomain);
                    //}
                    //catch { }
                    System.Threading.Thread.Sleep(1000);
                    e = ex;

                }

            }
            throw new Exception("转换失败", e);

        }
    }
}