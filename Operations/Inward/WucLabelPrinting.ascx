<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLabelPrinting.ascx.cs"
    Inherits="Operations_Outward_WucLabelPrinting" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Inward/LabelPrinting.js"></script>

<script type="text/javascript" language="javascript">

function Open_Details_Window(Path)
{  
  alert(Path); 
  window.open(Path,'LabelPrint','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
  return false;
}
</script>

<asp:ScriptManager ID="scm_LabelPrinting" runat="server">
</asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="LABEL PRINTING"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" id="td_gccontrol" runat="server">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <uc4:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="Div_LabelPrinting" class="DIV" style="height: 250px">
                        <asp:DataGrid ID="dg_LabelPrinting" runat="server" AutoGenerateColumns="False" DataKeyField="GC_Id"
                            CellPadding="2" CssClass="GRID" Style="border-top-style: none" Width="98%" OnItemDataBound="dg_LabelPrinting_ItemDataBound">
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                            <Columns>
                                <%-- <asp:TemplateColumn HeaderText="Attach">
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucLabelPrinting1_dg_LabelPrinting');" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                            OnClick="Check_Single(this,'WucLabelPrinting1_dg_LabelPrinting','1');" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:BoundColumn DataField="Item_No" HeaderText="GC No"></asp:BoundColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date" DataFormatString="{0:dd-MM-yyyy}">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Booking Branch"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Delivery_Location_Name" HeaderText="Delivery Location"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Cnee Name" HeaderText="Cnee Name"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_Booking_Branch_Id" Value='<%# DataBinder.Eval(Container.DataItem, "Booking_Branch_ID") %>'
                                            runat="server" />
                                        <asp:HiddenField ID="hdn_Delivery_Branch_ID" Value='<%# DataBinder.Eval(Container.DataItem, "Delivery_Branch_ID") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Total GC  :" CssClass="LABEL" Font-Bold="True" />&nbsp;
                    <asp:Label ID="lbl_Total_GC" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                    <asp:HiddenField ID="hdn_Total_GC" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" colspan="6" style="text-align: left">
            &nbsp;
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label><br />
            &nbsp;
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR"></asp:Label>&nbsp;
            <asp:HiddenField ID="hdn_GCCaption" runat="server" />
            <asp:HiddenField ID="hdn_LoginBranch_Id" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" Text="Print" AccessKey="p"
                OnClick="btn_Save_Print_Click" />
             
            <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClick="btn_Close_Click" />&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
        </td>
    </tr>
</table>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
