<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPendingDirectDeliveryList.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmPendingDirectDeliveryList" %>

<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">



function viewwindow_DirectDelivery(GCId,GCNo,VehicleId)
{
        var Path='../../../Operations/Delivery/FrmDirectDelivery.aspx?Menu_Item_Id=OAAzAA==&Mode=MQA=&GCId=' + GCId + '&GCNo=' + GCNo + '&VehicleId=' + VehicleId;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpDirectDelivery', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_GCView(GC_ID)
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

  function Summary(BranchId,AreaId,RegionId)
  {
      var Path ='';
      
      Path='../../../Reports/CL_Nandwana/User Desk/FrmPendingDirectDeliverySummary.aspx?Branch_ID=' + BranchId + '&Area_ID=' + AreaId + '&Region_ID=' + RegionId;
      
     
      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-300;
      var popH = h-200;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DirectDeliverySummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Direct Delivery</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_PendingPDS" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Direct Delivery"></asp:Label>
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
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" has_last_row_as_total="false"
                        runat="server" />
                </td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 11%;">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />
                </td>
                <td style="width: 11%;">
                </td>
                <td style="width: 58%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_Summary" runat="server" CssClass="BUTTON" Text="Summary" />
                </td>
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
                            <div class="DIV1" style="height: 510px; width: 90%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_Grid_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_GCNo" Text='<%# DataBinder.Eval(Container, "DataItem.GCNo") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.GCNo") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Count" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="GCDate" HeaderText="LR Date" DataFormatString="{0:dd/MM/yyyy}">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgBranch" HeaderText="Bkg Branch"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ToLocation" HeaderText="Destination"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Parcels" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total_Articles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Parcels" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Payment_Type" HeaderText="Pay Mode"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalFreight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="PayingParty" HeaderText="Paying Client"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PayingMobile" HeaderText="Mobile"></asp:BoundColumn>
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
