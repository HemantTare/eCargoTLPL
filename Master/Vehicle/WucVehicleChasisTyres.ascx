<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleChasisTyres.ascx.cs" Inherits="Master_Vehicle_WucVehicleChasisTyres" %>

<table class="TABLE">
    <tr>
        <td colspan="6" style="width:100%">&nbsp;</td> 
    </tr>
    <tr>
        <td colspan="3" style="width:50%;vertical-align:top">
           
               <table width="100%">
                   <tr>
                        <td colspan="2" style="width: 100%;">
                            <asp:Panel ID="pnl_ChasisTyres" runat="server"  GroupingText="Chasis/Tyre Configuration" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_ChasisTyresResource1">
                                <asp:UpdatePanel ID="upd_ChasisTyres" runat="server" UpdateMode="Conditional">
                                <Triggers><asp:AsyncPostBackTrigger ControlID="dg_ChasisTyres" /></Triggers>
                                    <ContentTemplate>
                                    <asp:DataGrid ID="dg_ChasisTyres" AutoGenerateColumns="False" ShowFooter="True" CellPadding  = "3"
                                    CssClass="Grid" runat="server" OnDeleteCommand="dg_ChasisTyres_DeleteCommand" OnItemCommand="dg_ChasisTyres_ItemCommand" 
                                    OnItemDataBound="dg_ChasisTyres_ItemDataBound" OnCancelCommand="dg_ChasisTyres_CancelCommand" 
                                    OnEditCommand="dg_ChasisTyres_EditCommand" OnUpdateCommand="dg_ChasisTyres_UpdateCommand" meta:resourcekey="dg_ChasisTyresResource1">
                                    <ItemStyle HorizontalAlign="Center"/><FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle  Height ="15px" Font-Size ="11px" 
                                    Font-Names="Verdana"  Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" BorderStyle="Solid"
                                    BorderColor="#9495A2" BorderWidth ="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="LO">
                                                <FooterTemplate>
                                                    <asp:Image ID="imgLO" runat="server" meta:resourcekey="imgLOResource1" />
                                                </FooterTemplate>
                                                <ItemTemplate><asp:Image ID="imgLO" runat="server" meta:resourcekey="imgLOResource2" /></ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Image ID="imgLO" runat="server" meta:resourcekey="imgLOResource3" />
                                                </EditItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" />
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="LI">
                                                
                                               <FooterTemplate>
                                                    <asp:Image ID="imgLI" runat="server" meta:resourcekey="imgLIResource1" />
                                                </FooterTemplate>
                                                <ItemTemplate><asp:Image ID="imgLI" runat="server" meta:resourcekey="imgLIResource2" /></ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Image ID="imgLI" runat="server" meta:resourcekey="imgLIResource3" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                
                                                <FooterTemplate>
                                                    <asp:Image ID="imgStick" runat="server" meta:resourcekey="imgStickResource1" />
                                                </FooterTemplate>
                                                <ItemTemplate><asp:Image ID="imgStick" runat="server" meta:resourcekey="imgStickResource2" /></ItemTemplate>
                                                <EditItemTemplate>
                                                     <asp:Image ID="imgStick" runat="server" meta:resourcekey="imgStickResource3" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="RI">
                                                
                                                <FooterTemplate>
                                                    <asp:Image ID="imgRI" runat="server" meta:resourcekey="imgRIResource1" />
                                                </FooterTemplate>
                                                <ItemTemplate><asp:Image ID="imgRI" runat="server" meta:resourcekey="imgRIResource2"/></ItemTemplate>
                                                <EditItemTemplate>
                                                      <asp:Image ID="imgRI" runat="server" meta:resourcekey="imgRIResource3" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="RO">
                                                
                                                <FooterTemplate>
                                                    <asp:Image ID="imgRO" runat="server" meta:resourcekey="imgROResource1" />
                                                </FooterTemplate>
                                                <ItemTemplate><asp:Image ID="imgRO" runat="server" meta:resourcekey="imgROResource2" /></ItemTemplate>
                                                <EditItemTemplate>
                                                      <asp:Image ID="imgRO" runat="server" meta:resourcekey="imgROResource3" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Duals"> 
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_dual" CssClass="DROPDOWN" runat="server" OnSelectedIndexChanged="ddl_dual_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddl_dualResource1"/>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Axle_Configuration") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_dual" CssClass="DROPDOWN" runat="server" OnSelectedIndexChanged="ddl_dual_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddl_dualResource2"/>
                                                </EditItemTemplate>
                                                <HeaderStyle Width="25%" />
                                            </asp:TemplateColumn>
                                               
                                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Update" meta:resourcekey="EditCommandColumnResource1">
                                                <HeaderStyle Width="10%" />
                                            </asp:EditCommandColumn>

                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtn_Add" Text="Add" Runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                                                </FooterTemplate>
                                                <ItemTemplate><asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" meta:resourcekey="lbtn_DeleteResource1"/></ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                            </asp:TemplateColumn>
                                        </Columns> 
                                    </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                          </asp:Panel>
                        </td> 
                     </tr> 
                     
                     <tr>
                      <td class="TD1" style ="width:90%">No. Of Stephney</td>
                      <td style="width:10%">
                        <asp:TextBox ID="txt_no_of_stephaney" runat="server" MaxLength="1" CssClass="TEXTBOXNOS" BorderWidth="1px" onkeypress="return Only_Integers(this,event)"></asp:TextBox>
                        <asp:HiddenField ID="hdn_old_no_of_stephaney" runat="server" />
                      </td>
                     </tr>
                 </table>
          
        </td>
        <td colspan="3" style="width:50%;vertical-align:top" >
            <table width="100%">
                <tr>
                    <td style="width:100%" colspan="6">
                        <asp:Panel ID="pnl_Front" runat="server"  GroupingText="Front" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_FrontResource1">
                            <table width="100%">
                                <tr>
                                    <td class="TD1"  style="width:30%">
                                    <asp:Label ID="lbl_Wheel_Size_Front"  runat="server" Text="Wheel Size :" meta:resourcekey="lbl_Wheel_Size_FrontResource1"/>
                                    </td>
                                    <td  style="width:60%"><asp:DropDownList ID="ddl_FrontWheelSize" runat="server" CssClass="DROPDOWN" Width="100%" meta:resourcekey="ddl_FrontWheelSizeResource1"></asp:DropDownList>
                                    </td>
                                    <td style="width:10%"></td>
                                </tr>
                                <tr>
                                    <td class="TD1"  style="width:30%">
                                    <asp:Label ID="lbl_Tyre_Size_Front"  runat="server" Text="Tyre Size :" meta:resourcekey="lbl_Tyre_Size_FrontResource1"/>
                                    </td>
                                    <td  style="width:60%"><asp:DropDownList ID="ddl_FrontTyresize" runat="server" CssClass="DROPDOWN" Width="100%" meta:resourcekey="ddl_FrontTyresizeResource1"/>
                                    </td>
                                    <td style="width:10%"></td>
                                </tr>
                                <tr>
                                    <td class="TD1"  style="width:30%">
                                    <asp:Label ID="lbl_PSI_Front"  runat="server" Text="PSI :" meta:resourcekey="lbl_PSI_FrontResource1"/>
                                    </td>
                                    <td  style="width:60%"><asp:TextBox ID="txt_FrontPSI" runat="server" CssClass="TEXTBOXNOS" MaxLength="20" BorderWidth="1px" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_FrontPSIResource1"></asp:TextBox>
                                    </td>
                                    <td style="width:10%"></td>
                                </tr>
                            </table>
                        </asp:Panel> 
                    </td>
                </tr>
                <tr>
                    <td style="width:100%" colspan="6">
                        <asp:Panel ID="pnl_Rear" runat="server"  GroupingText="Rear" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_RearResource1">
                             <table width="100%">
                                <tr>
                                    <td class="TD1" style="width:30%">
                                    <asp:Label ID="lbl_Wheel_Size_Rear"  runat="server" Text="Wheel Size :" meta:resourcekey="lbl_Wheel_Size_RearResource1"/>
                                    </td>
                                    <td  style="width:60%"><asp:DropDownList ID="ddl_RearWheelSize" runat="server" CssClass="DROPDOWN" Width="100%" meta:resourcekey="ddl_RearWheelSizeResource1"></asp:DropDownList>
                                    </td>
                                    <td style="width:10%"></td>
                                </tr>
                                <tr>
                                    <td class="TD1"  style="width:30%">
                                    <asp:Label ID="lbl_Tyre_Size_Rear"  runat="server" Text="Tyre Size :" meta:resourcekey="lbl_Tyre_Size_RearResource1"/>
                                    </td>
                                    <td  style="width:60%"><asp:DropDownList ID="ddl_RearTyresize" runat="server" CssClass="DROPDOWN" Width="100%" meta:resourcekey="ddl_RearTyresizeResource1"/>
                                    </td>
                                    <td style="width:10%"></td>
                                </tr>
                                <tr>
                                    <td class="TD1"  style="width:30%">
                                    <asp:Label ID="lbl_PSI_Rear"  runat="server" Text="PSI :" meta:resourcekey="lbl_PSI_RearResource1"/>
                                    </td>
                                    <td  style="width:60%"><asp:TextBox ID="txt_RearPSI" runat="server" MaxLength="20" CssClass="TEXTBOXNOS" BorderWidth="1px" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_RearPSIResource1"></asp:TextBox>
                                    </td>
                                    <td style="width:10%"></td>
                                </tr>
                            </table>
                        </asp:Panel> 
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
      <td colspan="8">
        <%--<asp:UpdatePanel ID="upd_Pnl_ChasisTyre_Save" UpdateMode="Conditional" runat="server">
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_ChasisTyres" />
            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
          </Triggers>
          <ContentTemplate>
            <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>--%>
        <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>

      </td>
    </tr>
 </table>
