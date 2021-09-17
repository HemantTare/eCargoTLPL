<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucEngineBodySpecification.ascx.cs" Inherits="Master_General_WucEngineBodySpecification" %>
<script type="text/javascript" src="../../Javascript/Common.js"></script>


<table class="TABLE">
    <tr>
        <td colspan="8">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="pnl_Engion_Body" runat="server" GroupingText="Engine Body" CssClass="PANEL"
                            Width="100%" meta:resourcekey="pnl_Engion_BodyResource1">
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_ChasisNo" runat="server" Text=" Chasis No :" meta:resourcekey="lbl_ChasisNoResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Chasis_No" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="50" meta:resourcekey="txt_Chasis_NoResource1" Width="210px"></asp:TextBox></td>
                                    <td class="TDMANDATORY" style="width: 2%">
                                      <asp:Label ID="lbl_MandatoryChasisNo" Text="*" runat="server" meta:resourcekey="lbl_MandatoryChasisNoResource1"></asp:Label></td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_EngineNo" Text="Engine No :" runat="server" meta:resourcekey="lbl_EngineNoResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Engine_No" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="25" meta:resourcekey="txt_Engine_NoResource1"></asp:TextBox></td>
                                    <td class="TDMANDATORY" style="width: 2%">
                                     <asp:Label ID="lbl_MandatoryEngineNo" Text="*" runat="server" meta:resourcekey="lbl_MandatoryEngineNoResource1"></asp:Label></td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_TrollyChasisNo" Text="Trolly Chasis No :" runat="server" meta:resourcekey="lbl_TrollyChasisNoResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_TrollyChasisNo" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="50" meta:resourcekey="txt_TrollyChasisNoResource1" Width="210px"></asp:TextBox></td>
                                    <td class="TDMANDATORY" style="width: 2%">
                                    </td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 50%" colspan="8">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_FuelType" Text="Fuel Type :" runat="server" meta:resourcekey="lbl_FuelTypeResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:DropDownList ID="ddl_Fuel_Type" runat="server" CssClass="DROPDOWN" Width="100%"
                                            meta:resourcekey="ddl_Fuel_TypeResource1">
                                        </asp:DropDownList></td>
                                    <td class="TDMANDATORY" style="width: 2%">
                                     <asp:Label ID="lbl_MandatoryFuelType" Text="*" runat="server" meta:resourcekey="lbl_MandatoryFuelTypeResource1"></asp:Label></td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_FuelTankCapacity" runat="server" Text="Fuel Tank Capacity :" meta:resourcekey="lbl_FuelTankCapacityResource1"></asp:Label></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_FuelTankCapacity" BorderWidth="1px" runat="server" CssClass="TEXTBOXNOS"
                                            MaxLength="18" onkeypress="return Only_Integers(this,event)"  onblur="return valid(this)" meta:resourcekey="txt_FuelTankCapacityResource1"></asp:TextBox>
                                    </td>
                                    <td class="TD1" style="width: 2%">
                                        Ltrs</td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_GrossVehicleWt" Text="Vehicle Wt :" runat="server" meta:resourcekey="lbl_GrossVehicleWtResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Gross_Veh_Wt" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                            MaxLength="20" onblur="Calculate_Vehicle_Capacity()" onkeypress="return Only_Integers(this,event)"
                                            meta:resourcekey="txt_Gross_Veh_WtResource1" /></td>
                                    <td align="left" style="width: 2%">
                                        Kg</td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                        <asp:Label ID="lbl_MandatoryGrossVehWt" Text="*" runat="server" meta:resourcekey="lbl_MandatoryGrossVehWtResource1"></asp:Label>
                                        </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_UnladenWt" Text="Unladen Wt :" runat="server" meta:resourcekey="lbl_UnladenWtResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Unladen_Wt" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                            MaxLength="20" onblur="Calculate_Vehicle_Capacity()" onkeypress="return Only_Integers(this,event)"
                                            meta:resourcekey="txt_Unladen_WtResource1" /></td>
                                    <td align="left" style="width: 2%">
                                        Kg</td>
                                    <td class="TDMANDATORY" style="width: 1%">
                                     <asp:Label ID="lbl_MandatoryUnladenWt" Text="*" runat="server" meta:resourcekey="lbl_MandatoryUnladenWtResource1"></asp:Label>
                                        </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_VehicleCapacity" Text="Vehicle Capacity :" runat="server" meta:resourcekey="lbl_VehicleCapacityResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:Label ID="lbl_Vehicle_Capacity" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                                            Width="98%" meta:resourcekey="lbl_Vehicle_CapacityResource1"></asp:Label>
                                        <asp:HiddenField ID="hdn_Vehicle_Capacity" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 2%">
                                        Kg</td>
                                    <td style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_wheelBase" Text="Wheel Base :" runat="server" meta:resourcekey="lbl_wheelBaseResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Wheel_Base" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)"  onblur="return valid(this)"
                                             meta:resourcekey="txt_Wheel_BaseResource1"></asp:TextBox></td>
                                    <td style="width: 2%">
                                        MM</td>
                                    <td style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_Length" Text="Length :" runat="server" meta:resourcekey="lbl_LengthResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Length" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)"
                                            meta:resourcekey="txt_LengthResource1" /></td>
                                    <td style="width: 2%">
                                        Ft</td>
                                    <td style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_Power" Text="Power :" runat="server" meta:resourcekey="lbl_PowerResource1" /></td>
                                    <td class="TD1" style="width: 27%">
                                        <asp:TextBox ID="txt_Power" runat="server" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="5"
                                            meta:resourcekey="txt_PowerResource1"></asp:TextBox></td>
                                    <td style="width: 2%">
                                        cc</td>
                                    <td class="TD1" style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_Height" Text="Height :" runat="server" meta:resourcekey="lbl_HeightResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Height" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)" onblur="return valid(this)"
                                            meta:resourcekey="txt_HeightResource1" /></td>
                                    <td style="width: 2%">
                                        Ft</td>
                                    <td style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 50%" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_Width" Text="Width :" runat="server" meta:resourcekey="lbl_WidthResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Width" runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px"
                                            MaxLength="18" onkeypress="return Only_Numbers(this,event)"  onblur="return valid(this)"
                                             meta:resourcekey="txt_WidthResource1" /></td>
                                    <td style="width: 2%">
                                        Ft</td>
                                    <td style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 50%" colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_PaintCode" Text="Paint Code :" runat="server" meta:resourcekey="lbl_PaintCodeResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Paint_Code" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="10" meta:resourcekey="txt_Paint_CodeResource1"></asp:TextBox></td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_PaintColor" Text="Paint Color:" runat="server" meta:resourcekey="lbl_PaintColorResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Paint_color" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="25" meta:resourcekey="txt_Paint_colorResource1"></asp:TextBox></td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_IgnitionKeyCode" Text="Ignition Key Code :" runat="server" meta:resourcekey="lbl_IgnitionKeyCodeResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Ignition_Code" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="20" meta:resourcekey="txt_Ignition_CodeResource1"></asp:TextBox></td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
                                    </td>
                                    <td class="TD1" style="width: 20%">
                                        <asp:Label ID="lbl_DoorKeyCode" Text="Door Key Code :" runat="server" meta:resourcekey="lbl_DoorKeyCodeResource1" /></td>
                                    <td style="width: 27%">
                                        <asp:TextBox ID="txt_Door_Code" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                            MaxLength="20" meta:resourcekey="txt_Door_CodeResource1"></asp:TextBox></td>
                                    <td style="width: 2%">
                                    </td>
                                    <td style="width: 1%">
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
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upd_pnl_dg_Specification" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Specification" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnl_Specification" runat="server" GroupingText="Specification" CssClass="PANEL"
                                    Width="800px" meta:resourcekey="pnl_SpecificationResource1" ScrollBars="Both">
                                    <table cellpadding="3" cellspacing="3" border="0" width="700">
                                        <tr>
                                            <td colspan="8">
                                                <asp:DataGrid ID="dg_Specification" AutoGenerateColumns="False" ShowFooter="True"
                                                    CellPadding="3" CssClass="Grid" runat="server" OnItemDataBound="dg_Specification_ItemDataBound"
                                                    OnItemCommand="dg_Specification_ItemCommand" OnCancelCommand="dg_Specification_CancelCommand"
                                                    OnEditCommand="dg_Specification_EditCommand" OnUpdateCommand="dg_Specification_UpdateCommand"
                                                    OnDeleteCommand="dg_Specification_DeleteCommand" meta:resourcekey="dg_SpecificationResource1"
                                                    Width="750px" OnSelectedIndexChanged="dg_Specification_SelectedIndexChanged">
                                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                                    </HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Description">
                                                            <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Description" CssClass="TEXTBOX" MaxLength="255" runat="server"
                                                                    BorderWidth="1px" meta:resourcekey="txt_DescriptionResource1" Width="400px" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Description") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Description" CssClass="TEXTBOX" runat="server" MaxLength="255"
                                                                    BorderWidth="1px" meta:resourcekey="txt_DescriptionResource2" Width="400px" />
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="400px" />
                                                            <FooterStyle Width="400px" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Value/Quantity">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Value" CssClass="TEXTBOXNOS" MaxLength="15" onkeypress="return Only_Numbers(this,event)"
                                                                    onblur="return valid(this)" runat="server" BorderWidth="1px" meta:resourcekey="txt_ValueResource1"
                                                                    Width="200px" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "ValueQuantity") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Value" CssClass="TEXTBOXNOS" runat="server" MaxLength="15" onkeypress="return Only_Numbers(this,event)"
                                                                    onblur="return valid(this)" BorderWidth="1px" meta:resourcekey="txt_ValueResource2"
                                                                    Width="200px" />
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="100px" />
                                                            <ItemStyle Width="100px" HorizontalAlign="Right" />
                                                            <FooterStyle Width="100px" />
                                                        </asp:TemplateColumn>
                                                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                                                            meta:resourcekey="EditCommandColumnResource1">
                                                            <HeaderStyle Width="50px" />
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Delete">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                                                    meta:resourcekey="lbtn_DeleteResource1" /></ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            &nbsp;</td>
    </tr>
    <%-- <tr>
        <td align="center" colspan="8">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" OnClientClick="return ValidateUI()"/>
        </td>
    </tr>--%>
    <tr>
        <td colspan="8">
            <asp:UpdatePanel ID="upd_Pnl_Engine_Body_Save" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Specification" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--><asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
--%>
        </td>
    </tr>
</table>
