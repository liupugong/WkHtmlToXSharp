using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using WkHtmlToXSharp;

    public partial class _Default : System.Web.UI.Page
    {
        private static readonly global::Common.Logging.ILog _Log = global::Common.Logging.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void T()
        {}

        protected void btnConvert_Click(object sender, EventArgs e)
        {


            //TryRegisterLibraryBundles();

            SimpleConversions();

            SimpleConversion(txtUrl.Text.Trim());

        }

        //private void TryRegisterLibraryBundles()
        //{
        //    var ignore = Environment.GetEnvironmentVariable("WKHTMLTOXSHARP_NOBUNDLES");

        //    if (ignore == null || ignore.ToLower() != "true")
        //    {
        //        // Register all available bundles..
        //        //WkHtmlToXLibrariesManager.Register(new Linux32NativeBundle());
        //        //WkHtmlToXLibrariesManager.Register(new Linux64NativeBundle());
        //        WkHtmlToXLibrariesManager.Register(new Win32NativeBundle());
        //        WkHtmlToXLibrariesManager.Register(new Win64NativeBundle());
        //    }
        //}

        //private MultiplexingConverter _GetConverter()
        //{
        //    var obj = new MultiplexingConverter();
        //    obj.Begin += (s, e) => _Log.DebugFormat("Conversion begin, phase count: {0}", e.Value);
        //    //obj.Error += (s, e) => _Log.Error(e.Value);
        //    obj.Warning += (s, e) => _Log.Warn(e.Value);
        //    //obj.PhaseChanged += (s, e) => _Log.InfoFormat("PhaseChanged: {0} - {1}", e.Value, e.Value2);
        //    //obj.ProgressChanged += (s, e) => _Log.InfoFormat("ProgressChanged: {0} - {1}", e.Value, e.Value2);
        //    obj.Finished += (s, e) => _Log.InfoFormat("Finished: {0}", e.Value ? "success" : "failed!");
        //    return obj;
        //}

        private void SimpleConversions()
        {
            SimpleConversion("http://vacations.ctrip.com/around/p1846543s2.html#ctm_ref=va_hom_s2_prd_p1_l1_2_img", false);
            SimpleConversion("http://vacations.ctrip.com/grouptravel/p2419712s2.html#ctm_ref=va_gpt_s2_prd_p1_l1_1_img", false);
            SimpleConversion("http://vacations.ctrip.com/grouptravel/p2392985s2.html#ctm_ref=va_gpt_s2_prd_p1_l2_1_img", false);
        }

        private void SimpleConversion(string simplePageFile)
        {
            SimpleConversion(simplePageFile, true);
        }

        private void SimpleConversion(string simplePageFile, bool bOpen)
        {
            var wk = DemoWeb.PDFConvertor.Instance;

            var tmp = wk.TryConvert(simplePageFile);


            if (bOpen)
                openPDF(tmp);


        }

        //private void SimpleConversionBk(string simplePageFile, bool bOpen)
        //{
        //    using (var wk = _GetConverter())
        //    {
        //        _Log.DebugFormat("Performing conversion..");

        //        wk.GlobalSettings.Margin.Top = "0cm";
        //        wk.GlobalSettings.Margin.Bottom = "0cm";
        //        wk.GlobalSettings.Margin.Left = "0cm";
        //        wk.GlobalSettings.Margin.Right = "0cm";


        //        wk.ObjectSettings.Web.EnablePlugins = false;
        //        wk.ObjectSettings.Web.EnableJavascript = false;
        //        wk.ObjectSettings.Page = simplePageFile;

        //        wk.ObjectSettings.Load.Proxy = "none";

        //        var tmp = wk.Convert();


        //        if (bOpen)
        //            openPDF(tmp);

        //    }
        //}
        protected void openPDF(byte[] dados)
        {
           


            HttpContext contexto = HttpContext.Current;

            
            contexto.Response.Clear();
            contexto.Response.AppendHeader("content-type", "application/pdf");
            contexto.Response.AppendHeader("Expires", "Mon, 26 Jul 1990 05:00:00 GMT");
            contexto.Response.AppendHeader("Cache-Control", "no-cache, must-revalidate");
            contexto.Response.AppendHeader("Pragma", "no-cache");
            contexto.Response.Expires = -1;
            contexto.Response.AppendHeader("Content-Disposition", "inline; filename=labels.pdf");
            contexto.Response.AddHeader("content-length", dados.Length.ToString());
            contexto.Response.OutputStream.Write(dados, 0, dados.Length);
            contexto.Response.OutputStream.Flush();
            contexto.Response.End();
        }
    }
