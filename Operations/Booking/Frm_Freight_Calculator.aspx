<%@ Page AutoEventWireup="true" CodeFile="Frm_Freight_Calculator.aspx.cs" Inherits="Operations_Booking_Frm_Freight_Calculator"
    Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

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


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Freight Calculator</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Freight Calculator"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%;" class="TD1">
                    From Branch :
                </td>
                <td style="width: 20%;">
                    &nbsp;<asp:DropDownList ID="ddlFromBranch" runat="server" CssClass="DROPDOWN" Width="90%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlFromBranch_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;" class="TD1">
                    To Branch
                </td>
                <td style="width: 20%;">
                    <asp:DropDownList ID="ddlToBranch" runat="server" CssClass="DROPDOWN" Width="90%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlToBranch_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;" class="TD1">
                    Delivery Area :
                </td>
                <td style="width: 20%;">
                    <asp:DropDownList ID="ddlDeliveryArea" runat="server" CssClass="DROPDOWN" Width="90%">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 20%;" class="TD1">
                    Freight / Parcl :</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtFreightRate" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="0" Width="100px"></asp:TextBox></td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%;" class="TD1">
                    Hamali / Kg :</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtHamaliPerKg" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="0" Width="100px"></asp:TextBox></td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%;" class="TD1">
                    &nbsp;Statistic Chrg :</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtStatisticCharge" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="0" Width="100px"></asp:TextBox></td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;" class="TD1">
                    Size :</td>
                <td style="width: 20%;">
                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="DROPDOWN" Width="90%" AutoPostBack="true">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 20%;" class="TD1">
                    FOV %</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtFovPercent" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="0" Width="100px"></asp:TextBox></td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;" class="TD1">
                    No. Of Parcls :</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtNoOfParcls" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="1" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%;" class="TD1">
                    AOC %</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtAOCPercent" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="0" Width="100px"></asp:TextBox></td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;" class="TD1">
                    Invoice Value :</td>
                <td style="width: 20%;">
                    <asp:TextBox ID="txtInvoiceValue" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                        onkeypress="return Only_Numbers(this,event)" Text="0" Width="100px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 20%;" class="TD1">
                </td>
                <td style="width: 20%;">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%;">
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
                <td style="width: 20%;">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="300px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DlyAreaName" HeaderText="Delivery Area"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DlyAreaDiscPercent" HeaderText="DlyAreaDisc%" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ToBe" HeaderText="ToBe" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Freight" HeaderText="Freight" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="QtyDiscount" HeaderText="QtyDiscount" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-ForeColor="Red"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Hamali" HeaderText="Hamali" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Statistic" HeaderText="Statistic" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FOV" HeaderText="FOV" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DDCharge" HeaderText="DDCharge" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="AOC" HeaderText="AOC" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="SubTotal" HeaderText="SubTotal" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RoundOff" HeaderText="RoundOff" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TotalLRAmount" HeaderText="TotalLRAmount" ItemStyle-HorizontalAlign="Center">
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        &nbsp;
    </form>
</body>
</html>
