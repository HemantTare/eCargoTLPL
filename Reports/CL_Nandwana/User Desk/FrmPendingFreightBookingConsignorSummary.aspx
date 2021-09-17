<%@ Page AutoEventWireup="true" CodeFile="FrmPendingFreightBookingConsignorSummary.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmPendingFreightBookingConsignorSummary"
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



function viewwindow_DeliveryArea(PathDlyStk)
{
          
//        var w = screen.availWidth;
//        var h = screen.availHeight;
//        var popW = 900;
//        var popH = 650;
//        var leftPos = (w-popW)/2;
//        var topPos = (h-popH)/2; 
              
         var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        
 
        window.open(PathDlyStk, 'PendingFrtConsignorSummary', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_ClientConsignor(ClientId,IsRegular)
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
 
        window.open(Path, 'PendingFrtConsignorDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Freight Booking Consignor Summary</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Freight Booking Consignor Summary"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lblBkgBranch" runat="server" Font-Bold="True" Text="Bkg Branch :"></asp:Label></td>
                            <td style="width: 24%">
                                &nbsp;<asp:Label ID="txtlblBkgBranch" runat="server" Font-Bold="True" Text="Label"></asp:Label></td>
                            <td class="TD1" style="width: 9%">
                            </td>
                            <td style="width: 9%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                            <td style="width: 24%">
                            </td>
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
            <tr style="background-color: Yellow" align="center">
                <td style="height: 15px">
                    <asp:Label ID="lbl_Other" runat="server" CssClass="HEADINGLABEL" Font-Bold="true"
                        ForeColor="Navy" Text="For Booking Branch"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="150px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="True" AllowPaging="True"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanged="dg_Details_PageIndexChanged" PageSize="5" OnItemDataBound="dg_Details_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Consignor" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Consignor" Text='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No" HeaderStyle-Width="10%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgDate" HeaderText="Booking Date" HeaderStyle-Width="10%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason" HeaderStyle-Width="40%"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Gc" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Articles" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalArticles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Articles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_TotalFreight" Text='<%# DataBinder.Eval(Container, "DataItem.TotalFreight") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr style="background-color: Lime" align="center">
                <td style="height: 15px">
                    <asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Font-Bold="true" ForeColor="Black"
                        Text="For MUMBAI OFFICE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server" Height="150px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_detailsHO" runat="server" ShowFooter="True" AllowPaging="True"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanged="dg_DetailsHO_PageIndexChanged" PageSize="5" OnItemDataBound="dg_DetailsHO_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Consignor" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Consignor" Text='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No" HeaderStyle-Width="10%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgDate" HeaderText="Booking Date" HeaderStyle-Width="10%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason" HeaderStyle-Width="40%"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Gc" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Articles" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalArticles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Articles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_TotalFreight" Text='<%# DataBinder.Eval(Container, "DataItem.TotalFreight") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr style="background-color: Navy" align="center">
                <td style="height: 15px">
                    <asp:Label ID="lbl_WeeklyPayment" runat="server" CssClass="HEADINGLABEL" Font-Bold="true"
                        ForeColor="Yellow" Text="WEEKLY PAYMENT - SATURDAY KO PAYMENT MILEGA"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details2" runat="server" ShowFooter="True" AllowPaging="True"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False"
                                    OnPageIndexChanged="dg_Details2_PageIndexChanged" PageSize="5" OnItemDataBound="dg_Details2_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Consignor" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Consignor" Text='<%# DataBinder.Eval(Container, "DataItem.Consignor_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="20%" />
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No" HeaderStyle-Width="10%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BkgDate" HeaderText="Booking Date" HeaderStyle-Width="10%">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason" HeaderStyle-Width="40%"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Gc" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Articles" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalArticles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Articles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_TotalFreight" Text='<%# DataBinder.Eval(Container, "DataItem.TotalFreight") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
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
