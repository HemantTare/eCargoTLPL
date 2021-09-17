<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_BranchWiseDailyClosingCash.aspx.cs"
    Inherits="Finance_Reports_Frm_BranchWiseDailyClosingCash" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
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
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Wise Daily Closing Cash</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch Wise Daily Closing Cash"></asp:Label>
                </td>
            </tr>
        </table>
        
         <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%; height: 41px;">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>

            <tr>
                <td style="width: 50%">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
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
                    <asp:Label ID="Label1" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="Button1" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>

        <table class="TABLE">
            <tr>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
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
                            <div class="DIV1" style="height: 510px; width: 100%;">
                                <asp:GridView ID="dg_Grid" runat="server" ShowFooter="True"   
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-HorizontalAlign="Left"
                                    PageSize="1000" OnPageIndexChanging="dg_Grid_PageIndexChanging" OnRowCreated="dg_Grid_RowCreated" ShowHeader="False">
                                    <AlternatingRowStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                                    <Columns>

                                        <asp:BoundField DataField="Date" HeaderText="Date">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Opening" HeaderText="Opening">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Receipt" HeaderText="Receipt">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Payment" HeaderText="Payment">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                                                                             
                                        <asp:BoundField DataField="Balance" HeaderText="Balance">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="NonCashReceipt" HeaderText="NonCashReceipt">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
