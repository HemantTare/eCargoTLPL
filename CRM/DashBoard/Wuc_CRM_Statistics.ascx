<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wuc_CRM_Statistics.ascx.cs" Inherits="CRM_DashBoard_Wuc_CRM_Statistics" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
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
    <%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.ExcelExport.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" TagPrefix="igtblexp" %>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<table class="TABLE">
    <tr>
        <td colspan ="6" class="TDGRADIENT">
            <asp:Label ID="Label1" runat="server" CssClass="HEADINGLABEL" Text="Complaint Statistics Report"></asp:Label></td>
    </tr>
    <tr>
         <td class="TD1" colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 15%;">Type of Report :</td>
        <td style="width: 20%;">
            <asp:DropDownList ID="DDL_Report_Type" runat="server" CssClass="DROPDOWN"></asp:DropDownList>
        </td>
        <td class="TD1" style="width: 10%;"></td>
        <td style="width: 15%;"></td>
        <td style="width: 10%;"></td>
        <td style="width: 20%; "></td>
    </tr>
    
    <tr>
        <td colspan="6">
            <uc1:wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
    </tr>
    <tr>
        <td  style="width: 12%" class="TD1">From Date</td>
        <td style="width: 15%"><uc2:wuc_Date_Picker ID="From_Date" runat="server" /></td>
        <td  style="width: 15%" class="TD1">To Date</td>
        <td style="width: 15%"><uc2:wuc_Date_Picker ID="To_Date" runat="server" /></td>
        <td colspan="2" style="width: 15%" class="TD1">
          <asp:Button ID="btn_Display" runat="server" CssClass="BUTTON" 
          Text="Display" OnClick="btn_Display_Click" />
        </td>
    </tr>

    <tr>
        <td align="center" colspan="6"></td>
    </tr>
    
    <tr>
        <td colspan="6">
               <igtbl:UltraWebGrid id="GridDetails" EnableViewState="true" DisplayLayout-ScrollBar="Always" runat="server"
               Browser="xml" OnPageIndexChanged="GridDetails_PageIndexChanged" OnInitializeDataSource ="GridDetails_InitializeDataSource">
                    <Bands>
                        <igtbl:UltraGridBand>
                            <AddNewRow View="NotSet" Visible="NotSet"></AddNewRow>
                        </igtbl:UltraGridBand>
                    </Bands>

                    <DisplayLayout ViewType="OutlookGroupBy" Version="4.00"
                    StationaryMargins="Header" AllowColSizingDefault="Free" AllowUpdateDefault="Yes" 
                    StationaryMarginsOutlookGroupBy="True" HeaderClickActionDefault="SortMulti" Name="ctl00xGridDetails" 
                    BorderCollapseDefault="Separate" AllowDeleteDefault="Yes" RowSelectorsDefault="No" TableLayout="Fixed" 
                    RowHeightDefault="20px" SelectTypeRowDefault="Extended" ScrollBar="Always" AllowSortingDefault="OnClient" RowsRange="0">

                    <GroupByBox Hidden="True">
                    <Style BorderColor="Window" BackColor="ActiveBorder"></Style>
                    </GroupByBox>

                    <RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" BackColor="Window">
                    <BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>

                    <Padding Left="3px"></Padding>
                    </RowStyleDefault>


                    <HeaderStyleDefault HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </HeaderStyleDefault>


                    <Pager MinimumPagesForDisplay="2"  Alignment="Left" AllowPaging="True" PageSize="10" QuickPages="10" StyleMode="QuickPages">
                    
                    <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray" Font-Bold="true">
                    <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                    </Style>
                    </Pager>
                        <ActivationObject BorderColor="" BorderWidth="">
                        </ActivationObject>
                    </DisplayLayout>
                </igtbl:UltraWebGrid> 
        </td>
    </tr>

      <tr runat="server" visible="false" id="Tr1">
       <td style="width: 100%" colspan="8">
               <igtbl:UltraWebGrid id="ExportGrid" runat="server" width="100%">                
                    <DisplayLayout>
                        <HeaderStyleDefault BorderStyle="Solid" BackColor="LightGray">
                        </HeaderStyleDefault>
                        <FrameStyle BorderStyle="Solid" Font-Size="8.25pt" Font-Names="Microsoft Sans Serif" Width="100%" Height="100%"></FrameStyle>
                    </DisplayLayout>
                </igtbl:UltraWebGrid> 
        </td>
    </tr>
    <tr>
        <td colspan="8" style="width: 100%">
        <div style="z-index:0">
            <asp:ImageButton ID="IB_ExportExcel" runat="server" ImageUrl="~/images/excel.gif" OnClick="IB_ExportExcel_Click" />
        </div> 
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="8">
        <div style="z-index:0">
               <igtblexp:ultrawebgridexcelexporter id="UltraWebGridExcelExporter1" runat="server"></igtblexp:ultrawebgridexcelexporter>
        </div> 
        </td>
    </tr>
    
    
    <%--<tr>
      <td colspan="6" width="100%">
        <table width="100%">
          <tr>
          <td style="width:14%"><asp:Label ID="lbl_no_of_complaints" runat="server" Text="Label"/></td>
          <td style="width:14%"><asp:Label ID="lbl_open" runat="server" Text="Label"/></td>
          <td style="width:14%"><asp:Label ID="lbl_Pending" runat="server" Text="Label"/></td>
          <td style="width:18%">
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;
            <asp:Label ID="lbl_Assigned" runat="server" Text="Label"/></td>
          <td style="width:14%">
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
            &nbsp; &nbsp;<asp:Label ID="lbl_in_progress" runat="server" Text="Label"/></td>
          <td style="width:14%">
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="lbl_closed" runat="server" Text="Label"/></td>
          <td style="width:14%">
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="lbl_Archieved" runat="server" Text="Label"/></td>
          </tr>
        </table>
      </td>
    </tr>--%>
</table>
<br />
