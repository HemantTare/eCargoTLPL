<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMultiDocumentCommonRptViewer.aspx.cs" Inherits="Reports_Direct_Printing_FrmMultiDocumentCommonRptViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                DisplayGroupTree="False" PrintMode="ActiveX" HasCrystalLogo="False" 
                HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
          
        </div>
        <script type="text/javascript" >
          var HiddenField1=document.getElementById('<%=HiddenField1.ClientID%>');
          var HiddenField2=document.getElementById('<%=HiddenField2.ClientID%>');

          if ((HiddenField1.value != "reach") || (HiddenField2.value != "143"))
          {
            var i;
            i = document.getElementById('CrystalReportViewer1$ctl02$ctl01').click();
            <%if( (int)Session["count"] >= 2) 
            { %>
            self.close();

            <% Session["count"]=0;
            } %>
         }
            
        </script>
    </form>
</body>
</html>
