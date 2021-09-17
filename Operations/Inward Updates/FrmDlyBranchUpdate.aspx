<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDlyBranchUpdate.aspx.cs" Inherits="Operations_Inward_Updates_FrmDlyBranchUpdate" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function Allow_To_Save()
{
var ATS = false;
var ddl_NewDeliveryBranch = document.getElementById('<%=ddl_NewDeliveryBranch.ClientID%>');
var ddl_ServiceLocation = document.getElementById('<%=ddl_ServiceLocation.ClientID%>');
var txt_Reason = document.getElementById('<%=txt_Reason.ClientID%>');

var lbl_Error =document.getElementById('<%=lbl_Error.ClientID%>');
var BranchID = get_branch_id();

lbl_Error.innerText='';
if(ddl_NewDeliveryBranch.value == '0')
 {
     lbl_Error.innerText = 'Please Select New DeliveryBranch';
     ddl_NewDeliveryBranch.focus();
 }
 else if(ddl_ServiceLocation.value == '0')
 {
     lbl_Error.innerText = 'Please Select ServiceLocation';
     ddl_ServiceLocation.focus();
 }
 else if(BranchID==0)
 {
    lbl_Error.innerText = 'Please Select Branch';
 }
 else if(txt_Reason.value == '')
 {
     lbl_Error.innerText = 'Please Enter Reason';
     txt_Reason.focus();
 }
 else
  ATS = true;
 return ATS; 
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Branch Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scm_DeliveryBranchUpdate" runat="server">
            </asp:ScriptManager>
            <table class="TABLE">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Branch Update"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_TransactionDate" Text="Transaction Date:" runat="server" CssClass="LABEL">
                        </asp:Label>
                    </td>
                    <td colspan="5" style="width: 80%">
                        <uc3:WucDatePicker ID="WucDatePicker1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_NewDeliveryBranch" Text="New Delivery Branch:" runat="server"
                            CssClass="LABEL" Width="150px"></asp:Label>
                    </td>
                    <td style="width: 29%">
                      <%--  <asp:UpdatePanel ID="up_DDLNewDeliveryBranch" runat="server">
                            <ContentTemplate>--%>
                                <asp:DropDownList runat="SERVER" ID="ddl_NewDeliveryBranch" CssClass="DROPDOWN" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddl_NewDeliveryBranch_SelectedIndexChanged" Width="200px">
                                </asp:DropDownList>
                           <%-- </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                    <td style="width: 1%" class="TDMANDATORY">
                        <asp:Label ID="lbl_Mandatory_NewDelBranch" runat="server" CssClass="LABEL" Text="*"></asp:Label>
                    </td>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="Label1" Text="Service Location:" runat="server" CssClass="LABEL" Width="150px"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="up_ServiceLocation" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList runat="SERVER" ID="ddl_ServiceLocation" CssClass="DROPDOWN" AutoPostBack="True"
                                    Width="200px" OnSelectedIndexChanged="ddl_ServiceLocation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_NewDeliveryBranch" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%" class="TDMANDATORY">
                        <asp:Label ID="lbl_Mandatory_ServiceLocation" runat="server" CssClass="LABEL" Text="*"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 11%">
                                    &nbsp;
                                </td>
                                <td style="width: 89%">
                                    <asp:UpdatePanel ID="up_RegionAreaBranch" runat="server">
                                        <ContentTemplate>
                                            &nbsp;
                                            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Wuc_Region_Area_Branch1" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                     <%--   <asp:UpdatePanel ID="up_SelectedItem" runat="server">
                            <ContentTemplate>--%>
                                <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                         <%--   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        &nbsp;
                    </td>
                </tr>
                <tr id="tr_DlyBranchGrid" runat="server">
                    <td colspan="6">
                        <asp:UpdatePanel ID="up_DlyBranch" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_DlyBranch" runat="server" CssClass="PANEL" ScrollBars="Auto">
                                    <asp:DataGrid ID="dg_DlyBranchUpdate" runat="server" AutoGenerateColumns="False"
                                        CssClass="GRID">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <ItemStyle CssClass="GRIDITEMCSS" />
                                        <Columns>
                                            <asp:BoundColumn DataField="gc_caption no" HeaderText="gc_caption No"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="gc_caption date" HeaderText="gc_caption DATE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Booking_branch" HeaderText="Booking Branch "></asp:BoundColumn>
                                            <asp:BoundColumn DataField="dly_branch" HeaderText="Delivery Branch"></asp:BoundColumn>
                                            <asp:TemplateColumn Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_AUSDate" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "AUS_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Reasons" runat="server" Text="Reason :"></asp:Label>
                    </td>
                    <td style="width: 79%" colspan="4">
                        <asp:TextBox ID="txt_Reason" runat="server" TextMode="MultiLine" CssClass="TEXTBOX"
                            Width="100%"></asp:TextBox>
                    </td>
                    <td style="width: 1%">
                        <asp:Label ID="lbl_MD_Reason" runat="server" CssClass="TDMANDATORY" Text="*"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                      <%--  <asp:UpdatePanel ID="up_Errors" runat="server">
                            <ContentTemplate>--%>
                                <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label>
                                <asp:HiddenField ID="hdn_AUSCaption" runat="server"></asp:HiddenField>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
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
