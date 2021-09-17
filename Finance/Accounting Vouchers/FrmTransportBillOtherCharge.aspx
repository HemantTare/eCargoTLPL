<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmTransportBillOtherCharge.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmTransportBillOtherCharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transport Bill Other Charge</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="scm_OtherCharges" runat="server"></asp:ScriptManager>
       <div>
       <table width="100%">
            <tr>
                <td style="width:100%">
                    <asp:UpdatePanel ID="upnl_dg_TransportOtherCharges" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <asp:DataGrid ID="dg_TransportOtherCharges" runat="server" AutoGenerateColumns="False"
                            CellPadding="3" CssClass="GRID" ShowFooter="True" Width="100%" OnItemCommand="dg_TransportOtherCharges_ItemCommand"
                            OnItemDataBound="dg_TransportOtherCharges_ItemDataBound" OnCancelCommand="dg_TransportOtherCharges_CancelCommand" 
                            OnDeleteCommand="dg_TransportOtherCharges_DeleteCommand" OnEditCommand="dg_TransportOtherCharges_EditCommand" 
                            OnUpdateCommand="dg_TransportOtherCharges_UpdateCommand">
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS"/>
                            <Columns>
                            <asp:TemplateColumn>
                                    <FooterTemplate>
                                        <asp:HiddenField ID="hdn_SrNo" Value='<%# DataBinder.Eval(Container.DataItem, "Sr_No")%>' runat="server"></asp:HiddenField>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdn_SrNo" Value='<%# DataBinder.Eval(Container.DataItem, "Sr_No")%>' runat="server"></asp:HiddenField>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hdn_SrNo" Value='<%# DataBinder.Eval(Container.DataItem, "Sr_No")%>' runat="server"></asp:HiddenField>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Other Charges Head">
                                    <FooterTemplate>
                                        <asp:DropDownList ID="ddl_OtherChargesHead" runat="server" CssClass="DROPDOWN"></asp:DropDownList>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "GC_Other_Charge_Head")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_OtherChargesHead" runat="server" CssClass="DROPDOWN"></asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Description">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="100">
                                        </asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Description" runat="server" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="100">
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                            MaxLength="10" onkeypress="return Only_Numbers(this,event)"/>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Amount") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                            MaxLength="10" onkeypress="return Only_Numbers(this,event)"/>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit" UpdateText="Update">
                                </asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Delete">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtn_Add" runat="server" CommandName="Add" Text="Add">
                                        </asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_Delete" runat="server" CommandName="Delete" Text="Delete">
                                        </asp:LinkButton></ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>                      
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dg_TransportOtherCharges" />
                    </Triggers>
                    </asp:UpdatePanel>
               </td>
            </tr>
           <tr>
                <td style="width:100%">
                    <asp:UpdatePanel ID="up_lbl_Errors" runat="server" UpdateMode="conditional">
                        <ContentTemplate>
                            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_TransportOtherCharges" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="3" style="text-align: center">
                &nbsp;<asp:Button ID="btn_Exit" runat="server" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" OnClick="btn_Exit_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:HiddenField ID="hdn_IsFrom_GC" runat="server" />
                </td>
            </tr>
            </table>
        </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">

    function updateparentdataset()
    {
        var hdn_IsFrom_GC = document.getElementById('<%=hdn_IsFrom_GC.ClientID%>');

        if(hdn_IsFrom_GC.value == "false")
        {
          window.opener.update_grid_GCDetails();
        }
        else
        {
          window.opener.update_freightDetails();
        }
    }
//    function updatefreightdataset()
//    {
//     window.opener.update_freightDetails();
//    }

//    function updateparentdataset()
//    { 
//    window.opener.update_grid_GCDetails();
//    }

</script>