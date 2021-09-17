<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedgerBillWiseDetail.ascx.cs" Inherits="Finance_Reports_WucLedgerBillWiseDetail" %>
<%@ Register Src="../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
    TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
    
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<script type="text/javascript">

    function ChangePeriod()
    {
    
        var Path='../../CommonControls/FrmDateRange.aspx'
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-600);
        var popH = (h-500);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    </script>
    <%--<asp:ScriptManager ID="Scm_LedgerBillWiseDetail" runat="server">
</asp:ScriptManager>--%>
    <table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LEDGER BILLWISE DETAILS" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
        
      
        
         <tr class="TD1" style="width:80%;">
         <td colspan="5" style="height: 5px;width:50%" align="left">
         &nbsp;<asp:Button ID="btn_ShowBillWiseDetails" runat="server" CssClass="BUTTON" Font-Names="verdana"
                Text="SHOW LEDGER OUTSTANDING" Width="200px" OnClick="btn_ShowLedgerOutstanding_Click"/>
                 &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_Ageing" runat="server" CssClass="BUTTON" Font-Names="verdana"
                Text="AGEING" Width="100px"  OnClick="btn_Ageing_Click" />
               &nbsp;&nbsp               
                 <asp:Button ID="btn_Detailed" runat="server" CssClass="BUTTON" Font-Names="verdana"
                Text="VIEW DETAILED" Width="150px" OnClick="btn_Detailed_Click"  />
                
               </td>
                </tr>
             <tr>
        <td colspan="7"></td>
        </tr>
 <tr>
        <td colspan="1" style="width: 8px;">
            
        </td>
        <td style="width: 50%;" colspan="5">
        </td>
    </tr>
    <tr>
        <td style="width: 50%;" colspan="7">
            <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server">
            </igtblexp:UltraWebGridExcelExporter>
        </td>
        
    </tr>
        <tr>
            <td colspan="7">
                <uc1:WucStartEndDate ID="WucStartEndDate1" runat="server" />
            </td>
        </tr>
         <tr>
        <td colspan="7"></td>
        </tr>
        
      
                  <tr>
        <td  style="width: 20%; font-weight: bold;" class="TD1">
           <asp:Label ID="lbl_ledger_Name" runat="server"  CssClass="LABEL" Text="Ledger Name:"></asp:Label></td>
            <td  style="width: 50%">
            <asp:Label ID="lbl_LedgerName" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
               </tr>
               
       
        
        <tr> 
      <td class="TD1" style="width: 20%"> </td>
               <td style="width: 50%">             
        <asp:Label ID="lbl_Debit" runat="server"  Font-Bold="true" CssClass="LABEL" Text="(-Ve)Amount indicates Debit (Dr)"></asp:Label>
        </td>
        </tr>
        
        <tr>
        <td class="TD1" style="width: 20%"> </td>
                   <td  style="width: 50%">
        <asp:Label ID="lbl_Credit" runat="server" CssClass="LABEL"  Font-Bold="true" Text="(+Ve)Amount indicates Credit (Cr)"></asp:Label>
        </td>
        </tr>
         <tr>
        <td colspan="7" style="height: 21px"></td>
        </tr>
        
      
        
                  <tr>
        <td colspan="6" >
                <igtbl:UltraWebGrid ID="grid1" runat="server" Height="360px" Width="100%"  OnInitializeRow="grid1_InitializeRow" OnInitializeLayout="grid1_InitializeLayout" EnableTheming="True">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                AllowSortingDefault="OnClient" BorderCollapseDefault="Separate"
                                HeaderClickActionDefault="SortMulti" Name="ctl00xgrid1" RowHeightDefault="20px"
                                RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header" SelectTypeCellDefault="Single"
                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                <GroupByBox>
                                    <Style BackColor="ActiveBorder" BorderColor="Window"></Style>
                                </GroupByBox>
                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                </GroupByRowStyleDefault>
                                <ActivationObject BorderColor="" BorderWidth="">
                                </ActivationObject>
                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </FooterStyleDefault>
                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Verdana" Font-Size="8.25pt">
                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                    <Padding Left="3px" />
                                </RowStyleDefault>
                                <FilterOptionsDefault AllowRowFiltering="OnServer">
                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px">
                                        <Padding Left="2px" />
                                    </FilterOperandDropDownStyle>
                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                    </FilterHighlightRowStyle>
                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                        CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                        Font-Size="11px" Height="300px" Width="200px">
                                        <Padding Left="2px" />
                                    </FilterDropDownStyle>
                                </FilterOptionsDefault>
                                <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid" HorizontalAlign="Left">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </HeaderStyleDefault>
                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                </EditCellStyleDefault>
                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="360px"
                                    Width="100%" >
                                </FrameStyle>
                               <%--<Pager  >
                                    <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                                </Style>
                                </Pager>--%>
                                <RowSelectorStyleDefault BackColor="#FFC0FF">
                                </RowSelectorStyleDefault>
                                <SelectedRowStyleDefault BackColor="BlanchedAlmond"  Font-Bold="True">
                                </SelectedRowStyleDefault>
                                
                                
                            </DisplayLayout>
                        </igtbl:UltraWebGrid>
                        </td>
    </tr>   
        <tr>
            <td colspan="6">
            <table>
                <tr>
                    <td style="width: 100px">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Excel.gif" OnClick="ImageButton1_Click"
                            ToolTip="Export To Excel" />
                    </td>
                </tr>
            </table>
            </td>
        </tr>
   
       </table>