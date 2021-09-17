<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTripExpenseHead.aspx.cs"
    Inherits="Master_General_FrmTripExpenseHead" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">
 
 function RsPerDayOnBlur()
    { 
        
        var txtRsPerDay = document.getElementById('<%=txtRsPerDay.ClientID %>');
        
        var txtFixedRs = document.getElementById('<%=txtFixedRs.ClientID %>');
        
        if (val(txtRsPerDay.value)>0)
        {
            txtFixedRs.value=0
            txtFixedRs.disabled = true;
        }
        else
        {
            txtFixedRs.disabled = false;
            txtFixedRs.focus();
        }
        return;
    }
    
     function FixedRsOnBlur()
    { 
        
        var txtFixedRs = document.getElementById('<%=txtFixedRs.ClientID %>');
        var txtRsPerDay = document.getElementById('<%=txtRsPerDay.ClientID %>');
        
        
        if (val(txtFixedRs.value)>0)
        {
            txtRsPerDay.value=0
            txtRsPerDay.disabled = true;
        }
        else
        {
            txtRsPerDay.disabled = false;
            txtRsPerDay.focus();
        }
        return;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Trip Expense Head</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="width: 100%;">
            <table class="TABLE" width="100%" border="0">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Trip Expense Head"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Expense Head</td>
                    <td style="width: 79%" colspan="4">
                        <asp:TextBox ID="txtExpenseHead" runat="server" CssClass="TEXTBOX" MaxLength="50"
                            Width="80%" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Sr No</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtSrNo" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TDMANDATORY" style="width: 50%" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Rs./day</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtRsPerDay" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                            onblur="RsPerDayOnBlur();" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 50%" colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Fixed Rs.</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtFixedRs" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" 
                        onblur="FixedRsOnBlur();" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 50%" colspan="6">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Description</td>
                    <td style="width: 79%" colspan="4">
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="TEXTBOX"
                            MaxLength="250" Width="80%" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr id="TrIsActive" runat="server" visible="false">
                    <td class="TD1" style="width: 20%">
                        Is Active</td>
                    <td style="width: 29%">
                        <asp:CheckBox ID="chkIsActive" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TDMANDATORY" style="width: 50%" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" />
                        <asp:HiddenField ID="hdnExpenseHeadID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="Upd_Pnl_Bank" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
