<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Unsubmitted_CN_Register.aspx.cs" Inherits="Reports_Delivery_Frm_Unsubmitted_CN_Register" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc6" %>

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
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="scm_Unbilled_CN_Register" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Unsubmitted CN Register"></asp:Label>
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
                        <td style="width: 9%;" class="TD1">
                            Booking Type:</td>
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
                        <td style="width: 10%;" class="TD1">
                            Document Type:</td>
                        <td style="width: 24%;">
                            <asp:DropDownList ID="ddl_document_type" runat="server" CssClass="DROPDOWN" AutoPostBack="True">
                                <asp:ListItem Value="0">Actual </asp:ListItem>
                                <asp:ListItem Value="1">As On Date</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 9%;" class="TD1">
                            <asp:Label ID="lbl_from" runat="server" CssClass="LABEL"></asp:Label></td>
                        <td style="width: 24%;">
                            <uc6:WucDatePicker ID="WucDatePicker1" runat="server" OnLoad="WucDatePicker1_Load" />
                            </td>
                        <td style="width: 9%;" class="TD1">
                            <asp:Label ID="lbl_to" runat="server" CssClass="LABEL"></asp:Label></td>
                        <td style="width: 24%;">
                            <uc6:WucDatePicker ID="WucDatePicker2" runat="server" />
                        </td>
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
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                         <asp:TemplateColumn HeaderText="gc_caption No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                          <asp:BoundColumn DataField="gc_caption date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="consignor_name" HeaderText="Consignor Name"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="consignee_name" HeaderText="Consignee Name"></asp:BoundColumn>
                      
                          <asp:BoundColumn HeaderText="From" DataField="from"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="To" DataField="to"></asp:BoundColumn>
                          
                          <asp:BoundColumn HeaderText="Client Name" DataField="client_name"></asp:BoundColumn>
                       
                          <asp:BoundColumn HeaderText="FOC" DataField="FOC"></asp:BoundColumn>
                          <asp:TemplateColumn HeaderText="gc_caption Amount">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "gc_caption Amount")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_gccaption_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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
