<%@ Page AutoEventWireup="true" CodeFile="Frm_Dly_BranchWise_Pending_Stock_Details.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_Details"
    Language="C#" %>

<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
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

function viewwindow_general(Path)
{
 
//        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
//        var Path='../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var Path=Path;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_DeliveryArea(PathDlyStk)
{
          
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2; 
                    
        window.open(PathDlyStk, 'CustomPopUpDeliveryArea', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_ClientConsignee(ClientId,IsRegular)
{
        if(IsRegular == 'True')
        {
            var Path='../../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + ClientId;
        }
        else
        {
            var Path='../../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=NAA=&Id=' + ClientId;
        }
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'CustomPopUpAUS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_UndeliverReason(GC_Id,GC_No,Reason_Id)
{   
       var Path='../User Desk/Frm_Dly_BranchWise_Pending_Stock_UndeliverReason.aspx?GC_Id=' + GC_Id + '&GC_No=' + GC_No + '&Reason_Id=' + Reason_Id;
       
        var popW = 600;
        var popH = 300;
        var leftPos = (popW)/2;
        var topPos = (popH)/2;
 
        window.open(Path, 'PopUpUndeliverReason', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_ReasonCount(GC_Id,GC_No)
{   

       var Path='../User Desk/Frm_Dly_BranchWise_Pending_Stock_GC_AllUndeliveredReason.aspx?GC_Id=' + GC_Id + '&GC_No=' + GC_No;
       
        var popW = 600;
        var popH = 500;
        var leftPos = (popW)/2;
        var topPos = (popH)/5;
 
        window.open(Path, 'PopUpUndeliverReasonCount', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Stock List View</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Stock List"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Font-Bold="true" ForeColor="Green"
                        Text="Rows in Green Color Indicates Delivery Address Updated Afterwards."></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                    <asp:Label ID="lbl_division" runat="server" CssClass="LABEL" Text="label" /></td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%" class="TD1">
                            </td>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 14%">
                                <asp:Label ID="lblDlyBranch" runat="server" Text="Dly Branch :" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 24%">
                                <asp:Label ID="txtlblDlyBranch" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                            <td style="width: 9%">
                            </td>
                            <td style="width: 24%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Dly Area" HeaderText="Dly Area"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Consignee">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Consignee" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Cnee Name") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="gc_caption No">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "gc_caption No") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_Gc" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Articles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Package" HeaderText="Packaging"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Description" HeaderText="Description"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Size" HeaderText="Size"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Total<br/>Freight">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="gc_caption Date" HeaderText="LR Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="IsAddressUpdated" HeaderText="AddUpdated" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Undeliver Reason">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Undeliver_Reason" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Reason") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ReasonCount">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_ReasonCount" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "ReasonCount") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
