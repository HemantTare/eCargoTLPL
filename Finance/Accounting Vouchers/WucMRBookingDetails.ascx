<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMRBookingDetails.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucMRBookingDetails" %>
<%@ Register Src="WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails" TagPrefix="uc2" %>
<%@ Register Src="WucMRGeneralDetails.ascx" TagName="WucMRGeneralDetails" TagPrefix="uc1" %>

<script type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/MRBookingDetails.js"></script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script type="text/javascript">

function Allow_To_Save()
{
    var lbl_Error = document.getElementById('<%=lbl_Errors.ClientID %>');
  
    if(validateGeneralDetails(lbl_Error) == false)
    { return false;}
    if(validateWUCCheque(lbl_Error) == false)
    {return false;}
    
      return true;
}


function GetTotalAmount()
{
    var txt_Total_Receivable = document.getElementById('<%=txt_TotalReceivables.ClientID %>')
    
    return val(txt_Total_Receivable.value);
}

</script>


<table class="TABLE" width="100%">
     <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Money Receipt (Booking)"></asp:Label></td>
    </tr>
    
    <tr>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    
    <tr>
    <td colspan="6">
        <uc1:WucMRGeneralDetails ID="WucMRGeneralDetails1" runat="server" />
     
    </td>
    </tr>
     <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr runat="server" visible="false">
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_Rebookedcharges" runat="server" CssClass="LABEL" Text="Rebooked Charges :" ></asp:Label></td>
        <td style="width: 29%; ">
            <asp:TextBox ID="txt_RebookedCharges" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="60%" ></asp:TextBox></td>
        <td style="width: 1%; ">
        </td>
        <td style="width: 20%; ">
        </td>
        <td style="width: 29%; ">
        </td>
        <td style="width: 1%; ">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_TotalReceivables" runat="server" CssClass="LABEL" Text="Total Receivables :" ></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>
            <asp:TextBox ID="txt_TotalReceivables" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="60%" ></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>   
    
    <tr>
    <td colspan="6">
         
    </td>
        
    </tr>
     <tr>
    <td colspan="6" style="height: 76px">
        <uc2:WucMRCashChequeDetails ID="WucMRCashChequeDetails1" runat="server" />
         
    </td>
        
    </tr>
    
    <tr>
    <td colspan="6">
         
    </td>
        
    </tr>
    
    <tr>
    <td colspan="6">
         
    </td>
        
    </tr>
    
    <tr>
        <td align="center"  colspan="6">
            <asp:Button ID="btn_Save" runat="server"  CssClass="BUTTON" Text="Save & Exit" OnClick="btn_Save_Click" />
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" AccessKey="p" Text="Save & Print" OnClick="btn_Save_Print_Click"  />
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E" OnClick="btn_Close_Click"/>
      </td>
    </tr>
    <tr>
    <td colspan="6">
        &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                Font-Bold="True" Text="Fields With * Mark Are Mandatory" ></asp:Label>    
            </ContentTemplate>
        </asp:UpdatePanel>
       
    </td>
    </tr>
   
</table>
        
        
        
 
