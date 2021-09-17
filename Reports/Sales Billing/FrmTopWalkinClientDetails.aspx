<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTopWalkinClientDetails.aspx.cs"
    Inherits="Reports_Sales_Billing_FrmTopWalkinClientDetails" %>

<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
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

function viewwindow_TopClientDetails(PathClient)
{   

         var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        
        window.open(PathClient, 'CustomPopUpDeliveryArea', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Top WalkIn Client Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Top WalkIn Client Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 100%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <asp:RadioButtonList ID="rdl_ReportType" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rdl_ReportType_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Selected="True" Value="0">Delivery Area Wise (Consignee)</asp:ListItem>
                        <asp:ListItem Value="1">Booking Branch Wise (Consignor)</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server"></uc1:Wuc_Region_Area_Branch>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 40%">
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp;
                    <asp:Label ID="lbl_DeliveryArea" runat="server" Text="Dly Area:" CssClass="LABEL"></asp:Label><asp:DropDownList
                        ID="ddl_DeliveryArea" runat="server" Width="300px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </td>
                <td style="width: 60%">
                    <br />
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
            <%--<tr>
                <td style="width: 40%">
                    &nbsp;</td>
                <td style="width: 60%">
                    &nbsp;</td>
            </tr>--%>
        </table>
        <table class="TABLE" style="background-color:Silver;">
            <tr>
                <td style="width: 40%">
                    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                    <asp:Label ID="lbl_TargetBranch" runat="server" Text="Booking Branch:" CssClass="LABEL"></asp:Label><asp:DropDownList
                        ID="ddl_TargetBranch" runat="server" Width="300px" OnSelectedIndexChanged="ddl_TargetBranch_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                </td>
                <td style="width: 60%">
                    <br />
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker2" runat="server" />
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 575px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="true" AllowCustomPaging="true"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false" PagerStyle-HorizontalAlign="Left"
                                    PageSize="15" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" HorizontalAlign="Center" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DlyArea" HeaderText="DlyArea">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Client Name" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Client_Name" Text='<%# DataBinder.Eval(Container, "DataItem.Client_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Client_Id") %>' />
                                                <asp:HiddenField ID="hdn_Client_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Client_Id") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone1" HeaderText="Phone1">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone2" HeaderText="Phone2">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Address1" HeaderText="Address1">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Address2" HeaderText="Address2">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="NoOfLR" HeaderText="NoOfLR">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Qty" HeaderText="Qty">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="QtyPerLR" HeaderText="Q/LR">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="IsClubbed" HeaderText="IsClubbed">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="NoOfLR2" HeaderText="TargetBr NoOfLR">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="NoOfLRSurat" HeaderText="Surat NoOfLR">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="NoOfLRVapi" HeaderText="Vapi NoOfLR" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="NoOfLRPune" HeaderText="Pune NoOfLR">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
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
