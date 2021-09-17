<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyVehicleLoadingPlan.aspx.cs" Inherits="Operations_Outward_FrmDailyVehicleLoadingPlan" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Outward/DailyVehicleLoadingPlan.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Vehicle Loading Plan</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Daily Vehicle Loading Plan"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 25%">Loading Date:</td>
                    <td style="width: 24%">
                        <uc1:WucDatePicker ID="dtpLoadingDate" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td style="width: 50%" align="left" colspan="3">&nbsp</td>
                </tr>
                <tr>
                    <td class="TD1">Vehicle No:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <uc2:WucVehicleSearch ID="DDLVehicle" runat="server" />
                                <asp:HiddenField ID="hdn_VehicleID" runat="server" />
                                <asp:HiddenField ID="hdn_VehicleCategoryIds" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td style="width: 50%" align="left" colspan="3"></td>
                </tr>
                <tr id="trBrokerName" runat="server">
                    <td style="width: 20%" class="TD1">Broker Name:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>                                    
                                <asp:TextBox ID="txtBroker" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                    onblur="On_LostFocus('txtBroker','lstBroker','hdnBroker')" onkeyup="Search_Broker(event,this,'lstBroker',2);"
                                    onkeydown="return on_keydown(event,'txtBroker','lstBroker');" onfocus="On_Focus('txtBroker','lstBroker');"
                                    MaxLength="50" EnableViewState="False"></asp:TextBox>
                                <asp:ListBox ID="lstBroker" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtBroker')"
                                    runat="server" TabIndex="20"></asp:ListBox>
                                <asp:HiddenField ID="hdnBroker" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                        <asp:LinkButton ID="lnkAddBroker" Font-Bold="true" OnClientClick="return Add_Broker_Window()"
                            runat="server" Text="Add New"></asp:LinkButton>
                        <asp:HiddenField ID="hdnBrokerPath" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">From Branch:</td>
                    <td style="width: 29%">
                    <asp:TextBox ID="txt_FromBranch" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                        onblur="On_BranchLostFocus('txt_FromBranch','lst_FromBranch','hdnFrombranch')" onkeyup="Search_Branch(event,this,'lst_FromBranch','FromBranch',2);"
                        onkeydown="return on_keydown(event,'txt_FromBranch','lst_FromBranch');" onfocus="On_Focus('txt_FromBranch','lst_FromBranch');"
                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                    <asp:ListBox ID="lst_FromBranch" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_FromBranch')"
                        runat="server" TabIndex="20"></asp:ListBox>
                        <asp:HiddenField ID="hdnFrombranch" Value="0" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td style="width: 20%" class="TD1">To Branch:</td>
                    <td style="width: 29%">
                    <asp:TextBox ID="txt_ToBranch" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                        onblur="On_BranchLostFocus('txt_ToBranch','lst_ToBranch','hdn_Tobranch')" onkeyup="Search_Branch(event,this,'lst_ToBranch','ToBranch',2);"
                        onkeydown="return on_keydown(event,'txt_ToBranch','lst_ToBranch');" onfocus="On_Focus('txt_ToBranch','lst_ToBranch');"
                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                    <asp:ListBox ID="lst_ToBranch" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_ToBranch')"
                        runat="server" TabIndex="20"></asp:ListBox>
                    <asp:HiddenField ID="hdn_Tobranch" Value="0" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">Attach Branches:</td>
                    <td colspan="5" style="width: 200px">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3"
                                    CssClass="Grid" runat="server" OnCancelCommand="dgGrid_CancelCommand" OnDeleteCommand="dgGrid_DeleteCommand"
                                    OnEditCommand="dgGrid_EditCommand" OnItemCommand="dgGrid_ItemCommand" OnItemDataBound="dgGrid_ItemDataBound"
                                    OnUpdateCommand="dgGrid_UpdateCommand">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Branch Name">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Branch_Name")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>                                                    
                                                    <asp:TextBox ID="txt_AttachBranch" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX" MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_AttachBranch" Style="position: absolute; z-index: 1000" runat="server" TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_AttachBranchId" runat="server" Value="0"></asp:HiddenField>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                    <asp:TextBox ID="txt_AttachBranch" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX" MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                    <asp:ListBox ID="lst_AttachBranch" Style="position: absolute; z-index: 1000" runat="server" TabIndex="50"></asp:ListBox>
                                                    <asp:HiddenField ID="hdn_AttachBranchId" runat="server" Value="0"></asp:HiddenField>
                                            </FooterTemplate>
                                            <HeaderStyle Width="50%" />
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                            <HeaderStyle Width="10%" />
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">Driver Name:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDriver" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                    onblur="On_DriverLostFocus('txtDriver','lstDriver','hdnDriver','Driver');" onkeyup="Search_Driver(event,this,'lstDriver','Driver',2);"
                                    onkeydown="return on_keydown(event,'txtDriver','lstDriver');" onfocus="On_Focus('txtDriver','lstDriver');"
                                    MaxLength="50" EnableViewState="False"></asp:TextBox>
                                <asp:ListBox ID="lstDriver" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtDriver')"
                                    runat="server" TabIndex="20"></asp:ListBox>
                                <asp:HiddenField ID="hdnDriver" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td style="width: 50%" align="left" colspan="3">
                        <asp:LinkButton ID="lnkAddDriver" Font-Bold="true" OnClientClick="return Add_Driver_Window_New();"
                            runat="server" Text="Add New"></asp:LinkButton>
                        <asp:HiddenField ID="hdnDriverpath" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">Mobile No 1:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_MobileNo1" runat="server" CssClass="TEXTBOX" onkeypress="return Only_Integers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">Mobile No 2:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_MobileNo2" runat="server" CssClass="TEXTBOX" onkeypress="return Only_Integers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">Cleaner Name:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>                                    
                                <asp:TextBox ID="txtCleaner" AutoCompleteType="Disabled" runat="server" CssClass="TEXTBOX"
                                    onblur="On_DriverLostFocus('txtCleaner','lstCleaner','hdnCleaner','Cleaner');" onkeyup="Search_Cleaner(event,this,'lstCleaner','Cleaner',2);"
                                    onkeydown="return on_keydown(event,'txtCleaner','lstCleaner');" onfocus="On_Focus('txtCleaner','lstCleaner');"
                                    MaxLength="50" EnableViewState="False"></asp:TextBox>
                                <asp:ListBox ID="lstCleaner" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtCleaner')"
                                    runat="server" TabIndex="20"></asp:ListBox>
                                <asp:HiddenField ID="hdnCleaner" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">*</td>
                    <td style="width: 50%" align="left" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Mobile No 1:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_MobileNo1CL" runat="server" CssClass="TEXTBOX" onkeypress="return Only_Integers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">
                        Mobile No 2:</td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_MobileNo2CL" runat="server" CssClass="TEXTBOX" onkeypress="return Only_Integers(this,event);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        TDS Certificate To:</td>
                    <td style="width: 29%">
                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
              <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlTDSCertificateTo" runat="server" CssClass="DROPDOWN" onchange="TDSCertificateToChange()">
                            <asp:ListItem Value="0">-- Select One --</asp:ListItem>
                            <asp:ListItem Value="1">Owner</asp:ListItem>
                            <asp:ListItem Value="2">Broker</asp:ListItem>
                        </asp:DropDownList>
                        <%--</ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                <asp:AsyncPostBackTrigger ControlID="hdn_VehicleCategoryIds" />
                <asp:AsyncPostBackTrigger ControlID="DDLFromBranch" />
                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
              </Triggers>
            </asp:UpdatePanel>--%>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr id="trIsRCRecieved" runat="server">
                    <td style="width: 20%" class="TD1">
                        Is RC Recieved:</td>
                    <td style="width: 5%">
                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
              <ContentTemplate>--%>
                        <asp:CheckBox ID="chkIsRCRecieved" onclick="CalculateTDSPercent()" runat="server" />
                        <%--</ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                <asp:AsyncPostBackTrigger ControlID="ddlTDSCertificateTo" />
                <asp:AsyncPostBackTrigger ControlID="DDLFromBranch" />
                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
              </Triggers>
            </asp:UpdatePanel>--%>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr id="trIsPanCardRecieved" runat="server">
                    <td style="width: 20%" class="TD1">
                        Is Pan Card Recieved:</td>
                    <td style="width: 29%">
                        <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
              <ContentTemplate>--%>
                        <asp:CheckBox ID="chkIsPanCardRecieved" onclick="CalculateTDSPercent()" runat="server" />
                        <%--</ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                <asp:AsyncPostBackTrigger ControlID="ddlTDSCertificateTo" />
                <asp:AsyncPostBackTrigger ControlID="DDLFromBranch" />
                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
              </Triggers>
            </asp:UpdatePanel>--%>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Hire Amount:</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtHireAmount" runat="server" CssClass="TEXTBOXNOS" onblur="CalculateBalance();"
                            onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lblTDSPercent" runat="server" Text="TDS" CssClass="LABEL"></asp:Label></td>
                    <asp:HiddenField ID="hdnTDSPercent" runat="server" />
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblTDSAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnTDSAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lblSurChargePercent" runat="server" Text="Surcharge" CssClass="LABEL"></asp:Label></td>
                    <asp:HiddenField ID="hdnSurchargePercent" runat="server" />
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblSurChargeAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnSurChargeAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lblAdditionalSurchargeCessPercent" runat="server" Text="Additional Surcharge Cess"
                            CssClass="LABEL"></asp:Label></td>
                    <asp:HiddenField ID="hdnAdditionalSurchargeCessPercent" runat="server" />
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblAdditionalSurchargeCessAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnAdditionalSurchargeCessAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lblAddistionalEducationCessPercent" runat="server" Text="Additional Education Cess"
                            CssClass="LABEL"></asp:Label></td>
                    <asp:HiddenField ID="hdnAddistionalEducationCessPercent" runat="server" />
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblAddistionalEducationCessAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnAddistionalEducationCessAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lblTotalTDS" runat="server" Text="Total TDS Amount:" CssClass="LABEL"></asp:Label></td>
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblTotalTDSAmount" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnTotalTDSAmount" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Truck Hire Payable:</td>
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblTruckHirePayable" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnTruckHirePayable" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Advance:</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtAdvance" runat="server" CssClass="TEXTBOXNOS" onblur="CalculateBalance();"
                            onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Balance:</td>
                    <td style="width: 29%" class="TD1">
                        <asp:Label ID="lblBalance" runat="server" CssClass="LABEL"></asp:Label>
                        <asp:HiddenField ID="hdnBalance" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        &nbsp</td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
              <ContentTemplate>
                        <uc3:WucHierarchyWithID ID="WucHierarchyWithIDATH" runat="server"></uc3:WucHierarchyWithID>
                        </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                
              </Triggers>
            </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
              <ContentTemplate>
                        <uc3:WucHierarchyWithID ID="WucHierarchyWithIDBTH" runat="server"></uc3:WucHierarchyWithID>
                         </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
              </Triggers>
            </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Remarks:</td>
                    <td style="width: 80%" colspan="5">
                        <asp:TextBox ID="txtRemarks" CssClass="TEXTBOX" TextMode="MultiLine" Height="30px"
                            MaxLength="100" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                        <asp:Button ID="btn_hidden" runat="server" Text="" OnClick="btn_hidden_Click"  style="display:none" />                        
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>                           
                        </Triggers>
                    </asp:UpdatePanel>
                                     
                        </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClientClick="return validateUI()"
                            OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                                <asp:HiddenField ID="hdnlhpoID" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
<script language ="javascript" type ="text/javascript">
//setFocusonPageLoad();        

function update_ATDBTH()
{
    document.getElementById('<%=btn_hidden.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_hidden.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_hidden.ClientID%>').click();
}

</script>
