<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Rpt_Trip_Settlement_Nandwana.aspx.cs" Inherits="Reports_CL_Nandwana_Operation_frm_Rpt_Trip_Settlement_Nandwana" %>
<%@ Register Src="../../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc2" %>
<%@ Register Src="../../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="../../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../../Javascript/Common.js"></script>

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
<head id="Head1" runat="server">
  <title>Trip Settlement Register</title>
  <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_Truck_Unloading" runat="server">
    </asp:ScriptManager>
    <table  runat="server" id="tbl_input_screen" class="TABLE">
      <tr>
        <td colspan="6" class="TDGRADIENT">
          <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Trip Settlement Register" />
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <uc3:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <uc5:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" Set_FromDate_Caption="Trip Sheet From : "
            Set_ToDate_Caption="Trip Sheet To : " Set_TD_Caption_Width="20%" />
        </td>
      </tr>
      <tr>
        <td class="TD1" style="width: 20%">
          Vehicle No :</td>
        <td style="width: 29%">
          <uc2:WucVehicleSearch ID="WucVehicleNo" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
          Driver :</td>
        <td style="width: 29%">
          <cc2:ddlsearch id="ddl_Driver" callbackafter="2" iscallback="True" runat="server"
            callbackfunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName" />
        </td>
        <td class="TDMANDATORY" style="width: 1%" ></td>
      </tr>
    </table>
    <table class="TABLE">
      <tr>
        <td style="width: 10%">
          <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" OnClick="btn_view_Click"
            Text="View" />
        </td>
        <td style="width: 10%">
          <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
            Text="Close Window" />
        </td>
        <td style="width: 11%">
          <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
        </td>
        <td style="width: 11%">
          <a href="javascript:input_screen_action('view');">View Input</a>
        </td>
        <td style="width: 11%">
          <a href="javascript:input_screen_action('hide');">Hide Input</a>
        </td>
        <td style="width: 58%">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
    <div class="DIV" style="height: 510px; width: 986px;">
      <table class="TABLE">
        <tr>
          <td>
            <asp:UpdatePanel ID="Upd_Pnl_GC_Costing" runat="server" UpdateMode="Conditional">
              <ContentTemplate>
                <asp:Panel ID="pnl_GC_Costing" runat="server">
                  <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="true" ShowFooter="true" AllowSorting="True"
                    AutoGenerateColumns="true" CssClass="GRID" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                    PagerStyle-HorizontalAlign="Left" PageSize="9" OnItemDataBound="dg_Grid_ItemDataBound">
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                    <FooterStyle CssClass="GRIDFOOTERCSS" Font-Bold="true" />
                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                  </asp:DataGrid>
                </asp:Panel>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
    </div>
  </form>
</body>
</html>
