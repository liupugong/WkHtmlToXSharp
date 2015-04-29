using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pdfConvertor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SimpleConversion("http://localhost/demoweb/index.html", true);
    }

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

    private void Swap(string a, string b)
    {

    }
}