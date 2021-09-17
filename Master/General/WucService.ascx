<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucService.ascx.cs" Inherits="Master_Work_Order_WucService" %>
<script language="javascript" type="text/javascript" src="../../JavaScript/Master/General/Service.js"></script>
 <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
 
<asp:ScriptManager ID="scm_Service" runat="server" />

<table class="TABLE">
    <tr>
      <td class="TDGRADIENT" colspan="8">&nbsp;
        <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="SERVICE" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
      </td>
     </tr>
    <tr>
      <td colspan="8">&nbsp;</td>
    </tr>
    
   <tr>
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_ServiceName" Text="Service Name:" runat="Server" meta:resourcekey="lbl_ServiceNameResource1"></asp:Label></td>
     <td style="width:27%">
        <asp:TextBox ID="txt_ServiceName" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="50" meta:resourcekey="txt_ServiceNameResource1"></asp:TextBox></td>
     <td style="width: 2%"></td>
     <td class="TDMANDATORY"  style="width: 1%">*</td>
  
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_ServiceCategory" Text="Service Category:" runat="Server" meta:resourcekey="lbl_ServiceCategoryResource1"></asp:Label></td>
     <td style="width:27%">
        <asp:DropDownList ID="ddl_ServiceCategory" runat="server" CssClass ="DROPDOWN" Width="100%" meta:resourcekey="ddl_ServiceCategoryResource1"/></td>
     <td style="width: 2%"></td>
     <td class="TDMANDATORY"  style="width: 1%">*</td>
   </tr>
   <tr class="HIDEGRIDCOL">
   
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_ParentService" runat="Server" Text="Parent Service:" meta:resourcekey="lbl_ParentServiceResource1"></asp:Label></td>
     <td style="width:27%">
        <asp:DropDownList ID="ddl_ParentService" runat="server" CssClass ="DROPDOWN" Width="100%" meta:resourcekey="ddl_ParentServiceResource1"/></td>
     <td style="width: 2%"></td>
     <td class="TDMANDATORY"  style="width: 1%"></td>
     <td class="TD1" style="width:50%" colspan="4"></td>
   </tr>
   <tr class="HIDEGRIDCOL">
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_EstimatedCheckingTime" Text="Estimated Cheking Time:" runat="Server" meta:resourcekey="lbl_EstimatedCheckingTimeResource1"></asp:Label></td>
     <td style="width:27%">
        <asp:TextBox ID="txt_ChekingTime" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="10" 
            onkeyup="return Validate_Time(this)" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_ChekingTimeResource1"></asp:TextBox></td>
     <td style="width: 2%"> <asp:Label ID="lbl_Hrs" Text="Hrs." runat="Server" meta:resourcekey="lbl_HrsResource1"></asp:Label></td>
     <td class="TDMANDATORY"  style="width: 1%">*</td>
  
     <td class="TD1" style="width:20%"><asp:Label ID="lbl_EstimatedRepairTime" runat="Server" Text="Estimated Repair Time:" meta:resourcekey="lbl_EstimatedRepairTimeResource1"></asp:Label></td>
     <td style="width:26%">
        <asp:TextBox ID="txt_RepairTime" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="10" 
        onkeyup="return Validate_Time(this)" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_RepairTimeResource1"></asp:TextBox></td>
     <td style="width: 2%"><asp:Label ID="lbl_Hrs1" Text="Hrs." runat="server" meta:resourcekey="lbl_Hrs1Resource1"></asp:Label></td>
     <td class="TDMANDATORY" style="width: 1%">*</td>
   </tr>
   <tr>
   
     <td class="TD1" style="width:20%;vertical-align:top;"><asp:Label ID="lbl_ServiceDescription" runat="Server" Text="Service Description:" meta:resourcekey="lbl_ServiceDescriptionResource1"></asp:Label></td>
     <td style="width:77%" colspan="5">
        <asp:TextBox ID="txt_Description" runat="server" CssClass ="TEXTBOX" BorderWidth="1px" MaxLength="255" Height="60px" TextMode="MultiLine" Width="100%" meta:resourcekey="txt_DescriptionResource1"/></td>
     <td class="TDMANDATORY"  style="width: 2%"></td>
     <td class="TDMANDATORY"  style="width: 1%"></td>
   </tr>
    <tr>
      <td colspan="8">&nbsp;</td>
    </tr>
    
     <tr>
          <td class="TD1" style="width:20%">&nbsp;</td>
      <td colspan="7">&nbsp;<asp:DataGrid ID="dg_ServiceTask" runat="server" AutoGenerateColumns="False"
              CellPadding="3" CssClass="Grid" 
              OnCancelCommand="dg_ServiceTask_CancelCommand" OnDeleteCommand="dg_ServiceTask_DeleteCommand"
              OnEditCommand="dg_ServiceTask_EditCommand" OnItemCommand="dg_ServiceTask_ItemCommand"
              OnItemDataBound="dg_ServiceTask_ItemDataBound" OnUpdateCommand="dg_ServiceTask_UpdateCommand"
              ShowFooter="True">
              <ItemStyle HorizontalAlign="Left" />
              <HeaderStyle BackColor="#D6D7E1" BorderColor="#9495A2" BorderStyle="Solid" BorderWidth="1px"
                  CssClass="DataGridFixedHeader" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                  ForeColor="Black" Height="15px" HorizontalAlign="Left" VerticalAlign="Bottom" />
              <Columns>
                  <asp:TemplateColumn HeaderText="Sr.No.">
                      <ItemTemplate>
                          <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Right" />
                      <HeaderStyle HorizontalAlign="Right" Width="5%" />
                  </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderText="Service Task">
                      <FooterTemplate>
                          <asp:DropDownList ID="ddl_ServiceTask" runat="server" CssClass="DROPDOWN" >
                          </asp:DropDownList>
                      </FooterTemplate>
                      <ItemTemplate>
                          <%# DataBinder.Eval(Container.DataItem, "ServiceTask_Name") %>
                      </ItemTemplate>
                      <EditItemTemplate>
                          <asp:DropDownList ID="ddl_ServiceTask" runat="server" CssClass="DROPDOWN" >
                          </asp:DropDownList>
                      </EditItemTemplate>
                      <HeaderStyle Width="60%" />
                  </asp:TemplateColumn>
                  <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit" 
                      UpdateText="Update">
                      <HeaderStyle Width="10%" />
                  </asp:EditCommandColumn>
                  <asp:TemplateColumn HeaderText="Delete">
                      <FooterTemplate>
                          <asp:LinkButton ID="lbtn_Add" runat="server" CommandName="Add" 
                              Text="Add"></asp:LinkButton>
                      </FooterTemplate>
                      <ItemTemplate>
                          <asp:LinkButton ID="lbtn_Delete" runat="server" CommandName="Delete" 
                              Text="Delete"></asp:LinkButton></ItemTemplate>
                      <HeaderStyle Width="10%" />
                  </asp:TemplateColumn>
              </Columns>
          </asp:DataGrid></td>
    </tr>
    
    
     <tr>
      <td colspan="8">&nbsp;</td>
    </tr>
    <tr>
      <td align="center" colspan="8">
        <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  meta:resourcekey="btn_SaveResource1"/>
      </td>
    </tr>
    
    <tr>
      <td colspan="8">
        <asp:UpdatePanel ID="Upd_Pnl_Service" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
          </Triggers>
          <ContentTemplate>
            <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
      </td>
    </tr>
 </table>