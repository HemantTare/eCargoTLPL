<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmIncomingTrucksAlert_Nandwana.aspx.cs" Inherits="Reports_CL_Nandwana_User_Desk_FrmIncomingTrucksAlert_Nandwana" %>

<%@ Register Src="../../../CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>

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
function viewwindow_general(DocType,DocNo)
    {
    var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 500;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUpGC6767', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
          return false;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Incoming Vehicle</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_IncommingTrucksAlert" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
         <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Incoming Vehicle"></asp:Label>
        </td>
      </tr>
    </table>
    <table runat="server" id="tbl_input_screen" class="TABLE">
        <tr>
            <td>
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
        </tr>     

        <tr>
            <td>
           <uc3:WucFilter id="WucFilter1" runat="server"> </uc3:WucFilter>
          </td>
        </tr>     
                          
        </table>
        <table class="TABLE">
         <tr>
         
         
           
<%--                <td id="td_lblTruckNo" runat="server" style="width: 20%; text-align:right">
                    <asp:Label ID="lbl_TruckNo" runat="server" Text="Truck No:" />
                </td>
                <td id="td_TruckNo" runat="server" style="width: 29%">
                   <asp:TextBox ID="txt_TruckNo" CssClass="TEXTBOX" runat="server" />
                </td>
                <td style="width:20%" />
                 <td style="width:29%" />--%>
            </tr>        
        </table>
    <table class="TABLE" >
      <tr>
            <td style="width:10%;">
              <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click"  />
            </td>
            
            <td style="width:10%">
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
        <td>
          <asp:UpdatePanel ID="Upd_Pnl_IncommingTrucksAlert" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                <div class="DIV" style="height: 510px; width:998px;">
                     <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true"
                      AllowPaging="True" AllowCustomPaging="true" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="15">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                      <Columns>
                      <asp:BoundColumn DataField="Truck No" HeaderText="Truck No"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Vehicle Category" HeaderText="Vehicle Category"></asp:BoundColumn> 
                 <%--     <asp:BoundColumn DataField="LHC No" HeaderText="LHC No"></asp:BoundColumn> --%>
                        <asp:TemplateColumn HeaderText="LHC No">
                           <ItemTemplate>    
                          <asp:LinkButton ID="lnk_LHC_No" runat="server" Text='<%# Eval("LHC No") %>'  />
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_LHC_No" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" />
                          </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="LHC Date">
                        <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "LHC Date")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      
                      
                        <asp:TemplateColumn HeaderText="From" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "From")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_TotalCountFrom" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      
                       <asp:TemplateColumn HeaderText="To" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                       <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "To")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_TotalCountTo" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                         
                                      
                      <asp:TemplateColumn HeaderText="Loaded Articles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Loaded Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_LoadedArticles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Loaded Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Loaded Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_LoadedWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Vehicle Capactiy" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Vehicle Capacity")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_VehicleCapacity" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Loaded Article For Us" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Loaded Articles For Us")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_LoadedArticleForUs" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Loaded Wt For Us" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Loaded Weight For Us")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_LoadedWtForUs" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:BoundColumn DataField="LHPO_ID" HeaderText="LHPO_ID" Visible="true"></asp:BoundColumn> 
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
