<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDateSettings.ascx.cs" Inherits="Administration_Rights_WucDateSettings" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Administration/Rights/DateSettings.js"></script>

<asp:ScriptManager ID="scm_DateSetting" runat="server">
</asp:ScriptManager>

<table width="100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="10">
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="DATE SETTINGS"></asp:Label>
        </td>        
    </tr>  
    
     <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 29%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 29%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 20%">
            Process Name:</td>
        <td style="width: 29%" colspan="3">
        
        <asp:TextBox ID="txt_ProcessName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"></asp:TextBox>
         
        </td>
        <td class="TDMANDATORY" style="width: 1%">*
        </td>
        
        <td style="width:50%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Code:</td>
        <td style="width: 29%" colspan="3">
        
        <asp:TextBox ID="Txt_code" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50"></asp:TextBox>
         
        </td>
        <td  class="TDMANDATORY" style="width: 1%">*
        </td>
        
        <td style="width:50%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Minimum Hrs:</td>
        <td style="width: 29%" colspan="3">
        
        <asp:TextBox ID="txt_MinHrs" runat="server" BorderWidth="1px"  onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                MaxLength="50"></asp:TextBox>
         
        </td>
        <td  style="width: 1%">
        </td>
        
        <td style="width:50%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Maximum Hrs:</td>
        <td style="width: 29%" colspan="3">
        
        <asp:TextBox ID="txt_MaxHrs" runat="server" BorderWidth="1px"  onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                MaxLength="50"></asp:TextBox>
         
        </td>
        <td  style="width: 1%">
        </td>
        
        <td style="width:50%" />
    </tr>
    <tr>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" /></td>
    </tr>
   <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_DateSetting" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" ></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    </table>      