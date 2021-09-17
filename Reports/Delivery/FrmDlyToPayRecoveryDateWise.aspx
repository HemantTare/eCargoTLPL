<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDlyToPayRecoveryDateWise.aspx.cs"
    Inherits="Reports_Delivery_FrmDlyToPayRecoveryDateWise" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
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
 
function DlyToPayRecGC(Path)
{ 
    window.open(Path,'DlyToPayRecGC','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
    return false;
} 

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dly ToPay Recovery DateWise</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DlyToPayRecoveryDateWise" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Dly ToPay Recovery Date Wise"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 10%; text-align: right;">
                    <asp:Label ID="lblMONTHNAME" runat="server" CssClass="LABEL" Font-Bold="True" Font-Size="Larger"></asp:Label>
                </td>
                <td style="width: 10%; text-align: left;">
                    <%--  <asp:Label ID="lblYearID" runat="server" CssClass="LABEL" Font-Bold="True" Font-Size="Larger"></asp:Label>--%>
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lblClient_Name" runat="server" CssClass="LABEL" Font-Bold="True" Font-Size="Larger"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
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
                            <div class="DIV1" style="height: 510px; width: 850px;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="true" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DDC_ID" HeaderText="DDC_ID">
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Dly Date">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DlyDate")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_DlyDate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Dly Type">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DlyType")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_DlyType" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Dly No">
                                            <ItemTemplate>
                                                <%--<%# DataBinder.Eval(Container.DataItem, "DlyNo")%>--%>
                                                <asp:LinkButton ID="lbtn_DlyNo" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "DlyNo") %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdfn_DDC_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DDC_ID") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_DlyNo" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cash" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Cash")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Cash" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cheque" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Cheque")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Cheque" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Credit" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Credit")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Credit" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Discount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Discount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Discount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TempoFrt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TempoFrt")%>
                                                <asp:HiddenField ID="hdfn_TempoFrt" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TempoFrt") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TempoFrt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Bonus">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Bonus")%>
                                                <asp:HiddenField ID="hdfn_Bonus" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Bonus") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Bonus" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="TotalTmpFrt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalTmpFrt")%>
                                                <asp:HiddenField ID="hdfn_TotalTmpFrt" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "TotalTmpFrt") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_TotalTmpFrt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
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
