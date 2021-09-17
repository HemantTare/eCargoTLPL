<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranchWiseBookingRegisterStatusView.aspx.cs" Inherits="Reports_CL_Nandwana_Booking_FrmBranchWiseBookingRegisterStatusView" %>

<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
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
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Branch Wise Booking Register View</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    

    <form id="form1" runat="server">
<asp:ScriptManager ID="scm_BookingRegister" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE" onclick="rr()">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch wise Booking Register"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
 
      <tr>
        <td style="width:50%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
      </tr>
      <tr>
        <td style="width:50%">
            <uc3:Wuc_GC_Parameters ID="Wuc_GC_Parameters1" runat="server" />
        </td>
      </tr>
      <tr>
       <td style="width:50%">
            <uc6:WucFilter ID="WucFilter1" runat="Server" />
        </td>
      </tr>
      <tr>
        <td style="width:50%">
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
        </td>
      </tr>

    <tr>
        <td style="width: 50%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%; height: 41px;" class="TD1">
                     <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                    <td style="width: 24%; height: 41px;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                    <td colspan="4" style="height: 41px">
                        <asp:RadioButtonList ID="rdl_BookingRegister" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected="True">BranchWise Booking Register</asp:ListItem>
                        <asp:ListItem Value="1">DeliveryWise Booking Register</asp:ListItem>
                      </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
          
    </table>

    <table class="TABLE">
      <tr>
            <td style="width:10%">
              <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
            </td>
            <td style="width:10%">
              <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
            </td>

            <td style="width:10%">
              <a href="javascript:input_screen_action('view');">View Input</a>
            </td>

            <td style="width:10%">
              <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>


            <td style="width:50%">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                    Text="Close Window" /></td>
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
                <div class="DIV" style="height: 510px; width:998px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true"
                      AllowPaging="True" AllowCustomPaging="true" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="9">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                      <Columns>
                      <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption No"></asp:BoundColumn>                                                 
                      <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Bkg Branch" HeaderText="Bkg Branch"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="From Location Name" HeaderText="From Location Name"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Dly Branch" HeaderText="Dly Branch"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Cnee Name" HeaderText="Cnee Name"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Cnee Tel No" HeaderText="Cnee Tel No"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Cnr Name" HeaderText="Cnr Name"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Cnr Tel No" HeaderText="Cnr Tel No"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Booking Mode" HeaderText="Booking Mode"></asp:BoundColumn> 
                      <%--<asp:BoundColumn DataField="Pay Mode" HeaderText="Pay Mode"></asp:BoundColumn>--%> 

                      <asp:TemplateColumn HeaderText="Pay Mode">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Pay Mode")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                                      
                      <asp:TemplateColumn HeaderText="Charged Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ChargedWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Actual Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ActualWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Articles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Basic Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Basic Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_BasicFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Local Charges" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Local Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Local_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="FOV Charges" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "FOV Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_FOVCharges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="ODA Charges" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "ODA Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ODACharges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Other Charges" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Other Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_OtherCharges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Sub Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Sub Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_SubFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="STax Amt" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "STax Amt")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_STaxAmt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Invoice Value" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Invoice Value")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_InvoiceValue" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Hamali Charge" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Hamali Charge")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_HamaliCharge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      
                      <asp:TemplateColumn HeaderText="DD Charge" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "DD Charge")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_DDCharge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      
                      <asp:TemplateColumn HeaderText="Bilti Charges" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Bilti Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_BiltiCharges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:BoundColumn DataField="Booking_Mr" HeaderText="Booking Mr"></asp:BoundColumn>                                                 
                      <asp:BoundColumn DataField="Delivery_Mr" HeaderText="Delivery Mr"></asp:BoundColumn>                                                 
                      <asp:BoundColumn DataField="Credit_Memo" HeaderText="Credit Memo"></asp:BoundColumn>                                                 
                      <asp:BoundColumn DataField="Bill_No" HeaderText="Bill No"></asp:BoundColumn>                                                 
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
