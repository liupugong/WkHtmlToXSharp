<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to ASP.NET!
    </h2>
    <p>
        To learn more about ASP.NET visit <a href="http://www.asp.net" title="ASP.NET Website">www.asp.net</a>.
    </p>
    <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>

   <span>URL: </span><asp:TextBox ID="txtUrl" runat="server">http://vacations.ctrip.com/grouptravel/p2419712s2.html#ctm_ref=va_gpt_s2_prd_p1_l1_1_img</asp:TextBox>
    <asp:Button ID="btnConvert" runat="server" Text="Convert" 
        onclick="btnConvert_Click" Visible=false />

        <a href="pdfConvertor.aspx" target=_blank>Convert</a>
</asp:Content>
