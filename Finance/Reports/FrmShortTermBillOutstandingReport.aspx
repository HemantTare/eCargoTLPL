<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmShortTermBillOutstandingReport.aspx.cs" Inherits="Finance_Reports_FrmShortTermBillOutstandingReport" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
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
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Short Term Bill Outstanding</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ShortTermBill" runat="server"></asp:ScriptManager>

   
        <table class="TABLE" style="width: 100%">
            <tr>
                <td class="TDGRADIENT" colspan="8">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Short Term Bill Outstanding Report"></asp:Label></td>
            </tr>
        </table>
    
   
        <table id = "tbl_input_screen" style="width: 100%" class = "TABLE">
            <tr>
                <td id="td_lblBranch" runat="server" style="width: 20%">
                    <asp:Label ID="lbl_Branch" runat="server" Text="Branch:" />
                </td>
                <td id="td_BranchData" runat="server" style="width: 29%">
                    <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN" AutoPostBack="false"
                        runat="server" />
                </td>
                <td style="width:20%;text-align:right;" >
                  <asp:Label ID="lbl_ClientName" runat="server" Text="Client Name:" />
                </td>
                 <td style="width:29%" >
                   <asp:TextBox ID="txt_ClientName" CssClass="TEXTBOX" runat="server" />
                </td>
            </tr>
            <tr>
                 <td  runat="server" style="width: 20%">
                    <asp:Label ID="lbl_AsOnDate" runat="server" Text="As On Date:" />
                </td>
                  <td id="td2" runat="server" style="width: 29%">
                    <uc3:WucDatePicker ID="WucDatePicker1" runat="server"  />
                </td>
            </tr>   
  </table>
  <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" OnClick="btn_view_Click" Text="View" />
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
               <td colspan="2">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_shortTermBillDetails" UpdateMode="Conditional" runat="server">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                    </Triggers>
                    <ContentTemplate>
                         <div class="DIV" style="width: 100%; height:510Px">
                            <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="true"
                                ShowFooter="True" CssClass="GRID" OnItemDataBound="dg_Grid_ItemDataBound" OnPageIndexChanged="dg_Grid_PageIndexChanged" PageSize="50">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <Columns>
                                    <asp:BoundColumn DataField="Branch_Name" HeaderText="Branch"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Credit_Memo_No_For_print" HeaderText="Credit Memo No"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Credit_Memo_date" HeaderText="Credit Memo date"></asp:BoundColumn>
                                    
                                    <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption No"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Ledger_Name" HeaderText="Ledger Name"></asp:BoundColumn> 
                                    <asp:BoundColumn DataField="OverDueDays" HeaderText="OverDueDays"></asp:BoundColumn>                            
                                    <asp:TemplateColumn HeaderText="Pending Amount">                         
                                    <ItemTemplate>
                                     <%#convertToDrCr(Eval("Pending Amount"))%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_PendingAmount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>                                                      
                                    </Columns>
                                <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
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
