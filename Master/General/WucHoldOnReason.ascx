<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucHoldOnReason.ascx.cs" Inherits="Master_General_WucHoldOnReason" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<script type ="text/javascript" language="javascript">
 function Allow_To_Save()
 {
    var txt_Reason=document.getElementById('<%=txt_Hold_On_Reason.ClientID %>');
    var lbl_Errors=document.getElementById('<%=lbl_Errors.ClientID %>');

    var ATS=false;
    if(txt_Reason.value=='')
    {
        lbl_Errors.innerHTML="Please Enter Hold On Reason"; //objResource.GetMsg("Msg_HoldOnReason");
        txt_Reason.focus();
    }
    else
    {
        ATS=true;
    }
    return ATS;
 }
</script>
<table class="TABLE">
     <tr>
        <td colspan="6" style="width: 100%" class="TDGRADIENT">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" Text="Hold On Reason" CssClass="HEADINGLABEL" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="TD">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:20%; vertical-align: top;" class="TD1"> 
               <asp:Label ID="lbl_HoldOnReason" runat="server" CssClass="LABEL" Text="Hold On Reason :" meta:resourcekey="lbl_HoldOnReasonResource1"></asp:Label></td>
        <td colspan="3" class="TDMANDATORY">
             <asp:TextBox ID="txt_Hold_On_Reason" 
              runat="server"  BorderWidth="1px" MaxLength="250" CssClass="TEXTBOX" meta:resourcekey="txt_Hold_On_ReasonResource1" Width="600px"></asp:TextBox>&nbsp;
            </td>
        <td style="width:1%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td colspan="6"> &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width:100%;vertical-align:middle;text-align:center">
            <asp:Button ID="Btn_Save" runat="Server" CssClass="BUTTON" Text="Save" OnClientClick="return Allow_To_Save()" OnClick="Btn_Save_Click" meta:resourcekey="Btn_SaveResource1" />
        </td>
    </tr>
   
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" CssClass="LABELERROR" runat="server" Text="Fields With * Mark Are Mandatory"></asp:Label>
         </td>
    </tr>
</table>