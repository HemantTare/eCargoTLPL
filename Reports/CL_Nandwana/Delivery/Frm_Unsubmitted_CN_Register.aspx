<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Unsubmitted_CN_Register.aspx.cs" Inherits="Reports_Delivery_Frm_Unsubmitted_CN_Register" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx"TagName="WucDatePicker" TagPrefix="uc6" %>
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
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>UnBilled CN Register</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="scm_Unbilled_CN_Register" runat="server"></asp:ScriptManager>
    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="UnBilled CN Register"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
      <tr>
        <td style="width:100%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
         </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%;" class="TD1">
                        <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">Client Name:</td>
                        <td style="width: 24%;">
                            <asp:TextBox ID="Txt_Client_name" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%;" class="TD1">Booking Type:</td>
                        <td style="width: 24%;">
                        <asp:DropDownList ID="ddl_booking_type" runat="server" CssClass="DROPDOWN">
                            <asp:ListItem Value="0">All</asp:ListItem>
                            <asp:ListItem Value="1">Sundry</asp:ListItem>
                            <asp:ListItem Value="2">FTL</asp:ListItem>
                            <asp:ListItem Value="3">ODC</asp:ListItem>
                            <asp:ListItem Value="4">Super ODC</asp:ListItem>
                        </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%;" class="TD1">From Date:</td>
                        <td style="width: 24%;">
                            <uc6:WucDatePicker ID="WucDatePicker1" runat="server" OnLoad="WucDatePicker1_Load" />
                            </td>
                        <td style="width: 9%;" class="TD1">To Date:</td>
                        <td style="width: 24%;">
                            &nbsp;<uc6:WucDatePicker ID="WucDatePicker2" runat="server" />
                            </td>
                        <td class="TD1" style="width: 9%">As On Date:</td>
                        <td style="width: 24%">
                            <uc6:WucDatePicker ID="WucDatePicker3" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 10%">
                        </td>
                        <td style="width: 24%">
                            <asp:RadioButtonList ID="Rbtn_Type" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="Rbtn_Type_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="1">Summary</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Detail</asp:ListItem>
                            </asp:RadioButtonList></td>

                        <td style="width: 33%" colspan="2">
                            <asp:RadioButtonList ID="rbtn_wise" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtn_wise_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Value="0" Selected="True">Booking Branch Wise</asp:ListItem>
                                <asp:ListItem Value="1">Billing Branch Wise</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td class="TD1" style="width: 9%"></td>
                        <td style="width: 24%"></td>
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
              <a href="javascript:input_screen_action('view');">View Inupt</a>
            </td>
            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Inupt</a>
            </td>
            <td style="width:58%;">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
            </td>
      </tr>
    </table>
   <table class="TABLE">
      <tr>
        <td>
        <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Monitor" UpdateMode="Conditional" runat="server">
           <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
               <ContentTemplate>
                 <asp:Panel ID="pnl_Unbilled_CN_Register" runat="server" ScrollBars="Auto" Height="495px">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                         <asp:TemplateColumn HeaderText="CN No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "CN No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                          <asp:BoundColumn DataField="CN date" HeaderText="CN Date"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Consignor" HeaderText="Consignor"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Consignee" HeaderText="Consignee"></asp:BoundColumn>
                      
                          <asp:BoundColumn HeaderText="From" DataField="from"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="To" DataField="to"></asp:BoundColumn>
                          
                          <asp:BoundColumn HeaderText="Billing Party" DataField="Billing Party"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="Billing Location" DataField="Billing Location"></asp:BoundColumn>
               
                          <asp:BoundColumn HeaderText="FCO" DataField="FCO"></asp:BoundColumn>
                          <asp:TemplateColumn HeaderText="CN Amount">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "CN Amount")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_gccaption_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                      </Columns>
                  </asp:DataGrid>
                  <asp:DataGrid ID="DataGrid1"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="200" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                          <asp:TemplateColumn HeaderText="Client Name">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Client Name")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total1" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>                 
                         <asp:TemplateColumn HeaderText="No of CN">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "No of CN")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_No_of_CN" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>                 
                          <asp:TemplateColumn HeaderText="CN Amount">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "CN Amount")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_CN_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
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
