<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrnNewGCInsuranceDetails.aspx.cs" Inherits="Operations_Booking_NewGC_FrnNewGCInsuranceDetails" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Insurance Details</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/Common.js"></script>
    <script type="text/javascript" src="../../../Javascript/Operations/Booking/GCNew.js"></script>
<script type="text/javascript">
    
    function updateInsuranceDetails(isInsurancefilled)
    { 
        window.opener.call_InsuranceUpdate(isInsurancefilled);
    } 

    function Allow_To_Exit()
    {
        var ATE = false;
        if (confirm("Do you want to Exit...")==false)
        {
            ATE=false;
        }
        else
        {
            window.close();
            ATE=true;
        }
        return ATE;
    }

    function On_View()
    {    
        var hdn_Mode = document.getElementById('hdn_Mode'); 
        var btn_Exit = document.getElementById('btn_Exit');
        var btn_Save = document.getElementById('btn_Save');
        
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
            btn_Save.style.visibility = 'hidden';
        }  
    }

</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM_GCInsurenceDetails" runat="server"></asp:ScriptManager>

   <table class="TABLE" border="0" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_InsurenceDetails" runat="server" CssClass="HEADINGLABEL" Text="Insurence Details"></asp:Label></td>
    </tr>   
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_InsuranceCompany" CssClass="LABEL" Text="Insurance Company :" runat="server"></asp:Label>
        </td>
        <td style="width: 79%;" colspan="4">
            <asp:TextBox ID="txt_InsuranceCompany" runat="server" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>             
    </tr>
    <tr>
       <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_PolicyNo" CssClass="LABEL" Text="Policy No. :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_PolicyNo" runat="server" CssClass="TEXTBOX" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td> 
        <td class="TD1" style="width:20%">
            <asp:Label ID="lbl_PolicyExpDate" CssClass="LABEL" Text="Policy Exp Date :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;">
            <componentart:calendar id="wuc_PolicyExpDate" runat="server" cssclass="TEXTBOX" borderwidth="1px"
                pickerformat="Custom" pickercustomformat="MMMM d yyyy" controltype="Picker" pickercssclass="picker"
                allowdayselection="True" allowmonthselection="True" mindate="1900-01-01" width="95%">
            </componentart:calendar>
        </td>
        <td style="width: 1%;">&nbsp;</td>
    </tr>
    <tr>
         <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_PolicyAmount" CssClass="LABEL" Text="Policy Amount :" runat="server"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_PolicyAmount" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>
        <td class="TD1" style="width:20%;">
            <asp:Label ID="lbl_RiskAmount" CssClass="LABEL" Text="Risk Amount :" runat="server"></asp:Label>
        </td>
        <td class="TD1" style="width:29%">
            <asp:TextBox ID="txt_RiskAmount" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);" MaxLength="10"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" align="right">&nbsp;</td>
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
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click" AccessKey="S" />
            &nbsp;<asp:Button ID="btn_Exit" OnClientClick="return Allow_To_Exit()" runat="server" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
        </td>
    </tr>
</table>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">    
 On_View(); 
</script>