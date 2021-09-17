<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_Missing_document.aspx.cs" Inherits="Reports_CL_Nandwana_DOC_Monitoring_Frm_Rpt_Missing_document" %>

<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type ="text/javascript">
    function input_screen_action(action)
    {
        if(action == "view")
        {
            tbl_input_screen.style.display = 'inline';
        }
        else
        {
            tbl_input_screen.style.display = 'none';
        }
    }

    function viewwindow(Allocation_Id,Document_Id,Branch_Name)
    {
        var Path='../../../Reports/CL_Nandwana/DOC_Monitoring/Frm_Rpt_Missing_Document_Details.aspx?Allocation_Id='+ Allocation_Id +'&Document_Id=' + Document_Id + '&Branch_Name='+ Branch_Name;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-440);
        var popH = (h-300);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(Path, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }

    function viewwindow_ddc(DelTypeID,DocNo)     //For Delivery Details
    {
        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=DDC&Doc_No=' + DocNo +'&DeliveryType_Id='+ DelTypeID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-25);
        var popH = (h-25);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(Path, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Missing Document</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager id="scm_Cancellation_Register" runat="Server"></asp:ScriptManager>
        <table class="TABLE" style="width: 100%">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Missing Document"></asp:Label></td>
            </tr>
        </table>
        <table id="tbl_input_screen" class="TABLE" style="width: 100%">
            <tr>
                <td class="TD1" style="width: 10%"></td>
                <td style="width: 24%"></td>
                <td class="TD1" style="width: 9%"></td>
                <td style="width: 24%"></td>
                <td style="width: 9%"></td>
                <td style="width: 24%"></td>
            </tr>
            <tr>
                <td colspan="6"> 
                    <uc4:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; " class="TD1">
                        <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/>
                </td>
                <td style="width: 24%">
                    <uc2:WucDivisions ID="WucDivisions1" runat="server" />
                </td>
                <td style="width: 9%" class="TD1">
                    Module:
                </td>
                <td style="width: 24%" >
                    <asp:DropDownList ID="ddl_Module" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_Module_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0" Text="Select One"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Operation"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Finance"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td class="TD1" style="width: 9%">
                    Document :
                </td>
                <td style="width: 24%">
                    <asp:UpdatePanel ID="Upd_Pnl_MenuItem" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_Menu_Item" runat="server" CssClass="DROPDOWN"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_Module" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 9%"></td>
                <td style="width: 24%"></td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc1:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
        </table>
        <table class="TABLE" style="width: 100%">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_View_Click" />
                </td>
                <td style="width: 10%">
                    <uc3:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href = "javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href = "Javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label>
                  
                </td>
            </tr>
        </table>
        
        <table class="TABLE" style="width: 100%">
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="upd_pnl_Cancellation_Register" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_Cancellation_Register" runat="server" ScrollBars="Auto" Height="500px">
                                <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CssClass="GRID" OnPageIndexChanged="dg_Grid_PageIndexChanged" AllowCustomPaging="True" OnItemDataBound="dg_Grid_ItemDataBound" PageSize="27">
                                    <HeaderStyle CssClass = "GRIDHEADERCSS" />
                                    <AlternatingItemStyle CssClass = "GRIDALTERNATEROWCSS" />
                                    <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" />
                                    <Columns>                           
                                         <asp:BoundColumn DataField="Branch Name" HeaderText="Branch Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Document Name" HeaderText="Document Type"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Date of Allocation" HeaderText="Date of Allocation"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Start No" HeaderText="Start No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="End No" HeaderText="End No"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Balance">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Balance" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Balance") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               </td>
            </tr>
        </table>
    </form>
</body>
</html>