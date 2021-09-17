<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPendingDeliveryAddress.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmPendingDeliveryAddress" %>

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



function viewwindow_Client(ClientId,GCId)
{
        var Path='../../../Master/Sales/FrmRegularClientPendingAddress.aspx?Menu_Item_Id=MwA2AA==&Mode=MgA=&Id=' + ClientId + '&GCId=' + GCId;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'CustomPopUpAUS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
 
        self.close();
        return false;
}


function viewwindow_ClientConsignor(ClientId)
{
        var Path='../../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + ClientId;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'CustomPopUpAUS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}


function viewwindow_GC(GC_ID)
{
 
        var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
//        var Path='../../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Delivery Address</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_PendingPDS" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Delivery Address"></asp:Label>
                </td>
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
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" has_last_row_as_total="false"
                        runat="server" />
                </td>
                <td style="width: 11%;">
                </td>
                <td style="width: 58%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr style="background-color: Yellow;">
                <td align="Center">
                    <asp:Label ID="lbl_DlyLocation" runat="server" Font-Bold="true" ForeColor="Maroon"
                        Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="Pending">
                <td align="center">
                    <asp:UpdatePanel ID="Upd_Pnl_PendingPDS" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 510px; width: 95%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="false" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    PagerStyle-HorizontalAlign="Left" OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DlyArea" HeaderText="Dly Area"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_GCNo" Text='<%# DataBinder.Eval(Container, "DataItem.GC_No") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.GC_No") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Consignee" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_ConsigneeName" Text='<%# DataBinder.Eval(Container, "DataItem.Consignee_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Consignee_Name") %>' />
                                                <asp:Label ID="lbl_ConsigneeName" Text='<%# DataBinder.Eval(Container, "DataItem.Consignee_Name") %>'
                                                    runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Consignee_Name") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Consignee_Address" HeaderText="Consignee Address"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignee_Mobile_No" HeaderText="Consignee Mobile"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Consignor" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_ConsignorName" Text='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>' />
                                                <asp:Label ID="lbl_ConsignorName" Text='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>'
                                                    runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Consignor_Mobile_No" HeaderText="Consignor Mobile"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ConsigneePhone1" HeaderText="Consignee Phone1"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ConsigneePhone2" HeaderText="Consignee Phone2"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GC_Date" HeaderText="LR Date" DataFormatString="{0:dd/MM/yyyy}">
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
