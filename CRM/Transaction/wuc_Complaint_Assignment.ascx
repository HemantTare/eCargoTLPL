<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Complaint_Assignment.ascx.cs"
    Inherits="CRM_wuc_Complaint_Assignment" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebGrid.v7.1, Version=7.1.20071.40, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="KeySortDropDownList" Namespace="KeySortDropDownList.Thycotic.Web.UI.WebControls" TagPrefix="cc1" %>
<script language="javascript" src="../../Javascript/ddlsearch.js" type="text/javascript"></script>

<script language="javascript" type="text/javascript">
function selectAll()
{
    var chk=document.getElementById("chkSelectAll");
    var oGrid=igtbl_getGridById("<%= dg_AssignUser.ClientID %>");
    
    var chked=chk.checked;
    var i=0

	    for (i = 0; i < oGrid.Rows.length; i++)
	    {
		       oGrid.Rows.getRow(i).getCell(0).setValue(chk.checked);
	    }		
}

function OpenWindow(url)
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-400);
    var popH = (h-400);
//    var leftPos = (w-popW)/2;
//    var topPos = (h-popH)/2;
    
    var leftPos =100;
    var topPos = 100;
                
    window.open(url, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;
}

</script>


<asp:ScriptManager ID="SM_Complaint_Assignment" runat="server"></asp:ScriptManager>
<table class="TABLE" width="100%" border="0">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Complaint_Assignment_Heading" CssClass="HEADINGLABEL" runat="server" Text="COMPLAINT ASSIGNMENT"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    
     <tr>
        <td class="TD1" style="width: 20%;">Ticket No :
        </td>
        <td style="width: 29%;"><asp:Label ID="lbl_TicketNo" runat="server" Font-Bold="true"></asp:Label>
        </td>
        
        <td class="TDMANDATORY" style="width: 1%;"></td>
        
         <td class="TD1" style="width: 20%;">
           <asp:Label ID="lbl_GcNoCaption" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;"><asp:Label ID="lbl_GcNo" runat="server" Font-Bold="true"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
      </tr>  
    
    <tr>
        <td class="TD1" style="width: 20%;">Search By :</td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_SearchBy" OnSelectedIndexChanged="ddl_SearchBy_SelectedIndexChanged"
                runat="server" CssClass="DROPDOWN" AutoPostBack="true">
                <asp:ListItem Text="User Name" Value="U"></asp:ListItem>
                <asp:ListItem Text="Employee Name" Value="E"></asp:ListItem>
                <asp:ListItem Text="Profile" Value="P"></asp:ListItem>
                <asp:ListItem Text="HO User" Value="H"></asp:ListItem>
                <asp:ListItem Text="Region Name" Value="R"></asp:ListItem>
                <asp:ListItem Text="Area Name" Value="A"></asp:ListItem>
                <asp:ListItem Text="Branch Name" Value="B"></asp:ListItem>
            </asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>        
        <td colspan="3" style="width: 50%;">&nbsp;</td>
      </tr>
    <tr>
    <td colspan="6">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
         <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_SearchBy" />
        </Triggers>
        <ContentTemplate>
          <table border="0">
    <tr runat="server" id="tr_FilterBy1">
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_CaptionFilterBy1" runat="server"></asp:Label></td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_FilterBy1"  runat="server" CssClass="DROPDOWN" ></asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>
         <td colspan="3" style="width: 50%;">&nbsp;</td>
    </tr>    
    <tr runat="server" id="tr_FilterBy2">
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_CaptionFilterBy2" runat="server"></asp:Label></td>
        <td style="width: 29%;" align="left">
           <cc1:DDLSearch ID="ddl_FilterBy2" runat="server" AllowNewText="false" CallBackFunction="Raj.EC.CRM.CallBack.GetSearchUser" IsCallBack="true"  /></td>
        <td class="TDMANDATORY" style="width: 1%;">*</td>
        
        <td colspan="3" style="width: 50%;">&nbsp;</td>
    </tr>
    </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    </td>
    </tr>
    
    <tr>
    <td style="width: 20%">&nbsp;</td>
    
        <td style="width: 29%" align="left">
            <asp:Button ID="btn_Add" runat="server" Text="Add In Grid"
                CssClass="BUTTON" OnClick="btn_Add_Click" /></td>
                
       <td colspan="3" style="width: 50%;">
        
           <asp:Button ID="btn_Attachments" runat="server" Text="Attachment"
                CssClass="BUTTON"/></td>
    </tr>
    <tr>
    <td>
    &nbsp;
    </td>
    </tr>
    
    <tr>
        <td style="width: 20%; vertical-align: top;" class="TD1">
            Select User:</td>
        <td colspan="5" style="width: 80%;">
 
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Add" />
                </Triggers>
                <ContentTemplate>
                    <igtbl:UltraWebGrid  ID="dg_AssignUser" runat="server" TabIndex="3" Height="100px" Width="780">
                        <Bands>
                            <igtbl:UltraGridBand AddButtonCaption="Orders" BaseTableName="Orders" Key="Orders">
                                <Columns>
                                    <igtbl:UltraGridColumn  Key="" Width="25px" Type="CheckBox"  BaseColumnName="" AllowUpdate="Yes" SortIndicator="Disabled">
                                        <Footer Key="">
                                        </Footer>
                                        <Header Key=""  Caption = "<input type='checkbox' id='chkSelectAll' onClick='selectAll()' name='chkSelect'>">
                                        </Header>
                                    </igtbl:UltraGridColumn>
                                </Columns>
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:CheckBox ID="CheckBox1" Text="Select/DeSelect" runat="server" onclick="CheckAllDataGridCheckBoxes(this,'Chk_All_User_List')" />--%>
        </td>
    </tr>
    
    
    
    <tr>
        <td colspan="6" style="width: 25%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>                 
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" EnableViewState="false"></asp:Label>&nbsp;
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Add" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save"
                CssClass="BUTTON" OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
    <td>
    &nbsp;
    </td>
    </tr>
    
</table>