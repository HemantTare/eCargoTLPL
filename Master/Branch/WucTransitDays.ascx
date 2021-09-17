<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTransitDays.ascx.cs" Inherits="Master_Branch_WucTransitDays" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
<asp:ScriptManager ID="scm_TransitDays" runat="server"></asp:ScriptManager>

<script type="text/javascript">

function NewWindow()
    {
        
       var Path='FrmTransitDaysStateToState.aspx';
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-20);
        var popH = (h-250);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
          window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }

</script>


<table style="width: 100%" class="TABLE">
        <tr>
        <td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="TRANSIT DAYS (CITY TO CITY)" meta:resourcekey="lbl_HeadingResource1" ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromCity" runat="server" Text="From City:" CssClass="LABEL" meta:resourcekey="lbl_FromCityResource1"></asp:Label></td>
        <td style="width: 28%">
               <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_TransitDays" />                
            </Triggers>
            <ContentTemplate>
            <asp:DropDownList ID="ddl_City" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_City_SelectedIndexChanged" meta:resourcekey="ddl_CityResource1">
            </asp:DropDownList>
            </ContentTemplate>
            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToState" runat="server" Text="To State:" CssClass="LABEL" meta:resourcekey="lbl_ToStateResource1"></asp:Label></td>
        <td style="width: 28%">
               <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_TransitDays" />
                
            </Triggers>
            <ContentTemplate>
            <asp:DropDownList ID="ddl_State" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_State_SelectedIndexChanged" meta:resourcekey="ddl_StateResource1">
            </asp:DropDownList>
            </ContentTemplate>
            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%">
            <asp:Button ID="btn_Copy" runat="server" CssClass="BUTTON" OnClick="btn_Copy_Click" OnClientClick="return NewWindow()"
                Text="Copy" meta:resourcekey="btn_CopyResource1" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; ">
            <asp:Label ID="lbl_Vehicle" runat="server" Text="Vehicle Type:" CssClass="LABEL" meta:resourcekey="lbl_VehicleResource1"></asp:Label></td>
        <td style="width: 28%;">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_TransitDays" />                
            </Triggers>
            <ContentTemplate>
            <asp:DropDownList ID="ddl_VehicleType" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_VehicleType_SelectedIndexChanged" meta:resourcekey="ddl_VehicleTypeResource1">
            </asp:DropDownList>
            </ContentTemplate>
            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%; ">
        </td>
        <td style="width: 20%;" class="TD1">
        </td>
        <td style="width: 28%;">
        </td>
        <td style="width: 2%; ">
        </td>
    </tr>
    <tr>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="Upd_Pnl_dg_TransitDays" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType" />
                <asp:AsyncPostBackTrigger ControlID="ddl_State" />
                <asp:AsyncPostBackTrigger ControlID="ddl_City" />
            </Triggers>
            <ContentTemplate>
                
           
        <asp:DataGrid ID="dg_TransitDays" runat="server" AutoGenerateColumns="False" CssClass="Grid" AllowPaging="True" AllowSorting="True" Width="100%" CellPadding="2" PageSize="15" OnCancelCommand="dg_TransitDays_CancelCommand" OnEditCommand="dg_TransitDays_EditCommand" OnItemDataBound="dg_TransitDays_ItemDataBound" OnPageIndexChanged="dg_TransitDays_PageIndexChanged" OnUpdateCommand="dg_TransitDays_UpdateCommand" meta:resourcekey="dg_TransitDaysResource1">
                <FooterStyle CssClass="GridFooterCss" />
                <HeaderStyle CssClass="GRIDHEADERCSS" />
                <Columns>
                   
                    <asp:BoundColumn DataField ="From_City_Id" >
                        <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn >
                    <ItemTemplate>
                           <asp:Label ID="lbl_TransitDaysId" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Transit_Days_ID") %>' meta:resourcekey="lbl_TransitDaysIdResource1"></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                            <asp:Label ID="lbl_TransitDaysId" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Transit_Days_ID") %>' meta:resourcekey="lbl_TransitDaysIdResource2"></asp:Label>
                     </EditItemTemplate>
                     <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn >
                    <ItemTemplate>
                           <asp:Label ID="lbl_ToCityId" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "To_City_Id") %>' meta:resourcekey="lbl_ToCityIdResource1"></asp:Label>
                     </ItemTemplate>
                     <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:TemplateColumn>
                                                     
                    <asp:TemplateColumn HeaderText="To City">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "City_Name") %>
                     </ItemTemplate>
                    </asp:TemplateColumn>
                                      
                    <asp:TemplateColumn HeaderText="Transit Day(s)">
                        <ItemTemplate>
                          <asp:Label ID="lbl_transit_day" runat="server" Text ='<%# DataBinder.Eval(Container.DataItem, "Transit_Days") %>' Font-Names="Verdana" meta:resourcekey="lbl_transit_dayResource1"  ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_transit_day" runat="server"  
                            onkeypress="return Only_Integers(this,event)"  onblur="return valid(this)" Text ='<%# DataBinder.Eval(Container.DataItem, "Transit_Days") %>' Font-Names="Verdana" BorderWidth="1px"  CssClass="TEXTBOXNOS"   MaxLength="4" meta:resourcekey="txt_transit_dayResource1"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Distance (in Kms)">
                        <ItemTemplate>
                        <asp:Label ID="lbl_distance" runat="server" Text ='<%# DataBinder.Eval(Container.DataItem, "Distance_In_Km") %>' Font-Names="Verdana" meta:resourcekey="lbl_distanceResource1"  ></asp:Label>                           
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_distance" runat="server"  
                            onkeypress="return Only_Integers(this,event)" onblur="return valid(this)" Text ='<%# DataBinder.Eval(Container.DataItem,"Distance_In_Km") %>' Font-Names="Verdana" BorderWidth="1px"  CssClass="TEXTBOXNOS"   MaxLength="4" meta:resourcekey="txt_distanceResource1"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                       
                    <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="Edit" meta:resourcekey="EditCommandColumnResource1" ></asp:EditCommandColumn>
                           
                    <asp:TemplateColumn HeaderText="View" Visible="False">
                        <ItemTemplate>
                          <asp:LinkButton ID="lnk_btn_view" runat="server" meta:resourcekey="lnk_btn_viewResource1">View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle Mode="NumericPages" />
            </asp:DataGrid>
             </ContentTemplate>
        </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6"
             style="font-weight: bold; font-size: 11px; font-family: Verdana">
            
            <asp:Label ID="lbl_Updated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="UPDATEDLBL" Width="50px" meta:resourcekey="lbl_UpdatedResource1"></asp:Label>&nbsp; Updated &nbsp;
            <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="NOTUPDATEDLBL" Width="50px" meta:resourcekey="lbl_NotUpdatedResource1"></asp:Label>&nbsp; Not Updated
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="6">
        <asp:UpdatePanel ID="up_error" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            </ContentTemplate>
    </asp:UpdatePanel>
            </td>
    
    </tr>
</table>
<table border="1" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td class="TDTRANSIT" style="width: 25%">
            ND - Next Day
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            2 - One Day Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            3 - Two Days Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            4 - Three Days Transit
        </td>
    </tr>
    <tr>
        <td class="TDTRANSIT" style="width: 25%">
            5 - Four Days Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            6 - Five Days Transit
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            NC - No Commitment
        </td>
        <td class="TDTRANSIT" style="width: 25%">
            0 - Source to Source
        </td>
    </tr>
</table>
