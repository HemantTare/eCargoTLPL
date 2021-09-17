<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClosingCashBalNew.aspx.cs"
    Inherits="Finance_Reports_FrmClosingCashBalNew" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function viewwindow_GC(GC_ID)
{
 
        //var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
       var Path='../../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1100;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        window.open(Path, 'BranchCashBalanceNew', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function openVoucherWindow(DocNo)
    {
        var Path='../../Finance/VoucherView/FrmVoucher.aspx?Id='+ DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpClosingCashNew', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }

function DlyBranchToPayRecovery(Path)
  {

      var w = screen.availWidth;
      var h = screen.availHeight;
      var popW = w-250;
      var popH = h-50;
      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open(Path, 'DlyBranchToPayRecovery', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=yes,statusbar=no');
      return false;
  } 
  
  function Open_Details_Window(Path)
{ 
  window.open(Path,'BkgReg','width=1000,height=800,top=0,left=0,menubar=no,resizable=yes,scrollbars=yes')
  return false;
} 
 
  function openSummaryWindow(DetailType,BranchID,AsOnDate)     
    {
        var Path='FrmClosingCashBalDetailsNew.aspx?DetailType=' + DetailType + '&BranchID='+ BranchID + '&AsOnDate=' + AsOnDate;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 700;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUp_Track_And_Traceddc', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }

    function openFrtDiscountWindow(DocNo)
    {
        var Path='../../Operations/Delivery/FrmFrghtDisVoucher.aspx?Menu_Item_Id=MgA4ADcA&Mode=NAA=&Id=' + DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpClosingCashFrtDiscount', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
  
  function openDieselExpenseWindow(DocNo)
    {
        var Path='../../Finance/Accounting Vouchers/FrmDieselExpense.aspx?Menu_Item_Id=MwA1ADgA&Mode=NAA=&Id=' + DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpClosingCashDieselExpense', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
    
   function openConveyanceExpenseWindow(DocNo)
    {
        var Path='../../Finance/Accounting Vouchers/FrmConveyanceVoucher.aspx?Menu_Item_Id=MwA1ADkA&Mode=NAA=&Id=' + DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpClosingCashConveyanceExpense', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
    
   function openPetrolExpenseWindow(DocNo)
    {
        var Path='../../Finance/Accounting Vouchers/FrmPetrolExpense.aspx?Menu_Item_Id=MwA2ADAA&Mode=NAA=&Id=' + DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpClosingCashPetrolExpense', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Cash Balance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      .SHOWSELECTEDLINK{FONT-SIZE: 11px;FONT-FAMILY: Verdana;color:#0033ff;text-decoration:underline;}
</style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ClosingCashBal" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Cash Balance" ></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 12%">
                    <asp:Label ID="lblBranch" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="DarkMagenta"></asp:Label></td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 12%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right;">
                    <asp:Label ID="lbl_SelectDate" runat="server" Text="Select Date : "></asp:Label></td>
                <td style="width: 10%">
                    <uc2:WucDatePicker ID="Dtp_AsOnDate" runat="server"></uc2:WucDatePicker>
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_Print" runat="server" CssClass="BUTTON" OnClick="btn_Print_Click"
                        Text="Print" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <%--            <tr>
                <td style="width: 50%; height: 15px;" align="Center">
                    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbl_OpeningBalance" ForeColor="Red" Text="0.00" Font-Bold="true" Font-Size="Medium"
                                runat="server" CssClass="LABEL"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%; height: 15px;" align="Center">
                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbl_ClosingBalance" ForeColor="Red" Text="0.00" Font-Bold="true" Font-Size="Medium"
                                runat="server" CssClass="LABEL"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
--%>
            <tr>
                <td style="width: 50%; height: 15px; font-weight: bold; color: Purple; font-size: medium;"
                    align="Center">
                    <table width="100%" style="background-color: White;">
                        <tr>
                            <td align="Left" style="width: 100%">
                                <asp:UpdatePanel ID="Upd_ClosingCashBal" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_View" />
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="DIV" style="height: 450px; width: 100%;">
                                            <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" CssClass="GRID" AllowSorting="True"
                                                AutoGenerateColumns="False" OnItemDataBound="dg_Grid_ItemDataBound" Width="95%">
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Header" HeaderText="Type">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Document No.">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_TransactionNoCr" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="Particulars" HeaderText="Particulars">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Cr Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_AmountCr" runat="server" ToolTip='<%# DataBinder.Eval(Container.DataItem, "Narration") %>'
                                                                CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%; height: 15px; font-weight: bold; color: Purple; font-size: medium;"
                    align="Center">
                    <table width="100%" style="background-color: White;">
                        <tr>
                            <td align="Left" style="width: 100%">
                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_View" />
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid2" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="DIV" style="height: 450px; width: 100%;">
                                            <asp:DataGrid ID="dg_Grid2" runat="server" ShowFooter="True" CssClass="GRID" AllowSorting="True"
                                                AutoGenerateColumns="False" OnItemDataBound="dg_Grid2_ItemDataBound" Width="95%">
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="Header" HeaderText="Type">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Document No.">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_TransactionNoDr" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="Particulars" HeaderText="Particulars">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="Dr Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_AmountDr" runat="server" ToolTip='<%# DataBinder.Eval(Container.DataItem, "Narration") %>'
                                                                CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 15px;" align="right">
                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbl_TotalCredit" ForeColor="Navy" Text="0.00" Font-Bold="true" runat="server"
                                CssClass="LABEL"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%; height: 15px;" align="right">
                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Label ID="lbl_TotalDebit" ForeColor="Navy" Text="0.00" Font-Bold="true" runat="server"
                                CssClass="LABEL"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                    z-index: 100">
                    <span id="ajaxloading">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Wait! Action in Progress...</td>
                            </tr>
                        </table>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
