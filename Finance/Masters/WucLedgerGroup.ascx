<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedgerGroup.ascx.cs" Inherits="Finance_Masters_WucLedgerGroup" %>
 
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager> 
 <script type ="text/javascript" language ="javascript" >
function Check()
{
var a= document.getElementById('<%=ddl_Parent_Ldg_Grp.ClientID%>');
var a_text = a.options[a.selectedIndex].text;

var b= document.getElementById('<%=ddl_Nature.ClientID%>');
var c= document.getElementById('<%=lbl_Nature.ClientID%>');

if (a_text=='Primary')
{
b.style.visibility='visible';
c.style.visibility='visible';
}
else
{
b.style.visibility='hidden';
c.style.visibility='hidden';  
}

Check_Gross_Profit();
}
function Check_Gross_Profit()
{
var a= document.getElementById('<%=ddl_Parent_Ldg_Grp.ClientID%>');
var a_text = a.options[a.selectedIndex].text;

var ddl_nature= document.getElementById('<%=ddl_Nature.ClientID%>');
var nature_text = ddl_nature.options[ddl_nature.selectedIndex].text;

var gross_profit=document.getElementById('<%=Chk_Gross_Profit.ClientID%>'); 
var lbl_gross_profit= document.getElementById('<%=lbl_affect_gross_profit.ClientID%>');

if (a_text=='Primary' && (nature_text=='Expenses' || nature_text=='Income'))
{
gross_profit.style.visibility='visible';
lbl_gross_profit.style.visibility='visible';
}
else
{
gross_profit.style.visibility='hidden'
lbl_gross_profit.style.visibility='hidden';
}
}
function Allow_To_Save()
{
    return true;
}
</script> 
<table  class="TABLE">
    <tr>
        <td colspan="6" class="TDGRADIENT" >
            &nbsp;
            <asp:Label ID="lbl_Add_Ledger_Grp" runat="server" CssClass="HEADINGLABEL"  Text="LEDGER GROUP"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6" class="TDUnderline">
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Ledger Group Name:</td>
        <td class="TD1" style="width: 29%">
            <asp:TextBox ID="txt_Ledger_Gr_Name" runat="server" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="100"></asp:TextBox></td>
        <td style="width: 12px" class="TDMANDATORY">*
           </td>
        <td class="TD1" style="width: 20%">
            Alias:</td>
        <td class="TD1" style="width: 29%">
            <asp:TextBox ID="txt_Alias" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox></td>
        <td class="TD1" style="width: 1%">
            <asp:CompareValidator ID="CV_Alias" runat="server" ControlToCompare="txt_Ledger_Gr_Name"
                ControlToValidate="txt_Alias" ErrorMessage="*" Operator="NotEqual" ToolTip="Must be different than Ledger Group Name"></asp:CompareValidator></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 12px">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td  class="TD1" style="width: 20%">
            Under:</td>
        <td  class="TD1" style="width: 29%">
            <asp:DropDownList ID="ddl_Parent_Ldg_Grp"   runat="server"  CssClass="DROPDOWN"  onchange="Check()">
            </asp:DropDownList> </td>
        <td  class="TD1" style="width: 12px">
            <asp:RequiredFieldValidator ID="rfv_Parent_Ldg_Grp" runat="server" ErrorMessage="*" ValidationGroup="Save" ControlToValidate="ddl_Parent_Ldg_Grp"></asp:RequiredFieldValidator></td>
        <td  class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Nature" runat="server" CssClass="LABEL" Text="Nature:"></asp:Label></td>
        <td  class="TD1" style="width: 29%">
            <asp:DropDownList ID="ddl_Nature" runat="server" CssClass="DROPDOWN" onchange ="Check_Gross_Profit()">
            </asp:DropDownList></td>
        <td   class="TD1" style="width: 1%">
            <asp:RequiredFieldValidator ID="RFV_Nature" runat="server" ErrorMessage="*" ControlToValidate="ddl_Nature" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 12px">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Index No:</td>
        <td class="TD1" style="width: 29%" align="left"  >
            <asp:TextBox ID="txt_Index_no" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox></td>
        <td class="TD1" style="width: 12px">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_affect_gross_profit" runat="server" Text="Affect Gross Profit:" CssClass="LABEL" Visible ="true" ></asp:Label></td>
        <td  style="width: 29%">
            <asp:CheckBox ID="Chk_Gross_Profit" runat="server" Visible="true" /></td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 12px">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            </td>
        <td class="TD1" style="width: 29%; text-align: left;">
            </td>
        <td class="TD1" style="width: 12px" >
            </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%" >
            </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 12px" >
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="text-align: center;" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" Text="Save" ValidationGroup="Save" ToolTip="Click here to Add New Ledger Group" OnClick="btn_save_Click" /></td>
    </tr>
    <tr>
        <td colspan="6" style="height: 24px">
        <asp:UpdatePanel ID="up_error" runat="server">
        <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" EnableViewState="false">
                </asp:Label>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
    </tr>
</table>
<script type="text/javascript">
    Check();
    Check_Gross_Profit()
</script>
