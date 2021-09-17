<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCRectification.aspx.cs" Inherits="Operations_Booking_NewGC_FrmNewGCRectification" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript"  src="../../../Javascript/Common.js"></script>
    <script type="text/javascript"  src="../../../Javascript/Operations/Booking/GCNew.js"></script>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
 
 function Allow_To_Save()
 {
   var ATS = true;
   return ATS;
 }
 
function Validate_GCNo()
{
    var txt_GC_No_For_Print = document.getElementById('wucGC1_txt_GC_No_For_Print');
    document.getElementById('<%=btn_ValidateGCNo.ClientID%>').click();
}

function OPenGCPopUpRectify(Path)
{
    w = screen.availWidth;
    h = screen.availHeight;
    var popW = (w-100);
    var popH = h-40;
    var leftPos = (w-popW)/2;
    var topPos = 0;
    window.open(Path,'MainPopUp','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',, menubar=no, resizable=no,scrollbars=yes,color=blue');      

   return false;
}

</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM_GCRectification" runat="server"></asp:ScriptManager>

    <div>
    <table class="TABLE" border="0">
    <tr>
        <td class="TDGRADIENT" colspan="4">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="GC Rectification"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TDUnderline" colspan="4">&nbsp;</td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_GCNoFrom" runat="server" CssClass="LABEL" Text="GC No :" ></asp:Label>
        </td>
        <td style="width: 15%;">
        <asp:UpdatePanel ID="upd_txt_GC_No_For_Print" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txt_GC_No_For_Print" onblur="Validate_GCNo();" runat="server"
                    onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS"></asp:TextBox>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Go"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_ValidateGCNo"></asp:AsyncPostBackTrigger>                  
            </Triggers>
        </asp:UpdatePanel>
                
        </td>
        <td style="width:20%">
           <asp:UpdatePanel ID="upd_btn_Go" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btn_Go" runat="Server" CssClass="SMALLBUTTON" Text="Go" OnClick="btn_Go_Click" />                
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Go"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_ValidateGCNo"></asp:AsyncPostBackTrigger>                  
                </Triggers>
            </asp:UpdatePanel>
        </td>   
        <td style="width: 45%"></td>
    </tr>
    
    <tr>
        <td class="TD1" colspan="3">
            <asp:HiddenField ID="hdn_GC_No_Length" runat="server" />
            <asp:HiddenField ID="hdn_No_For_Padd" runat="server" />
        </td>
        <td style="display:none">
            <asp:Button ID="btn_ValidateGCNo" runat="server" CssClass="BUTTON" OnClick="btn_ValidateGCNo_Click" />
        </td>
    </tr>   
    <tr>
      <td class="TD1" style="text-align: left;" colspan="4">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    <asp:Label runat="server" CssClass="LABELERROR" ID="lbl_Errors"></asp:Label>
                    <asp:HiddenField ID="hdn_Rectification_GC_ID" runat="server"></asp:HiddenField>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Go"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_ValidateGCNo"></asp:AsyncPostBackTrigger>                  
            </Triggers>
        </asp:UpdatePanel>
      </td> 
    </tr>   
    <tr>
        <td colspan="4">&nbsp;</td>
    </tr>
</table>
</div>
</form>
<script type="text/javascript">    
     self.parent.hideload();    
</script>
</body>
</html>
