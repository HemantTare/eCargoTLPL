<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucGCInsurenceDetails.ascx.cs" Inherits="Operations_Booking_wucGCInsurenceDetails" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>

<asp:ScriptManager ID="SM_GCInsurenceDetails" runat="server"></asp:ScriptManager>

<script type="text/javascript">
   
    function Allow_To_Exit()
    {
        var ATE = false;
        if (confirm("Do you want to Exit...")==false)
        {
            ATE=false;
        }
        else
        {
            ATE=true;
        }
        
        if (ATE)
        {  
            window.close();
            return true;
        }
        else
        {
            return false;
        }
    }

    function On_View()
    {    
        var hdn_Mode = document.getElementById('wucGCInsurenceDetails1_hdn_Mode'); 
        var btn_Exit = document.getElementById('wucGCInsurenceDetails1_btn_Exit');
        var Enable = true;

        if (val(hdn_Mode.value)== 4)
        {
            for(i = 0; i < document.forms[0].elements.length; i++) 
            {
                elm = document.forms[0].elements[i];

                if(elm.id!='')
                {
                    var elm_id = document.getElementById(elm.id);
                    var elm_name = elm.name;
                    var arr = elm_name.split("$");                                     

                    if (elm.type != 'lable')
                    {                    
                        elm.disabled = Enable;
                    }
                }
            }
            btn_Exit.disabled = false;
        }  
    }

</script>

<table class="TABLE" border="0" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_InsurenceDetails" runat="server" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>   
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_InsuranceCompany" CssClass="LABEL" Text="Insurance Company :"
                runat="server" meta:resourcekey="lbl_InsuranceCompanyResource1"></asp:Label>
        </td>
        <td style="width: 79%;" colspan="4">
            <asp:TextBox ID="txt_InsuranceCompany" runat="server" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>             
    </tr>
    <tr>
       <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_PolicyNo" CssClass="LABEL" Text="Policy No. :" runat="server"
                meta:resourcekey="lbl_PolicyNoResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_PolicyNo" runat="server" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td> 
        <td class="TD1" style="width:20%">
            <asp:Label ID="lbl_PolicyExpDate" CssClass="LABEL" Text="Policy Exp Date :" runat="server"
                meta:resourcekey="lbl_PolicyExpDateResource1"></asp:Label>
        </td>
        <td style="width: 29%;">
            <componentart:calendar id="wuc_PolicyExpDate" runat="server" cssclass="TEXTBOX" borderwidth="1px"
                pickerformat="Custom" pickercustomformat="MMMM d yyyy" controltype="Picker" pickercssclass="picker"
                allowdayselection="True" allowmonthselection="True" mindate="1900-01-01" autopostbackonselectionchanged="True"
                onselectionchanged="wuc_PolicyExpDate_SelectionChanged" autopostbackonvisibledatechanged="True"
                width="95%">
            </componentart:calendar>
            <asp:UpdatePanel ID="upd_hdn_PolicyExpDate" runat="server">
                <contenttemplate>
                     <asp:HiddenField runat="server" ID="hdn_PolicyExpDate"></asp:HiddenField>
                </contenttemplate>
                <triggers>
                     <asp:AsyncPostBackTrigger ControlID="wuc_PolicyExpDate"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%;">&nbsp;</td>       
    </tr>
    <tr>
         <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_PolicyAmount" CssClass="LABEL" Text="Policy Amount :" runat="server"
                meta:resourcekey="lbl_PolicyAmountResource1"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_PolicyAmount" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>
        <td class="TD1" style="width:20%;">
            <asp:Label ID="lbl_RiskAmount" CssClass="LABEL" Text="Risk Amount :" runat="server"
                meta:resourcekey="lbl_RiskAmountResource1"></asp:Label>
        </td>
        <td class="TD1" style="width:29%">
            <asp:TextBox ID="txt_RiskAmount" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" align="right">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <contenttemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                 </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr align="left">
        <td align="center" colspan="6">
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="k" AccessKey="S" />
            &nbsp;<asp:Button ID="btn_Exit" runat="server" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <input id="hdn_Consignee_Addess" runat="server" type="hidden" />
            <input id="hdn_Consignee_TelNo" runat="server" type="hidden" />
            <input id="hdn_Co_Name" runat="server" type="hidden" />
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">    
    On_View();    
</script>