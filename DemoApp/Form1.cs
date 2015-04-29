using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WkHtmlToXSharp;

namespace DemoApp
{
    public partial class Form1 : Form
    {
        private static readonly global::Common.Logging.ILog _Log = global::Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            TryRegisterLibraryBundles();

            if (txtUrl.Text.Trim().Length > 0)
                SimpleConversion(txtUrl.Text.Trim());
            else
                SimpleConversion(txtHtml.Text);

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

        private MultiplexingConverter _GetConverter()
        {
            var obj = new MultiplexingConverter();
            obj.Begin += (s, e) => _Log.DebugFormat("Conversion begin, phase count: {0}", e.Value);
            //obj.Error += (s, e) => _Log.Error(e.Value);
            obj.Warning += (s, e) => _Log.Warn(e.Value);
            //obj.PhaseChanged += (s, e) => _Log.InfoFormat("PhaseChanged: {0} - {1}", e.Value, e.Value2);
            //obj.ProgressChanged += (s, e) => _Log.InfoFormat("ProgressChanged: {0} - {1}", e.Value, e.Value2);
            obj.Finished += (s, e) => _Log.InfoFormat("Finished: {0}", e.Value ? "success" : "failed!");
            return obj;
        }

        private void SimpleConversion(string simplePageFile)
        {
            using (var wk = _GetConverter())
            {
                _Log.DebugFormat("Performing conversion..");

                wk.GlobalSettings.Margin.Top = "0cm";
                wk.GlobalSettings.Margin.Bottom = "0cm";
                wk.GlobalSettings.Margin.Left = "0cm";
                wk.GlobalSettings.Margin.Right = "0cm";
                //wk.GlobalSettings.Out = @"c:\temp\tmp.pdf";

                wk.ObjectSettings.Web.EnablePlugins = false;
                wk.ObjectSettings.Web.EnableJavascript = false;
                wk.ObjectSettings.Page = simplePageFile;
                //wk.ObjectSettings.Page = "http://doc.trolltech.com/4.6/qstring.html";
                wk.ObjectSettings.Load.Proxy = "none";

                var tmp = wk.Convert();

                
                // var number = 0;
                // lock (this) number = count++;
                System.IO.File.WriteAllBytes(@"e:\demo\temp.pdf", tmp);
            }
        }
    }
}
