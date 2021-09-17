<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCBillingDetails.aspx.cs" Inherits="Operations_Booking_NewGC_FrmNewGCBillingDetails" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=CompanyManager.getCompanyParam().GcCaption%> Billing Details</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/Common.js"></script>
    <script type="text/javascript" src="../../../Javascript/ddlsearch.js"></script>

 <script type="text/javascript">
 
 function updateparentdataset(Is_ServiceTaxForBillingParty)
 { 
  window.opener.set_ServiceTaxForBillingParty(Is_ServiceTaxForBillingParty);
 } 
 function Allow_To_Exit()
 {
    var ATE = false;

    if (confirm("Do you want to Exit...")==false)
        ATE=false;
    else
    {
        window.close();
        ATE=true;
    }
    return ATE;
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM_GCBillingDetails" runat="server"></asp:ScriptManager>

    <div>
        <table class="TABLE" cellpadding="4">
        <tr>
            <td class="TDGRADIENT" colspan="3">&nbsp;<asp:Label ID="lbl_BillingDetails" runat="server" CssClass="HEADINGLABEL"></asp:Label></td>
        </tr>      
        <tr><td colspan="3">&nbsp;</td></tr>
       
        <tr>
            <td colspan="3" style="height: 77px">
                <asp:Panel ID="pnl_BillingDetails" runat="server" GroupingText="Billing Details" Font-Bold="True" Width="100%" CssClass="TABLE">
                    <asp:UpdatePanel ID="upd_dg_Billing" runat="server" UpdateMode="conditional">
                        <contenttemplate>
                            <table width="100%" border="0">
                                    <tr>
                                        <td style="width:100%" colspan="4">
                                            <asp:DataGrid ID="dg_Billing"  runat="server" CssClass="Grid" 
                                                OnCancelCommand="dg_Billing_CancelCommand" OnUpdateCommand="dg_Billing_UpdateCommand"
                                                OnEditCommand="dg_Billing_EditCommand"
                                                OnItemCommand="dg_Billing_ItemCommand" OnItemDataBound="dg_Billing_ItemDataBound"
                                                OnDeleteCommand="dg_Billing_DeleteCommand" AutoGenerateColumns="False" ShowFooter="True"
                                                PageSize="5" CellPadding="3" AllowSorting="True">
                                                <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                                <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS"></PagerStyle>
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                                <HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>
                                                <Columns>
                                                     <asp:TemplateColumn HeaderText="Billing Hierarchy  &nbsp;&nbsp; Billing Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <FooterTemplate>
                                                            <uc2:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />
                                                        </FooterTemplate>  
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "Hierarchy_Name") %>
                                                            &nbsp;&nbsp;&nbsp;
                                                            <%# DataBinder.Eval(Container.DataItem, "Billing_Branch_Name") %>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <uc2:WucHierarchyWithID ID="WucHierarchyWithID1"  runat="server" />                                    
                                                        </EditItemTemplate>                                                                                     
                                                    </asp:TemplateColumn> 
                                                    
                                                    <asp:TemplateColumn HeaderText="Billing Party" >
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <FooterTemplate>                                                 
                                                                <cc1:DDLSearch  ID="ddl_BillingParty" runat="server" IsCallBack="True" 
                                                                    CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetSearchBillingParty" 
                                                                    AllowNewText="True" PostBack="true" OnTxtChange="ddl_BillingParty_Changed"/>    
                                                                <asp:CheckBox ID="chk_Is_Service_Tax_Applicable" runat="server" Visible="false"/>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <%# (DataBinder.Eval(Container.DataItem, "Billing_Client_Name")) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                        <EditItemTemplate>                                                  
                                                              <cc1:DDLSearch  ID="ddl_BillingParty" runat="server" IsCallBack="True" 
                                                                CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetSearchBillingParty" 
                                                                  AllowNewText="True"  PostBack="true" OnTxtChange="ddl_BillingParty_Changed"/>
                                                              <asp:CheckBox ID="chk_Is_Service_Tax_Applicable" runat="server" Visible="false"/>
                                                        </EditItemTemplate>                                                    
                                                    </asp:TemplateColumn>                                         
                                                   
                                                    <asp:TemplateColumn HeaderText="Bill Ratio">
                                                        <FooterTemplate>
                                                            <asp:TextBox runat="server" Width="95%" CssClass="TEXTBOXNOS" ID="txt_Ratio"
                                                               onkeyPress="return Only_Numbers(this,event);"
                                                               MaxLength="3"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Bill_Ratio"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <EditItemTemplate>
                                                            <asp:TextBox runat="server" Width="95%" CssClass="TEXTBOXNOS"
                                                                ID="txt_Ratio" MaxLength="3" onkeyPress="return Only_Numbers(this,event);" ></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:TemplateColumn HeaderText="Description">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Description" runat="server" Width="98%" CssClass="TEXTBOX" 
                                                               MaxLength="100"  onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" TextMode="MultiLine" ></asp:TextBox>
                                                        </FooterTemplate>
                                                        <ItemTemplate>                                                        
                                                            <%# (DataBinder.Eval(Container.DataItem, "Description")) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Description" runat="server" Width="98%" CssClass="TEXTBOX" 
                                                               MaxLength="100" onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" TextMode="MultiLine"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateColumn>
                                                    
                                                    <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update" EditText="Edit">
                                                        <HeaderStyle Width="5%"></HeaderStyle>
                                                    </asp:EditCommandColumn>
                                                    <asp:TemplateColumn HeaderText="Delete">
                                                        <FooterTemplate>
                                                            <asp:LinkButton runat="server" ID="lbtn_Add_BillingDetails" Text="Add" CommandName="Add"></asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lbtn_Delete_BillingDetails" Text="Delete" CommandName="Delete" ></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%"></HeaderStyle>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 40%">&nbsp;</td>
                                        <td class="TD1" style="width: 20%" >
                                            <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Text="Total Ratio :" Font-Bold="True"></asp:Label>
                                        </td>                                       
                                        <td style="width: 20%" class="TD1">
                                            <asp:Label ID="lbl_TotalRatio" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalRatio" runat="server"></asp:HiddenField>
                                        </td>
                                        <td style="width: 20%" >&nbsp;</td>
                                    </tr>
                            </table>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Billing" />
                        </triggers>
                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <contenttemplate>
                        <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                     </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_Billing"></asp:AsyncPostBackTrigger>
                    </triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr align="left">
            <td align="center" colspan="3">
                &nbsp;<asp:Button ID="btn_Exit" runat="server" OnClientClick="return Allow_To_Exit()" OnClick="btn_Exit_Click" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdn_IsMultipleLocationBillingAllowed"></asp:HiddenField>
                <asp:HiddenField runat="server" ID="hdn_Is_Service_Tax_Applicable"></asp:HiddenField>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>