<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRegistrationPermit.ascx.cs" Inherits="Master_Vehicle_WucRegistrationPermit" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"  TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<script type="text/javascript">
function newwindow(Main_SrNo,ddl_state,StateName,txt_permit_number,dtpValidFrom,dtpValidUpto,Is_From_Footer)
{
var Path='';
var StateID;
var StateName;
var PermitNumber;
var PermitValidFrom;
var PermitValidUpTo;


if (Is_From_Footer==1)
{
    ddl_state = document.getElementById(ddl_state);
    txt_permit_number = document.getElementById(txt_permit_number);
    
    dtpValidFrom = dtpValidFrom.GetSelectedDate();
    dtpValidFrom = new Date(dtpValidFrom.getFullYear(),dtpValidFrom.getMonth(),dtpValidFrom.getDate());
    PermitValidFrom = dtpValidFrom.toDateString();
    
    
    dtpValidUpto = dtpValidUpto.GetSelectedDate();
    dtpValidUpto = new Date(dtpValidUpto.getFullYear(),dtpValidUpto.getMonth(),dtpValidUpto.getDate());
    PermitValidUpTo = dtpValidUpto.toDateString();
    
    if (ddl_state.value ==0 || ddl_state.options.length == 0)
    {
        alert("Please Select State");
        ddl_state.focus();
        return false;
    }
    var StateID = ddl_state.value;
    var StateName = ddl_state.options[ddl_state.selectedIndex].text;
    var PermitNumber = txt_permit_number.value;
}
else
{
  StateID = ddl_state;
  PermitNumber = txt_permit_number;
  PermitValidFrom = dtpValidFrom;
  PermitValidUpTo = dtpValidUpto;
}

if (parseInt(Main_SrNo) == 0) // footer
{
  hdn_Main_SrNo = document.getElementById('<%=hdn_Main_SrNo.ClientID%>');
  Main_SrNo = hdn_Main_SrNo.value;
}

Path = 'FrmTemporaryPermitTaxDetails.aspx?SrNo=' + Main_SrNo + 
       '&StateID=' + StateID + 
       '&StateName=' + StateName +
       '&PermitNumber=' + PermitNumber + 
       '&PermitValidFrom=' + PermitValidFrom + 
       '&PermitValidUpTo=' + PermitValidUpTo  


var w = screen.availWidth;
var h = screen.availHeight;
var popW = 700;
var popH = 200;
var leftPos = (w-popW)/2;
var topPos = (h-popH)/1.3;

window.open(Path, 'CustomPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
}
</script>
<%--<asp:ScriptManager ID="scm_VehicleRegistrationPermit" runat="server" />--%>


<table class="TABLE" style="width: 100%">
   
    <tr>
        <td colspan="6">&nbsp;</td> 
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>        
         <td>
            <asp:Panel ID="pnl_Permits" runat="server" GroupingText="Registration Permit Details" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_RegistrationPermitDetailsResource1">
            <table cellpadding="3" cellspacing="3" border="0" style="width: 100%">
          
                <tr>
                    <td style="width: 100%" colspan="6">
                       
                            <table width="100%">
                             
                              
                                <tr>
                                    <td style="width: 20%" class="TD1">
                                     <asp:Label ID="lbl_Permit_Type"  runat="server" Text="Permit Type :" meta:resourcekey="lbl_Permit_TypeResource1"/>
                                    </td>
                                    <td style="width: 29%"><asp:DropDownList ID="ddl_PermitType" runat="server"  Width="100%" CssClass="DROPDOWN" meta:resourcekey="ddl_PermitTypeResource1"/></td>
                                    <td style="width: 1%" class="TDMANDATORY">*</td>
                                    <td style="width: 20%"></td>
                                    <td style="width: 29%"></td>
                                    <td style="width: 1%"></td>
                                </tr>
                                <tr>
                                  <td style="width:20%" class="TD1">
                                  <asp:Label ID="lbl_Permit_No"  runat="server" Text="Permit No :" meta:resourcekey="lbl_Permit_NoResource1"/>
                                  </td>
                                  <td style="width:29%" class="TD1"><asp:TextBox ID="txt_RegPermitNo" runat="server" Width="99%" CssClass="TEXTBOX" MaxLength="25" meta:resourcekey="txt_RegPermitNoResource1"></asp:TextBox></td>
                                  <td class="TDMANDATORY" style="width:1%">*</td>
                                  <td style="width:20%" class="TD1">
                                   <asp:Label ID="lbl_Document_No"  runat="server" Text="Document No :" meta:resourcekey="lbl_Document_NoResource1"/>
                                  
                                  </td>
                                  <td style="width:29%" class="TD1"><asp:TextBox ID="txt_DocumentNo" runat="server" CssClass="TEXTBOX" MaxLength="25" meta:resourcekey="txt_DocumentNoResource1"></asp:TextBox></td>
                                  <td class="TDMANDATORY" style="width:1%"></td>
                                </tr>
                                <tr>
                                     <td style="width:20%" class="TD1">
                                      <asp:Label ID="lbl_Valid_From"  runat="server" Text="Valid From :" meta:resourcekey="lbl_Valid_FromResource1"/>
                                     
                                     </td>
                                     <td style="width:29%"><uc1:wuc_Date_Picker ID="dtp_RegPermitValidFrom" runat="server" /></td>
                                     <td class="TDMANDATORY" style="width:1%"></td>
                                     <td style="width:20%" class="TD1">
                                     <asp:Label ID="lbl_Valid_UpTo"  runat="server" Text="Valid UpTo :" meta:resourcekey="lbl_Valid_UpToResource1"/>
                                     
                                     </td>
                                     <td style="width:29%"><uc1:wuc_Date_Picker ID="dtp_RegPermitValidUpTo" runat="server" /></td>
                                     <td class="TDMANDATORY" style="width:1%"></td>
                                </tr>
                                <tr>
                                 <td colspan="6">
                                 </td>
                                </tr>
                                <tr>
                                    <td colspan="6" style="width: 100%">
                                        <div id="Div1" runat="server" class="DIV">
                                            <asp:UpdatePanel ID="UpDatePanel_PegistrationPermit" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Dg_RegistrationPermit" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="Dg_RegistrationPermit" runat="server" ShowFooter="True"
                                                        AutoGenerateColumns="False" CssClass="GRID"
                                                        Width="97%" OnItemDataBound="Dg_RegistrationPermit_ItemDataBound"
                                                        OnItemCommand="Dg_RegistrationPermit_ItemCommand"
                                                        OnEditCommand="Dg_RegistrationPermit_EditCommand"
                                                        OnUpdateCommand="Dg_RegistrationPermit_UpdateCommand"
                                                        OnCancelCommand="Dg_RegistrationPermit_CancelCommand"
                                                        OnDeleteCommand="Dg_RegistrationPermit_DeleteCommand"
                                                        meta:resourcekey="Dg_RegistrationPermitResource1">
                                                        
                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Sr.No" Visible="False">
                                                                <ItemTemplate>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="State">
                                                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "PermitStateName") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:DropDownList ID="ddl_PermitState" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_PermitStateResource1"/>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:DropDownList ID="ddl_PermitState" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_PermitStateResource2"/>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Receipt No">
                                                                <ItemStyle  Width="10%" />
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ReceiptNo") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_PermitReceiptNo" Width="90%" MaxLength="20" runat="server"  CssClass="TEXTBOX" meta:resourcekey="txt_PermitReceiptNoResource1"/>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_PermitReceiptNo" Width="90%" MaxLength="20" runat="server"  CssClass="TEXTBOX" meta:resourcekey="txt_PermitReceiptNoResource2"/>
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateColumn>
                                                             <asp:TemplateColumn HeaderText="Tax Amount">
                                                                <ItemStyle Width="10%" HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "TaxAmount") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_PermitTaxAmount" MaxLength="18" Width="90%" onkeypress="return Only_Integers(this,event)" runat="server" CssClass="TEXTBOXNOS" meta:resourcekey="txt_PermitTaxAmountResource1"/>
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_PermitTaxAmount" MaxLength="18" Width="90%" onkeypress="return Only_Integers(this,event)" runat="server" CssClass="TEXTBOXNOS" meta:resourcekey="txt_PermitTaxAmountResource2"/>
                                                                </EditItemTemplate>
                                                                 <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Valid From">
                                                               <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:MM/dd/yyyy}") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_PermitValidFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                                        ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                                        MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidFromResource1"  />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_PermitValidFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                                        ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                                        MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidFromResource2"   />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateColumn>
                                                           
                                                            <asp:TemplateColumn HeaderText="Valid Upto">
                                                               <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "ValidUpto", "{0:MM/dd/yyyy}") %>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_PermitValidUpto" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                                        ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                                        MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidUptoResource1"  />
                                                                </FooterTemplate>
                                                                <EditItemTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_PermitValidUpto" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                                        ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                                        MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidUptoResource2"   />
                                                                </EditItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="Edit" meta:resourcekey="EditCommandColumnResource1">
                                                                <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                            </asp:EditCommandColumn>
                                                            <asp:TemplateColumn HeaderText="Add/Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete" meta:resourcekey="lbtn_DeleteResource1"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtn_Add" CommandName="Add" runat="server" Text="Add" meta:resourcekey="lbtn_AddResource1"></asp:LinkButton>
                                                                </FooterTemplate>
                                                                <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                       
                    </td>
                </tr>
    

    <tr>
       <td style="width:20%" class="TD1">
       <asp:Label ID="lbl_PermitType" ForeColor="Black" runat="server" Text="Permit Type :" meta:resourcekey="lbl_PermitTypeResource1"></asp:Label>
       </td>
       <td style="width:29%;text-align:left">
        <asp:Label ID="lbl_TampararyPermit" ForeColor="Black" runat="server"  Font-Bold="True" Text=" Temporary Permit" meta:resourcekey="lbl_TampararyPermitResource1"></asp:Label>
       </td>
       <td style="width:1%"></td>
       <td style="width:50%" colspan="3"></td>
    </tr>
    <tr>
      <td colspan="6">
        <table style="width: 100%">
            <tr>
                <td colspan="6" style="width: 100%;">
                    <div id="Div2" runat="server" class="DIV">
                        <asp:UpdatePanel ID="UpDatePanel_TemparayPermit" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Dg_TemparayRegistrationPermit" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DataGrid ID="Dg_TemparayRegistrationPermit" runat="server" 
                                    ShowFooter="True" CssClass="GRID"
                                    AutoGenerateColumns="False" Width="97%"
                                    OnItemDataBound="Dg_TemparayRegistrationPermit_ItemDataBound"
                                    OnItemCommand="Dg_TemparayRegistrationPermit_ItemCommand"
                                    OnEditCommand="Dg_TemparayRegistrationPermit_EditCommand"
                                    OnUpdateCommand="Dg_TemparayRegistrationPermit_UpdateCommand"
                                    OnCancelCommand="Dg_TemparayRegistrationPermit_CancelCommand" OnDeleteCommand="Dg_TemparayRegistrationPermit_DeleteCommand" meta:resourcekey="Dg_TemparayRegistrationPermitResource1">

                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Sr.No"  Visible="False">
                                           <ItemStyle Width="5%" />
                                                <ItemTemplate>
                                                </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="State">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PermitStateName") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_PermitState" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_PermitStateResource3"/>
                                                 <asp:HiddenField ID="Hdn_MainSrNo" runat="server" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_PermitState" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_PermitStateResource4"/>
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="15%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Permit No">
                                           <ItemStyle Width="10%"  />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "PermitNo") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_PermitNo" MaxLength="20"  runat="server" Width="90%" CssClass="TEXTBOX"
                                                    Text='<%# DataBinder.Eval(Container.DataItem,"PermitNo") %>' meta:resourcekey="txt_PermitNoResource1"></asp:TextBox>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_PermitNo"   MaxLength="20" Width="75%"  runat="server" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem,"PermitNo") %>' meta:resourcekey="txt_PermitNoResource2"></asp:TextBox>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Document No">
                                           <ItemStyle  Width="10%" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DocumentNo") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_TemproryDocumentNo" CssClass="TEXTBOX" MaxLength="20" Width="75%" runat="server" meta:resourcekey="txt_TemproryDocumentNoResource1"  />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_TemproryDocumentNo" Width="90%" MaxLength="20" runat="server"  CssClass="TEXTBOX" meta:resourcekey="txt_TemproryDocumentNoResource2"/>
                                            </EditItemTemplate>
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Valid From">
                                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ValidFrom", "{0:MM/dd/yyyy}") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ComponentArt:Calendar ID="dtp_PermitValidFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                    ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                    MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidFromResource3"   />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <ComponentArt:Calendar ID="dtp_PermitValidFrom" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                    ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                    MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidFromResource4"   />
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:TemplateColumn>
                                       
                                        <asp:TemplateColumn HeaderText="Valid Upto">
                                           <ItemStyle Width="5%" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ValidUpto", "{0:MM/dd/yyyy}") %>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <ComponentArt:Calendar ID="dtp_PermitValidUpto" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                    ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                    MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidUptoResource3" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <ComponentArt:Calendar ID="dtp_PermitValidUpto" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                                    ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                                    MinDate="1900-01-01" meta:resourcekey="dtp_PermitValidUptoResource4"  />
                                            </EditItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tax Details" >
                                          <ItemTemplate >
                                            <asp:LinkButton ID="lbtnTaxDetails" runat="server" Text="Tax Details" meta:resourcekey="lbtnTaxDetailsResource2" ></asp:LinkButton>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                            <asp:LinkButton ID="lbtnTaxDetails" runat="server" Text="Tax Details" meta:resourcekey="lbtnTaxDetailsResource1" ></asp:LinkButton>
                                          </FooterTemplate>
                                          <EditItemTemplate>
                                              <asp:LinkButton ID="lbtnTaxDetails" runat="server" Text="Tax Details" meta:resourcekey="lbtnTaxDetailsResource3" ></asp:LinkButton>
                                          </EditItemTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn HeaderText="Edit" UpdateText="Update" CancelText="Cancel" EditText="Edit" meta:resourcekey="EditCommandColumnResource2">
                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Add/Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete" meta:resourcekey="lbtn_DeleteResource2"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtn_Add" CommandName="Add" runat="server" Text="Add" meta:resourcekey="lbtn_AddResource2"></asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderStyle CssClass="GRIDHEADERCSS" Width="5%" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                 <asp:HiddenField ID="hdn_Main_SrNo"  runat="server" />
                                 <asp:HiddenField ID="hdn_Main_SrNo_Edit"  runat="server" />
                            </ContentTemplate>
                           
                        </asp:UpdatePanel>
                    </div>
                    
                </td>
            </tr>
          
            <tr>
              <td colspan="6">
               
             </td>
            </tr>
        </table>
          
      </td>
    </tr>
     
    </table> 
            </asp:Panel></td> </tr>
            </table>
            </td>
            </tr>
    <tr>
        <td colspan="6">
            <%--<asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="Server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" CssClass="LABELERROR" runat="server" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="Dg_RegistrationPermit" />
                    <asp:AsyncPostBackTrigger ControlID="Dg_TemparayRegistrationPermit" />
                </Triggers>
            </asp:UpdatePanel>--%>
           <asp:Label ID="lbl_Errors" CssClass="LABELERROR" runat="server" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            <asp:UpdatePanel ID="UpDatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Dg_RegistrationPermit" />
                        <asp:AsyncPostBackTrigger ControlID="Dg_TemparayRegistrationPermit" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Label ID="lbl_Grid_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                    </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    
</table>
