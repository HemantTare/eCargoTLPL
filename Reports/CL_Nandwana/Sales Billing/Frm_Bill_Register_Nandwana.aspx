<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Bill_Register_Nandwana.aspx.cs" Inherits="Reports_Sales_Billing_Frm_Bill_Register_Nandwana" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
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
    var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = 950;
    var popH = 650;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
    window.open(Path, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
    return false;
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="scm_Bill_Register" runat="server"/>
    <table runat="server" id="Table1" class="TABLE">
        <tr>
            <td class="TDGRADIENT" style="width: 100%">
                <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Bill  Register"></asp:Label>
            </td>
        </tr>
    </table>
      
    <table runat="server" id="tbl_input_screen" class="TABLE">
        <tr>
            <td style="width:100%; height: 23px;">
                <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width:100%">
                <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1" id="Division" runat="server"></td>
                        <td style="width: 24%;"></td>
                        <td align="center" colspan="2">
                            <asp:RadioButtonList ID="rbtn_Details_Summary" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">Details</asp:ListItem>
                                <asp:ListItem Value="1">Summary</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <table class="TABLE" >
        <tr>
            <td style="width:10%;">
                <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click"  />
            </td>
            <td style="width:10%;">
                <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
            </td>
            <td style="width:11%;">
                <a href="javascript:input_screen_action('view');">View Input</a>
            </td>
            <td style="width:11%;">
                <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>
            <td style="width:58%;">
                <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR" />
            </td>
        </tr>
    </table>
    
    <table class="TABLE" >
        <tr>
            <td style="width:90%">
                <div class="DIV" style="height: 510px;">
                    <table class="TABLE" width="90%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="Upd_Pnl_Bill_Register_Details" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid_Details" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DataGrid ID="dg_Grid_Details"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                                            AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_Details_PageIndexChanged"
                                            OnItemDataBound="dg_Grid_Details_ItemDataBound" PageSize="15" AllowCustomPaging="True" Width="95%">
                                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                                            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Bill No">
                                                    <ItemTemplate>
                                                        <asp:Label id = "lbl_Bill_No" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Bill No") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Total" runat="server" Text="Total"/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                    <FooterStyle HorizontalAlign = "Left" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="Bill Date" HeaderText="Bill Date">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn> 
                                                <asp:BoundColumn DataField="Bill Type" HeaderText="Bill Type">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="LR No">
                                                    <ItemTemplate>
                                                        <asp:Label id = "lbl_LR_No" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LR No") %>'/>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign = "Left" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="LR Date" HeaderText="LR Date">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Consignor Name" HeaderText="Consignor Name">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Consignee Name" HeaderText="Consignee Name">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="From Branch" HeaderText="From Branch">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn> 
                                                <asp:BoundColumn DataField="To Branch" HeaderText="To Branch">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Total LR">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Total LR") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Total_LR" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total LR") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Sub Total">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Sub Total") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Sub_Total" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Sub Total") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Other Charge">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Other Charge") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Other_Charge" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Other Charge") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="GST">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Service Tax") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Service_Tax" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Service Tax") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Octroi Amount">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Octroi Amount") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Octroi_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Octroi Amount") %>'/>                              
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Total Bill Amount">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Total Bill Amount") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Total_Bill_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total Bill Amount") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "True"/>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td  style="width:90%">
                                <asp:UpdatePanel ID="Upd_Pnl_Bill_Register_Summary" UpdateMode="Conditional" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dg_Grid_Summary" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DataGrid ID="dg_Grid_Summary"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                                            AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_Summary_PageIndexChanged"
                                            OnItemDataBound="dg_Grid_Summary_ItemDataBound" PageSize="15" AllowCustomPaging="True" Width="95%">
                                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                                            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Bill No">
                                                    <ItemTemplate>
                                                        <asp:Label id = "lbl_Bill_No" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Bill No") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Total" runat="server" Text="Total"/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                    <FooterStyle HorizontalAlign = "Left" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="Bill Date" HeaderText="Bill Date">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn> 
                                                <asp:BoundColumn DataField="Client Name" HeaderText="Client Name">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Total LR">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Total LR") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Total_LR" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total LR") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="Bill Type" HeaderText="Bill Type">
                                                    <ItemStyle HorizontalAlign = "Left" />
                                                    <FooterStyle HorizontalAlign = "Left" Font-Bold = "true"/>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Sub Total">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Sub Total") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Sub_Total" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Sub Total") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Other Charge">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Other Charge") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Other_Charge" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Other Charge") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="GST">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Service Tax") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Service_Tax" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Service Tax") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Octroi Amount">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Octroi Amount") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Octroi_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Octroi Amount") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Total Bill Amount">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container, "DataItem.Total Bill Amount") %>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label id = "lbl_Total_Bill_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total Bill Amount") %>'/>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign = "Right" />
                                                    <FooterStyle HorizontalAlign = "Right" Font-Bold = "true"/>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</form>
</body>
</html>
