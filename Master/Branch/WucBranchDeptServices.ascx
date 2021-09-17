<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranchDeptServices.ascx.cs" Inherits="Master_Branch_WucBranchDeptServices" %>

<table class="TABLE">
 <tr>
        <td colspan="6">&nbsp;</td>
 </tr>
<tr>
<td colspan="6">
<asp:Panel ID="pnl_Departments"  CssClass="PANEL" Font-Bold="False" GroupingText="Department" runat="server">
<table width="100%">
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td style="WIDTH: 6%" class="TD1"></td>
        <td colspan="5">
            <asp:CheckBoxList id="chk_List_Department" CssClass="CHECKBOXLIST" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
            </asp:CheckBoxList>
        </td>
    </tr>
</table>
</asp:Panel>
</td>
</tr>
<tr>
     <td colspan="6">&nbsp;</td>
</tr>
<tr>
<td colspan="6">
<asp:Panel ID="pnl_service" CssClass="PANEL" Font-Bold="False" GroupingText="Services Offered" runat="server">
            <table width="100%" border="0">  
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>  
                <tr>
                    <td style="WIDTH: 6%" class="TD1"><asp:CheckBox id="chkbx_isBookingallowed" runat="server"></asp:CheckBox></td>
                    <td style="WIDTH: 13%" ><asp:Label ID="label1" Font-Bold="False" runat="server" CssClass="LABEL" Text="Booking Allowed?"></asp:Label>  </td>
                    <td style="WIDTH: 2%">
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">  
                            <ContentTemplate>--%>
                                <asp:CheckBox id="chkbx_isDelivaryallowed" runat="server"></asp:CheckBox>
                           <%-- </ContentTemplate>                           
                        </asp:UpdatePanel>--%>
                        </td>
                    <td style="WIDTH: 15%" ><asp:Label ID="label2" Font-Bold="False" runat="server" CssClass="LABEL" Text="Delivery Allowed?"></asp:Label></td>
                    <td style="WIDTH: 2%">
                       <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">  
                            <ContentTemplate>--%>
                                <asp:CheckBox id="chkbx_isCrossingbranch" runat="server"></asp:CheckBox>
                            <%--</ContentTemplate>                           
                        </asp:UpdatePanel>  --%>          
                    </td>
                    <td style="WIDTH: 10%"><asp:Label ID="label3" Font-Bold="False" runat="server" CssClass="LABEL" Text="Crossing Branch?"></asp:Label></td>
                    <td style="WIDTH: 20%"></td>
                </tr>
               
                <tr>
                    <td style="WIDTH: 6%"  class="TD1"><asp:CheckBox id="chkbx_isFranchiseebranch" runat="server"></asp:CheckBox></td>
                    <td style="WIDTH: 13%"><asp:Label ID="label4" Font-Bold="False" runat="server" CssClass="LABEL" Text="Franchisee Branch?"></asp:Label></td>
                    <td style="WIDTH: 2%"><asp:CheckBox id="chkbx_isComputerisedbranch" runat="server"></asp:CheckBox></td>
                    <td style="WIDTH: 15%"><asp:Label ID="label5" Font-Bold="False" runat="server" CssClass="LABEL" Text="Computerised Branch?"></asp:Label></td>
                    <td style="WIDTH: 2%"><asp:CheckBox id="chkbx_isOctroiApplicable" runat="server"></asp:CheckBox></td>
                    <td style="WIDTH: 10%" ><asp:Label ID="label6" Font-Bold="False" runat="server" CssClass="LABEL" Text="Octroi Applicable?"></asp:Label></td>
                    <td style="WIDTH: 20%"></td>

                </tr>   
                <tr>
                 <td colspan="6">&nbsp;</td>
                </tr>
            </table>
</asp:Panel>
</td>
</tr>
<tr>
     <td colspan="6">&nbsp;</td>
</tr>
 <tr>
    <td colspan="6">
        <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
    </td>
    </tr>
</table>
<%--<script type="text/javascript" language="javascript">
 EnableDisable_IsDeliveryAllowed();
 EnableDisable_IsCrossingAllowed();
</script>--%>

<script type="text/javascript" language="javascript">
 EnableDisable_Agency();
</script>
