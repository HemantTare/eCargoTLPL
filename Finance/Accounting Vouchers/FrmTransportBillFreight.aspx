<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTransportBillFreight.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_FrmTransportBillFreight" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Finance/Accounting Vouchers/TransportBill.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Freight</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
      
    function Show_GC_OtherCharge(ItemIndex,GCId)
    {
        var Path = '../Accounting Vouchers/FrmTransportBillOtherCharge.aspx?ItemIndex='+ ItemIndex+'&GCID='+GCId+'&IsFromGC=true&Menu_Item_Id=MQA0ADMA';//
        window.open(Path,'OtherCharge','width=700,height=500,top=200,left=250,menubar=no,resizable=no,scrollbars=no')
        return false;
    }
    
    function Calculate_TotalFreight()
    {
        var txt_Freight = document.getElementById('<%=txt_Freight.ClientID %>');
        var txt_LocalCharge = document.getElementById('<%=txt_LocalCharge.ClientID %>');
        var txt_LoadingCharges = document.getElementById('<%=txt_LoadingCharges.ClientID %>');
        var txt_StationaryCharges = document.getElementById('<%=txt_StationaryCharges.ClientID %>');
        var txt_FovRiskCharges = document.getElementById('<%=txt_FovRiskCharges.ClientID %>'); 
        var txt_DDCharges = document.getElementById('<%=txt_DDCharges.ClientID %>');
        var txt_IBACharges = document.getElementById('<%=txt_IBACharges.ClientID %>');  
        var txt_COD = document.getElementById('<%=txt_COD.ClientID %>'); 
        
        var lbl_SubTotal = document.getElementById('<%=lbl_SubTotal.ClientID %>');
        var hdn_SubTotal = document.getElementById('<%=hdn_SubTotal.ClientID %>');
        var lbtn_OtherCharge = document.getElementById('<%=lbtn_OtherCharges.ClientID %>');

        lbl_SubTotal.innerHTML = val(txt_Freight.value)+val(txt_LocalCharge.value)+val(txt_LoadingCharges.value)+
                                 val(txt_StationaryCharges.value)+val(txt_FovRiskCharges.value)+
                                 val(txt_DDCharges.value)+val(lbtn_OtherCharge.innerHTML) +
                                 val(txt_IBACharges.value)+val(txt_COD.value);
                                 
        hdn_SubTotal.value = val(txt_Freight.value)+val(txt_LocalCharge.value)+val(txt_LoadingCharges.value)+
                             val(txt_StationaryCharges.value)+val(txt_FovRiskCharges.value)+
                             val(txt_DDCharges.value)+val(lbtn_OtherCharge.innerHTML) + 
                             val(txt_IBACharges.value)+val(txt_COD.value);
    }
    </script>

</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_freight" runat="server">
        </asp:ScriptManager>
        <div>
            <table class="TABLE">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                            </Triggers>
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="Lbl_GC_Text" Font-Bold="True" runat="server" meta:resourcekey="Lbl_GC_TextResource1" /></td>
                                        <td style="width: 49%;">
                                            <asp:Label ID="lbl_GCNo" runat="server" ForeColor="#732C3E" Width="70%" Font-Bold="True"
                                                CssClass="TEXTBOXNOS" Text="0" meta:resourcekey="lbl_GCNoResource1"></asp:Label></td>
                                        <td class="TD1" style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_Freight" CssClass="LABEL" Text="Freight :" runat="server" meta:resourcekey="lbl_FreightResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_Freight" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"
                                                meta:resourcekey="txt_FreightResource1"></asp:TextBox>--%>
                                            <asp:Label ID="txt_Freight" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TD1" style="width: 1%;">
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="TD1" style="width: 50%">
                                            <asp:Label ID="lbl_Surcharge" runat="server" Text="Surcharge :" CssClass="LABEL"></asp:Label></td>
                                        <td style="width: 49%">
                                            <asp:TextBox ID="txt_Surcharge" runat="server" Width="70%" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                Text="0" MaxLength="8" onblur="Calculate_TotalFreight()" onkeypress="return Only_Numbers(this,event);"></asp:TextBox></td>
                                        <td class="TD1" style="width: 1%">
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_StationaryCharges" CssClass="LABEL" Text="Statistical Charges :"
                                                runat="server" meta:resourcekey="lbl_StationaryChargesResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_StationaryCharges" runat="server" Width="70%" MaxLength="8"
                                                onkeypress="return Only_Numbers(this,event)" onblur="Calculate_TotalFreight()"
                                                BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0" meta:resourcekey="txt_StationaryChargesResource1"></asp:TextBox>--%>
                                            <asp:Label ID="txt_StationaryCharges" runat="server" Width="70%" Font-Bold="True"
                                                BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_FOVRiskCharges" CssClass="LABEL" Text="FOV/Risk Charges :" runat="server"
                                                meta:resourcekey="lbl_FOVRiskChargesResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_FovRiskCharges" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"
                                                meta:resourcekey="txt_FovRiskChargesResource1"></asp:TextBox>--%>
                                            <asp:Label ID="txt_FovRiskCharges" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TD1" style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_LoadingCharges" CssClass="LABEL" Text="Hamali Charge :" runat="server"
                                                meta:resourcekey="lbl_LoadingChargesResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_LoadingCharges" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"
                                                meta:resourcekey="txt_LoadingChargesResource1"></asp:TextBox>--%>
                                            <asp:Label ID="txt_LoadingCharges" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TD1" style="width: 1%;">
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td class="TD1" style="width: 50%">
                                            <asp:Label ID="lblDKCharge" runat="server" CssClass="LABEL" Text="D.K :"></asp:Label></td>
                                        <td style="width: 49%">
                                            <asp:TextBox ID="txt_DKCharge" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"></asp:TextBox></td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_LocalCharge" CssClass="LABEL" Text="Local Cartages :" runat="server"
                                                meta:resourcekey="lbl_LocalChargeResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_LocalCharge" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"
                                                meta:resourcekey="txt_LocalChargeResource1"></asp:TextBox>--%>
                                            <asp:Label ID="txt_LocalCharge" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_DDCharges" CssClass="LABEL" Text="Door Delivery Charge :" runat="server"
                                                meta:resourcekey="lbl_DDChargesResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_DDCharges" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"
                                                meta:resourcekey="txt_DDChargesResource1"></asp:TextBox>--%>
                                            <asp:Label ID="txt_DDCharges" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TD1" style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="Label1" CssClass="lbl_COD" Text="COD :" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:TextBox ID="txt_COD" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"></asp:TextBox>--%>
                                            <asp:Label ID="txt_COD" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TD1" style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr id="tr_IBACharges" runat="server">
                                        <td class="TD1" style="width: 50%;" runat="server">
                                            <asp:Label ID="lbl_IBACharges" CssClass="LABEL" Text="IBA Charges :" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 49%;" runat="server">
                                            <%-- <asp:TextBox ID="txt_IBACharges" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"></asp:TextBox>--%>
                                            <asp:Label ID="txt_IBACharges" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                        </td>
                                        <td class="TD1" style="width: 1%;" runat="server">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <asp:Label ID="lbl_OtherCharges" CssClass="LABEL" Text="Other Charges :" runat="server"
                                                meta:resourcekey="lbl_OtherChargesResource1"></asp:Label>
                                        </td>
                                        <td style="width: 49%;">
                                            <%--<asp:LinkButton ID="lbtn_OtherCharges" Width="70%" BorderWidth="1px" Font-Bold="True"
                                                runat="server" CssClass="TEXTBOXNOS" Text="0" meta:resourcekey="lbtn_OtherChargesResource1"></asp:LinkButton>--%>
                                                
                                            <asp:Label ID="lbtn_OtherCharges" runat="server" Width="70%" BorderWidth="1px" Font-Bold="True"
                                               CssClass="TEXTBOXNOS" Text="0" ></asp:Label>
      
                                        </td>
                                        <td class="TDMANDATORY" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%;">
                                            <b>Sub Total :</b></td>
                                        <td style="width: 49%;">
                                            <asp:Label ID="lbl_SubTotal" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0" meta:resourcekey="lbl_SubTotalResource1"></asp:Label></td>
                                        <td class="TD1" style="width: 1%;">
                                            <asp:HiddenField runat="server" ID="hdn_SubTotal" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%">
                                            <asp:Label ID="lbl_ServiceTax" runat="server" CssClass="LABEL" Text="Service Tax :"></asp:Label></td>
                                        <td style="width: 49%">
                                            <asp:Label ID="lbl_ServiceTaxValue" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label><asp:HiddenField ID="hdn_ServiceTax" runat="server" />
                                            <asp:HiddenField ID="hdn_Actual_ServiceTax" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%">
                                            <asp:Label ID="lbl_Round_Off" runat="server" CssClass="LABEL" Text="Round Off :"></asp:Label>
                                        </td>
                                        <td style="width: 49%">
                                            <%--<asp:TextBox ID="txt_Round_Off" runat="server" Width="70%" MaxLength="8" onkeypress="return Only_Numbers(this,event)"
                                                onblur="Calculate_TotalFreight()" BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0"></asp:TextBox>--%>
                                                <asp:Label ID="txt_Round_Off" runat="server" Width="70%" Font-Bold="True" BorderWidth="1px"
                                                CssClass="TEXTBOXNOS" Text="0"></asp:Label>
                                                <asp:HiddenField ID="hdn_Round_Off" runat="server"></asp:HiddenField>
                                        </td>
                                        <td class="TD1" style="width: 1%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 50%">
                                            <asp:Label ID="lbl_TotalGCAmount" runat="server" CssClass="LABEL" Text="Total GR Amount :"></asp:Label>
                                        </td>
                                        <td style="width: 49%">
                                            <asp:Label ID="lbl_TotalGCAmountValue" runat="server" Width="70%" Font-Bold="True"
                                                BorderWidth="1px" CssClass="TEXTBOXNOS" Text="0">
                                            </asp:Label><asp:HiddenField ID="hdn_TotalGCAmount" runat="server"></asp:HiddenField>
                                        </td>
                                        <td class="TD1" style="width: 1%">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr style="display: none">
                    <td class="TD1" colspan="6">
                        <asp:Button ID="btn_update_grid" CssClass="BUTTON" runat="server" Text="UpdateGrid"
                            OnClick="btn_update_grid_Click" meta:resourcekey="btn_update_gridResource1" />
                        <asp:HiddenField runat="server" ID="hdn_Is_IBA"></asp:HiddenField>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3" style="text-align: center">
                        <asp:Button ID="btn_Save" runat="server" CssClass="SMALLBUTTON" AccessKey="S" Text="Save"
                            OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" Visible="false" />
                        &nbsp;<asp:Button ID="btn_Exit" runat="server" CssClass="SMALLBUTTON" Text="Exit"
                            AccessKey="E" OnClick="btn_Exit_Click" meta:resourcekey="btn_ExitResource1" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3" style="text-align: left">
                        &nbsp;
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label><br />
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
  
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
  
function update_freightDetails()
{
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
document.getElementById('<%=btn_update_grid.ClientID%>').style.visibility = "hidden";
document.getElementById('<%=btn_update_grid.ClientID%>').click();
}
 
 function updateparentdataset()
 { 
  window.opener.update_grid_GCDetails();
 }
 
 Hide_Controls();
 
</script>

