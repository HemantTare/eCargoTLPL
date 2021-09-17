<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucContractFreightDetails.ascx.cs" Inherits="Master_Sales_WucContractFreightDetails" %>
 <%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script type="text/javascript" src="../../Javascript/Common.js" language="javascript" ></script>
<script type="text/javascript" src="../../Javascript/Master/Sales/ContractFreightDetails.js" language="javascript" ></script>

<table style="width: 100%" class="TABLE">

     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="Panel1" runat="server" Width="100%" meta:resourcekey="Panel1Resource1">
            <table width="100%">
                <tr>
                    <td class="TD1" style="width: 20%; ">
                        <asp:Label ID="lbl_UnitOfFreight" runat="server" Text="Unit Of Freight:" CssClass="LABEL" meta:resourcekey="lbl_UnitOfFreightResource1"></asp:Label></td>
                    <td style="width: 28%; ">
                    
                    <asp:UpdatePanel ID="Upd_Pnl_ddl_UnitOfFreight" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:DropDownList ID="ddl_UnitOfFreight" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_UnitOfFreight_SelectedIndexChanged" meta:resourcekey="ddl_UnitOfFreightResource1">
                        </asp:DropDownList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 2%; " class="TDMANDATORY">
                        *</td>
                    <td class="TD1" style="width: 20%; ">
                        <asp:Label ID="lbl_SubUnit" runat="server" Text="Sub Unit:" CssClass="LABEL" meta:resourcekey="lbl_SubUnitResource1"></asp:Label></td>
                    <td style="width: 28%; ">
                     <asp:UpdatePanel ID="Upd_Pnl_ddl_SubUnit" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:DropDownList ID="ddl_SubUnit" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_SubUnit_SelectedIndexChanged" meta:resourcekey="ddl_SubUnitResource1">
                        </asp:DropDownList>
                        </ContentTemplate>                        
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 2%; ">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_FreightBasis" runat="server" Text="Freight Basis:" CssClass="LABEL" meta:resourcekey="lbl_FreightBasisResource1"></asp:Label></td>
                    <td style="width: 28%">
                     <asp:UpdatePanel ID="Upd_Pnl_ddl_FreightBasis" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:DropDownList ID="ddl_FreightBasis" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_FreightBasis_SelectedIndexChanged" meta:resourcekey="ddl_FreightBasisResource1">
                        </asp:DropDownList>
                        </ContentTemplate>                        
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 2%" class="TDMANDATORY">
                        *</td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 28%">
                    </td>
                    <td style="width: 2%">
                    </td>
                </tr>
    </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Panel ID="Panel2" runat="server" GroupingText="Freight Details" Width="100%" meta:resourcekey="Panel2Resource1"><table width="100%">
                <tr>
                    <td class="TD1" style="width: 20%">
                    <asp:UpdatePanel ID="Upd_Pnl_UnitFreightDetails" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfFreight" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:Label ID="lbl_UnitFreightDetails" runat="server" CssClass="LABEL" meta:resourcekey="lbl_UnitFreightDetailsResource1"></asp:Label>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 28%">
                    <asp:UpdatePanel ID="Upd_Pnl_ddl_UnitFreight" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfFreight" />
                            <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:DropDownList ID="ddl_UnitFreight" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_UnitFreight_SelectedIndexChanged" meta:resourcekey="ddl_UnitFreightResource1">
                        </asp:DropDownList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 2%">
                    </td>
                    <td class="TD1" style="width: 20%; height: 61px;">
                       <asp:UpdatePanel ID="Upd_Pnl_lbl_SubUnitFreightDetails" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_SubUnit" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:Label ID="lbl_SubUnitFreightDetails" runat="server" CssClass="LABEL" meta:resourcekey="lbl_SubUnitFreightDetailsResource1"></asp:Label>
                        </ContentTemplate>
                        
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 28%">
                          <asp:UpdatePanel ID="Upd_Pnl_ddl_SubUnitFreight" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_SubUnit" />
                            <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:DropDownList ID="ddl_SubUnitFreight" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_SubUnitFreight_SelectedIndexChanged" meta:resourcekey="ddl_SubUnitFreightResource1">
                        </asp:DropDownList>
                        </ContentTemplate>
                        
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 2%">
                    </td>
                </tr>
                 
                <tr >                
                    <td class="TD1" style="width: 20%">
                     <asp:UpdatePanel ID="Upd_Pnl_lbl_CFTFactor" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfFreight" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:Label ID="lbl_CFTFactor" runat="server" Text="CFT Factor" CssClass="LABEL" meta:resourcekey="lbl_CFTFactorResource1"></asp:Label>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 28%">
                     <asp:UpdatePanel ID="Upd_Pnl_txt_CFTFactor" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfFreight" />
                        </Triggers>
                        <ContentTemplate>
                        <asp:TextBox ID="txt_CFTFactor"  runat="server" onkeypress="return Only_Numbers(this,event)" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_CFTFactorResource1">0</asp:TextBox>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    <td style="width: 2%">
                    </td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 28%">
                    </td>
                    <td style="width: 2%">
                    </td>
                </tr>
                <tr>
                    <td  colspan="6">
                        &nbsp;<table border="0" cellpadding="5" cellspacing="5" width="100%">
                            <tr>
                                <td>
                                    <asp:Panel ID="pnl_Grid"  GroupingText=" " runat="server" meta:resourcekey="pnl_GridResource1"  >
                                        <table border="0" cellpadding="3" cellspacing="3" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="Upd_Pnl_dg_FreightDetails" runat="server" UpdateMode="Always">
                                                    <Triggers>                                                        
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfFreight" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_SubUnitFreight" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_FreightBasis" />
                                                        <asp:AsyncPostBackTrigger ControlID="ddl_UnitOfFreight" />
                                                        <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                                                    </Triggers>
                                                        <ContentTemplate>
                                                            <asp:DataGrid ID="dg_FreightDetails" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                                                 ShowFooter="True" Width="100%" OnCancelCommand="dg_FreightDetails_CancelCommand" OnEditCommand="dg_FreightDetails_EditCommand" OnItemDataBound="dg_FreightDetails_ItemDataBound" OnUpdateCommand="dg_FreightDetails_UpdateCommand" OnDeleteCommand="dg_FreightDetails_DeleteCommand" OnItemCommand="dg_FreightDetails_ItemCommand" meta:resourcekey="dg_FreightDetailsResource1">
                                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                                <Columns>
                                                              
                                                                    <asp:TemplateColumn FooterStyle-CssClass="HIDEGRIDCOL" ItemStyle-CssClass="HIDEGRIDCOL" HeaderStyle-CssClass="HIDEGRIDCOL">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_SrNo" Text='<%# DataBinder.Eval(Container.DataItem,"SrNo") %>' runat="server" meta:resourcekey="txt_SrNoResource2"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                        <asp:TextBox ID="txt_SrNo" Text='<%# DataBinder.Eval(Container.DataItem,"SrNo") %>' runat="server" meta:resourcekey="txt_SrNoResource3"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                        <asp:TextBox ID="txt_SrNo" Text='<%# DataBinder.Eval(Container.DataItem,"SrNo") %>' runat="server" meta:resourcekey="txt_SrNoResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="From Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_FromLocation" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FromLocationName") %>' meta:resourcekey="lbl_FromLocationResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <cc1:DDLSearch  ID="ddl_FromLocation" runat="server" IsCallBack="True" CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetServiceLocation" AllowNewText="True" Text="" CallBackAfter="2" PostBack="False"/>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                        <cc1:DDLSearch  ID="ddl_FromLocation" runat="server" IsCallBack="True" CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetServiceLocation" AllowNewText="True" Text="" CallBackAfter="2" PostBack="False"/>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="20%" />
                                                                    </asp:TemplateColumn>
                                                                     <asp:TemplateColumn HeaderText="To Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ToLocation" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "ToLocationName") %>' meta:resourcekey="lbl_ToLocationResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <%--<asp:DropDownList ID="ddl_ToLocation" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ToLocationResource2" >
                                                                            </asp:DropDownList>--%>
                                                                             <cc1:DDLSearch  ID="ddl_ToLocation" runat="server" IsCallBack="True" CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetServiceLocation" AllowNewText="True" Text="" CallBackAfter="2" PostBack="False"/>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                        <cc1:DDLSearch  ID="ddl_ToLocation" runat="server" IsCallBack="True" CallBackFunction = "Raj.EF.CallBackFunction.CallBack.GetServiceLocation" AllowNewText="True" Text="" CallBackAfter="2" PostBack="False"/>
                                                                            <%--<asp:DropDownList ID="ddl_ToLocation" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ToLocationResource1" >
                                                                            </asp:DropDownList>--%>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="20%"/>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="From KMS">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_FromKMS" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FromKMS") %>' meta:resourcekey="lbl_FromKMSResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_FromKMS" onkeypress="return Only_Integers(this,event)" 
                                                                            onblur="return valid(this)" MaxLength="9"   Width="94%" Text='<%# DataBinder.Eval(Container.DataItem,"FromKMS") %>' CssClass="TEXTBOXNOS"   runat="server" meta:resourcekey="txt_FromKMSResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_FromKMS" onkeypress="return Only_Integers(this,event)" 
                                                                           onblur="return valid(this)"  MaxLength="9"  Width="94%" Text='<%# DataBinder.Eval(Container.DataItem,"FromKMS") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_FromKMSResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="6%"/>
                                                                    </asp:TemplateColumn>
                                                                       <asp:TemplateColumn HeaderText="To KMS">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ToKMS" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "ToKMS") %>' 
                                                                                meta:resourcekey="lbl_ToKMSResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_ToKMS" Width="94%" onkeypress="return Only_Integers(this,event)" 
                                                                            onblur="return valid(this)"   MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"ToKMS") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_ToKMSResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_ToKMS" Width="94%" onkeypress="return Only_Integers(this,event)" 
                                                                           onblur="return valid(this)"   MaxLength="9"   Text='<%# DataBinder.Eval(Container.DataItem,"ToKMS") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_ToKMSResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="6%"/>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="From Days">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_FromDays" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FromDays") %>'
                                                                                 meta:resourcekey="lbl_FromDaysResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_FromDays" Width="94%" onkeypress="return Only_Integers(this,event)" 
                                                                            onblur="return valid(this)" MaxLength="9"  Text='<%# DataBinder.Eval(Container.DataItem,"FromDays") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_FromDaysResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_FromDays" Width="94%" onkeypress="return Only_Integers(this,event)"
                                                                           onblur="return valid(this)" MaxLength="9"  Text='<%# DataBinder.Eval(Container.DataItem,"FromDays") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_FromDaysResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="6%" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="To Days">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_ToDays" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "ToDays") %>'
                                                                                 meta:resourcekey="lbl_ToDaysResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_ToDays" Width="94%" onkeypress="return Only_Integers(this,event)"
                                                                            onblur="return valid(this)"  MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"ToDays") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_ToDaysResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_ToDays" Width="94%" onkeypress="return Only_Integers(this,event)"
                                                                           onblur="return valid(this)"  MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"ToDays") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_ToDaysResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="6%"/>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="KMS" HeaderStyle-Width="10%" >
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_KMS" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "Kms") %>' meta:resourcekey="lbl_KMSResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_KMS" Width="94%" Text='<%# DataBinder.Eval(Container.DataItem,"Kms") %>' onkeypress="return Only_Integers(this,event)" 
                                                                            onblur="return valid(this)" MaxLength="9" CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_KMSResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_KMS" Width="94%"  onkeypress="return Only_Integers(this,event)"
                                                                           onblur="return valid(this)"  MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"Kms") %>'  CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_KMSResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle Width="6%" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn HeaderText="Transit Days">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_TransitDays" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "TransitDays") %>' meta:resourcekey="lbl_TransitDaysResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_TransitDays" Width="94%"  onkeypress="return Only_Integers(this,event)" 
                                                                            onblur="return valid(this)" MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"TransitDays") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_TransitDaysResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_TransitDays" Width="94%" onkeypress="return Only_Integers(this,event)"
                                                                           onblur="return valid(this)"  MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"TransitDays") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_TransitDaysResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                        <ItemStyle  Width="6%"/>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn  ItemStyle-Width = "10%" >                                                                     
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lbl_FreightHeading" runat="server" meta:resourcekey="lbl_FreightHeadingResource1"></asp:Label>
                                                                    </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Freight" runat="server" 
                                                                                Text='<%# DataBinder.Eval(Container.DataItem, "FreightRate") %>' meta:resourcekey="lbl_FreightResource1"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate >
                                                                            <asp:TextBox ID="txt_Freight" Text='<%# DataBinder.Eval(Container.DataItem,"FreightRate") %>' 
                                                                            onkeypress="return Only_Numbers(this,event)"  Width="90%" onblur="return valid(this)" MaxLength="9" CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_FreightResource2"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_Freight" onkeypress="return Only_Numbers(this,event)"
                                                                           onblur="return valid(this)"  width="90%" MaxLength="9" Text='<%# DataBinder.Eval(Container.DataItem,"FreightRate") %>' CssClass="TEXTBOXNOS" runat="server" meta:resourcekey="txt_FreightResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />                                                                        
                                                                    </asp:TemplateColumn>
                                                                    
                                                                    <asp:TemplateColumn  Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_FromID" Text='<%# DataBinder.Eval(Container.DataItem,"FromID") %>' runat="server" meta:resourcekey="txt_FromIDResource2"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_FromID" Text='<%# DataBinder.Eval(Container.DataItem,"FromID") %>' runat="server" meta:resourcekey="txt_FromIDResource3"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_FromID" Text='<%# DataBinder.Eval(Container.DataItem,"FromID") %>' runat="server" meta:resourcekey="txt_FromIDResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                    </asp:TemplateColumn>
                                                                    
                                                                    <asp:TemplateColumn  Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_ToID" Text='<%# DataBinder.Eval(Container.DataItem,"ToID") %>' runat="server" meta:resourcekey="txt_ToIDResource2"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_ToID" Text='<%# DataBinder.Eval(Container.DataItem,"ToID") %>' runat="server" meta:resourcekey="txt_ToIDResource3"></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_ToID" Text='<%# DataBinder.Eval(Container.DataItem,"ToID") %>' runat="server" meta:resourcekey="txt_ToIDResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                    </asp:TemplateColumn>
                                                                    
                                                                    <asp:TemplateColumn  Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_RangeFrom" Text='<%# DataBinder.Eval(Container.DataItem,"Range_From") %>' runat="server" ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_RangeFrom" Text='<%# DataBinder.Eval(Container.DataItem,"Range_From") %>' runat="server" ></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_RangeFrom" Text='<%# DataBinder.Eval(Container.DataItem,"Range_From") %>' runat="server" ></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                    </asp:TemplateColumn>
                                                                              <asp:TemplateColumn  Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_RangeTo" Text='<%# DataBinder.Eval(Container.DataItem,"Range_To") %>' runat="server" ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_RangeTo" Text='<%# DataBinder.Eval(Container.DataItem,"Range_To") %>' runat="server" ></asp:TextBox>                                                                            
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                           <asp:TextBox ID="txt_RangeTo" Text='<%# DataBinder.Eval(Container.DataItem,"Range_To") %>' runat="server" ></asp:TextBox>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS"  />
                                                                    </asp:TemplateColumn>
                                                                      <asp:TemplateColumn FooterStyle-CssClass="HIDEGRIDCOL" ItemStyle-CssClass="HIDEGRIDCOL" HeaderStyle-CssClass="HIDEGRIDCOL">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txt_ID" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat="server" meta:resourcekey="txt_SrNoResource2"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                        <asp:TextBox ID="txt_ID" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat="server" meta:resourcekey="txt_SrNoResource3"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                        <asp:TextBox ID="txt_ID" Text='<%# DataBinder.Eval(Container.DataItem,"ID") %>' runat="server" meta:resourcekey="txt_SrNoResource1"></asp:TextBox>
                                                                        </FooterTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:EditCommandColumn  CancelText="Cancel" EditText="Edit" HeaderText="Edit" 
                                                                        UpdateText="Update" meta:resourcekey="EditCommandColumnResource1">
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:EditCommandColumn>
                                                                    <asp:TemplateColumn HeaderText="Add/Delete">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtn_Delete" runat="server" CommandName="Delete" 
                                                                                Text="Delete" meta:resourcekey="lbtn_DeleteResource1"></asp:LinkButton>                                                                               
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:LinkButton ID="lbtn_Add" runat="server" CommandName="ADD" 
                                                                                Text="Add" meta:resourcekey="lbtn_AddResource1"></asp:LinkButton>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                                        <ItemStyle Width="7%" />
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn>
                                                                    <HeaderTemplate>
                                                                        <asp:Label ID="lbl_DetailsHeader"  Text="Details" runat="server" CssClass="LABEL" meta:resourcekey="lbl_DetailsHeaderResource1" ></asp:Label>
                                                                    </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtn_Details" runat="server" CommandName="Details" 
                                                                                Text="Details" meta:resourcekey="lbtn_DetailsResource1"></asp:LinkButton>                                                                               
                                                                        </ItemTemplate>
                                                                       <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                                        <ItemStyle Width="6%" />
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
            </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="height: 21px">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
    
    <tr>
        <td style="height: 21px;" colspan="6">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <Triggers>                                                        
                                                        <asp:AsyncPostBackTrigger ControlID="dg_FreightDetails" />
                                                    </Triggers>
                                                        <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" EnableViewState="false"></asp:Label>
            </ContentTemplate>            
            </asp:UpdatePanel>
            </td>
    </tr>
</table>
