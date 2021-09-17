<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Branch_Daily_Collection_Nandwana.aspx.cs" Inherits="Reports_CL_Nandwana_frm_Branch_Daily_Collection_Nandwana" %>
<%@ register src="~/CommonControls/WucHierarchyFiltration.ascx" tagname="WucHierarchyFiltration" tagprefix="uc6" %>
<%@ register src="~/CommonControls/WucHierarchyWithID.ascx" tagname="WucHierarchyWithID" tagprefix="uc7" %>
<%@ register src="~/CommonControls/WucDivisions.ascx" tagname="WucDivisions" tagprefix="uc5" %>
<%@ register src="~/CommonControls/Wuc_Export_To_Excel.ascx" tagname="Wuc_Export_To_Excel" tagprefix="uc4" %>
<%@ register src="~/CommonControls/Wuc_GC_Parameters.ascx" tagname="Wuc_GC_Parameters" tagprefix="uc3" %>
<%@ register src="~/CommonControls/Wuc_Region_Area_Branch.ascx" tagname="Wuc_Region_Area_Branch" tagprefix="uc1" %>
<%@ register src="~/CommonControls/Wuc_From_To_Datepicker.ascx" tagname="Wuc_From_To_Datepicker" tagprefix="uc2" %>
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

function viewwindow_general(DocType,DocNo)
{
    var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = 950;
    var popH = 650;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
    window.open(Path, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
    return false;
}

function viewwindow_Voucher(Voucher_ID)
{
    var Path='../../Finance/VoucherView/FrmVoucher.aspx?Id='+ Voucher_ID;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = 950;
    var popH = 350;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
    window.open(Path, 'CustomPopUp_Voucher', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
    return false;
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="scm_Vehicle_Monitor" runat="server"/>

    <table class="TABLE">
        <tr>
            <td class="TDGRADIENT" colspan="6">
                <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch Daily Collection Report"/></td>
        </tr>
        <tr>
            <td class="TD1" style="width: 10%">&nbsp;</td>
            <td style="width: 24%"></td>
            <td class="TD1" style="width: 10%"></td>
            <td style="width: 23%"></td>
            <td class="TD1" style="width: 10%"></td>
            <td style="width: 24%"></td>
        </tr>
        <tr>
            <td class="TD1" style="width: 10%">
                <asp:Label ID="lbl_Branch" runat="server" Text="Branch :"/>
            </td>
            <td style="width: 24%">
                <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="DROPDOWN"/>
            </td>
            <td class="TD1" style="width: 10%"></td>
            <td style="width: 23%"></td>
            <td class="TD1" style="width: 10%"></td>
            <td style="width: 24%"></td>
        </tr>
        <tr>
            <td colspan="6">
                <uc2:wuc_from_to_datepicker id="Wuc_From_To_Datepicker1" runat="server"/>
            </td>
        </tr>
    </table>

    <table class="TABLE">
        <tr>
            <td style="width: 10%">
                <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" />
            </td>
            <td style="width: 10%">
                <asp:Button ID="btn_Export_To_Export" runat="server" CssClass="BUTTON" Text="Export To Excel" OnClick="btn_Export_To_Export_Click" />
            </td>
            <td style="width: 11%">
                <a href="javascript:input_screen_action('view');">View Input</a>
            </td>
            <td style="width: 11%">
                <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>
            <td style="width: 58%">
                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""/>
            </td>
        </tr>
    </table>
    
    <table class="TABLE">
        <tr>
            <td>
                <asp:UpdatePanel ID="Upd_Pnl_Booking" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl_Booking" Font-Size = "Small" GroupingText = "Booking" runat="server" Height="330px" ScrollBars="Auto">
                        <asp:DataGrid ID="dg_Grid_Booking" runat="server" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" ShowFooter="True"
                            OnItemDataBound="dg_Grid_Booking_ItemDataBound" OnPageIndexChanged="dg_Grid_Booking_PageIndexChanged" 
                            CellPadding="4" ForeColor="#003333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                            <Columns>
                                <asp:BoundColumn DataField="Sr No." HeaderText="Sr. No.">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%"/>
                                    <ItemStyle HorizontalAlign = "Center" Width = "10%"/>
                                    <FooterStyle HorizontalAlign = "Center" Width = "10%"/>  
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="MR Date">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <FooterStyle HorizontalAlign = "Center" Width = "15%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.MR Date") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total"/>
                                        </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="gc_caption No">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <FooterStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemTemplate>
                                        <asp:Label ID = "lbl_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container, "DataItem.gc_caption No") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MR No">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <FooterStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemTemplate>
                                        <asp:Label ID = "lbl_MR_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container, "DataItem.MR No") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cash Amount">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemStyle HorizontalAlign = "Right" Width = "15%"/>
                                    <FooterStyle HorizontalAlign = "Right" Width = "15%"/>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Cash Amount") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID = "lbl_Cash_Amount" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Cheque No" HeaderText="Cheque No">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <FooterStyle HorizontalAlign = "Center" Width = "15%"/>
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Cheque Amount">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                    <ItemStyle HorizontalAlign = "Right" Width = "15%"/>
                                    <FooterStyle HorizontalAlign = "Right" Width = "15%"/>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Cheque Amount") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID = "lbl_Cheque_Amount" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#E3EAEB" ForeColor="Blue" HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#E3EAEB" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>
                        
                        <table class="TABLE">
                            <tr>
                                <td align = "center">
                                    <asp:Label ID="lbl_Total_Booking" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" Text="TOTAL BOOKING AMOUNT (Cash + Cheque) : "/>
                                    <asp:Label ID="lbl_Total_Booking_Amount" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Grid_Booking" />
                </Triggers>
                </asp:UpdatePanel>

                
            </td>
        </tr>
    </table>
    
    <table class="TABLE">
        <tr>
            <td>
                <asp:UpdatePanel ID="Upd_Pnl_Delivery" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl_Delivery" Font-Size = "Small" GroupingText = "Delivery" runat="server" Height="330px" ScrollBars="Auto">
                        <asp:DataGrid ID="dg_Grid_Delivery" runat="server" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" ShowFooter="True" 
                            OnItemDataBound="dg_Grid_Delivery_ItemDataBound" OnPageIndexChanged="dg_Grid_Delivery_PageIndexChanged" 
                            CellPadding="4" ForeColor="#333333">
                                <Columns>
                                 <asp:BoundColumn DataField="Sr No." HeaderText="Sr. No.">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%"/>
                                    <ItemStyle HorizontalAlign = "Center" Width = "10%"/>
                                    <FooterStyle HorizontalAlign = "Center" Width = "10%"/>  
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="MR Date" >
                                    <HeaderStyle HorizontalAlign = "Center" Width = "12%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "12%" />
                                    <FooterStyle HorizontalAlign = "Center" Width = "12%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.MR Date") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID = "lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text = "Total"/>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="gc_caption No">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "10%" />
                                    <FooterStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemTemplate>
                                        <asp:Label ID = "lbl_GC_No" runat="server" CssClass="LABEL" Text = '<%# DataBinder.Eval(Container, "DataItem.gc_caption No") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="From Branch" HeaderText="From Branch">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "15%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "15%" />
                                    <FooterStyle HorizontalAlign = "Center" Width = "15%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Payment Type" HeaderText="Payment Type">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "13%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "13%" />
                                    <FooterStyle HorizontalAlign = "Center" Width = "13%" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Ref No">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "10%" />
                                    <FooterStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemTemplate>
                                        <asp:Label ID = "lbl_Ref_No" runat="server" CssClass="LABEL" Text = '<%# DataBinder.Eval(Container, "DataItem.Ref No") %>'/>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cash Amount">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemStyle HorizontalAlign = "Right" Width = "10%" />
                                    <FooterStyle HorizontalAlign = "Right" Width = "10%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Cash Amount") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID = "lbl_Cash_Amount" runat="server"  CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Cheque No" HeaderText="Cheque No">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "10%" />
                                    <FooterStyle HorizontalAlign = "Center" Width = "10%" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Cheque Amount">
                                    <HeaderStyle HorizontalAlign = "Center" Width = "10%" />
                                    <ItemStyle HorizontalAlign = "Right" Width = "10%" />
                                    <FooterStyle HorizontalAlign = "Right" Width = "10%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Cheque Amount") %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID = "lbl_Cheque_Amount" runat="server"  CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#003399" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            <PagerStyle BackColor="#EFF3FB" ForeColor="Blue" HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>
                        
                        <table class="TABLE">
                            <tr>
                                <td align = "center">
                                    <asp:Label ID="lbl_Total_Delivery" runat="server" Font-Bold = "true" ForeColor = "#003399" CssClass="LABEL" Text="TOTAL DELIVERY AMOUNT (Cash + Cheque) : "/>
                                    <asp:Label ID="lbl_Total_Delivery_Amount" runat="server" Font-Bold = "true" ForeColor = "#003399" CssClass="LABEL"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Grid_Delivery" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <table class="TABLE">
        <tr>
            <td>
                <asp:UpdatePanel ID="Upd_Pnl_Credit_Memo" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_Credit_Memo" Font-Size = "Small" GroupingText = "Credit Memo" runat="server" Height="330px" ScrollBars="Auto">
                            <asp:DataGrid ID="dg_Grid_Credit_Memo" runat="server" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" ShowFooter="True"
                            OnItemDataBound="dg_Grid_Credit_Memo_ItemDataBound" OnPageIndexChanged="dg_Grid_Credit_Memo_PageIndexChanged" 
                            CellPadding="4" ForeColor="#003333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                <Columns>
                                    <asp:BoundColumn DataField="Sr No." HeaderText="Sr. No.">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "10%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "10%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "10%"/>
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Credit Memo Date">
                                        <HeaderStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <ItemStyle HorizontalAlign = "Left"  Width = "15%"/>
                                        <FooterStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.[Credit Memo Date]")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="gc_caption No">
                                        <HeaderStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <ItemStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <FooterStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <ItemTemplate>
                                            <asp:Label ID = "lbl_GC_No" runat="server" CssClass="LABEL" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.gc_caption No") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Credit Memo No">
                                        <HeaderStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <ItemStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <FooterStyle HorizontalAlign = "Left" Width = "15%"/>
                                        <ItemTemplate>
                                            <asp:Label ID = "lbl_Credit_Memo_No" runat="server" CssClass="LABEL" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.[Credit Memo No]") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Total Amount">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "15%"/>
                                        <ItemStyle HorizontalAlign = "Right" Width = "15%"/>
                                        <FooterStyle HorizontalAlign = "Right" Width = "15%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.[Total Amount]")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Total_Amount" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditItemStyle BackColor="#7C6F57" />
                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#E3EAEB" ForeColor="Blue" HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#E3EAEB" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            </asp:DataGrid>
                         <table class="TABLE">
                            <tr>
                                <td align = "center">
                                    <asp:Label ID="lbl_Total_Credit_Memo" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" Text="TOTAL CREDIT MEMO AMOUNT : "/>
                                    <asp:Label ID="Label2" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Grid_Credit_Memo" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

    <table class="TABLE">
        <tr>
            <td style="width: 50%">
                <asp:UpdatePanel ID="Upd_Pnl_Expenses" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_Expenses" Font-Size = "Small" GroupingText = "Expenses" runat="server" Height="330px" ScrollBars="Auto">
                            <asp:DataGrid ID="dg_Grid_Expenses" runat="server" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" ShowFooter="True"
                            OnItemDataBound="dg_Grid_Expenses_ItemDataBound" OnPageIndexChanged="dg_Grid_Expenses_PageIndexChanged" 
                            CellPadding="4" ForeColor="#003333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                                <Columns>
                                     <asp:BoundColumn DataField="Sr No." HeaderText="Sr. No.">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "10%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "10%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "10%"/>  
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Date">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Date") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Voucher No">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <ItemTemplate>
                                            <asp:Label ID = "lbl_Voucher_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container, "DataItem.Voucher No") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="Ledger Name" HeaderText="Ledger Name">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "25%"/>
                                    </asp:BoundColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Cash Amount">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <FooterStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.CashAmount")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Amount_Expenses_Cash" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                  <asp:TemplateColumn HeaderText="Chq Amount">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <FooterStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.ChqAmount")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Amount_Expenses_Chq" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditItemStyle BackColor="#7C6F57" />
                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#E3EAEB" ForeColor="Blue" HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#E3EAEB" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            </asp:DataGrid>
                            
                            <table class="TABLE">
                            <tr>
                                <td align = "center">
                                    <asp:Label ID="lbl_Total_Expenses" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" Text="TOTAL EXPENSE AMOUNT : "/>
                                    <asp:Label ID="lbl_Total_Expenses_Amount" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Grid_Expenses" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            
            <td style="width: 50%" >
                <asp:UpdatePanel ID="Upd_Pnl_Income" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_Income" Font-Size = "Small" GroupingText = "Other Income" runat="server" Height="330px" ScrollBars="Auto">
                            <asp:DataGrid ID="dg_Grid_Income" runat="server" AllowCustomPaging="True" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" ShowFooter="True"
                            OnItemDataBound="dg_Grid_Income_ItemDataBound" OnPageIndexChanged="dg_Grid_Income_PageIndexChanged"
                            CellPadding="4" ForeColor="#003333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" >
                                <Columns>
                                     <asp:BoundColumn DataField="Sr No." HeaderText="Sr. No.">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "10%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "10%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "10%"/>  
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Date">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.Date") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Voucher No">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <ItemTemplate>
                                            <asp:Label ID = "lbl_Voucher_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container, "DataItem.Voucher No") %>'/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    
                                    <asp:BoundColumn DataField="Ledger Name" HeaderText="Ledger Name">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <ItemStyle HorizontalAlign = "Center" Width = "25%"/>
                                        <FooterStyle HorizontalAlign = "Center" Width = "25%"/>
                                    </asp:BoundColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Cash Amount">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <FooterStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.CashAmount") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Amount_Income_Cash" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    
                                  <asp:TemplateColumn HeaderText="Chq Amount">
                                        <HeaderStyle HorizontalAlign = "Center" Width = "20%"/>
                                        <ItemStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <FooterStyle HorizontalAlign = "Right" Width = "20%"/>
                                        <ItemTemplate>
                                            <%# DataBinder.Eval(Container, "DataItem.ChqAmount")%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID = "lbl_Amount_Income_Chq" runat="server" CssClass="LABEL" Font-Bold="true"/>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                <EditItemStyle BackColor="#7C6F57" />
                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#E3EAEB" ForeColor="Blue" HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#E3EAEB" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            </asp:DataGrid>
                            
                            <table class="TABLE">
                            <tr>
                                <td align = "center">
                                    <asp:Label ID="lbl_Total_Other_Income" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" Text="TOTAL OTHER INCOME AMOUNT : "/>
                                    <asp:Label ID="lbl_Total_Other_Income_Amount" runat="server" Font-Bold = "true" ForeColor = "#1C5E55" CssClass="LABEL" />
                                </td>
                            </tr>
                        </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Grid_Income" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <table class="TABLE">
        <tr>
            <td>
                <asp:UpdatePanel ID="Upd_Pnl_Total" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnl_Total" Font-Size = "Small" GroupingText = "Finalise Report" runat="server" Height="95px" ScrollBars="Auto">
                        <asp:DataGrid ID="dg_Grid_Total" runat="server" AllowCustomPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" 
                            CellPadding="4" ForeColor="#333333" PageSize="1" AllowPaging="True">
                                <Columns>
                                <asp:TemplateColumn HeaderText="TOTAL INCOME (BOOKING + CREDIT MEMO + DELIVERY + OTHER INCOME)" >
                                    <HeaderStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Total_1") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TOTAL EXPENSE AMOUNT" >
                                    <HeaderStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Total_2")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TOTAL CASH AMOUNT SENT TO HO (CASH INCOME - TOTAL EXPENSE)" >
                                    <HeaderStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Total_3")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TOTAL CHEQUE AMOUNT SENT TO HO" >
                                    <HeaderStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Total_4")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CURRENT AVAILABLE IN BO" >
                                    <HeaderStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemStyle HorizontalAlign = "Center" Width = "20%" />
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container, "DataItem.Total_5")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <EditItemStyle BackColor="#2461BF" />
                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <ItemStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#003399" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            <PagerStyle BackColor="#EFF3FB" ForeColor="Blue" HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" Visible="False" />
                            <AlternatingItemStyle BackColor="White" />
                            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid>
                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Grid_Delivery" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

