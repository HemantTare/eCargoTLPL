<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_BranchWise_Expenses.aspx.cs"
  Inherits="Reports_Finance_frm_BranchWise_Expenses" %>

<%@ Register Src="../../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
  TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
  TagPrefix="uc2" %>
<%@ Register Src="../../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
  TagPrefix="uc3" %>
<%@ Register Src="../../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
  TagPrefix="uc4" %>
<%@ Register Src="../../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
  TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function viewwindow_general(GC_ID)
{
    var Path='../../Reports/Finance/frm_GC_Costing_View.aspx?&GC_ID=' + GC_ID ;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = 950;
    var popH = 650;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
    window.open(Path, 'CustomPopUp_GC_Costing_View', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
    return false;
}

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

 // For Print Preview
function Open_Show_Window(Path)
{            
    queryString = Path;
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-190);
    var popH = (h-75);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
              
    window.open(queryString, 'FMainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes') ;
    return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Branch Wise Expenses</title>
  <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_Branch_Wise_Expenses" runat="server">
    </asp:ScriptManager>
    <table class="TABLE">
      <tr>
        <td colspan="6" class="TDGRADIENT">
          <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Branch Wise Expenses" />
        </td>
      </tr>
      <tr>
        <td colspan="6">
          &nbsp;</td>
      </tr>
      <tr>
        <td colspan="6">
          <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" HoVisibility="true" />
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <uc5:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
        </td>
      </tr>
    </table>
    <table class="TABLE">
      <tr>
        <td style="text-align: center">
          <asp:Button ID="Btn_Preview" runat="server" CssClass="BUTTON" Text="Print Preview"
            Width="181px" OnClick="Btn_Preview_Click" />&nbsp;</td>
      </tr>
    </table>
  </form>

  <script type="text/jscript"> self.parent.hideload();</script>

</body>
</html>
