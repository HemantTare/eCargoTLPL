<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Complaint_Analysis.ascx.cs"
    Inherits="Complaint_Analysis_wuc_Complaint_Analysis" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="KeySortDropDownList" Namespace="KeySortDropDownList.Thycotic.Web.UI.WebControls"
    TagPrefix="cc1" %>

<script language="javascript" src="../../Javascript/Common.js" type="text/javascript"></script>
<script language="javascript" src="../../Javascript/ddlsearch.js" type="text/javascript"></script>

<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="SM_BTH" runat="server"></asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td colspan="6" class="TDGRADIENT">&nbsp;
            <asp:Label ID="lbl_Complaint_Analysis_Heading" runat="server" Text="COMPLAINT ANALYSIS" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">Ticket No:</td>
        <td style="width: 25%">
            <asp:Label ID="lbl_Ticket_No" runat="server" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="hdn_Ticket_Id" runat="server" />
        </td>
        <td style="width: 5%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_GC_Docket" runat="server"></asp:Label>
        </td>
        <td style="width: 25%">
            <asp:Label ID="lbl_GC_Docket_No" runat="server" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="hdn_GC_Docket_Id" runat="server" />
        </td>
        <td style="width: 5%">
        </td>
    </tr>
    <%--<tr>
        <td class="TD1" style="width: 20%">
            Reference No:</td>
        <td style="width: 25%">
            <asp:TextBox ID="txt_Reference_No" runat="server" BorderWidth="1px" MaxLength="20"
                CssClass="TEXTBOX"></asp:TextBox></td>
        <td style="width: 5%">
        </td>
        <td class="TD1" style="width: 20%">
            Voucher Date:</td>
        <td style="width: 25%">
            <ComponentArt:Calendar ID="Picker_Voucher_Date"   runat="server" CellPadding="2" ControlType="Picker"
                PickerCssClass="picker" PickerCustomFormat="dd MMMM yyyy" PickerFormat="Custom"
                SelectedDate="2005-12-13">
            </ComponentArt:Calendar>
        </td>
        <td style="width: 5%">
        </td>
    </tr>--%>
    <tr>
        <td colspan="6" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="5" style="width: 90%" align="right">
            <%--   <asp:Panel ID="pnl_Complaint_Analysis_Details" runat="server" GroupingText="Other Charges Details"
                CssClass="PANEL" Width="98%" HorizontalAlign="Right">--%>
            <%-- <div class="DIV" id="Div1" style="height: 150px; width: 100%; text-align: right">--%>
             <asp:UpdatePanel ID="upd_pnl_dg_Complaint_Analysis_Details" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Complaint_Analysis_Details" />
                    </Triggers>
                    <ContentTemplate>
            <asp:DataGrid ID="dg_Complaint_Analysis_Details" AutoGenerateColumns="False" ShowFooter="True"
                CellPadding="3" CssClass="GRID" Width="98%" runat="server" OnCancelCommand="dg_Complaint_Analysis_Details_CancelCommand"
                OnEditCommand="dg_Complaint_Analysis_Details_EditCommand" OnItemCommand="dg_Complaint_Analysis_Details_ItemCommand"
                OnItemDataBound="dg_Complaint_Analysis_Details_ItemDataBound" OnUpdateCommand="dg_Complaint_Analysis_Details_UpdateCommand"
                OnDeleteCommand="dg_Complaint_Analysis_Details_DeleteCommand" meta:resourcekey="dg_Complaint_Analysis_DetailsResource1">
                <ItemStyle HorizontalAlign="Left" />
                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                <FooterStyle CssClass="GridFooterCss" />
                        <HeaderStyle CssClass="GridHeaderCss" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Sr.No.">
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                        </ItemTemplate>
                        <HeaderStyle Width="5%" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Reason_Fault_ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Reason_Fault_ID" runat="server" Text='<%#Eval("Reason_Fault_ID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Reason For Fault" FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Left">
                        <FooterTemplate>
                            <asp:DropDownList ID="ddl_Resion_For_Fault" runat="server" CssClass="DROPDOWN">
                            </asp:DropDownList>
                        </FooterTemplate>
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Reason_For_Fault")%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddl_Resion_For_Fault" runat="server" CssClass="DROPDOWN">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="40%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn  ItemStyle-HorizontalAlign ="Center"  FooterStyle-HorizontalAlign="Center"  HeaderText="VA Booking" HeaderStyle-HorizontalAlign="Center"
                         >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_VA_Booking" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate  >
                            <asp:CheckBox ID="chk_VA_Booking" runat="server"  Enabled="false" Checked='<%#  Convert.ToBoolean(   DataBinder.Eval(Container.DataItem, "VA_Booking"))%>' />
                            
                            
                            
                           <%--<%# DataBinder.Eval(Container.DataItem, "VA_Booking")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_VA_Booking" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn  ItemStyle-HorizontalAlign ="Center" FooterStyle-HorizontalAlign="Center"  HeaderText="Booking Branch" HeaderStyle-HorizontalAlign="Center"
                         >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_Booking_Branch" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Booking_Branch" runat="server"  Enabled="false" Checked='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem, "Booking_Branch"))%>' />
                                <%--<%# DataBinder.Eval(Container.DataItem, "Booking_Branch")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_Booking_Branch" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Booking Hub"  FooterStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign ="Center" HeaderStyle-HorizontalAlign="Center"
                         >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_Booking_Hub" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Booking_Hub" runat="server"  Enabled="false" Checked='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem, "Booking_Hub"))%>' />
                            <%--<%# DataBinder.Eval(Container.DataItem, "Booking_Hub")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_Booking_Hub" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Delivery Hub" FooterStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center"
                         ItemStyle-HorizontalAlign ="Center" >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_Delivery_Hub" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Delivery_Hub" runat="server"  Enabled="false" Checked='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem, "Delivery_Hub"))%>' />
                           <%-- <%# DataBinder.Eval(Container.DataItem, "Delivery_Hub")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_Delivery_Hub" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Delivery Branch" FooterStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center"
                         ItemStyle-HorizontalAlign ="Center" >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_Delivery_Branch" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Delivery_Branch"  Enabled="false" runat="server" Checked='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem, "Delivery_Branch"))%>' />
                            <%--<%# DataBinder.Eval(Container.DataItem, "Delivery_Branch")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_Delivery_Branch" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="VA Delivery"  FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                         ItemStyle-HorizontalAlign ="Center" >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_VA_Delivery" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                           <asp:CheckBox ID="chk_VA_Delivery" Enabled="false"  runat="server" Checked='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem, "VA_Delivery"))%>' />
                            <%--<%# DataBinder.Eval(Container.DataItem, "VA_Delivery")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_VA_Delivery" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText="Customer"  FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                         ItemStyle-HorizontalAlign ="Center" >
                        <FooterTemplate>
                            <asp:CheckBox ID="chk_Customer" runat="server" />
                        </FooterTemplate>
                        <ItemTemplate>
                           <asp:CheckBox ID="chk_Customer" Enabled="false"  runat="server" Checked='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem, "Customer"))%>' />
                            <%--<%# DataBinder.Eval(Container.DataItem, "Customer")%>--%>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chk_Customer" runat="server" />
                        </EditItemTemplate>
                        <HeaderStyle Width="15%" />
                    </asp:TemplateColumn>
                    
                    <asp:EditCommandColumn UpdateText="Update" HeaderStyle-HorizontalAlign="Center" CancelText="Cancel"
                        EditText="Edit" HeaderText="Edit" meta:resourcekey="EditCommandColumnResource1">
                        <HeaderStyle Width="10%" />
                    </asp:EditCommandColumn>
                    <asp:TemplateColumn HeaderText="Delete" FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                meta:resourcekey="lbtn_DeleteResource1" />
                        </ItemTemplate>
                        <HeaderStyle Width="5%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            </ContentTemplate>
                </asp:UpdatePanel>
            <%-- </div>--%>
            <%-- </asp:Panel>--%>
        </td>
        <td style="width: 5%">
        </td>
    </tr>     
  
    <tr>
        <td colspan="6" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; vertical-align: top;">Person Responsible:</td>
        <td colspan="4" class="TDMANDATORY">
            <asp:TextBox ID="txt_Person_Responsible" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Width="80%"  MaxLength="100"></asp:TextBox>  &nbsp;*</td>
        <td style="width: 5%">
        </td>
    </tr>
    
     <tr>
        <td class="TD1" style="width: 20%; vertical-align: top;">Action Taken:</td>
        <td colspan="4" class="TDMANDATORY">
            <asp:TextBox ID="txt_Action_Taken" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                Height="60px" Width="80%" TextMode="MultiLine" MaxLength="200"></asp:TextBox>&nbsp;*</td>
        <td style="width: 1%;" class="TDMANDATORY"></td>
    </tr>
   
     
    <tr>
        <td colspan="6" class="TD1" style="width: 20%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                     <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Visible="true"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Complaint_Analysis_Details" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" ValidationGroup="Save" OnClick="btn_Save_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="6">         
            <asp:Label ID="Label1" CssClass="LABELERROR" runat="server" Text=" Fields with * mark are mandatory"></asp:Label>                 
        </td>
    </tr>
</table>

 

 

 
