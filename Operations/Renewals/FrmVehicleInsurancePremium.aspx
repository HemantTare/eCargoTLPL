<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleInsurancePremium.aspx.cs" Inherits="Operations_Renewals_FrmVehicleInsurancePremium" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Transactions/Renewals/VehicleInsurancePremium.js"></script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Vehicle Insurance Premium</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript" language="javascript">
function Calculate_TPP_FPP()
{
    var  txt_FPPremium = document.getElementById('<%=txt_FPPremium.ClientID%>');
    var  txt_TPPremium = document.getElementById('<%=txt_TPPremium.ClientID%>');
    var  txt_LoadingTPP = document.getElementById('<%=txt_LoadingTPP.ClientID%>');
    var  txt_LoadingFPP = document.getElementById('<%=txt_LoadingFPP.ClientID%>');
    var  txt_NCBFPP = document.getElementById('<%=txt_NCBFPP.ClientID%>');
    var  lbl_TPPAmount = document.getElementById('<%=lbl_TPPAmount.ClientID%>');
    var  lbl_FPPAmount = document.getElementById('<%=lbl_FPPAmount.ClientID%>');
    var  lbl_NCBAmount = document.getElementById('<%=lbl_NCBAmount.ClientID%>');
    
    var  hdn_LoadingTPP = document.getElementById('<%=hdn_LoadingTPP.ClientID%>');
    var  hdn_LoadingFPP = document.getElementById('<%=hdn_LoadingFPP.ClientID%>');
    var  hdn_NCBFPP = document.getElementById('<%=hdn_NCBFPP.ClientID%>');

    var FPPremium = parseFloat(txt_FPPremium.value);
    var TPPremium = parseFloat(txt_TPPremium.value);
    var LoadingPercentTPP = parseFloat(txt_LoadingTPP.value);
    var LoadingPercentFPP = parseFloat(txt_LoadingFPP.value);
    var NCBPercentFPP = parseFloat(txt_NCBFPP.value);
    var LoadingAmountTPP = parseFloat(hdn_LoadingTPP.value);
    var LoadingAmountFPP = parseFloat(hdn_LoadingFPP.value);
    var NCBAmountFPP = parseFloat(hdn_NCBFPP.value);

    if(isNaN(FPPremium))FPPremium = 0;
    if(isNaN(TPPremium))TPPremium = 0;
    if(isNaN(LoadingPercentTPP))LoadingPercentTPP = 0;
    if(isNaN(LoadingPercentFPP))LoadingPercentFPP = 0;
    if(isNaN(NCBPercentFPP))NCBPercentFPP = 0;
    if(isNaN(LoadingAmountTPP))LoadingAmountTPP = 0;
    if(isNaN(LoadingAmountFPP))LoadingAmountFPP = 0;
    if(isNaN(NCBAmountFPP))NCBAmountFPP = 0;
    if(isNaN(lbl_TPPAmount.innerHTML))lbl_TPPAmount.innerHTML = 0;
    if(isNaN(lbl_FPPAmount.innerHTML))lbl_FPPAmount.innerHTML = 0;
    if(isNaN(lbl_NCBAmount.innerHTML))lbl_NCBAmount.innerHTML = 0;

    lbl_TPPAmount.innerHTML=roundNumber((parseFloat(TPPremium)*parseFloat(LoadingPercentTPP))/100,2);
    hdn_LoadingTPP.value =roundNumber((parseFloat(TPPremium)*parseFloat(LoadingPercentTPP))/100,2);
    lbl_FPPAmount.innerHTML=roundNumber((parseFloat(FPPremium)*parseFloat(LoadingPercentFPP))/100,2);
    hdn_LoadingFPP.value = roundNumber((parseFloat(FPPremium)*parseFloat(LoadingPercentFPP))/100,2);
    lbl_NCBAmount.innerHTML=roundNumber((parseFloat(FPPremium)*parseFloat(NCBPercentFPP))/100,2);
    hdn_NCBFPP.value = roundNumber((parseFloat(FPPremium)*parseFloat(NCBPercentFPP))/100,2);                        
    
    Calculate_PremiumDetails();
}

function roundNumber(num, dec) 
{
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
}

function Calculate_PremiumDetails()
{
    var  txt_FPPremium = document.getElementById('<%=txt_FPPremium.ClientID%>');
    var  txt_TPPremium = document.getElementById('<%=txt_TPPremium.ClientID%>');
    var  txt_LoadingTPP = document.getElementById('<%=txt_LoadingTPP.ClientID%>');
    var  txt_LoadingFPP = document.getElementById('<%=txt_LoadingFPP.ClientID%>');
    var  txt_NCBFPP = document.getElementById('<%=txt_NCBFPP.ClientID%>');
    var  lbl_NetPremium = document.getElementById('<%=lbl_NetPremium.ClientID%>');
    var  lbl_ServiceTax = document.getElementById('<%=lbl_ServiceTax.ClientID%>');
    var  lbl_NetPayable = document.getElementById('<%=lbl_NetPayable.ClientID%>');
    var  hdn_LoadingTPP = document.getElementById('<%=hdn_LoadingTPP.ClientID%>');
    var  hdn_LoadingFPP = document.getElementById('<%=hdn_LoadingFPP.ClientID%>');
    var  hdn_NCBFPP = document.getElementById('<%=hdn_NCBFPP.ClientID%>');
    var  hdn_NetPremium = document.getElementById('<%=hdn_NetPremium.ClientID%>');
    var  hdn_ServiceTax = document.getElementById('<%=hdn_ServiceTax.ClientID%>');
    var  hdn_NetPayable = document.getElementById('<%=hdn_NetPayable.ClientID%>');
    var  hdn_TotalPremiumAmount = document.getElementById('<%=hdn_TotalPremiumAmount.ClientID%>');
    var txtSTRate = document.getElementById('<%=TxtServiceTaxRate.ClientID%>');
    var FPPremium = parseFloat(txt_FPPremium.value);
    var TPPremium = parseFloat(txt_TPPremium.value);
    var LoadingPercentTPP = parseFloat(txt_LoadingTPP.value);
    var LoadingPercentFPP = parseFloat(txt_LoadingFPP.value);
    var NCBPercentFPP = parseFloat(txt_NCBFPP.value);
    var LoadingAmountTPP = parseFloat(hdn_LoadingTPP.value);
    var LoadingAmountFPP = parseFloat(hdn_LoadingFPP.value);
    var NCBAmountFPP = parseFloat(hdn_NCBFPP.value);
    var TotalPremiumAmount = parseFloat(hdn_TotalPremiumAmount.value);

    var TotalNetPremiumAmount ;
    var TotalServiceTaxAmount ;
    var TotalNetPayableAmount ;

    if(isNaN(FPPremium))FPPremium = 0;
    if(isNaN(TPPremium))TPPremium = 0;
    if(isNaN(LoadingPercentTPP))LoadingPercentTPP = 0;
    if(isNaN(LoadingPercentFPP))LoadingPercentFPP = 0;
    if(isNaN(NCBPercentFPP))NCBPercentFPP = 0;
    if(isNaN(LoadingAmountTPP))LoadingAmountTPP = 0;
    if(isNaN(LoadingAmountFPP))LoadingAmountFPP = 0;
    if(isNaN(NCBAmountFPP))NCBAmountFPP = 0;
    if(isNaN(TotalPremiumAmount))TotalPremiumAmount = 0;
    
   TotalNetPremiumAmount = roundNumber((parseFloat(FPPremium)- parseFloat(NCBAmountFPP))+ parseFloat(TPPremium)+parseFloat(LoadingAmountTPP)+parseFloat(LoadingAmountFPP)+parseFloat(TotalPremiumAmount),2);
   TotalServiceTaxAmount = roundNumber((parseFloat(TotalNetPremiumAmount)* val(txtSTRate.value))/100,2);
   TotalNetPayableAmount = roundNumber(parseFloat(TotalNetPremiumAmount)+ parseFloat(TotalServiceTaxAmount),2);
   
   lbl_NetPremium.innerHTML = TotalNetPremiumAmount;
   hdn_NetPremium.value = TotalNetPremiumAmount;
   lbl_ServiceTax.innerHTML = TotalServiceTaxAmount;
   hdn_ServiceTax.value  = TotalServiceTaxAmount;
   lbl_NetPayable.innerHTML = TotalNetPayableAmount;
   hdn_NetPayable.value = TotalNetPayableAmount;
}

</script>
</head>
<body  leftmargin="2" topmargin="2" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_VehicleInsurance" runat="server" />
    <div>
    <table class="TABLE">
    <tr runat="server" id="tr_Heading">
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="VEHICLE INSURANCE PREMIUM"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td> 
    </tr>

     <tr>
        <td colspan="6">
          <asp:Panel ID="pnl_Insurance" runat="server" GroupingText="Insurance Details" CssClass="PANEL" Width="100%">

           <table width="100%">
               <tr runat="server" id="tr_Insurance_no">
                    <td id="Td1" class="TD1" style="width:20%" runat="server"><asp:Label ID="lbl_InsuranceNo" runat="server" CssClass="LABEL" Text="Insurance No :"></asp:Label></td>
                    <td id="Td2" style="width:29%" runat="server">
                        <asp:Label ID="lbl_Insurance_No" Width="99%" Font-Bold="True" runat="server" CssClass="LABEL"/>
                    </td>
                    <td id="Td3" class="TDMANDATORY"  style="width: 1%" runat="server"></td>
                    <td id="Td4" class="TD1" style="width:20%" runat="server"><asp:Label ID="lbl_InsuranceDate" runat="server" Text="Insurance Date :" CssClass="LABEL"></asp:Label></td>
                    <td id="Td5" style="width:29%" runat="server">
                        <uc1:wuc_Date_Picker ID="Wuc_Insurance_Date" runat="server" />
                    </td>
                    <td id="Td6" class="TDMANDATORY"  style="width: 1%" runat="server">
                        <asp:HiddenField ID="hdn_VehicleId" runat="server" />
                    </td>
                </tr>
    
                <tr runat="server" id="tr_vehicle_no">
                    <td id="Td7" class="TD1" style="width:20%" runat="server">
                     <asp:Label ID="lbl_Vehicle_No"  runat="server" Text=" Vehicle No :"/>
                    </td>
                    <td id="Td8" style="width:29%" runat="server">
                      <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                    </td>
                    <td id="Td9" class="TDMANDATORY"  style="width: 1%" runat="server">*</td>
                    <td id="Td10" class="TD1" style="width:20%" runat="server">
                      <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <asp:HiddenField runat="server" ID="hdn_EngineNo" />
                        <asp:HiddenField runat="server" ID="hdn_ChasisNo" />
                      </ContentTemplate>
                        <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                        </Triggers>
                      </asp:UpdatePanel>
                    </td>
                    <td id="Td11" style="width:29%" runat="server"></td>
                    <td id="Td12" class="TDMANDATORY"  style="width: 1%" runat="server">
                    </td>
                </tr>
    
                <tr>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Insurance_Company"  runat="server" Text="Insurance Company :"/>
                    </td>
                    <td style="width:29%">                    
                          <asp:DropDownList ID="ddl_InsuranceCompany" AutoPostBack="True" runat="server" CssClass ="DROPDOWN" Width="100%" OnSelectedIndexChanged="ddl_InsuranceCompany_SelectedIndexChanged"/>                        
                    </td>
                    <td class="TDMANDATORY"  style="width: 1%">*</td>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Issuing_Branch"  runat="server" Text="Issuing Branch :"/>
                    </td>
                    <td style="width:29%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <asp:DropDownList ID="ddl_IssuingBranch" runat="server" CssClass ="DROPDOWN" Width="100%"></asp:DropDownList>
                         </ContentTemplate>
                        <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="ddl_InsuranceCompany" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY"  style="width: 1%">*</td>
                </tr>
                <tr>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Policy_No"  runat="server" Text="Policy No :"/>
                    </td>
                    <td style="width:29%">
                        <asp:TextBox ID="txt_PolicyNo" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="25"></asp:TextBox></td>
                    <td class="TDMANDATORY"  style="width: 1%">*</td>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Agent"  runat="server" Text="Agent :"/>
                    </td>
                    <td style="width:29%">
                        <asp:DropDownList ID="ddl_Agent" runat="server" CssClass ="DROPDOWN" Width="100%"></asp:DropDownList></td>
                    <td class="TDMANDATORY"  style="width: 1%">*</td>
                   
                </tr>
                <tr>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Commence_Date"  runat="server" Text="Commence Date :"/>
                    </td>
                    <td style="width:29%">
                        <uc1:wuc_Date_Picker ID="Wuc_CommenceDate" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Expiry_Date"  runat="server" Text="Expiry Date :"/>
                    </td>
                    <td style="width:29%">
                        <uc1:wuc_Date_Picker ID="Wuc_ExpiryDate" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                </tr>
                <tr runat="server" id="tr_EngineChasis_no">
                    <td id="Td13" class="TD1" style="width:20%" runat="server">
                     <asp:Label ID="lbl_Engine_No"  runat="server" Text="Engine No :"/>
                    </td>
                    <td id="Td14" style="width:29%" runat="server">
                        <asp:TextBox ID="txt_EngineNo" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="25"/></td>
                    <td id="Td15" class="TDMANDATORY" style="width: 1%" runat="server">*</td>
                    <td id="Td16" class="TD1" style="width:20%" runat="server">
                     <asp:Label ID="lbl_Chasis_No"  runat="server" Text="Chasis No :"/>
                    </td>
                    <td id="Td17" style="width:29%" runat="server">
                        <asp:TextBox ID="txt_ChasisNo" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="50"/></td>
                    <td id="Td18" class="TDMANDATORY" style="width: 1%" runat="server">*</td>
                </tr>
                <tr>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_IDV"  runat="server" Text="IDV :"/>
                    </td>
                    <td style="width:29%">
                        <asp:TextBox ID="txt_IDV" runat="server" CssClass ="TEXTBOXNOS" Text="0" BorderWidth="1px" MaxLength="20" onkeypress="return Only_Numbers(this,event)"/></td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td class="TD1" style="width:20%"></td>
                    <td style="width:29%"></td>
                    <td style="width: 1%"></td>
                </tr>
                <tr>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_First_Party_Premium"  runat="server" Text="First Party Premium :"/>
                    </td>
                    <td style="width:29%">
                        <asp:TextBox ID="txt_FPPremium" runat="server" Text="0" CssClass ="TEXTBOXNOS" BorderWidth="1px" MaxLength="18" onblur="Calculate_TPP_FPP()" onkeypress="return Only_Numbers(this,event)"/></td>
                    <td class="TDMANDATORY" style="width: 1%"></td>
                    <td class="TD1" style="width:20%">
                     <asp:Label ID="lbl_Third_Party_Premium"  runat="server" Text="Third Party Premium :"/>
                    </td>
                    <td style="width:29%">
                        <asp:TextBox ID="txt_TPPremium" runat="server" CssClass ="TEXTBOXNOS" BorderWidth="1px" MaxLength="18" onblur="Calculate_TPP_FPP()" onkeypress="return Only_Numbers(this,event)"/></td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                </tr>
                <tr>
                    <td class="TD1" style="width:19%">
                     <asp:Label ID="lbl_Loading_TPP_Percent"  runat="server" Text="Loading TPP % :"/>
                    </td>
                    <td style="width:30%">
                        <table width="100%">
                            <tr>
                                <td style="width:30%"><asp:TextBox ID="txt_LoadingTPP" Text="0" runat="server" MaxLength="18" CssClass="TEXTBOXNOS" onblur="Calculate_TPP_FPP()" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)" Width="90%"/></td>
                                <td class="TD1" style="width:30%">
                                 <asp:Label ID="lbl_Amount_TPP"  runat="server" Text="Amount :"/>
                               </td>
                                <td style="width:40%"><asp:Label ID="lbl_TPPAmount" Text="0" Font-Bold="True" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"></asp:Label> </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%"></td>
                    <td class="TD1" style="width:50%" colspan="3"></td>
                </tr>
                <tr>
                    <td class="TD1" style="width:19%">
                     <asp:Label ID="lbl_Loading_FPP_Percent"  runat="server" Text="Loading FPP % :"/>
                   </td>
                    <td style="width:30%">
                        <table width="100%">
                            <tr>
                                <td style="width:30%"><asp:TextBox ID="txt_LoadingFPP" Text="0" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" onblur="Calculate_TPP_FPP()" MaxLength="18" onkeypress="return Only_Numbers(this,event)" Width="90%"/></td>
                                <td class="TD1" style="width:30%">
                                 <asp:Label ID="lbl_Amount_FPP"  runat="server" Text="Amount :"/>
                                </td>
                                <td style="width:40%"><asp:Label ID="lbl_FPPAmount" Text="0" Font-Bold="True" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"></asp:Label> </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%"></td>
                    <td class="TD1" style="width:50%" colspan="3"></td>
                </tr>
                <tr>
                    <td class="TD1" style="width:10%">
                     <asp:Label ID="lbl_NCB_Percent_On_FPP"  runat="server" Text="NCB % on FPP :"/>

                    </td>
                    <td style="width:30%">
                        <table width="100%">
                            <tr>
                                <td style="width:30%"><asp:TextBox ID="txt_NCBFPP" Text="0" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" onblur="Calculate_TPP_FPP()" MaxLength="18" onkeypress="return Only_Numbers(this,event)" Width="90%"/></td>
                                <td class="TD1" style="width:30%">
                                 <asp:Label ID="lbl_NCB_Amount"  runat="server" Text="NCB Amt :"/>
                                </td>
                                <td style="width:40%"><asp:Label ID="lbl_NCBAmount" Text="0" Font-Bold="True" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"></asp:Label> </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 1%"></td>
                    <td class="TD1" style="width:50%" colspan="3"></td>
                </tr>
                <tr>
                    <td  style="width:90%" colspan="6">
                        <asp:HiddenField runat="server" ID="hdn_LoadingTPP" Value="0"/>
                        <asp:HiddenField runat="server" ID="hdn_LoadingFPP" Value="0"/>
                        <asp:HiddenField runat="server" ID="hdn_NCBFPP" Value="0"/>
                    </td> 
                </tr> 
        </table>
         </asp:Panel>
        </td>
    </tr>    
 
 </table>
  <asp:UpdatePanel ID="upd_pnl_dg_PremiumDetails" runat="server" UpdateMode="Conditional">
                <Triggers><asp:AsyncPostBackTrigger ControlID="dg_PremiumDetails" /></Triggers>
                <ContentTemplate>
 <table class="TABLE">
    
    <tr>
        <td colspan="6" style="width:90%">
           
                    <asp:DataGrid ID="dg_PremiumDetails" AutoGenerateColumns="False" ShowFooter="True" CellPadding  = "3"
                    CssClass="Grid" runat="server" OnCancelCommand="dg_PremiumDetails_CancelCommand" OnDeleteCommand="dg_PremiumDetails_DeleteCommand" OnItemCommand="dg_PremiumDetails_ItemCommand" OnItemDataBound="dg_PremiumDetails_ItemDataBound" OnUpdateCommand="dg_PremiumDetails_UpdateCommand" OnEditCommand="dg_PremiumDetails_EditCommand">
                    <ItemStyle HorizontalAlign="Left"/>
                    <HeaderStyle  Height ="15px" Font-Size ="11px" 
                    Font-Names="Verdana"  Font-Bold="True" HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid"
                    BorderColor="#9495A2" BorderWidth ="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Sr.No.">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" Width="5%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Premium Type">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddl_PremiumType" CssClass="DROPDOWN" runat="server"/>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "PremiumType") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_PremiumType" CssClass="DROPDOWN" runat="server"/>
                                </EditItemTemplate>
                                <HeaderStyle Width="60%" />
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Amount">
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" MaxLength="18" runat="server" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)"/>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Amount") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" runat="server" MaxLength="18" BorderWidth="1px" onkeypress="return Only_Numbers(this,event)"/>
                                </EditItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" Width="15%" />
                            </asp:TemplateColumn>              
                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                <HeaderStyle Width="10%" />
                            </asp:EditCommandColumn>

                            <asp:TemplateColumn HeaderText="Delete">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtn_Add" Text="Add" Runat="server" CommandName="Add"/>
                                </FooterTemplate>
                                <ItemTemplate><asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"/></ItemTemplate>
                                    <HeaderStyle Width="10%" />
                            </asp:TemplateColumn>
                        </Columns> 
                    </asp:DataGrid>
                   <asp:HiddenField runat="server" ID="hdn_TotalPremiumAmount" />

        </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
 </table>
 <table class="TABLE">
     <tr>
        <td class="TD1" style="width:20%">
         <asp:Label ID="lbl_Net_Premium"  runat="server" Text="Net Premium :"/>

        </td>
         <td class="TD1" style="width:10%">
             &nbsp;</td>
        <td style="width:28%">
            <asp:Label ID="lbl_NetPremium" runat="server" CssClass ="TEXTBOXNOS" BorderWidth="1px" Font-Bold="True"/></td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width:20%"><asp:HiddenField ID="hdn_NetPremium" runat="server" /></td>
        <td style="width:1%"></td>
        <td style="width: 1%"></td>
     </tr>
     <tr>
        <td class="TD1" style="width:20%">
         <asp:Label ID="lbl_Service_Tax_12_36_Percent"  runat="server" Text="Service Tax:"/>
        </td>
         <td class="TD1" style="width:10%">
             <asp:TextBox ID="TxtServiceTaxRate" runat="server" CssClass = "TEXTBOXNOS" onkeypress = "return Only_Numbers(this,event)" onblur = "Calculate_PremiumDetails()" style="width:98%"></asp:TextBox>
         </td>
        <td style="width:29%">
            <asp:label ID="lbl_ServiceTax" runat="server" CssClass ="TEXTBOXNOS" BorderWidth="1px" Font-Bold="True"/></td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width:20%"><asp:HiddenField ID="hdn_ServiceTax" runat="server" /></td>
        <td style="width:29%"></td>
        <td style="width: 1%"></td>
     </tr>
     <tr>
        <td class="TD1" style="width:20%">
         <asp:Label ID="lbl_Net_Payable"  runat="server" Text="Net Payable :"/>
        </td>
         <td class="TD1" style="width:10%">
             &nbsp;</td>
        <td style="width:29%">
            <asp:Label ID="lbl_NetPayable" runat="server" CssClass ="TEXTBOXNOS" BorderWidth="1px" Font-Bold="True" /></td>
        <td style="width: 1%"></td>
        <td class="TD1" style="width:20%"><asp:HiddenField ID="hdn_NetPayable" runat="server" /></td>
        <td style="width:29%"></td>
        <td style="width: 1%"></td>
     </tr>
     <tr>
        <td colspan="7">&nbsp;</td>
     </tr>
 </table>
    </ContentTemplate>
            </asp:UpdatePanel>
            
 <table class="TABLE">
        <tr>
            <td style="width: 20%" class="TD1">
              <asp:Label ID="PaidBy" runat="Server" Text="Paid By:"></asp:Label>
            </td>
            <td style="width: 29%">
                <asp:RadioButtonList ID="rdl_Paid_By" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True" onclick="Enabled_Disabled_Controls_On_Cheque()">Cash</asp:ListItem>
                    <asp:ListItem Value="1"  onclick="Enabled_Disabled_Controls_On_Cheque()">Cheque</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="width: 1%" class="TDMANDATORY"></td>
            <td style="width: 50%" colspan="3" class="TD1"/>
        </tr>
        <tr id="tr_Cheque_Details" runat="server">
            <td style="width: 20%" class="TD1">
                <asp:Label ID="lbl_ChequeNo" runat="Server" Text="Cheque No :"></asp:Label>
            </td>
            <td style="width: 29%">
                <asp:TextBox ID="txt_Cheque_No" runat="server" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOXNOS" MaxLength="6"></asp:TextBox></td>
            <td style="width: 1%" class="TDMANDATORY">*</td>
            <td style="width: 20%" class="TD1">
                <asp:Label ID="lbl_ChequeDate" runat="Server" Text="Cheque Date :"></asp:Label></td>
            <td style="width: 29%">
                <uc1:wuc_Date_Picker ID="Wuc_Cheque_Date" runat="server" />
            </td>
            <td style="width: 1%" class="TDMANDATORY">*</td>
        </tr>
        <tr id="tr_Bank_Name" runat="server">
            <td style="width: 20%" class="TD1">
                <asp:Label ID="lbl_BankName" runat="Server" Text="Bank Name :"></asp:Label></td>
            <td style="width: 29%">
                <asp:DropDownList ID="ddl_Bank_Name" runat="server" CssClass="DROPDOWN" Width="99%"/></td>
            <td style="width: 1%" class="TDMANDATORY">*</td>
            <td style="width: 20%" class="TD1"></td>
            <td style="width: 29%"></td>
            <td style="width: 1%" class="TDMANDATORY">
                <asp:HiddenField ID="hdn_Is_Cheque" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="6">&nbsp;</td>
        </tr>
  </table>
  <table class="TABLE" runat="server" id="tbl_save">
   <tr>
        <td colspan="6">&nbsp;</td>
   </tr>
   <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save"  CssClass="BUTTON" OnClientClick="return ValidateUI_VehicleInsurancePremium()" OnClick="btn_Save_Click" />
        </td>
    </tr>

 </table> 
 <table class="TABLE">    
   <tr>
      <td colspan="6" style="width:100%">
        <asp:UpdatePanel ID="upd_Pnl_PremiumDetails_Save" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_PremiumDetails" />
            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
          </Triggers>
          <ContentTemplate>
            <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
      </td>
    </tr>    
    <tr>
        <td colspan="6">
           <asp:Label  ID="lbl_Client_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
        </td>
     </tr>
     <tr runat="server" id="tr_labelerror">
        <td colspan="6">
           <asp:Label  ID="Label1"  runat="server" CssClass="LABELERROR" EnableViewState="False" Text="Fields with * Mark are Mandatory"></asp:Label>
        </td>
     </tr>
    <tr>
        <td colspan="6" >
            <asp:HiddenField runat="server" ID="hdn_VehicleTypeID" Value="0" />
            <asp:HiddenField runat="server" ID="hdn_Vehicle_InsuranceID" Value="0" />
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />

        </td>
    </tr>
 </table>
    </div>
    </form>
    
<script type="text/javascript" language="javascript">
 Enabled_Disabled_Controls_On_Cheque();
</script>
</body>
</html>
