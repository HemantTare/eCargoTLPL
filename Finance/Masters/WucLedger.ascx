<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedger.ascx.cs" Inherits="Finance_Masters_WucLadger" %>
<%@ Register Src="WucLedgerDivision.ascx" TagName="WucLedgerDivision" TagPrefix="uc1" %>
<%@ Register Src="WucLedgerGeneral.ascx" TagName="WucLedgerGeneral" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc3" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" language="javascript">
function Allow_To_Save()
{
    return true;
}
</script>
<%--<script language="javascript" type="text/javascript" >
function ValidtateUI()
{
   var ATS=false;
   var lbl_Errors = document.getElementById("<%=lbl_Errors.ClientID%>");
      
   WucLedger1_TB_Ledger.SelectTabById('zero');
   if(!ValidtateLedgerGeneral(WucLedger1_TB_Ledger,'zero'))
   {
     
   }
   else
   {
      ATS=true;

   }
 return ATS;
}
 
 
 function validTextBox(txt_ID,Msg)
 {
    if(Trim(txt_ID.value) == '')
    {  
       lbl_Errors.innerText = 'Please Enter'+' '+Msg;
       //tabControl.SelectTabById(ID);
       txt_ID.focus();  
       return false;
    }
    return true;
}

</script>
--%> 
<asp:ScriptManager ID="scm_Ledger" runat="server" />

<ComponentArt:TabStrip ID="TB_Ledger" SiteMapXmlFile="~/XML/FA_Mst_Ledger.xml"
    EnableViewState="False" MultiPageId="MP_Ledger" runat="server">
</ComponentArt:TabStrip>
<table class="TABLE">
  <tr>
    <td>
<ComponentArt:MultiPage ID="MP_Ledger" CssClass="MULTIPAGE" runat="server" Style="left: 0px;
    top: 1px"  >
     <ComponentArt:PageView  runat="server">
        <uc2:WucLedgerGeneral ID="WucLedgerGeneral1" runat="server" />
     </ComponentArt:PageView>
     
     <ComponentArt:PageView runat="server">
        <table  class="TABLE" width="100%" border="0">
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Name:</td>
        <td class="TD1" colspan="4" style="width: 77%">
            <asp:TextBox ID="txt_Name" runat="server" Width="100%" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="100"></asp:TextBox></td>
        <td class="TD1" style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Contact Person :</td>
        <td class="TD1" colspan="4" style="width: 77%">
            <asp:TextBox ID="txt_ContactPerson" runat="server" Width="100%" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox></td>
        <td class="TD1" style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            <uc3:WucAddress id="WucAddress1" runat="server">
            </uc3:WucAddress></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">Note :
        </td>
        <td class="TD1" colspan="4">
            <asp:TextBox ID="txt_Note" runat="server" Width="100%" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="250" Height="50px"></asp:TextBox></td>
        <td class="TD1" style="width: 1%;">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="text-align: center;" colspan="6">
            </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 24px">
           <asp:Label ID="Label1" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Red" EnableViewState="false"></asp:Label></td>
    </tr>
</table>
     </ComponentArt:PageView>
    
    
     <ComponentArt:PageView runat="server">
       <uc1:WucLedgerDivision id="WucLedgerDivision1" runat="server"></uc1:WucLedgerDivision>
     </ComponentArt:PageView>
 </ComponentArt:MultiPage>
 </td>
  </tr>
</table>



<table width="100%"  class="TABLE">
     
    <tr>
        <td align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"/>
        </td>
    </tr>
  <tr>
     <td>
            <asp:UpdatePanel ID="Upd_Ledger" runat="server" UpdateMode="always" >
            <ContentTemplate> 
	                <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel> 
    </td>
    </tr> 
</table>
