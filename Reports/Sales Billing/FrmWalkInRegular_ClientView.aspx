<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmWalkInRegular_ClientView.aspx.cs"
    Inherits="Reports_Sales_Billing_FrmWalkInRegular_ClientView" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
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

function Open_Details_Window(Path,From_Date,To_Date,FilterTypeId,SearchFor,IsCreatedBy)
{ 
  window.open(Path + "&From_Date=" + From_Date + "&To_Date=" + To_Date + "&FilterTypeId=" + FilterTypeId + "&SearchFor=" + SearchFor + "&IsCreatedBy=" + IsCreatedBy,'Labels','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
  return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Client Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="New Client Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            
            
            <tr>
                <td style="width: 50%">
                    &nbsp;
                </td>
            </tr>
            
            <tr>
                <td style="width: 50%">
                    <asp:RadioButtonList ID="rdl_IsCreatedBy" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="1">Is Created Date Wise</asp:ListItem>
                        <asp:ListItem Value="0">Is Updated Date Wise</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            
            <tr>
                <td style="width: 50%">
                    &nbsp;</td>
            </tr>
           
            <tr>
                <td style="width: 50%">
                <asp:DropDownList ID="ddl_FilterType" runat="server" CssClass="DROPDOWN" Width="20%">
                        <asp:ListItem Value="1">City</asp:ListItem>
                        <asp:ListItem Value="2">Delivery Area</asp:ListItem>
                        <asp:ListItem Value="3">Client Name</asp:ListItem>
                         <asp:ListItem Value="4">Updated By</asp:ListItem>
                    </asp:DropDownList>&nbsp; &nbsp;Contains &nbsp;
                    <asp:TextBox ID="txt_SearchFor" runat="server" CssClass="TEXTBOX" Width="30%" MaxLength="25"></asp:TextBox>
                    
                    </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    &nbsp;</td>
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
                        Text="Close Window" />
                    <asp:Button ID="btn_Labels" runat="server" CssClass="BUTTON" OnClick="btn_Labels_Click"
                        Text="Print Labels" /></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 475px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="true" AllowCustomPaging="true"
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false" OnItemDataBound="dg_Grid_ItemDataBound"
                                    PagerStyle-HorizontalAlign="Left" PageSize="11" OnPageIndexChanged="dg_Grid_PageIndexChanged">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" HorizontalAlign="Center" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ClientType" HeaderText="Client Type">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Client_Name" HeaderText="Client Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DeliveryAreaName" HeaderText="Delivery<br/>Area">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Address1" HeaderText="Address1">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Address2" HeaderText="Address2">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="City_Name" HeaderText="City Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone1" HeaderText="Phone1">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone2" HeaderText="Phone2">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Fax" HeaderText="Fax">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Email_ID" HeaderText="Email ID">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="CreatedOn" HeaderText="Created On">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Created_By" HeaderText="Created By">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        
                                        <asp:BoundColumn DataField="UpdatedOn" HeaderText="Updated On">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Updated_By" HeaderText="Updated By">
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
