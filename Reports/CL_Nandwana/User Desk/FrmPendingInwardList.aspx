<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPendingInwardList.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmPendingInwardList" %>

<%@ Register Src="../../../CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
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



function viewwindow_AUS(VehicleId,LHPOId)
{
        var Path='../../../Operations/Inward/FrmAUS.aspx?Menu_Item_Id=NwAyAA==&Mode=MQA=&VehicleId=' + VehicleId + '&LHPOId=' + LHPOId;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpAUS', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

   function OpenF4Menu(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Add_', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

function viewwindow_Memo(Memo_Id)
{
        var Path='../../../Operations/Outward/FrmMenifest.aspx?Menu_Item_Id=NQAxAA==&Mode=NAA=&Id=' + Memo_Id;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpMemo', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function printwindow_Memo(Memo_Id)
{
        var Path='../../../Reports/CL_Nandwana/User Desk/FrmPendingInwardMenifestPrintingViewer.aspx?Memo_Id=' + Memo_Id;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpMemoPrint', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Inward</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_PendingPDS" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Inward"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%;">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 11%;">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" has_last_row_as_total="false"
                        runat="server" />
                </td>
                <td style="width: 11%;">
                </td>
                <td style="width: 29%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />
                </td>
                <td style="width: 29%">
                    <asp:LinkButton ID="lnk_btnPrintPivot" runat="server" Font-Names="Verdana" Font-Size="15px"
                        Font-Underline="True" Font-Bold="True" ForeColor="Blue" Text="Dly Branch Wise Load" />
                </td>
            </tr>
            
            
             <tr>
                <td style="width: 10%; height: 15px;">
                    &nbsp;</td>
                <td style="width: 10%; height: 15px;">
                    &nbsp;</td>
                <td style="width: 11%; height: 15px;">
                </td>
                <td style="width: 11%; height: 15px;">
                </td>
                <td style="width: 29%; height: 15px;">
                    <asp:Label ID="lbl_Branch" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Maroon"></asp:Label></td>
                <td style="width: 29%; height: 15px;">
                    
                </td>
            </tr>
            
        </table>
        <table class="TABLE">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="Upd_Pnl_PendingPDS" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 510px; width: 90%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="false" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_Grid_PageIndexChanged" PagerStyle-HorizontalAlign="Left"
                                    OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <%--<asp:BoundColumn DataField="ToBranch" HeaderText="Invoice To"></asp:BoundColumn>--%>
                                        
                                        <asp:TemplateColumn HeaderText="Vehicle" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_VehicleNo" Text='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:BoundColumn DataField="MemoFrom" HeaderText="From" HeaderStyle-Width="4%"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MemoNo" HeaderText="InvoiceNo" HeaderStyle-Width="6%"></asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="MemoDate" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="5%"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Driver_Name" HeaderText="Driver" HeaderStyle-Width="10%"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Driver_Mobile_1" HeaderText="Mobile 1" HeaderStyle-Width="6%"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Driver_Mobile_2" HeaderText="Mobile 2" HeaderStyle-Width="6%"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Cleaner_Name" HeaderText="Cleaner" HeaderStyle-Width="10%"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Cleaner_Mobile_1" HeaderText="Mobile 1" HeaderStyle-Width="6%"></asp:BoundColumn>
                                        
                                        <asp:TemplateColumn HeaderText="View" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_View" Text='View'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Print" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Print" Text='Print'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_DlyAreaWise" Text='DlyAreaWise'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
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
