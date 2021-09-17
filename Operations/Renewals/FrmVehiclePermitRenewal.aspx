<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehiclePermitRenewal.aspx.cs" Inherits="Operations_Renewals_FrmVehiclePermitRenewal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Transactions/Renewals/PermitRenewal.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Vehicle Permit Renewal</title>
   <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_PermitRenewal" runat="server"></asp:ScriptManager>
    <div>
    <table style="width: 100%" class="TABLE ">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="Permit Renewal"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_RenewalNo" runat="Server" Text="Permit Renewal No:" ></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:Label ID="lbl_Permit_Renewal_No" runat="server" CssClass="TEXTBOX" Font-Bold="True"></asp:Label></td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1"><asp:Label ID="lbl_Date" runat="Server" Text="Permit Renewal Date:" ></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="WucPermitDate" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1"><asp:Label ID="lbl_VehicleNo" runat="Server" Text="Vehicle No:"></asp:Label></td>
        <td style="width: 29%">
            <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_PermitType" runat="server" Text="Permit Type:"></asp:Label>
        </td>
        <td style="width: 29%">
        <asp:UpdatePanel ID="Upd_Pnl_ddl_Permit_Type" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
        </Triggers>
        <ContentTemplate>
            <asp:DropDownList ID="ddl_Permit_Type" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Permit_Type_SelectedIndexChanged">
            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
           <asp:Label ID="lbl_PermitNo" runat="Server" Text="Permit No:"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox ID="txt_Permit_No" runat="server" MaxLength="50" Width="99%" CssClass="TEXTBOX"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1"><asp:Label ID="lbl_DocumentNo" runat="Server" Text="Document No:"></asp:Label></td>
        <td style="width: 29%"><asp:TextBox ID="txt_Document_No" runat="server" MaxLength="50" Width="97%" CssClass="TEXTBOX"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
           <asp:Label ID="lbl_ValidFrom" runat="Server" Text="Valid From:"></asp:Label>
        </td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_Valid_From" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1"><asp:Label ID="lbl_ValidUpTo" runat="Server" Text="Valid UpTo:"></asp:Label></td>
        <td style="width: 29%">
            <uc1:WucDatePicker ID="Wuc_Valid_Upto" runat="server" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td colspan="6">
          <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td >
                    <asp:Panel ID="pnl_Grid" runat="server" GroupingText="Permit States">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td>
                <asp:UpdatePanel ID="Upd_Pnl_Permit_States" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                        <asp:AsyncPostBackTrigger ControlID="ddl_Permit_Type" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:DataGrid ID="dg_Permit_States" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                            CssClass="GRID" Width="100%" OnEditCommand="dg_Permit_States_EditCommand" OnItemDataBound="dg_Permit_States_ItemDataBound" OnCancelCommand="dg_Permit_States_CancelCommand" OnUpdateCommand="dg_Permit_States_UpdateCommand" OnDeleteCommand="dg_Permit_States_DeleteCommand" OnItemCommand="dg_Permit_States_ItemCommand">
                             <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />                          
                            <Columns>
                            <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_State_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"State_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn  HeaderText="State Name"  >
                                    <ItemTemplate>
                                      <asp:Label ID="lbl_State_Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "State_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddl_Permit_State" runat="server" CssClass="DROPDOWN"/>
                                    </EditItemTemplate>
                                      <FooterTemplate >
                                        <asp:DropDownList ID="ddl_Permit_State" runat="server" CssClass="DROPDOWN"/>
                                    </FooterTemplate>
                                    <HeaderStyle CssClass="GRIDHEADERCSS" Width="70%" />
                                    <ItemStyle Width="70%" />
                                </asp:TemplateColumn>
                                   <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="Edit">
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                </asp:EditCommandColumn>
                                 <asp:TemplateColumn HeaderText="Add/Delete">
                                  <ItemTemplate>
                                 <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete"></asp:LinkButton>
                                 </ItemTemplate>
                            <FooterTemplate>
                            <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add"></asp:LinkButton>
                            </FooterTemplate>
                            <HeaderStyle CssClass="GRIDHEADERCSS"  />
                            </asp:TemplateColumn>  
                            </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                            </asp:UpdatePanel>                        
                          </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON"  Text="Save" OnClientClick="return validateUI();" OnClick="btn_save_Click" /></td>
    </tr>
   <tr>
        <td colspan="6" >
            <asp:UpdatePanel ID="Upd_Pnl_PermitRenewal" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_save" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Permit_States" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdn_Is_Temporary_Permit" runat="server" />
            <asp:HiddenField ID="hdn_Valid_From" runat="server" />
            &nbsp;         
        </td>
    </tr>  
</table>
    </div>
    </form>
</body>
</html>
