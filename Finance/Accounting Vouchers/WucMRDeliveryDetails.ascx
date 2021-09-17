<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMRDeliveryDetails.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucMRDeliveryDetails" %>

<table class="TABLE" width="100%" border="0">   
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Total_Amount" runat="server" CssClass="LABEL" Text="Through Mr. :"></asp:Label></td>
        <td style="width: 29%">
        <asp:TextBox ID="txt_ThroughMr" runat="server" CssClass="TEXTBOX" />            
        </td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
        <td style="width: 20%"></td>  
        <td style="width:29%"></td>     
        <td style="width:1%"></td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DeliveredTo" runat="server" CssClass="LABEL" Text="Delivered To :"></asp:Label></td>
        <td style="width: 29%">
        <asp:DropDownList ID="ddl_DeliveredTo" runat="server" CssClass="DROPDOWN"  AutoPostBack="true" OnSelectedIndexChanged="ddl_DeliveredTo_SelectedIndexChanged" />          
        </td>
        <td style="width: 1%" class="TDMANDATORY">*
        </td>
        <td  class="TD1" style="width: 20%">
        <asp:Label ID="lbl_DeliveryAgainst" runat="server" CssClass="LABEL" Text="Delivery Against:"></asp:Label>
        </td>  
        <td style="width:29%">
        <asp:UpdatePanel ID="Upd_Pnl_DeliveryAgainst" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_DeliveredTo" />
            </Triggers>
                <ContentTemplate>
        <asp:DropDownList ID="ddl_DeliveryAgainst" runat="server" CssClass="DROPDOWN" />
          </ContentTemplate>
            </asp:UpdatePanel>
        </td>     
        <td style="width:1%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        Font-Bold="True" Text="Fields With * Mark Are Mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
       
    </table>