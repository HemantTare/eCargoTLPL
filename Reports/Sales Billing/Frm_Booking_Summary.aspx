<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Booking_Summary.aspx.cs" Inherits="Reports_Sales_Billing_Frm_Booking_Summary" %>
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
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_Booking_Summary" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Booking Summary"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
       <tr>
        <td style="width:100%;">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                       <td style="width: 10%; " class="TD1" id="Division" runat="server">
                            <asp:Label ID="lbl_division" runat="server" CssClass="LABEL"></asp:Label></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">
                            Report Type:</td>
                        <td style="width: 24%;">
                            <asp:DropDownList ID="ddl_Report_Type" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Booking Branch Wise</asp:ListItem>
                                <asp:ListItem Value="2">Booking Date Wise</asp:ListItem>
                                <asp:ListItem Value="3">Delivery Area Wise</asp:ListItem>
                                <asp:ListItem Value="4">Client Wise</asp:ListItem>
                                <asp:ListItem Value="5">Commodity Wise</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr>
        <td style="width:100%">
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
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
              <a href="javascript:input_screen_action('view');">View Input</a>
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>


            <td style="width:58%;">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                    Text="Close Window" /></td>
      </tr>
    </table>
      <table class="TABLE">
      <tr>
        <td style="height: 524px">
          <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Monitor" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
               <ContentTemplate>
                <div class="DIV" style="height: 510px; width:976px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                        <asp:TemplateColumn HeaderText="Column Name">
                        <HeaderTemplate>
                        <asp:Label ID="lbl_Column_Name" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>                    
                        <%# DataBinder.Eval(Container.DataItem, "Column Name")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Sundry NO OF GC" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry NO OF GC")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_NO_OF_GC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                 
                        <asp:TemplateColumn HeaderText="Sundry Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry Charged Weight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sundry ToPay Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry ToPay Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_ToPay_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sundry Paid Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry Paid Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_Paid_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sundry TBB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry TBB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_TBB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sundry STB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry STB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_STB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sundry FOC Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry FOC Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_FOC_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Sundry Total Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Sundry Total Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Sundry_Total_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="FTL NO OF GC" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL NO OF GC")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_NO_OF_GC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                 
                        <asp:TemplateColumn HeaderText="FTL Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL Charged Weight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FTL ToPay Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL ToPay Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_ToPay_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FTL Paid Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL Paid Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_Paid_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FTL TBB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL TBB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_TBB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FTL STB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL STB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_STB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FTL FOC Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL FOC Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_FOC_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="FTL Total Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "FTL Total Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_FTL_Total_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                             
                        <asp:TemplateColumn HeaderText="ODC NO OF GC" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC NO OF GC")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_NO_OF_GC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                 
                        <asp:TemplateColumn HeaderText="ODC Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC Charged Weight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ODC ToPay Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC ToPay Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_ToPay_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ODC Paid Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC Paid Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_Paid_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ODC TBB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC TBB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_TBB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ODC STB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC STB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_STB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ODC FOC Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC FOC Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_FOC_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ODC Total Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "ODC Total Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_ODC_Total_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn> 
                           
                        <asp:TemplateColumn HeaderText="Super ODC NO OF GC" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC NO OF GC")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_NO_OF_GC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                 
                        <asp:TemplateColumn HeaderText="Super ODC Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC Charged Weight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Super ODC ToPay Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC ToPay Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_ToPay_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Super ODC Paid Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC Paid Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_Paid_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Super ODC TBB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC TBB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_TBB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Super ODC STB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC STB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_STB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Super ODC FOC Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC FOC Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_FOC_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Super ODC Total Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Super ODC Total Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Super_ODC_Total_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>      
                        
                        <asp:TemplateColumn HeaderText="Total NO OF GC" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total NO OF GC")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_NO_OF_GC" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                 
                        <asp:TemplateColumn HeaderText="Total Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total Charged Weight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total ToPay Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total ToPay Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_ToPay_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total Paid Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total Paid Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_Paid_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total TBB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total TBB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_TBB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total STB Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total STB Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_STB_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total FOC Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total FOC Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_FOC_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                        </ItemTemplate>
                        <FooterTemplate>
                        <asp:Label ID="lbl_Grand_Total_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                
          
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
