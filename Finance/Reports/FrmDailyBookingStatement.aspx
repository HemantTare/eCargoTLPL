<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyBookingStatement.aspx.cs"
    Inherits="Finance_Reports_FrmDailyBookingStatement" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%--<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
    TagPrefix="uc3" %>--%>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
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
 
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1000;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
         
        window.open(Path, 'NewPath', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=yes,scrollbars=yes')
        return false;
}
 

function GridTopay(Path,RegionID,AreaID,BranchID,RegionText,AreaText,BranchText,BookingDate)
{   
if (RegionID.value =='')
{
  RegionID.value = '0';
  RegionText.value = 'All';
}
if (AreaID.value =='')
{
  AreaID.value = '0';
  AreaText.value = 'All';
}
if (BranchID.value =='')
{
  BranchID.value = '0';
  BranchText.value = 'All';
} 

    window.open(Path + "&RegionID=" + RegionID.value + "&AreaID=" + AreaID.value + "&BranchID=" + BranchID.value + "&RegionText=" + RegionText.value + "&AreaText=" + AreaText.value + "&BranchText=" + BranchText.value + "&AsOnDate=" + BookingDate.value,'Topay','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
    return false;
} 
function GridPaidToPay(Path)
{ 
    window.open(Path,'PaidTBB','width=400,height=200,top=400,left=500,menubar=no,resizable=no,scrollbars=no')
    return false;
} 
function GridTBB(Path)
{ 
    window.open(Path,'GridTBB','width=800,height=500,top=80,left=100,menubar=no,resizable=yes,scrollbars=yes')
    return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Booking Statement</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily Booking Statement"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%; height: 17px; text-align: right">
                </td>
                <td style="width: 10%; height: 17px">
                </td>
                <td style="width: 10%; height: 17px">
                </td>
                <td style="width: 10%; height: 17px">
                </td>
                <td style="width: 25%; height: 17px">
                </td>

                <td style="width: 25%; height: 17px">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="text-align: right">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 15px; text-align: right">
                </td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 25%; height: 15px">
                </td>
                <td style="width: 25%; height: 15px">
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right;">
                    <asp:Label ID="lbl_BookingDate" runat="server" Text="From "></asp:Label></td>
                <td style="width: 10%">
                    <uc2:WucDatePicker ID="Dtp_BookingDate" runat="server" />
                </td>
                <td style="width: 10%; text-align: right;">
                    <asp:Label ID="lbl_BookingDateToDate" runat="server" Text=" To "></asp:Label></td>
                <td style="width: 10%">
                <uc2:WucDatePicker ID="Dtp_BookingDateToDate" runat="server" />
                    </td>
                <td style="width: 25%; text-align: right;">
                    <asp:Label ID="lbl_PaymentType" runat="server" Text="Payment Type :"></asp:Label>&nbsp;<asp:DropDownList ID="ddl_PaymentType" runat="server">
                        <asp:ListItem Selected="True" Value="12">Paid+ToPay</asp:ListItem>
                        <asp:ListItem Value="3">TBB</asp:ListItem>
                    </asp:DropDownList>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdn_RegionText" runat="server" />
                            <asp:HiddenField ID="hdn_AreaText" runat="server" />
                            <asp:HiddenField ID="hdn_BranchText" runat="server" />
                            <asp:HiddenField ID="hdn_RegionID" runat="server" />
                            <asp:HiddenField ID="hdn_AreaID" runat="server" />
                            <asp:HiddenField ID="hdn_BranchID" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="Wuc_Region_Area_Branch1" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdn_BookingDate" runat="server" />
                            <asp:HiddenField ID="hdn_BookingDateToDate" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Dtp_BookingDate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 25%">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" /></td>
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
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_GridPaidToPay" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 510px; width: 100%; left: 0px; top: 0px;">
                                <asp:Label ID="lbl_Paid" runat="server" Font-Bold="True" Visible="False" Font-Size="Large"
                                    Text="      Paid      " BackColor="#FFE0C0" ForeColor="#C04000" Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridPaidToPay" runat="server" ShowFooter="true" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_GridPaidToPay_PageIndexChanged" OnItemDataBound="dg_GridPaidToPay_ItemDataBound"
                                    Width="80%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Particulars" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "Particulars")%>--%>
                                                <asp:LinkButton ID="lbtn_Particulars" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "Particulars") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Income" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Income")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Service Tax" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Service Tax")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Round Off" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Round Off")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total LR Amount" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total LR Amount")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <hr />
                                <asp:Label ID="lblToPay" runat="server" Font-Bold="True" Visible="False" Font-Size="Large"
                                    Text="      To Pay      " BackColor="#FFE0C0" ForeColor="#C04000" Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridTBB" runat="server" ShowFooter="false" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false" 
                                    OnItemDataBound="dg_GridTBB_ItemDataBound" Width="80%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <%--<FooterStyle CssClass="GRIDFOOTERCSS" BackColor="lightgreen" />--%>
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                    
                                        <asp:TemplateColumn HeaderText=" " ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>


                                        <asp:TemplateColumn HeaderText="Particulars" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdf_Client_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Client_ID")%>' />
                                                <asp:HiddenField ID="hdf_RowType" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "RowType")%>' />
                                                
                                                
                                                <asp:Label ID="lbl_Particulars" runat="server" CssClass="LABEL" Font-Bold="true"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Particulars") %>'></asp:Label>
                                                <asp:LinkButton ID="lbtn_Particulars" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "Particulars") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lbl_Particulars" runat="server" CssClass="LABEL" Font-Bold="true"
                                                    BackColor="lightgreen"></asp:Label>
                                            </FooterTemplate>--%>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="LR No." ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LRNo")%>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Income" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Income")%>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lbl_Income" runat="server" CssClass="LABEL" Font-Bold="true" BackColor="lightgreen"></asp:Label>
                                            </FooterTemplate>--%>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Service Tax" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Service Tax")%>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lbl_ServiceTax" runat="server" CssClass="LABEL" Font-Bold="true" BackColor="lightgreen"></asp:Label>
                                            </FooterTemplate>--%>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Round Off" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Round Off")%>
                                            </ItemTemplate>
                                            <%--<FooterTemplate>
                                                <asp:Label ID="lbl_RoundOff" runat="server" CssClass="LABEL" Font-Bold="true" BackColor="lightgreen"></asp:Label>
                                            </FooterTemplate>--%>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total LR Amount" ItemStyle-HorizontalAlign="Right"
                                            FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total LR Amount")%>
                                            </ItemTemplate>
<%--                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalLRAmount" runat="server" CssClass="LABEL" Font-Bold="true"
                                                    BackColor="lightgreen"></asp:Label>
                                            </FooterTemplate>--%>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>&nbsp;<br />
                                <asp:Button ID="btn_ToPay" runat="server" CssClass="BUTTON" Text="To Pay Details " />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
