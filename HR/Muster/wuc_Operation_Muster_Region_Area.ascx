<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Operation_Muster_Region_Area.ascx.cs" Inherits="Operations_Muster_wuc_Operation_Muster_Region_Area" %>

<%@Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc1" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>


<script type="text/javascript">
  
  function OpenPopUp()
  {
 
     var lbl_Errors =  document.getElementById('<%=lbl_Errors.ClientID%>'); 
    var Hierarchy_Code =  document.getElementById('<%=hdn_HCode.ClientID%>').value;
    var Main_Id =  document.getElementById('<%=hdn_Mainid.ClientID%>').value;
    var PayDivId =  document.getElementById('<%=ddl_division.ClientID%>').value;
    lbl_Errors.innerText='';
    if(Hierarchy_Code.value == '')
    {
        lbl_Errors.innerText='Please Select Hierarchy';
       return false;
    }
//    else if(Main_Id.value=='')
//    {
//        lbl_Errors.innerText='Please Select Hierarchy';
//       return false;
//    }
//    else (PayDivId == '0')
//    {
//        lbl_Errors.innerText='Please Select Division';
//       return false;
//    }
  

        
         var Path='../Muster/frm_Operation_Muster_Daily_Entry.aspx?Hierarchy_Code='+Hierarchy_Code+'&Main_Id='+Main_Id+'&PayDivId='+PayDivId;
       
        var w = screen.availWidth;;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
       
        window.open(Path, 'MainPopUp2', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
  
  }

  function Open_Popup_Window(Hierarchy_Code,Main_Id,Day)
    {

        var Path='../Muster/frm_Operation_Muster_Daily_Entry.aspx?Hierarchy_Code='+Hierarchy_Code+'&Main_Id='+Main_Id+'&Pay_Div_Id='+Is_VT;
       
        var w = screen.availWidth;;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
       
        window.open(Path, 'MainPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
        
    }

</script>


<table class="TABLE">
<asp:ScriptManager ID="ScriptManager1" runat="server" /> 
    <tr>
        <td class="TDGRADIENT" colspan="6" style="width: 100%">
            &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="MUSTER DAILY ENTRY"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <uc1:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />
                      
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        Division :
        </td>
        <td style="width: 29%">
        <asp:DropDownList ID="ddl_division" CssClass="DROPDOWN" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_division_SelectedIndexChanged"/>

        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 29%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>

        
    <tr>
        <td align="center" colspan="6" style="width: 100%">
            <asp:Button ID="btn_Show" runat="server" CssClass="BUTTON" Text="GO!" OnClientClick="return OpenPopUp()"
            
               /> <%--OnClick="btn_Show_Click"--%>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <asp:HiddenField ID="hdn_HCode" runat="server"  Value="0"/> 
                </ContentTemplate>
                  <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />
                  </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
            <asp:HiddenField ID="hdn_Mainid" runat="server" Value="0"/>
                </ContentTemplate>
                   <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />
                  </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
            <asp:HiddenField ID="hdn_Pay_Div_ID" runat="server" Value="0"/>
                </ContentTemplate>
                   <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_division" />
                  </Triggers>
            </asp:UpdatePanel>
            &nbsp;&nbsp;
         
          
        </td>
    </tr>
    <tr>
        
        <td colspan="6">
            &nbsp;<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
         
                    <asp:Label runat="server" ID="lbl_Errors" CssClass="LABELERROR" EnableViewState="false"></asp:Label>
                </ContentTemplate>
                   <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />
                  </Triggers>
            </asp:UpdatePanel>
         
        </td>
    </tr>
</table>
