<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDisplayInfo.ascx.cs" Inherits="CRM_Transaction_WucTicketInfo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
 <script language="javascript" type="text/javascript" >
    function OpenWindow(url)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.location.href=url;
        return false;
    }
</script>

<table class="TABLE" style="width: 100%">

 <tr>
       <td class="TDGRADIENT" colspan="6">
       <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="DETAILS"
                ></asp:Label>
      </td>
</tr>

<tr>
<td>
  &nbsp;
</td>
</tr>
<tr>
<td colspan="6">
  
                    <igtbl:UltraWebGrid ID="dg_DisplayInfo" runat="server" TabIndex="3" Height="100px" Width="780" OnInitializeRow="dg_DisplayInfo_InitializeRow">
                        <Bands>
                            <igtbl:UltraGridBand AddButtonCaption="Orders" BaseTableName="Orders" Key="Orders">
                                <%--<Columns>
                                    <igtbl:UltraGridColumn Key="" Width="25px" Type="CheckBox"  BaseColumnName="" AllowUpdate="Yes">
                                        <Footer Key="">
                                        </Footer>
                                        <Header Key="">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                </Columns>--%>
                            </igtbl:UltraGridBand>
                        </Bands>
                        <DisplayLayout ViewType="OutlookGroupBy" Version="4.00" AllowSortingDefault="OnClient"
                            StationaryMargins="Header" AllowColSizingDefault="Free" AllowUpdateDefault="No"
                            StationaryMarginsOutlookGroupBy="True" HeaderClickActionDefault="SortMulti" Name="ctl00xUltraWebGrid1"
                            BorderCollapseDefault="Separate" AllowDeleteDefault="No" RowSelectorsDefault="No"
                            TableLayout="Fixed" RowHeightDefault="20px" AllowColumnMovingDefault="OnServer"
                            SelectTypeRowDefault="Extended">
                            
                            <GroupByBox>
                                <Style BorderColor="Window" BackColor="ActiveBorder"></Style>
                            </GroupByBox>
                            <GroupByRowStyleDefault BorderColor="Window" BackColor="Control">
                            </GroupByRowStyleDefault>
                            <ActivationObject BorderWidth="" BorderColor="">
                            </ActivationObject>
                            <FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            </FooterStyleDefault>
                            <RowStyleDefault BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="8.25pt"
                                Font-Names="Microsoft Sans Serif" BackColor="Window">
                                <BorderDetails ColorTop="Window" ColorLeft="Window"></BorderDetails>
                                <Padding Left="3px"></Padding>
                            </RowStyleDefault>
                            <FilterOptionsDefault>
                                <FilterOperandDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid"
                                    Font-Size="11px" Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White"
                                    CustomRules="overflow:auto;">
                                    <Padding Left="2px"></Padding>
                                </FilterOperandDropDownStyle>
                                <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                                </FilterHighlightRowStyle>
                                <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="White" Width="200px"
                                    Height="300px" CustomRules="overflow:auto;">
                                    <Padding Left="2px"></Padding>
                                </FilterDropDownStyle>
                            </FilterOptionsDefault>
                            <HeaderStyleDefault HorizontalAlign="Left" BorderStyle="Solid" BackColor="LightGray">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            </HeaderStyleDefault>
                            <EditCellStyleDefault BorderWidth="0px" BorderStyle="None">
                            </EditCellStyleDefault>
                            <FrameStyle BorderWidth="1px" BorderColor="InactiveCaption" BorderStyle="Solid" Font-Size="8.25pt"
                                Font-Names="Microsoft Sans Serif" BackColor="Window" Width="325px" Height="200px">
                            </FrameStyle>
                            <Pager MinimumPagesForDisplay="2">
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">
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

<tr><td>&nbsp;</td></tr>
</table>