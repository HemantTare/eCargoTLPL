<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucHeader1.ascx.cs" Inherits="Bars_WucHeader1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<body>
<table border="0" width="100%" cellpadding ="0" cellspacing="0">
    <tr>
        <td style="width:1%;">
            <asp:Image ID="ImgLogoLeft" runat="Server" ImageUrl="~/Images/LogoLeft.gif" ImageAlign="top" />
        </td>
        <td class="BGMIDDLE" style="width:98%;" align="center">
            <%--<asp:Image ID="ImgLogoMiddle" runat="Server" ImageUrl="~/Images/LogoMiddle.gif" ImageAlign="top"/>--%>
            <asp:Label ID="lbl_companyName" runat="server" ForeColor="Brown" Font-Italic="true" Font-Size="Larger"></asp:Label>
        </td>
        
        <td style="width:1%;">
            <asp:Image ID="ImgLogoRight" runat="Server" ImageUrl="~/Images/LogoRight.gif" ImageAlign="top" />
        </td>
    </tr>
 </table>  
 </body>
