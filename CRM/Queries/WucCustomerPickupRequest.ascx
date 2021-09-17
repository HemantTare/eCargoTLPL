<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucCustomerPickupRequest.ascx.vb" Inherits="CRM_Queries_WucCustomerPickupRequest" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src= "../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/CRM/Queries/PickupRequest.js"></script>
    
<asp:ScriptManager ID="scm_pickup" runat="server" /> 
    
<table class="TABLE"> 
<tr>
    <td class="TDGRADIENT" colspan="6" >&nbsp;
        <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="PICKUP REQUEST"></asp:Label>
    </td>
</tr>
<tr><td colspan="6">&nbsp;</td></tr>  
<tr>
    <td colspan="6">
        <asp:Panel ID="pnl_pickuprequest" Width="100%" runat="Server" GroupingText="PickUp Request">           
       
            <table width="100%">   
             <tr>
                    <td class="TD1" style="width:20%;">PickUp No:</td>
                    <td style="width:29%;">
                        <asp:Label ID="lbl_Pickup_No" CssClass="LABEL" Font-Bold="true" runat="server"/>
                    </td>
                    <td class="TDMANDATORY" style="width:1%;"></td>  
                    <td class="TD1" style="width:20%;"></td>
                    <td class="TD1" style="width:29%;"></td>
                    <td class="TDMANDATORY" style="width:1%;"></td>                              
                </tr>                            
                <tr>
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Origin" Text="PickUp Point:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_Orgin" CssClass="TEXTBOX" onblur="Uppercase(this)" BorderWidth="1px" MaxLength="100" runat="server"/>
                    </td>
                     <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Origin_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>     
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Destination" Text="Destination:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_Destination" CssClass="TEXTBOX" onblur="Uppercase(this)" BorderWidth="1px" MaxLength="100" runat="server"/>
                    </td>
                    <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Destination_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                                
                </tr>                           
                <tr>
                     <td class="TD1" style="width:20%;vertical-align:middle">
                       <asp:Label ID="lbl_PickUpDate" Text="Pickup Date:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td style="width:29%;">
                        <uc1:wuc_Date_Picker ID="dtp_PickUpDate" runat="server" /></td>
                     <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Pickupdate_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>     
                     <td class="TD1" style="width:20%;vertical-align:middle">
                       <asp:Label ID="lbl_PickUptime" Text="Pickup Time:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td style="width:29%;">
                        <uc2:TimePicker ID="tp_PickUPTime" runat="server" /></td>
                     <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_PickUptime_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                                
                </tr>
                <tr>
                     <td class="TD1" style="width:20%; height: 24px;">
                       <asp:Label ID="lbl_Weight" Text="Weight(in Kg.)" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%; height: 24px;">
                        <asp:TextBox ID="txt_Weight" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" BorderWidth="1px" MaxLength="6" runat="server"/>
                    </td>
                      <td class="TDMANDATORY" style="width:1%; height: 24px;">
                       <asp:Label ID="lbl_Weight_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>   
                    <td class="TD1" style="width:20%; height: 24px;">
                       <asp:Label ID="lbl_Pkgs" Text="Pkgs:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%; height: 24px;">
                        <asp:TextBox ID="txt_Pkgs" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)" BorderWidth="1px" MaxLength="10" runat="server"/>
                    </td>
                     <td class="TDMANDATORY" style="width:1%; height: 24px;">
                       <asp:Label ID="lbl_Pkgs_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                             
                </tr>
                 <tr>                               
                    <td class="TD1" style="width:20%;">
                        <asp:Label ID="lbl_BookingLabel" runat="server" Text="Booking Type:" CssClass="LABEL"></asp:Label>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:DropDownList ID="ddl_BookingTypeMode" CssClass="DROPDOWN" runat="server"/>
                    </td>
                     <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Booking_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>   
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Packing_Type" Text="Packing Type:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:DropDownList ID="ddl_PackingType" CssClass="DROPDOWN" runat="server"/>
                    </td>
                   <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Packing_Type_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                         
                </tr>
                <tr>
                     <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Consignor_Name" Text="Name Of Consignor:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_Consignor" CssClass="TEXTBOX" onblur="Uppercase(this)" BorderWidth="1px" MaxLength="100" runat="server"/>
                    </td>
                      <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Consignor_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>   
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Commodity" Text="Commodity:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:DropDownList ID="ddl_CommodityType" onchange = "DDL_Commodity_Type_change()" CssClass="DROPDOWN" runat="server"/>
                    </td>
                     <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Commodity_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>   
                </tr>
                <tr>
                    <td class="TD1" style="width:20%;">Contact Name:</td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_ContactName" onblur="Uppercase(this)" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="100" runat="server"/>
                    </td>
                    <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Contact_Name_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>   
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Mobile_No" Text="Mobile No:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_MobileNo"  onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="10" runat="server"/>
                    </td>
                     <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Mobile_No_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                             
                </tr>
                <tr>
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Pickup_Add" Text="Pickup Address:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" colspan="4" style="width:79%;">
                        <asp:TextBox ID="txt_Address" Height="40px" CssClass="TEXTBOX" Width="99%" BorderWidth="1px" TextMode="MultiLine" MaxLength="300" runat="server"/>
                    </td>                               
                    <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Pickup_Add_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                          
                </tr>
                <tr>
                     <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Tel_No" Text="TelePhone No:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_TelephoneNo" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="25" runat="server"/>
                    </td>
                      <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Tel_No_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>     
                     <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_Email_Id" Text="Email Id:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_EmailId" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" runat="server"/>
                    </td>
                    <td class="TDMANDATORY" style="width:1%;">
                       <asp:Label ID="lbl_Email_Mandatory" Text="*" CssClass="LABEL" runat="server"/>
                    </td>                          
                </tr>
                 <tr>
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_City" Text="City:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_City" CssClass="TEXTBOX" onblur="Uppercase(this)" BorderWidth="1px" MaxLength="50" runat="server"/>
                    </td>
                    <td class="TD1" style="width:1%;"></td>  
                    <td class="TD1" style="width:20%;">
                       <asp:Label ID="lbl_State" Text="State:" CssClass="LABEL" runat="server"/>
                    </td>
                    <td class="TD1" style="width:29%;">
                        <asp:TextBox ID="txt_State" CssClass="TEXTBOX" onblur="Uppercase(this)" BorderWidth="1px" MaxLength="50" runat="server"/>
                    </td>
                    <td class="TD1" style="width:1%;"></td>                           
                </tr>
              </table>
        </asp:Panel>
    </td>
</tr>
<tr>
    <td colspan="6">
        <asp:Panel ID="pnl_ForwardingDetails" Width="100%" runat="Server" GroupingText="Forwarding Details">           
       
            <table width="100%">                            
                <tr>
                    <td class="TD1" style="width:20%;">Branch:</td>
                    <td style="width:29%;">
                        <cc1:DDLSearch id="ddl_Branch"  runat="server" PostBack="true" DBTableName="EC_Master_Branch" IsCallBack="true" CallBackFunction="Raj.EC.CRM.CallBack.GetSearchBranchCRMQueries" OtherColumns="PickupRequest"></cc1:DDLSearch>
                    </td>
                    <td class="TD1" style="width:1%;"></td>  
                    <td class="TD1" colspan="3" style="width:50%;"></td>                               
                </tr>
                <tr id="Tr1" runat="server" visible="false">
                    <td class="TD1" style="width:20%; vertical-align: middle;">Date:</td>
                    <td style="width:29%;"><uc1:wuc_Date_Picker ID="dtp_ForwardDate" runat="server" /></td>
                    <td class="TD1" style="width:1%;"></td>                              
                    <td class="TD1" style="width:20%;vertical-align:middle">Time:</td>
                    <td style="width:29%;"><uc2:TimePicker ID="tp_ForwardTime" runat="server" /></td>
                    <td class="TD1" style="width:1%;"></td>  
                </tr>                           
                <tr>
                    <td class="TD1" style="width:20%;">Forwarded To Name Of Staff/VA:</td>
                    <td class="TD1" style="width:29%;">
                    <asp:UpdatePanel ID="upnl_Staff" runat="server" UpdateMode="Conditional">
                    <ContentTemplate >
                           <asp:DropDownList ID="ddl_StaffVA" AutoPostBack="true" CssClass="DROPDOWN" runat="server" OnSelectedIndexChanged="ddl_StaffVA_SelectedIndexChanged"/>
                    </ContentTemplate>
                    <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="ddl_Branch" />
                    </Triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td class="TD1" style="width:1%;"></td>  
                    <td class="TD1"  style="width:20%;">Mobile No:
                    </td>
                    <td style="width:29%;">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate >
                       <asp:Label ID="lbl_MobileNo" runat="server" Font-Bold="true"></asp:Label>
                         </ContentTemplate>
                    <Triggers >
                    <asp:AsyncPostBackTrigger ControlID="ddl_StaffVA" />
                    </Triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td style="width:1%;">
                       &nbsp;
                    </td>                               
                </tr>
                <tr><td colspan="6">&nbsp;</td></tr>   
              </table>
        </asp:Panel>
    </td>
</tr>

<tr runat="server" id="tr_Manual_Closing">
    <td colspan="6">
        <asp:Panel ID="pnl_Closing" Width="100%" runat="Server" GroupingText="Manual Closing">           
       
            <table width="100%">                            
                <tr>
                    <td class="TD1" style="width:20%; height: 22px;">Close:</td>
                    <td style="width:29%; height: 22px;">
                        <asp:CheckBox id="chk_Close" OnClick="Check_Click();" runat="server"></asp:CheckBox>
                    </td>
                    <td class="TD1" style="width:1%; height: 22px;"></td>  
                    <td class="TD1" colspan="3" style="width:50%; height: 22px;"></td>                               
                </tr>                                                    
                <tr id="tr_GC" runat="server">
                    <td class="TD1" style="width:20%;">
                        <asp:Label ID="lbl_GCText" runat="server" CssClass="LABEL"></asp:Label></td>
                    <td style="width:29%;">
                        <cc1:DDLSearch id="ddl_GC_No" CallBackAfter="4" runat="server" IsCallBack="true" CallBackFunction="Raj.EC.CRM.CallBack.GetSearchPickupGcDoc"></cc1:DDLSearch>
                    </td>
                    <td class="TD1" style="width:1%;"></td>
                    <td class="TD1" colspan="3" style="width:50%;"></td>
                </tr>
                <tr id="tr_Reason" runat="server">
                    <td class="TD1" style="width:20%; height: 21px;">Reason:</td>
                    <td style="width:79%; height: 21px;" colspan="4">
                        <asp:TextBox id="txt_Reason" runat="server" Height="40px" Width="99%" TextMode="multiLine" CssClass="TEXTBOX" MaxLength="250" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td class="TD1" style="width:1%; height: 21px;"></td>  
                </tr>
                <tr><td colspan="6">&nbsp;</td></tr>
              </table>
        </asp:Panel>
    </td>
</tr>
<tr>
    <td colspan="6" align="center" style="vertical-align:middle">
        <asp:Button ID="btn_Save" OnClientClick="return AllowToSave()" runat="server" Text="Save" CssClass="BUTTON" />
    </td>
</tr>
<tr><td colspan="6">
    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
</td></tr>
<tr><td colspan="6">&nbsp;</td></tr>
<tr><td colspan="6">
    <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>
</td></tr>
</table>
        
<script type="text/javascript">    
Check_Click();    
</script>