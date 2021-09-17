<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucChart.ascx.cs" Inherits="CRM_WucChart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc1" %>
<%@ Register Assembly="Infragistics2.WebUI.Misc.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
    
<script type="text/javascript" language="javascript">

function ValidateDate()
{
var from_date = WucChart1_From_Date_Picker.GetSelectedDate();
from_date = new Date(from_date.getFullYear(), from_date.getMonth(),from_date.getDate())

var to_date = WucChart1_To_Date_Picker.GetSelectedDate();
to_date = new Date(to_date.getFullYear(), to_date.getMonth(),to_date.getDate())

  if (to_date < from_date)
  {
  alert("To Date Must Be greater Than From Date")
  return false;
  }
 
}
</script>

<table class="TABLE" border="2"> 
    <tr>
        <td class="TDGRADIENT" colspan="4" style="height: 23px">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="CRM DASHBOARD REPORT"></asp:Label>
        </td>
    </tr> 
    <tr>
          <td colspan="4" style="height: 23px">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <table width="100%">
            <tr>
                <td style="width:15%" class="TD1">
                <asp:Label ID="lbl_Select" CssClass="LABEL" runat="server" Text="Select Complaint By:"></asp:Label>
                </td>
                <td style="width:25%">
                <asp:DropDownList ID="ddl_ChartComplaint" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_ChartComplaint_SelectedIndexChanged">
                </asp:DropDownList>
                </td>
                <td style="width:25%" colspan="2">
                    <table width="100%"  runat="server" id="tbl_rangetype">
                        <tr>
                            <td style="width:30%" class="TD1">
                            <asp:Label ID="lbl_RangeType" runat="server" CssClass="LABEL" Text="Range Type :"></asp:Label></td>
                            <td style="width:70%">
                            <asp:RadioButtonList ID="rbtn_Range_Type" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtn_Range_Type_SelectedIndexChanged">
                            <asp:ListItem Value="1">Closed</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">Ticket</asp:ListItem>
                            </asp:RadioButtonList></td>
                        </tr>
                    </table>
                </td>
 
            </tr>
            <tr>               
                <td style="width:10%;" class="TD1">From Date:</td>
                <td style="width:20%;">
                    <uc1:wuc_Date_Picker ID="From_Date" runat="server" />
                </td>
                <td style="width:10%;" class="TD1" id="TD1" runat="server">To Date:</td>
                <td style="width:20%;">
                    <uc1:wuc_Date_Picker ID="To_Date" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btn_Display" runat="server" CssClass="BUTTON" Text="Display" OnClientClick="return ValidateDate()" OnClick="btn_Display_Click" />
                </td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" style="height: 299px">
        <table style="width:100%">
        <tr>
        <td style="width:50%">
        <igchart:UltraChart ID="cht_PieChart" runat="server" BorderWidth="0px" CrossHairColor="BlanchedAlmond" EmptyChartText="Data Not Available For Selected Date Range"
                Height="230px" Width="650px" SmoothingMode="HighQuality" Transform3D-XRotation="35" Transform3D-YRotation="-12" 
                Version="7.1" BackColor="WhiteSmoke" Transform3D-ZRotation="-5" Transform3D-Outline="True" Transform3D-Scale="60" OnChartDataClicked="cht_PieChart_ChartDataClicked">
                <Axis>
                    <Z LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                            Visible="False" />
                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                            Visible="True" />
                        <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString=""
                            Orientation="Horizontal" VerticalAlign="Center">
                            <Layout Behavior="Auto">
                            </Layout>
                            <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" Orientation="Horizontal"
                                VerticalAlign="Center">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                        </Labels>
                    </Z>
                    <Y2 LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="False">
                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                            Visible="False" />
                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                            Visible="True" />
                        <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                            Orientation="Horizontal" VerticalAlign="Center">
                            <Layout Behavior="Auto">
                            </Layout>
                            <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" FormatString="" HorizontalAlign="Near"
                                Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                        </Labels>
                    </Y2>
                    <X LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="True">
                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                            Visible="False" />
                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                            Visible="True" />
                        <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" ItemFormatString="&lt;ITEM_LABEL&gt;"
                            Orientation="VerticalLeftFacing" VerticalAlign="Center">
                            <Layout Behavior="Auto">
                            </Layout>
                            <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center"
                                Orientation="Horizontal" VerticalAlign="Center">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                        </Labels>
                    </X>
                    <Y LineThickness="1" TickmarkInterval="20" TickmarkStyle="Smart" Visible="True">
                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                            Visible="False" />
                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                            Visible="True" />
                        <Labels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" ItemFormatString="&lt;DATA_VALUE:00.##&gt;"
                            Orientation="Horizontal" VerticalAlign="Center">
                            <Layout Behavior="Auto">
                            </Layout>
                            <SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" FormatString="" HorizontalAlign="Far"
                                Orientation="VerticalLeftFacing" VerticalAlign="Center">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                        </Labels>
                    </Y>
                    <X2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                            Visible="False" />
                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                            Visible="True" />
                        <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" ItemFormatString="&lt;ITEM_LABEL&gt;"
                            Orientation="VerticalLeftFacing" VerticalAlign="Center">
                            <Layout Behavior="Auto">
                            </Layout>
                            <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center"
                                Orientation="Horizontal" VerticalAlign="Center">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                        </Labels>
                    </X2>
                    <PE ElementType="None" Fill="Cornsilk" />
                    <Z2 LineThickness="1" TickmarkInterval="0" TickmarkStyle="Smart" Visible="False">
                        <MinorGridLines AlphaLevel="255" Color="LightGray" DrawStyle="Dot" Thickness="1"
                            Visible="False" />
                        <MajorGridLines AlphaLevel="255" Color="Gainsboro" DrawStyle="Dot" Thickness="1"
                            Visible="True" />
                        <Labels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" ItemFormatString=""
                            Orientation="Horizontal" VerticalAlign="Center">
                            <Layout Behavior="Auto">
                            </Layout>
                            <SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" Orientation="Horizontal"
                                VerticalAlign="Center">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                        </Labels>
                    </Z2>
                </Axis>
                <Border Color="IndianRed" Raised="True" />
                      <Tooltips Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                          Font-Underline="False" />
                <Effects>
                    <Effects>
                        <igchartprop:GradientEffect>
                        </igchartprop:GradientEffect>
                    </Effects>
                </Effects>
                <ColorModel AlphaLevel="150" ColorBegin="SandyBrown" ColorEnd="0, 0, 192" ModelStyle="CustomLinear">
                </ColorModel>
                <Legend BackgroundColor="LightSteelBlue" DataAssociation="ColumnData" Visible="True">
                </Legend>
            <Data UseMinMax="True">
            </Data>
            </igchart:UltraChart>
        </td>
         <td style="width:50%;vertical-align:top;">
        <igtbl:ultrawebgrid id="PieChartGrid" runat="server" Height="230px" Width="350px" 
                OnPageIndexChanged="PieChartGrid_PageIndexChanged" OnInitializeDataSource="PieChartGrid_InitializeDataSource" Browser="Xml" >
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet"></AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>

                    <DisplayLayout ViewType="OutlookGroupBy" Version="4.00" StationaryMargins="Header"
                    AllowColSizingDefault="Free" StationaryMarginsOutlookGroupBy="True" 
                    HeaderClickActionDefault="SortMulti"
                    Name="ctl00xPieChartGrid" BorderCollapseDefault="Separate" RowSelectorsDefault="No" TableLayout="Fixed" 
                    RowHeightDefault="20px" LoadOnDemand="Xml" SelectTypeRowDefault="Extended" AllowSortingDefault="OnClient" >
                    <GroupByBox Hidden="True">
                    <Style BorderColor="Window" BackColor="ActiveBorder"></Style>
                    </GroupByBox>
                    
                    <GroupByRowStyleDefault BorderColor="Window" BackColor="Control"></GroupByRowStyleDefault>

                    <ActivationObject BorderWidth="" BorderColor=""></ActivationObject>

                    <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </FooterStyleDefault>

                    <RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window">
                    <BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>

                    <Padding Left="3px"></Padding>
                    </RowStyleDefault>

                    <FilterOptionsDefault>
                    <FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" CustomRules="overflow:auto;">
                    <Padding Left="2px"></Padding>
                    </FilterOperandDropDownStyle>

                    <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55"></FilterHighlightRowStyle>

                    <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" CustomRules="overflow:auto;">
                    <Padding Left="2px"></Padding>
                    </FilterDropDownStyle>
                    </FilterOptionsDefault>

                    <HeaderStyleDefault HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </HeaderStyleDefault>

                    <EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

                    <FrameStyle BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window" Height="100%"></FrameStyle>

                    <Pager  MinimumPagesForDisplay="2" Alignment="Left" AllowPaging="True" ChangeLinksColor="True" PageSize="10">
                    <Style BorderWidth="1px" Font-Bold="true" BorderStyle="Solid" BackColor="LightGray">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </Style>
                    </Pager>

                    <AddNewBox Hidden="False">
                    <Style BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" BackColor="Window">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </Style>
                    </AddNewBox>
                    </DisplayLayout>
                </igtbl:ultrawebgrid>
        </td>
        </tr>
        </table>  
        </td>  
    </tr>
     <tr>
        <td style="width: 100%" colspan="4">
               <igtbl:UltraWebGrid id="GridDetails" DisplayLayout-ScrollBar="Always" runat="server" width="100%" OnInitializeLayout="GridDetails_InitializeLayout" 
                   OnInitializeRow="GridDetails_InitializeRow" Browser="Xml" OnInitializeDataSource ="GridDetails_InitializeDataSource" OnPageIndexChanged="GridDetails_PageIndexChanged" Height="100%">
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet"></AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>

                    <DisplayLayout ViewType="OutlookGroupBy" Version="4.00"
                    StationaryMargins="Header" AllowColSizingDefault="Free" AllowUpdateDefault="Yes" 
                    StationaryMarginsOutlookGroupBy="True" HeaderClickActionDefault="SortMulti" Name="ctl00xGridDetails" 
                    BorderCollapseDefault="Separate" AllowDeleteDefault="Yes" RowSelectorsDefault="No" TableLayout="Fixed" 
                    RowHeightDefault="20px" SelectTypeRowDefault="Extended" LoadOnDemand="Xml" ScrollBar="Always" AllowSortingDefault="OnClient" RowsRange="0">
                                  
                    <GroupByBox Hidden="True">
                    <Style BorderColor="Window" BackColor="ActiveBorder"></Style>
                    </GroupByBox>

                    <GroupByRowStyleDefault BorderColor="Window" BackColor="Control"></GroupByRowStyleDefault>

                    <ActivationObject BorderWidth="" BorderColor=""></ActivationObject>

                    <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </FooterStyleDefault>

                    <RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window">
                    <BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>

                    <Padding Left="3px"></Padding>
                    </RowStyleDefault>

                    <FilterOptionsDefault>
                    <FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" CustomRules="overflow:auto;">
                    <Padding Left="2px"></Padding>
                    </FilterOperandDropDownStyle>

                    <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55"></FilterHighlightRowStyle>

                    <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="100%" Height="100%" CustomRules="overflow:auto;">
                    <Padding Left="2px"></Padding>
                    </FilterDropDownStyle>
                    </FilterOptionsDefault>

                    <HeaderStyleDefault HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </HeaderStyleDefault>

                    <EditCellStyleDefault BorderWidth="0px" BorderStyle="None"></EditCellStyleDefault>

                    <FrameStyle BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window" Width="100%" Height="100%"></FrameStyle>

                    <Pager StyleMode="QuickPages" QuickPages="10" MinimumPagesForDisplay="1"  Alignment="Left" AllowPaging="True" PageSize="10">
                    <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray" Font-Bold="true">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </Style>
                    </Pager>

                    <AddNewBox Hidden="False">
                    <Style BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" BackColor="Window">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </Style>
                    </AddNewBox>
                    </DisplayLayout>
                </igtbl:UltraWebGrid> 
        </td>
    </tr>
    <tr runat="server" visible="false">
        <td style="width: 100%" colspan="4">
               <igtbl:UltraWebGrid id="ExportGrid" runat="server" width="100%">                
                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortMulti" Name="ctl00xExportGrid" RowHeightDefault="20px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                        <HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray" HorizontalAlign="Left">
                          <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                        </HeaderStyleDefault>
                        <FrameStyle BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" Width="100%" Height="100%" BackColor="Window" BorderColor="InactiveCaption" BorderWidth="1px"></FrameStyle>
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
                        Font-Names="Microsoft Sans Serif" Font-Size="8.25pt">
                        <BorderDetails ColorLeft="Window" ColorTop="Window" />
                        <Padding Left="3px" />
                      </RowStyleDefault>
                      <FilterOptionsDefault>
                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                          BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                          Font-Size="11px">
                          <Padding Left="2px" />
                        </FilterOperandDropDownStyle>
                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                        </FilterHighlightRowStyle>
                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                          CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px"
                          Height="300px" Width="200px">
                          <Padding Left="2px" />
                        </FilterDropDownStyle>
                      </FilterOptionsDefault>
                      <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                      </EditCellStyleDefault>
                      <Pager MinimumPagesForDisplay="2" Alignment="Center">
                        <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                      <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                      </Style>
                      </Pager>
                      <AddNewBox Hidden="False">
                        <Style BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                        <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                        </Style>
                        
                      </AddNewBox>
                    </DisplayLayout>
                 <Bands>
                   <igtbl:UltraGridBand>
                     <AddNewRow View="NotSet" Visible="NotSet">
                     </AddNewRow>
                   </igtbl:UltraGridBand>
                 </Bands>
                </igtbl:UltraWebGrid> 
        </td>
    </tr>
    <tr>
        <td colspan="4" style="width: 100%">
        <div style="z-index:0">
            <asp:ImageButton ID="IB_ExportExcel" runat="server" ImageUrl="~/images/excel.gif" OnClick="IB_ExportExcel_Click" />
        </div> 
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="4">&nbsp;
        <div style="z-index:0">
            <igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter>
        </div> 
        </td>
    </tr>
</table>
