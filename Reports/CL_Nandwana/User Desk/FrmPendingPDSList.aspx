<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPendingPDSList.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmPendingPDSList" %>

<%@ Register Src="../../../CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function input_screen_action(action)
{
if (action == 'view')
  {
  tbl_input_screen.style.display='inline';
  }
else
  {
  tbl_input_screen.style.display='none';
  }
}



function viewwindow_PDS(PDSId,Delivery_Mode_Id,VehicleId)
{
        
        var Path='../../../Operations/Delivery/FrmDDC.aspx?Menu_Item_Id=OAAyAA==&Mode=MQA=&PendingPDSId=' + PDSId + '&Delivery_Mode_Id=' + Delivery_Mode_Id + '&VehicleId=' + VehicleId;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpPDS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending PDS</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_PendingPDS" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending PDS"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%;">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 11%;">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 11%;">
                </td>
                <td style="width: 58%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="Upd_Pnl_PendingPDS" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 560px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" CssClass="GRID" AllowSorting="True"
                                    AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    OnItemDataBound="dg_Grid_ItemDataBound" Width="95%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Branch_Name" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PDS_Date" HeaderText="PDS Date" DataFormatString="{0:dd/MM/yyyy}"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="PDS No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_PDSNo" Text='<%# DataBinder.Eval(Container, "DataItem.PDS_No_For_Print") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.PDS_No_For_Print") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No." HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DriverName" HeaderText="Driver" HeaderStyle-HorizontalAlign="Left"
                                            ItemStyle-HorizontalAlign="Left"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NoOfLR" HeaderText="No Of LR" ItemStyle-HorizontalAlign="Right">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
