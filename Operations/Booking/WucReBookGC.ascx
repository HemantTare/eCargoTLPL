<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucReBookGC.ascx.cs" Inherits="Operations_Booking_WucReBookGC" %>
<script type="text/javascript"  src="../../Javascript/Common.js"></script>

<script language="javascript" type="text/javascript">

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

function GC_Details(Path)
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

<asp:ScriptManager ID="SM_ReBookGC" runat="server"></asp:ScriptManager>

<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="4">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="ReBook GC"></asp:Label>
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
                <asp:TextBox ID="txt_GC_No_For_Print" onblur="Validate_GCNo()" 
                    onkeypress="return Only_Integers(this,event)" runat="server" CssClass="TEXTBOXNOS"></asp:TextBox>
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
        <td style="width: 45%;">&nbsp;</td>     
    </tr>     
   <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                    <asp:Label runat="server" CssClass="LABELERROR" ID="lbl_Errors"></asp:Label>
                    <asp:HiddenField ID="hdn_ReBook_GC_ID" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_Is_ReBook_GC_Octroi_Updated" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_Is_ReBook_GC_Octroi_Applicable" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdn_GC_No_Length" runat="server" />
                    <asp:HiddenField ID="hdn_No_For_Padd" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Go"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btn_ValidateGCNo"></asp:AsyncPostBackTrigger>                  
            </Triggers>
        </asp:UpdatePanel>
        </td> 
        <td style="display:none">
            <asp:Button ID="btn_ValidateGCNo" runat="server" CssClass="BUTTON" Text="Get Other Charges"
            OnClick="btn_ValidateGCNo_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="4">&nbsp;</td>
    </tr>     
</table>
