<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGCDlyTypeUpdate.aspx.cs"
    Inherits="Operations_Inward_Updates_FrmGCDlyTypeUpdate" %>

<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript">

 function viewwindow_general(GC_ID,Consignee_Name,GC_No)
    {
        var Path='../../Operations/Inward Updates/FrmGCConsigneeUpdate.aspx?GC_ID=' + GC_ID + '&Consignee_Name=' + Consignee_Name + '&GC_No=' + GC_No  ;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 400;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpGC6767', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes') ;
        return false;
    }
    
   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Type Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scm_GCDlyTypeUpdate" runat="server">
            </asp:ScriptManager>
            <table class="TABLE">
                <tr>
                    <td class="TDGRADIENT" colspan="3">
                        <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Type Update"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 11%">
                    </td>
                    <td colspan="2" style="width: 89%">
                        <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                    </td>
                </tr>
                <tr style="display: none">
                    <td class="TD1" colspan="6">
                        <asp:Button ID="btn_update_grid" CssClass="BUTTON" runat="server" Text="UpdateGrid"
                            OnClick="btn_update_grid_Click" />
                    </td>
                </tr>
                <tr id="tr_DlyBranchGrid" runat="server">
                    <td colspan="3">
                        <asp:UpdatePanel ID="up_GCDlyTypeUpdate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataGrid ID="dg_GCDlyTypeUpdate" runat="server" AutoGenerateColumns="False"
                                    CssClass="GRID" OnItemDataBound="dg_GCDlyTypeUpdate_ItemDataBound">
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <ItemStyle CssClass="GRIDITEMCSS" />
                                    <Columns>
                                        <asp:BoundColumn DataField="gc_caption no" HeaderText="gc_caption No"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="booking_branch" HeaderText="Booking Branch"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Delivery_Type" HeaderText="Current Delivery Type"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="New Delivery Type">
                                            <ItemTemplate>
                                                <asp:DropDownList runat="server" ID="ddl_DeliveryType" CssClass="DROPDOWN" Width="100px">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="TEXTBOX"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="GC_ID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_GC_ID" runat="SERVER" Text='<%# DataBinder.Eval(Container.DataItem, "GC_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" OnClick="btn_Save_Click"
                            Text="Save" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                        <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
    self.parent.hideload()
    </script>

</body>
</html>

<script type="text/javascript" language="javascript">
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
  
function update_consigneegrid()
{
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
document.getElementById('<%=btn_update_grid.ClientID%>').style.visibility = "hidden";
document.getElementById('<%=btn_update_grid.ClientID%>').click();
}
</script>

