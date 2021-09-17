<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDelete.ascx.cs" Inherits="Display_WucDelete" %>
<%@ Register Src="~/Display/WucCommaSeparatedGc.ascx" TagName="WucCommaSeparatedGC" TagPrefix="uc1" %>


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script src="../Javascript/Common.js" type="text/javascript"></script>

<script type="text/javascript">

function Allow_To_Save()
{
    var ATS = false;
    var txt_Reason =  document.getElementById('<%=txt_Reason.ClientID %>');
    var Lbl_Error = document.getElementById('<%=lbl_Errors.ClientID %>');
        
    Lbl_Error.innerText ='';
    
    if(Trim(txt_Reason.value) == '')
        {
           Lbl_Error.innerText = 'Please Fill Reason....';
           txt_Reason.focus();
           return false; 
        }
    else if(Trim(txt_Reason.value)!='')
        {
            
                return (confirm('Are you sure you want to Cancel?'));
           
        }
        else
        ATS=true;

    return ATS;
}

</script>

<table class="TABLE" width="100%">
    <tr>
        <td colspan="3" class="TDGRADIENT" >
        &nbsp;
    <asp:Label ID="lblHeader" runat="server" CssClass="HEADINGLABEL"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height:10px">
        </td>
    </tr>
    <tr>
        <td colspan="4" class="TD1">
          <uc1:WucCommaSeparatedGC ID="WucCommaSeparatedGC1" runat="server" />     
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            Reason :</td>
        <td style="width: 70%;">
        <asp:TextBox ID="txt_Reason" runat="server" CssClass="TEXTBOX" Width="644px"   onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" Height="100px" TextMode="MultiLine"></asp:TextBox>
        </td>
        <td style="width:10%;" >
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height:10px">
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center" style="height:10px">
        <asp:UpdatePanel ID="up_Btn" runat="server" UpdateMode="Always">
        <ContentTemplate>
        <asp:Button ID="btn_Cancel" runat="server" CssClass="BUTTON" OnClick="btn_Cancel_Click"/>
        </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3"style="height:10px"><asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                ></asp:Label>
        </td>
    </tr>
</table>
