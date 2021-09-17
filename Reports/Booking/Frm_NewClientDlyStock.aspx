<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_NewClientDlyStock.aspx.cs"
    Inherits="Reports_Booking_Frm_NewClientDlyStock" %>

<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions"
    TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc6" %>
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

//function Open_NewClientDlyStock_Window(Path)
//{ 
//  
//  window.open(Path,'NewClientDlyStock','width=900,height=800,top=0,left=0,menubar=no,resizable=yes,scrollbars=yes')
//  return false;
//}


function viewwindow_general(BranchID,BranchText,AreaText,RegionText,calledfrom,colid,criteria_id,Filtered_Text,Filtered_Date,Filtered_Bit)
{ 

        var Path='../../Reports/Booking/Frm_NewClientDlyStockViewer.aspx?BranchID=' + BranchID + '&BranchText=' + BranchText + '&AreaText=' + AreaText + '&RegionText=' + RegionText + '&calledfrom=' + calledfrom + '&colid=' + colid + '&criteria_id=' + criteria_id + '&Filtered_Text=' + Filtered_Text + '&Filtered_Date=' + Filtered_Date + '&Filtered_Bit=' + Filtered_Bit;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1000;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
         
        window.open(Path, 'NewClientDlyStock', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=yes,scrollbars=yes')
        return false;
}
 

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Stock List View</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Stock List"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <uc2:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <uc6:WucFilter ID="WucFilter1" runat="Server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" OnClick="btn_view_Click"
                        Text="View" />
                </td>
                <td colspan="3">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                <td style="width: 50%">
                    <%--&nbsp;<asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />--%></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </form>
     <script type="text/javascript">
          self.parent.hideload();
    </script>
</body>
</html>
