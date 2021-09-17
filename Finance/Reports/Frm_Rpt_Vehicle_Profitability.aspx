<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_Vehicle_Profitability.aspx.cs" Inherits="Finance_Reports_Frm_Rpt_Vehicle_Profitability" %>
<%@ Register Src="~/CommonControls/WucVehicleSearch.ascx" TagName="Wuc_Vehicle_Search" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type ="text/javascript">
    function input_screen_action(action)
    {
        if(action == "view")
        {
            tbl_input_screen.style.display = 'inline';
        }
        else
        {
            tbl_input_screen.style.display = 'none';
        }
    }
   
    function Open_GC_Details(LHPOID)
{
    var Path='../../Finance/Reports/Frm_Rpt_Vehicle_Profitability_GCwise.aspx?LHPO_ID=' + LHPOID;
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = 850;
    var popH = 600;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
      window.open(Path, 'CustomPopUp_M', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
      return false;
}
    </script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Vehicle Profitability</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ScriptManager id="scm_Vehicle_Profitability" runat="Server"></asp:ScriptManager>
        <table class="TABLE" style="width: 100%">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Vehicle Profitability"></asp:Label></td>
            </tr>
        </table>
        <table id="tbl_input_screen" class="TABLE" style="width: 100%">
            <tr>
                <td class="TD1" style="width: 10%">
                    <asp:Label ID="lbl_Vehicle" runat="server" Text="Vehicle No:" Width="69px"></asp:Label></td>
                <td style="width: 24%">
                    <asp:DropDownList ID="dd_Vehicle" runat="server" Width="147px">
                    </asp:DropDownList></td>
                <td class="TD1" style="width: 9%"></td>
                <td style="width: 24%"></td>
                <td style="width: 9%"></td>
                <td style="width: 24%"></td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc1:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
    </table>
     <table class="TABLE" style="width: 100%">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_View_Click"  />
                </td>
                <td style="width: 10%">
                    <uc3:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server"  />
                </td>
                <td style="width: 10%">
                    <a href = "javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href = "Javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label>
                  
                </td>
            </tr>
        </table>
        
        <table class="TABLE" style="width: 100%">
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="upd_pnl_Vehicle_Register" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_Vehicle_Profitability_Register" runat="server" ScrollBars="Auto" Height="500px">
                                <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                                AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15" AllowCustomPaging="True">
                                    <HeaderStyle CssClass = "GRIDHEADERCSS" />
                                    <AlternatingItemStyle CssClass = "GRIDALTERNATEROWCSS" />
                                    <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" />
                                    <Columns>                                                                                                           
                                       <asp:TemplateColumn HeaderText="Vehicle No">
                                          <ItemTemplate>                    
                                            <%# DataBinder.Eval(Container.DataItem, "Vehicle_No")%> 
                                          </ItemTemplate>
                                        <FooterTemplate>
                                         <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>                                          
                                      </asp:TemplateColumn>                                        
                                        <asp:TemplateColumn HeaderText="LHPO No">
                                        <ItemTemplate>                    
                                       <%# DataBinder.Eval(Container.DataItem, "LHPO_No_For_Print")%> 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" /> 
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="LHPO Date" HeaderText="LHPO Date"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="From Location">
                                        <ItemTemplate>                    
                                        <%# DataBinder.Eval(Container.DataItem, "From Location")%> 
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="left" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="To Location">
                                          <ItemTemplate>                    
                                     <%# DataBinder.Eval(Container.DataItem, "To Location")%>  
                                          </ItemTemplate>
                                      </asp:TemplateColumn>                                        
                                        <asp:TemplateColumn HeaderText="Total Truck Hire">
                                        <ItemTemplate>                    
                                        <%# DataBinder.Eval(Container.DataItem, "Total Truck Hire")%> 
                                        </ItemTemplate>
                                        <FooterTemplate>
                                         <asp:Label ID="lbl_Total_Truck_Hire" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />  
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                      <asp:TemplateColumn HeaderText="Total Fright Income">
                                          <ItemTemplate>                    
                                           <asp:LinkButton ID="btn_Total_Fright_Income" Text='<%#Eval("Total Freight Income")%>' runat="server"></asp:LinkButton>
                                           <asp:HiddenField ID="hdn_LHPO_Id" Value='<%#Eval("LHPO_Id")%>' runat="server"></asp:HiddenField>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                        <asp:Label ID="lbl_Total_Fright_Income" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />  
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateColumn>                                 
                                        <asp:TemplateColumn HeaderText="Other Charges">
                                        <ItemTemplate>                    
                                        <%# DataBinder.Eval(Container.DataItem, "Other Charges")%> 
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbl_Other_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />  
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn> 
                                         <asp:TemplateColumn HeaderText="Profit/Loss">
                                        <ItemTemplate>                    
                                       <%# DataBinder.Eval(Container.DataItem, "Profit_Loss")%> 
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lbl_Profit_And_Loss" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />  
                                             <FooterStyle HorizontalAlign="Right" />
                                             <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn> 
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
