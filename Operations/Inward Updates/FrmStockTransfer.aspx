<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmStockTransfer.aspx.cs" Inherits="Operations_Inward_Updates_FrmStockTransfer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc2" %>

<script type="text/javascript">
function Allow_To_Save()
{
var ATS = false;
var ddl_NewCurrentBranch = document.getElementById('<%=ddl_NewCurrentBranch.ClientID%>');
var txt_Reason = document.getElementById('<%=txt_Reason.ClientID %>');

var lbl_Error =document.getElementById('<%=lbl_Error.ClientID%>');
var BranchID = get_branch_id();
lbl_Error.innerText='';
if(ddl_NewCurrentBranch.value == '0')
 {
     lbl_Error.innerText = 'Please Select New Current Branch';
     ddl_NewCurrentBranch.focus();
 }
 else if(BranchID==0)
 {
    lbl_Error.innerText = 'Please Select Branch';
 }
 else if(txt_Reason.value == '')
 {
    lbl_Error.innerText='Please Enter Reason';
    txt_Reason.focus();
 }
 else
  ATS = true;
 return ATS; 
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stock Transfer</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scm_DeliveryBranchUpdate" runat="server">
            </asp:ScriptManager>
            <table class="TABLE">
                <tr>
                    <td class="TDGRADIENT" colspan="3">
                        <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Stock Transfer"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_TransactionDate" Text="Transaction Date:" runat="server" CssClass="LABEL">
                        </asp:Label>
                    </td>
                    <td colspan="2" style="width: 80%">
                        <uc3:WucDatePicker ID="WucDatePicker1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_NewCurrentBranch" Text="New Current Branch:" runat="server" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%">
                     <%--   <asp:UpdatePanel ID="up_DDLNewCurrentBranch" runat="server">
                            <ContentTemplate>--%>
                                <asp:DropDownList runat="SERVER" ID="ddl_NewCurrentBranch" CssClass="DROPDOWN" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_NewCurrentBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                           <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                    <td style="width: 1%" class="TDMANDATORY">
                        <asp:Label ID="lbl_Mandatory_NewDelBranch" runat="server" CssClass="LABEL" Text="*"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                                <td style="width: 90%">
                                    <asp:UpdatePanel ID="up_RegionAreaBranch" runat="server">
                                        <ContentTemplate>
                                            &nbsp;
                                            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="up_StockTransfer" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_StockTransfer" runat="server" CssClass="PANEL" ScrollBars="Auto">
                                    <asp:DataGrid ID="dg_StockTransfer" runat="server" AutoGenerateColumns="False" CssClass="GRID">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <ItemStyle CssClass="GRIDITEMCSS" />
                                        <Columns>
                                            <asp:BoundColumn DataField="gc_caption no" HeaderText="gc_caption No"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="gc_caption date" HeaderText="gc_caption Date"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="booking_branch" HeaderText="Booking Branch"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="dly_branch" HeaderText="Delivery Branch"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="articles" HeaderText="Articles"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Document Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_AUSDate" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Document_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                                <asp:AsyncPostBackTrigger ControlID="ddl_NewCurrentBranch" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Reasons" runat="server" Text="Reason :"></asp:Label>
                    </td>
                    <td style="width: 79%">
                        <asp:TextBox ID="txt_Reason" runat="server" TextMode="MultiLine" CssClass="TEXTBOX"
                            Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:Label ID="lbl_MD_Reason" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label></td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
                </tr>
                <tr><td colspan="3">&nbsp;</td></tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
    self.parent.hideload()
    </script>

</body>
</html>
