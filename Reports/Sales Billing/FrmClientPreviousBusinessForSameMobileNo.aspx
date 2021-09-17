<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClientPreviousBusinessForSameMobileNo.aspx.cs"
    Inherits="Reports_Sales_Billing_FrmClientPreviousBusinessForSameMobileNos" %>

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

function viewwindow_GC(GC_ID)
{
 
        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        window.open(Path, 'ClientBusinessGCTrace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_ClientConsignee(ClientId,IsRegular)
{
        if(IsRegular == 'True')
        {
            var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + ClientId;
        }
        else
        {
            var Path='../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=NAA=&Id=' + ClientId;
        }
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'PendingFrtConsigneeDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}


function viewwindow_Month(ClientName,ClientId,IsRegular,MonthName,Year)
{
        
        var Path='../../Reports/Sales Billing/FrmClientPreviousBusinessMonthlyDetails.aspx?ClientName=' + ClientName + '&Consignee_Client_ID=' + ClientId + '&Is_Consignee_Regular_Client=' + IsRegular + '&MonthName=' + MonthName + '&Year=' + Year;
        
        
        var w = 800;
        var h = 700;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'PreviousBusinessMonthlyDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Previous Business Check</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Client Previous Business Check"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 100%; height: 15px;">
                </td>
            </tr>
            
            <tr>
                <td style="width: 100%; height: 15px;">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text="" Visible="false"></asp:Label></td>
            </tr>
        </table>
        <table id="tbl_Header" runat="server" class="TABLE" style="background-color: Beige; font-weight: bold;
            border: 1px solid red">
            <tr>
                <td style="width: 40%;" align="center">
                    Consignee Name
                </td>
                <td style="width: 25%" align="center">
                    Delivery Area
                </td>
                <td style="width: 15%" align="center">
                    Category
                </td>
                <td style="width: 20%" align="center">
                    Registered On
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 10px;" align="center">
                </td>
            </tr>
            <tr>
                <td style="width: 40%;" align="center">
                    <asp:LinkButton ID="lnk_ClientName" runat="server" CssClass="LABEL" />
                </td>
                <td style="width: 25%" align="center">
                    <asp:Label ID="lbl_DeliveryArea" ForeColor="Red" runat="server" CssClass="LABEL"></asp:Label>
                </td>
                <td style="width: 15%" align="center">
                    <asp:Label ID="lbl_Category" ForeColor="Red" runat="server" CssClass="LABEL"></asp:Label>
                </td>
                <td style="width: 20%" align="center">
                    <asp:Label ID="lbl_RegisteredOn" ForeColor="Red" runat="server" CssClass="LABEL"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 15px;" align="center">
                </td>
            </tr>
        </table>
        <table id="tbl_LastBusiness" runat="server" class="TABLE">
            <tr>
                <td style="width: 60%; height: 15px; font-weight: bold; color: Purple; font-size:medium;" align="Center">
                    Last 10 Bookings
                    <table width="100%" style="background-color:#ffccff;">
                        <tr>
                            <td align="Left" style="width: 100%">
                                <asp:UpdatePanel ID="Upd_Pnl1" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Panel ID="pnl_Pnl1" runat="server" Height="250px" ScrollBars="Auto">
                                            <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="False" CssClass="GRID"
                                                AllowSorting="True" AllowCustomPaging="False" AutoGenerateColumns="False" OnItemDataBound="dg_Grid_ItemDataBound"
                                                PageSize="25">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="LR No.">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_GCNo" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No") %>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="LR Date" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "GC_Date")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="DlyArea" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "DlyArea")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Item" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Item")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Size" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Size")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Parcels" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_Articles")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalArticles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Inv. Value" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_Invoice_Value")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalInvValue" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            &nbsp;
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 10%; height: 15px; font-weight: bold; color: Navy;" align="center">
                    &nbsp;
                </td>
                <td style="width: 40%; height: 15px; font-weight: bold; color: Purple; font-size:medium;" align="Center">
                    Last 12 Months Business
                    <table width="100%" style="background-color:#ccff66">
                        <tr>
                            <td align="Left" style="width: 100%">
                                <asp:UpdatePanel ID="Upd_Pnl2" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid2" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Panel ID="pnl_Pnl2" runat="server" Height="250px" ScrollBars="Auto">
                                            <asp:DataGrid ID="dg_Grid2" runat="server" ShowFooter="True" AllowPaging="False"
                                                CssClass="GRID" AllowSorting="True" AllowCustomPaging="False" AutoGenerateColumns="False"
                                                OnItemDataBound="dg_Grid2_ItemDataBound" PageSize="25">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Month">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_Month" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "MonthName") %>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_Total2" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="NoOfLR" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "NoOfLR")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalNoOfLR2" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Parcels" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Parcls")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalArticles2" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Freight" HeaderStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "TotalFreight")%>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lbl_TotalFreight2" runat="server" CssClass="LABEL" Font-Bold="true" />
                                                        </FooterTemplate>
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            &nbsp;
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
