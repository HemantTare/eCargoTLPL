<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDirectDlyWithoutMR.aspx.cs"
    Inherits="Reports_Operation_FrmDirectDlyWithoutMR" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
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
  
                    
function Open_Details_Window(Path,lbtn_GC_No_For_Print,GC_Id,LHPO_Id,Memo_Id,Payment_Type_Id
,From_Service_Location_ID,To_Service_Location_ID 
,FromServiceLocation,ToServiceLocation,Vehicle_ID,Vehicle_No,GC_Date
,Memo_Date,Memo_No_For_Print,Memo_Branch_Id,MemoFromBranch_Name,MemoTo_Name
,LHPO_NO_For_Print,Total_Articles,Total_GC_Amount,Payment_Type)
{  
    var Path = Path + "&GC_No_For_Print=" + lbtn_GC_No_For_Print.innerText + "&GC_Id=" + GC_Id.value  
    + "&LHPO_Id=" + LHPO_Id.value + "&Memo_Id=" + Memo_Id.value + "&Payment_Type_Id=" + Payment_Type_Id.value 
    + "&From_Service_Location_ID=" + From_Service_Location_ID.value + "&To_Service_Location_ID=" + To_Service_Location_ID.value  
    + "&FromServiceLocation=" + FromServiceLocation.value  + "&ToServiceLocation=" + ToServiceLocation.value  
    + "&Vehicle_ID=" + Vehicle_ID.value  + "&Vehicle_No=" + Vehicle_No.value  + "&GC_Date=" + GC_Date.value  
    + "&Memo_Date=" + Memo_Date.value + "&Memo_No_For_Print=" + Memo_No_For_Print.value 
    + "&Memo_Branch_Id=" + Memo_Branch_Id.value + "&MemoFromBranch_Name=" + MemoFromBranch_Name.value 
    + "&MemoTo_Name=" + MemoTo_Name.value + "&LHPO_NO_For_Print=" + LHPO_NO_For_Print.value  
    + "&Total_Articles=" + Total_Articles.value + "&Total_GC_Amount=" + Total_GC_Amount.value 
    + "&Payment_Type=" + Payment_Type.value;
     
    window.open(Path,'DDWOMR','width=800,height=550,top=50,left=150,menubar=no,resizable=no,scrollbars=no')
    return false;
}


function viewwindow_general(Path)
{
 
//        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
//        var Path='../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var Path=Path;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Direct Delivery Without MR</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_Branch_TempoExp" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Direct Delivery Without MR"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="Upd_Pnl_Branch_TempoExp" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 510px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" CssClass="GRID" AllowSorting="True"
                                    AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                                    Width="90%" HorizontalAlign="Center">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Consignee" HeaderText="Consignee">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="GC_Date" HeaderText="LR Date">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="LR No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdn_Memo_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Memo_Id") %>' />
                                                <asp:HiddenField ID="hdn_LHPO_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.LHPO_Id") %>' />
                                                <asp:HiddenField ID="hdn_GC_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.GC_Id") %>' />
                                                <asp:HiddenField ID="hdn_Payment_Type_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Payment_Type_Id") %>' />
                                                <asp:HiddenField ID="hdn_From_Service_Location_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.From_Service_Location_ID") %>' />
                                                <asp:HiddenField ID="hdn_To_Service_Location_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.To_Service_Location_ID") %>' />
                                                <asp:HiddenField ID="hdn_Vehicle_No" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>' />
                                                <asp:HiddenField ID="hdn_Vehicle_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Vehicle_ID") %>' />
                                                <asp:HiddenField ID="hdn_GC_Date" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.GC_Date") %>' />
                                                <asp:HiddenField ID="hdn_Memo_Date" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Memo_Date") %>' />
                                                <asp:HiddenField ID="hdn_Memo_No_For_Print" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Memo_No_For_Print") %>' />
                                                <asp:HiddenField ID="hdn_FromServiceLocation" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.FromServiceLocation") %>' />
                                                <asp:HiddenField ID="hdn_ToServiceLocation" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ToServiceLocation") %>' />
                                                <asp:HiddenField ID="hdn_Memo_Branch_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Memo_Branch_Id") %>' />
                                                <asp:HiddenField ID="hdn_MemoFromBranch_Name" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.MemoFromBranch_Name") %>' />
                                                <asp:HiddenField ID="hdn_MemoTo_Name" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.MemoTo_Name") %>' />
                                                <asp:HiddenField ID="hdn_LHPO_NO_For_Print" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.LHPO_NO_For_Print") %>' />
                                                <asp:HiddenField ID="hdn_Total_Articles" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Total_Articles") %>' />
                                                <asp:HiddenField ID="hdn_Total_GC_Amount" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Total_GC_Amount") %>' />
                                                <asp:LinkButton ID="lbtn_GC_No_For_Print" Text='<%# DataBinder.Eval(Container, "DataItem.GC_No_For_Print") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="GCNoForPrint"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.GC_Id") %>' />
                                                <asp:HiddenField ID="hdn_Payment_Type" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Payment_Type") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Memo_No_For_Print" HeaderText="Inv No">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="FromServiceLocation" HeaderText="From">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="ToServiceLocation" HeaderText="To">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Payment_Type" HeaderText="Pay Type">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="Total_Articles" HeaderText="Qty">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>

                                        <asp:BoundColumn DataField="Total_GC_Amount" HeaderText="Frt">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>


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
