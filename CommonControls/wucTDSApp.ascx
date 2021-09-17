<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucTDSApp.ascx.cs" Inherits="CommonControls_wucTDSApp" %>
<script src="../Javascript/Common.js" type="text/javascript"></script>
<%--<asp:ScriptManager ID="scm_TDSApp" runat="server">
</asp:ScriptManager>--%>

<script type="text/javascript">

function Hide_Control()
{
    var chk_IsTDSApp = document.getElementById('<%=chk_IsTDSApplicable.ClientID %>');
    var chk_IsLower = document.getElementById('<%=chk_IsLowerNoDeductionApplicable.ClientID %>');
    var chk_IgnoreSurchargeExemptionLimit = document.getElementById('<%=chk_IgnoreSurchargeExemptionLimit.ClientID %>');
    var ddl_sectionNo = document.getElementById('<%=ddl_Section_Number.ClientID %>');
    var ddl_deducteeType = document.getElementById('<%=ddl_DeducteeType.ClientID %>');
    var txt_LowerRate = document.getElementById('<%=txt_TDSLowerRate.ClientID %>');
    
    
    var td_lblDeducteeType = document.getElementById('<%=td_lblDeducteeType.ClientID %>');
    var td_ddlDeducteeType = document.getElementById('<%=td_ddlDeducteeType.ClientID %>');
    var td_lblIsLower = document.getElementById('<%=td_lblIsLower.ClientID %>');
    var td_chkIsLower = document.getElementById('<%=td_chkIsLower.ClientID %>');
    var td_lblSectionNo = document.getElementById('<%=td_lblSectionNo.ClientID %>');
    var td_ddlSectionNo = document.getElementById('<%=td_ddlSectionNo.ClientID %>');
    var td_lblTDSLowerRate = document.getElementById('<%=td_lblTDSLowerRate.ClientID %>');
    var td_txtTDSLowerRate = document.getElementById('<%=td_txtTDSLowerRate.ClientID %>');
    var td_lblIgnore = document.getElementById('<%=td_lblIgnore.ClientID %>');
    var td_chkIgnore = document.getElementById('<%=td_chkIgnore.ClientID %>');
    var td_MDDeductee = document.getElementById('<%=td_MDDeducteeType.ClientID %>');
    var td_MDSection = document.getElementById('<%=td_MDSectionNumber.ClientID %>');
    
 
    if(chk_IsTDSApp.checked == false)
    {
    td_lblDeducteeType.style.display = 'none';
    td_ddlDeducteeType.style.display = 'none';
    
    td_lblIsLower.style.display = 'none';
    td_chkIsLower.style.display = 'none';
    
    td_lblSectionNo.style.display = 'none';
    td_ddlSectionNo.style.display = 'none';
    
    td_lblTDSLowerRate.style.display = 'none';
    td_txtTDSLowerRate.style.display = 'none';
    
    td_lblIgnore.style.display = 'none';
    td_chkIgnore.style.display = 'none';
    
    td_MDDeductee.style.display = 'none';
    td_MDSection.style.display = 'none';
    
   
    chk_IsLower.checked = false;
    ddl_deducteeType.value = '0';
    ddl_sectionNo.value='0';
    txt_LowerRate.value = '';
    chk_IgnoreSurchargeExemptionLimit.checked = false;
    }
    else
    {
   
    td_lblDeducteeType.style.display = 'inline';
    td_ddlDeducteeType.style.display = 'inline';
    td_MDDeductee.style.display = 'inline';
    
    td_lblIsLower.style.display = 'inline';
    td_chkIsLower.style.display = 'inline';
    
    td_lblIgnore.style.display = 'inline';
    td_chkIgnore.style.display = 'inline';
    
        if(chk_IsLower.checked == true)
        {
         td_lblSectionNo.style.display = 'inline';
         td_ddlSectionNo.style.display = 'inline';
         td_MDSection.style.display = 'inline';
         
            if(ddl_sectionNo.value == '197')
            {
             td_lblTDSLowerRate.style.display = 'inline';
             td_txtTDSLowerRate.style.display = 'inline';
            }
            else
            {
             td_lblTDSLowerRate.style.display = 'none';
             td_txtTDSLowerRate.style.display = 'none';
             
             txt_LowerRate.value = '';
            }
        }
        else
        {
          td_lblSectionNo.style.display = 'none';
          td_ddlSectionNo.style.display = 'none';
          td_MDSection.style.display = 'none';
         
          td_lblTDSLowerRate.style.display = 'none';
          td_txtTDSLowerRate.style.display = 'none';
          
          ddl_sectionNo.value='0';
          txt_LowerRate.value = '';
        }
    
    }



}


function ValidateWucTDS(lbl_Error)
    {
    
    var chk_IsTDSApp = document.getElementById('<%=chk_IsTDSApplicable.ClientID %>');
    var chk_IsLower = document.getElementById('<%=chk_IsLowerNoDeductionApplicable.ClientID %>');
    var chk_IgnoreSurchargeExemptionLimit = document.getElementById('<%=chk_IgnoreSurchargeExemptionLimit.ClientID %>');
    var ddl_sectionNo = document.getElementById('<%=ddl_Section_Number.ClientID %>');
    var ddl_deducteeType = document.getElementById('<%=ddl_DeducteeType.ClientID %>');
    var txt_LowerRate = document.getElementById('<%=txt_TDSLowerRate.ClientID %>');
    
        if (chk_IsTDSApp.checked == true)
        {
            if (ddl_deducteeType.value == '0')
            {
                lbl_Error.innerText = 'Please Select Deductee Type';
                return false;
            }

            if (chk_IsLower.checked == true)
            {

                if (ddl_sectionNo.value == "0")
                {
                    lbl_Error.innerText = 'Please Select Section Number';
                    return false;
                }

                if (ddl_sectionNo.value == '197')
                {
                    if (Trim(txt_LowerRate.value) == '')
                    {
                        lbl_Error.innerText = "Please Enter TDS%";
                        return false;
                    }
                }

            }

            return true;
        }
        else
        {
            return true;
        }
    
        
    }

</script> 


<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
        <td style="width: 100%;">
            <asp:Panel ID="Panel1" runat="server" GroupingText="TDS Details" Width="100%">

<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td style="width: 32%" class="TD1" id="td_lblIsTDSApp" runat="server">
                <asp:Label ID="lbl_IsTDSApplicable" runat="server" CssClass="LABEL" Text="Is TDS Applicable?"></asp:Label>
            
        </td>
        <td style="width: 10%" id="td_chkIsTDSApp" runat="server">
                <asp:CheckBox ID="chk_IsTDSApplicable" runat="server" onclick="return Hide_Control();"/>
            
        </td>
        <td>
        </td>
        <td style="width: 10%" class="TD1" runat="server" id="td_lblDeducteeType">
                <asp:Label ID="lbl_DeducteeType" runat="server" CssClass="LABEL" Text="Deductee Type :"></asp:Label>
            </td>
        <td style="width: 48%" runat="server" id="td_ddlDeducteeType">
                <asp:DropDownList ID="ddl_DeducteeType" runat="server"  Width="100%" CssClass="DROPDOWN" >
                </asp:DropDownList>
            
        </td>
        <td style="width: 1%" class="TDMANDATORY" id="td_MDDeducteeType" runat="server">*
        </td>
    </tr>
    
    <tr runat="server" id="tr_IsLower">
        <td style="width: 32%;" class="TD1" runat="server" id="td_lblIsLower">
                <asp:Label ID="lbl_IsLowerNoDeductionApplicable" runat="server" CssClass="LABEL" 
                    Text="Is Lower /No Deduction Applicable?"></asp:Label>
            
        </td>
        <td style="width: 10%" runat="server" id="td_chkIsLower">
                <asp:CheckBox ID="chk_IsLowerNoDeductionApplicable" runat="server" Width="1%" onclick="return Hide_Control();" />
           
        </td>
        <td>
        </td>
        <td style="width: 10%;" class="TD1" runat="server" id="td_lblSectionNo">
                <asp:Label ID="lbl_SectionNo" runat="server" CssClass="LABEL" Text="Section No :"></asp:Label>
          
        </td>
        <td style="width: 48%;" runat="server" id="td_ddlSectionNo">
                <asp:DropDownList ID="ddl_Section_Number" runat="server" Width="100%" onchange="return Hide_Control();"                    
                            CssClass="DROPDOWN">
                            <asp:ListItem Selected="True" Value="0">Select One</asp:ListItem>
                    <asp:ListItem Value="197A">197A</asp:ListItem>
                    <asp:ListItem Value="197">197</asp:ListItem>
                </asp:DropDownList>
           
        </td>
        <td style="width: 1%;" class="TDMANDATORY" id="td_MDSectionNumber" runat="server">*
        </td>
    </tr>
    
    <tr runat="server" id="tr_TDSLowerRate">
        <td style="width: 32%">
        </td>
        <td style="width: 10%">
        </td>
        <td>
        </td>
        <td style="width: 10%" class="TD1" runat="server" id="td_lblTDSLowerRate">
                <asp:Label ID="lbl_TDSLowerRate" runat="server" CssClass="LABEL" Text="TDS Lower Rate %:"></asp:Label>
           
        </td>
        <td style="width: 39%" runat="server" id="td_txtTDSLowerRate">
                <asp:TextBox ID="txt_TDSLowerRate" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                    onkeypress="return Only_Numbers(this,event);" onblur="valid(this)" Width="50%" MaxLength="9"></asp:TextBox>
           
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    
    <tr runat="server" id="tr_Ignore">
        <td style="width: 32%" class="TD1" runat="server" id="td_lblIgnore">
                <asp:Label ID="lbl_IgnoreSurchargeExemptionLimit" runat="server" CssClass="LABEL"
                    Text="Ignore Surcharge Exemption Limit"></asp:Label>
            
        </td>
        <td style="width: 10%" runat="server" id="td_chkIgnore">
                <asp:CheckBox ID="chk_IgnoreSurchargeExemptionLimit" runat="server"/>
            
        </td>
        <td>
        </td>
        <td style="width: 10%">
        </td>
        <td style="width: 48%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    
    <tr>
        <td style="width: 32%">
        </td>
        <td style="width: 19%">
        </td>
        <td>
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    
    <tr>
        <td style="width: 32%">
        </td>
        <td style="width: 19%">
        </td>
        <td>
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table> 

             </asp:Panel> 
       </td>
    </tr>
</table> 

<script type="text/javascript">

Hide_Control();

</script>