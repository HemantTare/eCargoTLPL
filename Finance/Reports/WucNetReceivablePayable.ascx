<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucNetReceivablePayable.ascx.cs" Inherits="Finance_Reports_WucNetReceivablePayable" %>
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
    <table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LEDGER OUTSTANDING" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
         <tr>
        <td colspan="7"></td>
        </tr>
        
         <tr>
        <td colspan="2" style="height: 5px" align="left">
         &nbsp;<asp:Button ID="btn_ShowBillWiseDetails" runat="server" CssClass="BUTTON" Font-Names="verdana"
                Text="SHOW BILLWISE DETAILS" Width="200px" OnClick="btn_ShowBillWiseDetails_Click"/>
                 &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_Ageing" runat="server" CssClass="BUTTON" Font-Names="verdana"
                Text="AGEING" Width="100px" OnClick="btn_Ageing_Click" />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               </td>
                </tr>
        <tr>
            <td align="left" colspan="2" style="height: 5px">
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2" style="height: 5px">
                <uc1:WucStartEndDate ID="WucStartEndDate1" runat="server" />
            </td>
        </tr>
             
                
                  <tr>
        <td colspan="6" >
                <igtbl:UltraWebGrid ID="grid" runat="server" Height="360px" Width="100%"  OnInitializeRow="grid_InitializeRow" OnInitializeLayout="grid_InitializeLayout" EnableTheming="True">
                            <Bands>
                                <igtbl:UltraGridBand>
                                    <AddNewRow View="NotSet" Visible="NotSet">
                                    </AddNewRow>
                                </igtbl:UltraGridBand>
                            </Bands>
                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                AllowSortingDefault="OnClient" BorderCollapseDefault="Separate"
                                HeaderClickActionDefault="SortMulti" Name="ctl00xgrid" RowHeightDefault="20px"
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
        <tr>
            <td colspan="6">
             <igtblexp:UltraWebGridExcelExporter ID="UltraWebGridExcelExporter1" runat="server">
              </igtblexp:UltraWebGridExcelExporter>
            </td>
        </tr>
    <tr>
     <asp:Panel ID="pnl_Total" runat="server" Width="100%">
        <table style="width:100%">        
        <tr style="width:80%">   
        <td style="width:10%"></td> 
         <td style="width:10%"></td>       
       <td class="TD1" style="width:50%;font-family:Verdana;font-size:small">Total Pending Amount :</td>  
        <td  style="width:40%">
         &nbsp<asp:Label Id="lbl_PendingAmount" runat="server" CssClass="LABEL" Font-Bold="true"  Text="TOTAL:" ></asp:Label> 
       
         </td>
         <td style="width :20%"></td>
         </tr>
         </table>
         </asp:Panel>
         
        
    </tr>
    
     <tr>
     <asp:Panel ID="Pnl_OnAccount" runat="server" Width="100%">
        <table style="width:100%">        
        <tr style="width:80%">   
        <td style="width:10%"></td> 
         <td style="width:10%"></td>       
       <td class="TD1" style="width:50%;font-family:Verdana;font-size:small">On Account :</td>  
        <td  style="width:40%">
         &nbsp<asp:Label Id="lbl_OnAccount" runat="server" CssClass="LABEL" Font-Bold="true"  Text="OnAccount:" ></asp:Label> 
       
         </td>
         <td style="width :20%"></td>
         </tr>
         </table>
         </asp:Panel>
         
        
    </tr>
     <tr>
     <asp:Panel ID="Pnl_FinalTotal" runat="server" Width="100%">
        <table style="width:100%">        
        <tr style="width:80%">   
        <td style="width:10%"></td> 
         <td style="width:10%"></td>       
       <td class="TD1" style="width:50%;font-family:Verdana;font-size:small">Net Amount :</td>  
        <td  style="width:40%">
         &nbsp<asp:Label Id="lbl_NetAmount" runat="server" CssClass="LABEL" Font-Bold="true"  Text="NET TOTAL:" ></asp:Label> 
       
         </td>
         <td style="width :20%"></td>
         </tr>
         </table>
         </asp:Panel>
         
        
    </tr>
   
       </table>