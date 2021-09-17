<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFilteration.ascx.cs"
    Inherits="Finance_Reports_WucFilteration" %>
<%@ Register Src="../../CommonControls/WucHierarchyFiltration_FA.ascx" TagName="WucHierarchyFiltration"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<asp:ScriptManager ID="scm_Filteration" runat="server">
</asp:ScriptManager>

<script src="../../Javascript/Common.js" type="text/javascript"></script>

<script type="text/javascript"> 

    function Open_Show_Window(Path)
    {        
        var dtFrom = WucFilteration1_Dtp_From_Picker.GetSelectedDate().toLocaleString();
        var dtTo = WucFilteration1_Dtp_To_Picker.GetSelectedDate().toLocaleString();
        var dtAsOn;
        if (document.getElementById('<%=tableRow_AsOnDt.ClientID%>').style.display == "inline") 
        {
            dtAsOn = WucFilteration1_Dtp_AsOnDate_Picker.GetSelectedDate().toLocaleString();
            
        }
        else
        {
            dtAsOn = null;
        }
        
        if (WucFilteration1_Dtp_From_Picker.GetSelectedDate() > WucFilteration1_Dtp_To_Picker.GetSelectedDate())
        {        
            alert("End Date Should Be Greater Than Start Date");
            return false;
        }
        else if (document.getElementById('<%=tableRow.ClientID%>').style.display == "inline")        
        {
            if (WucFilteration1_Dtp_To_Picker.GetSelectedDate() > WucFilteration1_Dtp_AsOnDate_Picker.GetSelectedDate())
            {                
                alert("End Date Should Be Less Than As On Date");
                return false;
            }
        }
//        else
//        {
            var queryString = '' ;

            var ddlLedgerGroup=document.getElementById('<%=ddl_LedgerGroupName.ClientID%>');
            var HierarchyCode = get_hierarchy_id() ;
            var AreaID = get_location_id();
            var IsConsol = get_IsConsol();
            var DivisionId = get_division_id();            
            var OnAccountType = null;
            if (document.getElementById('<%=tableRow_Receipt_Payment.ClientID%>').style.display == "inline")
            {
 	            var radioObj = document.getElementById ( "<%=rdl_Is_Receipt_Payment.ClientID %>" );	
 	            var radioList = radioObj.getElementsByTagName ( 'input' );	
  	            for ( var i = 0; i < radioList.length; i++)	
 	            {		
 	                if (radioList[i].checked)		
 	                {			
 	                    var OnAccountType = radioList[i].value;		
 	                }	
 	            }
 	         }
          var LedgerGroup
         if(ddlLedgerGroup!=null)
               LedgerGroup=ddlLedgerGroup.value
         else LedgerGroup=0
            queryString = Path + '&IsConsolidated=' + IsConsol + '&LedgerGroupId=' + LedgerGroup + '&HierarchyCode=' + HierarchyCode + '&Main_Id='+ AreaID + '&StartDate=' + dtFrom + '&EndDate='+ dtTo + '&Division_Id=' + DivisionId + '&AsOnDate='+ dtAsOn + '&OnAccountType='+ OnAccountType;
            
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-25);
        var popH = (h-75);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(queryString, 'FMainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes') ;
        return false;
        //}
    }
       
</script>
<form name="myform">
<table class="TABLE" width="80%">
    <tr>
        <td class="TDGRADIENT" colspan="6" style="width: 10%">
            &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="FILTERATION"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
    <td colspan="6">
    </td>
    </tr>
    
    <tr>
    <td colspan="6">
    </td>
    </tr>
    <tr>
        <td colspan="6">
          <uc2:WucHierarchyFiltration ID="WucHierarchyFiltration1" runat="server"/>
        </td>
    </tr>
     
    <tr>
        <td class="TD1" style="width: 7%">
        </td>
        <td style="width: 8%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr id="tableRow" runat="server">
        <td class="TD1" style="width: 13%">
            <asp:Label ID="lblFrom_Date" runat="server" Text="From Date :"></asp:Label></td>
        <td style="width: 22%">
            <uc1:WucDatePicker ID="Dtp_From" runat="server" />
        </td>
        <td class="TD1" style="width: 13%">
            <asp:Label ID="lblTo_Date" runat="server" Text="To Date :"></asp:Label></td>
        <td style="width: 22%">
            <uc1:WucDatePicker ID="Dtp_To" runat="server" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 13%">
        </td>
        <td class="TD1" style="width: 22%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr id="tableRow_AsOnDt" runat="server">
        <td class="TD1" style="width: 13%">
            <asp:Label ID="lbl_AsOnDate" runat="server" Text="As On Date :"></asp:Label></td>
        <td class="TD1" style="width: 22%">
            <uc1:WucDatePicker ID="Dtp_AsOnDate" runat="server" />
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 13%">
        </td>
        <td class="TD1" style="width: 22%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td style="width: 15%">
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 13%">
            <asp:Label ID="lbl_ledgerGroupName" Text="Ledger Group Name :" runat="server" CssClass="LABEL"></asp:Label>
        </td>
        <td class="TD1" style="width: 22%">
            <asp:UpdatePanel ID="UpLedgerGroup" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_LedgerGroupName" runat="server" CssClass="DROPDOWN">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%">
            <asp:Label ID="lbl_Select" runat="server" CssClass="LABEL" Text="Select :" Visible="false"></asp:Label></td>
        <td class="TD1" style="width: 15%">
            <asp:RadioButtonList ID="rdl_Is_Group_Ledger" runat="server" RepeatDirection="Horizontal" Visible="false">
                <asp:ListItem Selected="True" Value="1">GroupWise</asp:ListItem>
                <asp:ListItem Value="2">LedgerWise</asp:ListItem>
            </asp:RadioButtonList>
            </td>
        <td>
        </td>
        <td>
            </td>
    </tr>
    <tr id="tableRow_Receipt_Payment" runat="server">
        <td class="TD1" style="width: 13%">
        </td>
        <td class="TD1" style="width: 22%">
        </td>
        <td class="TD1" style="width: 1%">
            <asp:Label ID="lbl_Select_1" runat="server" CssClass="LABEL" Text="Select :" Visible="false"></asp:Label></td>
        <td class="TD1" style="width: 15%">
            <asp:RadioButtonList ID="rdl_Is_Receipt_Payment" runat="server" RepeatDirection="Horizontal" Visible="false">
                <asp:ListItem Selected="True" Value="1">Receipt</asp:ListItem>
                <asp:ListItem Value="2">Payment</asp:ListItem>
            </asp:RadioButtonList></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <fieldset id="fld_VoucherType" runat="server">
            <legend>Voucher Type :</legend>
            <asp:CheckBoxList ID="chklst_Voucher_Type" runat="server" RepeatColumns="5" Width="100%"></asp:CheckBoxList>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6" style="width: 10%">
            <asp:Button ID="btn_Show" runat="server" CssClass="BUTTON" Text="GO!" OnClick="btn_Show_Click"
                meta:resourcekey="btn_ShowResource1" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6" style="width: 10%">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" Width="100%" 
                meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </td>
    </tr>
</table>
</form> 