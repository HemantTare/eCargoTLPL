<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleInformation.ascx.cs" Inherits="Master_Vehicle_WucVehicleInformation" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx"TagName="WucAddress" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/wucTDSApp.ascx" TagName="WucTDSApp" TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<table class="TABLE" style="width: 100%">
    <tr><td colspan="6">&nbsp;</td> </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>  
        <td>      
            <asp:Panel ID="pnl_GeneralDetails" runat="server" CssClass="PANEL" GroupingText="General Details" meta:resourcekey="pnl_GeneralDetailsResource1">
            <table cellpadding="3" cellspacing="3" border="0" width="100%">
    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Vehicle_No"  runat="server" Text="Vehicle No :" meta:resourcekey="lbl_Vehicle_NoResource1"/>
        </td>
        <td style="width: 29%" >
            <table width="100%">
                <tr>
                    <td style="width: 25%">
                        <asp:TextBox ID="txt_Number_Part1" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="onlycharacters(this);Uppercase(this)"
                        MaxLength="3" Width="100%" meta:resourcekey="txt_Number_Part1Resource1" />
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txt_Number_Part2" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="Uppercase(this);valid(this)"
                            MaxLength="2" onkeypress = "return Only_Numbers(this,event)"  Width="100%" meta:resourcekey="txt_Number_Part2Resource1" />
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txt_Number_Part3" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="onlycharacters(this);Uppercase(this)"
                            onkeyup="onlycharacters(this)" MaxLength="2" Width="100%" meta:resourcekey="txt_Number_Part3Resource1" />
                    </td>
                    <td style="width: 25%">
                        <asp:TextBox ID="txt_Number_Part4" runat="server" CssClass="TEXTBOX" BorderWidth="1px" onblur="Uppercase(this);valid(this)"
                            MaxLength="4" onkeypress = "return Only_Numbers(this,event)"  Width="95%" meta:resourcekey="txt_Number_Part4Resource1" />
                        <input id="hdn_Vendor_Type" runat="server" type="hidden" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY">*</td>
        <td style="width: 50%" class="TD1" colspan="3"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        <asp:Label ID="lbl_Vehicle_Category_Head"  runat="server" Text="Vehicle Category :" meta:resourcekey="lbl_Vehicle_Category_HeadResource1"/>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_Vehicle_Category" CssClass="LABEL"  Font-Bold="True" Width="99%" BorderWidth="1px" runat="server" meta:resourcekey="lbl_Vehicle_CategoryResource1" />
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Vehicle_Type"  runat="server" Text="Vehicle Type:" meta:resourcekey="lbl_Vehicle_TypeResource1"/>
        </td>
        <td style="width: 29%" class="TD1">
            <asp:DropDownList ID="ddl_Vehicle_Type"  runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Vehicle_TypeResource1"/></td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY">
       <asp:Label ID="lbl_MandatoryVehicleType" Text="*" runat="server" meta:resourcekey="lbl_MandatoryVehicleTypeResource1"></asp:Label></td>
     </tr>
    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Vehicle_Body"  runat="server" Text="Vehicle Body :" meta:resourcekey="lbl_Vehicle_BodyResource1"/>
        </td>
        <td style="width: 29%" class="TD1">
            <asp:DropDownList ID="ddl_Vehicle_Body" Width="100%" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Vehicle_BodyResource1"/></td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY">
        <asp:Label ID="lbl_MandatoryVehicleBody" Text="*" runat="server" meta:resourcekey="lbl_MandatoryVehicleBodyResource1"></asp:Label></td>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Carrier_Category"  runat="server" Text="Carrier Category :" meta:resourcekey="lbl_Carrier_CategoryResource1"/>
        </td>
        <td style="width: 29%" class="TD1">
            <asp:DropDownList ID="ddl_Carrier_Category" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Carrier_CategoryResource1"/></td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY">
        <asp:Label  ID="lbl_MandatoryCarrierCategory" Text="*" runat="server" meta:resourcekey="lbl_MandatoryCarrierCategoryResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
        <asp:Label ID="lbl_Manufacturer"  runat="server" Text="Manufacturer :" meta:resourcekey="lbl_ManufacturerResource1"/>
        </td>
        <td style="width: 29%;" class="TD1">
            <asp:DropDownList ID="ddl_Manufacturer" Width="100%" OnSelectedIndexChanged="ddl_Manufacturer_SelectedIndexChanged"
                runat="server" CssClass="DROPDOWN" AutoPostBack="True" meta:resourcekey="ddl_ManufacturerResource1">
            </asp:DropDownList></td>
        <td style="width: 1%; text-align: left;" class="TDMANDATORY">
         <asp:Label ID="lbl_MandatoryManufacturer" Text="*" runat="server" meta:resourcekey="lbl_MandatoryManufacturerResource1"></asp:Label></td>
        <td style="width: 20%;" class="TD1">
        <asp:Label ID="lbl_Model"  runat="server" Text="Vehicle Model :" meta:resourcekey="lbl_ModelResource1"/>
        </td>
        <td style="width: 29%;" class="TD1">
            <asp:UpdatePanel ID="Upd_Pnl_Model" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Model" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ModelResource1" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Manufacturer" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%; text-align: left;" class="TDMANDATORY">
        <asp:label ID="lbl_MandatoryModal" Text="*" runat="server" meta:resourcekey="lbl_MandatoryModalResource1"></asp:label></td>
    </tr>
    <tr>
        <td style="width: 20%; height: 63px;" class="TD1">
        <asp:Label ID="lbl_Year_of_Manufacture"  runat="server" Text="Year of Manufacture :" meta:resourcekey="lbl_Year_of_ManufactureResource1"/>
        </td>
        <td style="width: 29%; height: 63px;" class="TD1">
            <asp:TextBox ID="txt_Yr_Manufacture" runat="server" onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)"
             MaxLength="4" CssClass="TEXTBOXNOS" meta:resourcekey="txt_Yr_ManufactureResource1" />
        </td>
        <td style="width: 1%; text-align: left; height: 63px;" class="TDMANDATORY"></td>
        <td style="width: 20%; height: 63px;" class="TD1">
        <asp:Label ID="lbl_GPS_Connectivity_ID"  runat="server" Text="GPS Connectivity ID :" meta:resourcekey="lbl_GPS_Connectivity_IDResource1"/>
        </td>
        <td style="width: 29%; height: 63px;" class="TD1">
            <asp:TextBox ID="txt_GPS_Con_Id" Width="96%" runat="server" MaxLength="25" CssClass="TEXTBOX" meta:resourcekey="txt_GPS_Con_IdResource1" />
        </td>
        <td style="width: 1%; text-align: left; height: 63px;" class="TDMANDATORY"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Driver_1"  runat="server" Text="Driver 1 :" meta:resourcekey="lbl_Driver_1Resource1"/>
        </td>
        <td style="width: 29%;text-align:left" class="TD1">
          <cc1:DDLSearch ID="ddl_Driver_1" runat="server" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName" IsCallBack="True" 
          CallBackAfter="2"  AllowNewText="True" InjectJSFunction="" PostBack="True" Text="" OnTxtChange="ddl_Driver_1_TxtChange"/>
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_DriverMobile1"  runat="server" Text="Driver Mobile 1:"/>
        </td>
        <td style="width: 29%" class="TD1">
            <asp:TextBox ID="txt_DriverMobile1" Width="96%" runat="server" MaxLength="25" CssClass="TEXTBOX" />
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY"></td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
        <asp:Label ID="lbl_Driver_2"  runat="server" Text="Driver 2 :" meta:resourcekey="lbl_Driver_2Resource1"/>
        </td>
        <td style="width: 29%;text-align:left" class="TD1">
          <cc1:DDLSearch ID="ddl_Driver_2" runat="server" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName" IsCallBack="True" CallBackAfter="2" meta:resourcekey="ddl_Driver_2Resource1" AllowNewText="True" InjectJSFunction="" PostBack="False" Text=""/>
        </td>
        <td style="width: 1%; text-align: left;" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_DriverMobile2"  runat="server" Text="Driver Mobile 2:"/>
        </td>
        <td style="width: 29%" class="TD1">
            <asp:TextBox ID="txt_DriverMobile2" Width="96%" runat="server" MaxLength="25" CssClass="TEXTBOX" />
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Cleaner"  runat="server" Text="Cleaner :" meta:resourcekey="lbl_CleanerResource1"/>
        </td>
        <td style="width: 29%" >
            <cc1:DDLSearch ID="ddl_Cleaner" runat="server" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchVehicleCleanerName" IsCallBack="True" meta:resourcekey="ddl_CleanerResource1" AllowNewText="True" CallBackAfter="2" InjectJSFunction="" PostBack="False" Text=""/>
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_OpeningOdometer" runat="server" CssClass="LABEL" Text="Opening Odometer :" meta:resourcekey="lbl_OpeningOdometerResource1"/></td>
        <td style="width: 29%" class="TD1">
            <asp:TextBox ID="txt_opening_Odometer"  Width="100%" onkeypress="return Only_Numbers(this,event)" onblur="CalculateOdometer()" BorderWidth="1px" runat="server"
                CssClass="TEXTBOXNOS" meta:resourcekey="txt_opening_OdometerResource1" />
        </td>
        <td style="width: 1%;"></td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_CurrentOdometer" runat="server" CssClass="LABEL" Text="Current Odometer :" meta:resourcekey="lbl_CurrentOdometerResource1"/></td>
        <td style="width: 29%;" class="TD1">
            <asp:Label ID="lbl_Current_Odometer" runat="server"  BorderWidth="1px" Width="96%" Font-Bold="True" CssClass="TEXTBOXNOS" meta:resourcekey="lbl_Current_OdometerResource1" />
        </td>
        <td style="width: 1%;"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_OnRoadDate" runat="server" CssClass="LABEL" Text="On Road Date :" meta:resourcekey="lbl_OnRoadDateResource1"></asp:Label></td>
        <td style="width: 29%; text-align:left">
            <uc2:wuc_Date_Picker ID="Wuc_DatePickerRoadDate" runat="server"></uc2:wuc_Date_Picker>
        </td>
        <td style="width: 1%;"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>
    </tr>   
     </table>
            </asp:Panel>
            </td>
           </tr>
           </table>           
        </td> 
    </tr>
    
    
    
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>  
                    <td>      
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_Broker" runat="server" CssClass="PANEL" GroupingText="Owner Details" meta:resourcekey="pnl_BrokerResource1">
                                <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                    <tr>
                                        <td style="width: 20%" class="TD1">
                                            <asp:Label ID="lbl_Vendor_Name" runat="server" CssClass="LABEL" Text="Broker Name :" meta:resourcekey="lbl_Vendor_NameResource1"></asp:Label></td>
                                        <td style="width: 29%" id="td_BrokerName" runat="server">
                                            <cc1:DDLSearch ID="ddl_Broker_Name" runat="server" CallBackAfter="2" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchVehicleOwnerName" 
                                            IsCallBack="True" OnTxtChange="ddl_Broker_Name_TxtChange" PostBack="True" AllowNewText="True" InjectJSFunction="" />  
                                            <asp:HiddenField ID="hdn_BrokerId" runat="server" />
                                        </td>
                                        <td style="width: 1%" class="TDMANDATORY">
                                         <asp:Label ID="lbl_MandatoryBroker" Text="*" runat="server" meta:resourcekey="lbl_MandatoryBrokerResource1"></asp:Label></td>

                                        <td style="width: 50%" class="TD1" colspan="3"></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Broker_Name" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td> 
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>  
        <td>      
            <asp:Panel ID="pnl_Owner" runat="server" CssClass="PANEL" GroupingText="Owner Details" meta:resourcekey="pnl_OwnerResource1">
            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_Owner_Name"  Width="100%" runat="server" CssClass="LABEL" Text="Owner Name :" meta:resourcekey="lbl_Owner_NameResource1"></asp:Label></td>
                    <td style="width: 29%" class="TD1">
                        <asp:TextBox ID="txt_Owner_Name" runat="server" CssClass="TEXTBOX" meta:resourcekey="txt_Owner_NameResource1" MaxLength="50" />
                    </td>
                    <td  style="width: 1%; text-align: left" class="TDMANDATORY">
                        <asp:Label ID="lbl_owner_name_mandotory" runat="server" CssClass="LABEL" Text="*" meta:resourcekey="lbl_owner_name_mandotoryResource1"></asp:Label>
                    </td>
                    <td style="width: 50%" class="TD1" colspan="3"></td>
                </tr>
                <tr>
                    <td class="TD1" colspan="6">
                        <uc1:WucAddress ID="WucAddress1" runat="server" />
                    </td>
                </tr>
            </table>
            </asp:Panel>
        </td> 
    </tr>
    </table>
    </td>
    </tr>   
    <tr id="tr_TDSDetails" runat="server">
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>  
        <td>   
    
        <asp:Panel ID="pnl_TDS" runat="server" CssClass="PANEL" GroupingText="TDS Details" meta:resourcekey="pnl_TDSResource1">
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <table cellpadding="5" cellspacing="5" border="0" width="100%">

        <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Select" runat="server" CssClass="LABEL" Text="TDS Certificate To :" meta:resourcekey="lbl_SelectResource1"></asp:Label></td>
        <td class="TD1" style="width: 29%;text-align:left">
        
            <asp:RadioButtonList ID="rbl_TDSCertificate" runat="server" Font-Size="11px" RepeatDirection="Horizontal"
                AutoPostBack="True" OnSelectedIndexChanged="rbl_TDSCertificate_SelectedIndexChanged" meta:resourcekey="rbl_TDSCertificateResource1">
                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="Broker"></asp:ListItem>
                <asp:ListItem Value="1" meta:resourcekey="ListItemResource2" Selected="True" Text="Owner"></asp:ListItem>
            </asp:RadioButtonList>
           
        </td>
        <td style="width: 1%; text-align: left" class="TDMANDATORY"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>
        </tr>
        
        <tr>
        <td style="width:100%" colspan="6">
        <uc3:WucTDSApp ID="WucTDSApp1" runat="server" />
        
        </td>
        
        </tr>
        
        </table>
        </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Broker_Name" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_TDSCertificate" />
                </Triggers>
            </asp:UpdatePanel> 
        </asp:Panel>
         
    </td>
    </tr>
    </table>
    </td>
    </tr>
     <tr>
        <td colspan="6" style="width: 100%" align="left">
         <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>  
        <td>    
            <asp:Panel ID="Panel1" runat="server" CssClass="PANEL" GroupingText="Notes" meta:resourcekey="pnl_NotesResource1" ScrollBars="Both">
            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                <tr>                
                <td class="TD1" style="width: 20%">        
              <asp:Label ID="lbl_Notes"  runat="server" Text="Notes :" meta:resourcekey="lbl_NotesResource1"/>
        </td>
        <td class="TD1" style="width: 79%;">
            <asp:TextBox ID="txt_Notes" CssClass="TEXTBOX" runat="server" TextMode="MultiLine" Height="60px" Width="750px" meta:resourcekey="txt_NotesResource1" />
        </td>
        <td class="TDMANDATORY" style="width: 1%;"></td>
       </tr>
       </table>
       </asp:Panel>
       </td>
       </tr>
       </table>
       </td>
       </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>

    <tr>
        <td colspan="6">
        <%--<asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                  <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"/>
             </ContentTemplate>
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
          </asp:UpdatePanel>--%> 
         <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"/>

        </td>
    </tr>
  <asp:HiddenField ID="hdf_ResourecString" runat="server" />
  <asp:HiddenField ID="hdn_Old_Curr_Odometer" runat="server" />
  <asp:HiddenField ID="hdn_Curr_Odometer" runat="server" />
  <asp:HiddenField ID="hdn_Old_Open_Odometer" runat="server" />
  <asp:HiddenField ID="hdn_Vehicle_Category_ID" runat="server" />
</table>
<script type="text/javascript">
//DisableControlOnChecked();
//Hide_Control();
</script>

<%--<tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_TDS_Applicable" Text="TDS Applicable(Yes/No) :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_TDS_ApplicableResource1" /></td>
        <td style="width: 29%">
           <asp:CheckBox ID="Chk_Is_Tds" CssClass="CHECKBOX" runat="server" AutoPostBack="True" OnCheckedChanged="Chk_Is_Tds_CheckedChanged" meta:resourcekey="Chk_Is_TdsResource1" />
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>
        </tr>
        <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Tds_Category" Text="TDS Category :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_CategoryResource1"></asp:Label>
        </td>
        <td style="width: 29%">
              <asp:DropDownList ID="ddl_Tds_Category" CssClass="DROPDOWN" runat="server" meta:resourcekey="ddl_Tds_CategoryResource1" />
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td style="width: 50%" class="TD1" colspan="3"></td>
        </tr>
        <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Tds_Rate_Percent" Text="TDS % :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_Rate_PercentResource1"></asp:Label>
        </td>
        <td style="width: 29%; text-align: left;">
            <asp:TextBox ID="txt_Tds_Rate_Percent" runat="server" CssClass="TEXTBOXNOS" 
            onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)" BorderWidth="1px" MaxLength="20" Width="99%" meta:resourcekey="txt_Tds_Rate_PercentResource1"/>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Tds_Exemption_Limit" Text="TDS Exemption :" runat="server" CssClass="LABEL" meta:resourcekey="lbl_Tds_Exemption_LimitResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Tds_Exemption_Limit" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                        MaxLength="20" Width="99%" onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)" meta:resourcekey="txt_Tds_Exemption_LimitResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%"></td>
        </tr>
--%>
