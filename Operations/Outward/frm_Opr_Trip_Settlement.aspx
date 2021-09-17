<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Opr_Trip_Settlement.aspx.cs"
  Inherits="frm_Opr_Trip_Settlement" Culture="auto" meta:resourcekey="PageResource1"
  UICulture="auto" %>

<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
  TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
  TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <%-- <meta http-equiv="page-enter" content="blendtrans(duration=-1)" />
    <meta http-equiv="page-end" content="blendtrans(duration=-1)" />
    <meta http-equiv="page-exit" content="blendtrans(duration=-1)" />--%>
  <title>TRIP SETTLEMENT</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

  <script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

  <script type="text/javascript" src="../../Javascript/Operations/Outward/Trip Settlement_2.js"></script>

  <script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
  <form id="form1" runat="server">
    <div>
      <asp:ScriptManager ID="SCM_TripSettlement" runat="server" />
      <table class="TABLE">
        <tr>
          <td colspan="6" class="TDGRADIENT">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="TRIP SETTLEMENT"
              meta:resourcekey="lbl_HeadingResource1"></asp:Label>
          </td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Trip Settlement No :</td>
          <td style="width: 29%">
            <asp:TextBox ID="txt_TripSettlementNo" Width="60%" runat="Server" CssClass="TEXTBOXASLABEL"
              ReadOnly="true" /></td>
          <td class="TDMANDATORY" style="width: 1%">
          </td>
          <td class="TD1" style="width: 20%">
            Trip Settlement Date :</td>
          <td style="width: 29%">
            <uc1:WucDatePicker ID="WucSettelemtDate" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Vehicle No :</td>
          <td style="width: 29%">
            <uc2:WucVehicleSearch ID="WucVehicleNo" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%" colspan="4">
            *</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Driver :</td>
          <td style="width: 29%">
            <cc2:DDLSearch ID="ddl_Driver" CallBackAfter="2" IsCallBack="True"
              runat="server" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchDriverName"/>
          </td>
          <td class="TDMANDATORY" style="width: 1%" colspan="4">
            *</td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Trip Start Date :</td>
          <td style="width: 29%">
            <uc1:WucDatePicker ID="dtp_TripStartDate" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
          </td>
          <td class="TD1" style="width: 20%">
            Trip End Date :</td>
          <td style="width: 29%">
            <uc1:WucDatePicker ID="dtp_TripEndDate" runat="server" />
          </td>
          <td class="TDMANDATORY" style="width: 1%">
          </td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
      </table>
      <table class="TABLE">
        <tr>
          <td style="width: 100%" colspan="6">
            <fieldset>
              <legend>Trip Details</legend>
              <asp:UpdatePanel ID="upd_pnl_dg_TripHireChallans" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_TripHireChallans" />
                </Triggers>
                <ContentTemplate>
                  <div id="div_Trip" class="DIV">
                    <asp:DataGrid ID="dg_TripHireChallans" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
                      CellPadding="2" CssClass="GRID" runat="server" OnCancelCommand="dg_TripHireChallans_CancelCommand"
                      OnDeleteCommand="dg_TripHireChallans_DeleteCommand" OnEditCommand="dg_TripHireChallans_EditCommand"
                      OnItemCommand="dg_TripHireChallans_ItemCommand" OnItemDataBound="dg_TripHireChallans_ItemDataBound"
                      OnUpdateCommand="dg_TripHireChallans_UpdateCommand">
                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <Columns>
                        <asp:TemplateColumn HeaderText="Trip Memo No">
                          <FooterTemplate>
                            <asp:TextBox ID="txt_THCNo" CssClass="TEXTBOX" Width="97%" runat="server" BorderWidth="1px"
                              onblur="Uppercase(this)" MaxLength="25" meta:resourcekey="txt_THCNoResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Trip_Memo_No") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_THCNo" CssClass="TEXTBOX" Width="97%" runat="server" BorderWidth="1px"
                              MaxLength="25" meta:resourcekey="txt_THCNoResource1" />
                          </EditItemTemplate>
                          <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Date">
                          <FooterTemplate>
                            <ComponentArt:Calendar ID="dtp_THCDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                              ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                              MinDate="1900-01-01" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Trip_Date", "{0:dd/MM/yyyy}") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <ComponentArt:Calendar ID="dtp_THCDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                              ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                              MinDate="1900-01-01" />
                          </EditItemTemplate>
                          <ItemStyle />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="From Branch">
                          <FooterTemplate>
                            <asp:DropDownList ID="ddl_FromBranch" CssClass="DROPDOWN" runat="server" Width="100%"
                              meta:resourcekey="ddl_FromBranchResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "From_Branch_Name") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:DropDownList ID="ddl_FromBranch" CssClass="DROPDOWN" runat="server" Width="100%"
                              meta:resourcekey="ddl_FromBranchResource2" />
                          </EditItemTemplate>
                          <HeaderStyle Width="15%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="To Branch">
                          <FooterTemplate>
                            <asp:DropDownList ID="ddl_ToBranch" CssClass="DROPDOWN" runat="server" Width="100%"
                              meta:resourcekey="ddl_ToBranchResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "To_Branch_Name") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:DropDownList ID="ddl_ToBranch" CssClass="DROPDOWN" runat="server" Width="100%"
                              meta:resourcekey="ddl_ToBranchResource2" />
                          </EditItemTemplate>
                          <HeaderStyle Width="15%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Start Kms">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_StartKms" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="8" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_StartKmsResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Start_KM") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_StartKms" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="8" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_StartKmsResource1" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="End Kms">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_EndKms" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="8" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_EndKmsResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "End_KM") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_EndKms" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="8" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_EndKmsResource1" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Kms Run">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:Label ID="lbl_KmsRun" CssClass="TEXTBOXNOS" Width="96%" runat="server" Font-Bold="True"
                              meta:resourcekey="lbl_KmsRunResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "KM_Run") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:Label ID="lbl_KmsRun" CssClass="TEXTBOXNOS" Width="96%" runat="server" Font-Bold="True"
                              meta:resourcekey="lbl_KmsRunResource1" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total Act. Wt.">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_ActWt" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_ActWtResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total_Act_Wt") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_ActWt" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_ActWtResource1" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="12%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Hire Amt">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_HireAmount" CssClass="TEXTBOXNOS" Width="96%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_HireAmountResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Hire_Amount") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_HireAmount" CssClass="TEXTBOXNOS" Width="96%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_HireAmountResource1" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="12%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Advance">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_Advance" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_AdvanceResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Advance_Amount") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_Advance" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_AdvanceResource1" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                          meta:resourcekey="EditCommandColumnResource1">
                          <HeaderStyle Width="3%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Delete">
                          <FooterTemplate>
                            <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                              meta:resourcekey="lbtn_DeleteResource1" /></ItemTemplate>
                          <HeaderStyle Width="3%" />
                        </asp:TemplateColumn>
                      </Columns>
                    </asp:DataGrid>
                  </div>
                </ContentTemplate>
              </asp:UpdatePanel>
            </fieldset>
          </td>
        </tr>
        <tr>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <asp:HiddenField ID="hdn_KMsRun" runat="server" />
            <asp:HiddenField ID="hdn_total_hire_amount" Value="0" runat="server" />
            <asp:HiddenField ID="hdn_total_advance_amount" Value="0" runat="server" />
          </td>
        </tr>
        <tr>
          <td colspan="6" style="width: 100%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_TripHireChallans" />
              </Triggers>
              <ContentTemplate>
                <table width="100%">
                  <tr>
                    <td style="width: 40%" class="TD1" colspan="2">
                      <asp:Label ID="lbl_Challan_Details_Total" runat="server" CssClass="LABELNOS" Width="50px"
                        Font-Bold="True" Text="Totals :" />
                    </td>
                    <td style="width: 15%" align="right">
                      <asp:Label ID="lbl_TotalKmsRun" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" meta:resourcekey="lbl_TotalKmsRunResource1" />
                    </td>
                    <td style="width: 15%" class="TD1">
                      <asp:Label ID="lbl_Total_Actual_Wt" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                    <td style="width: 15%" class="TD1">
                      <asp:Label ID="lbl_Hire_Aount" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                    <td style="width: 15%" align="right">
                      <asp:Label ID="lbl_Total_Advance" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                  </tr>
                  <%--<tr visible="false">
                    <td style="width: 10%" class="TD1" >
                      <b>
                        <asp:Label ID="lbl_Start_Kms" Font-Bold="True" runat="server" Text="Start Kms :"
                          meta:resourcekey="lbl_Start_KmsResource1"></asp:Label></b></td>
                    <td style="width: 20%" align="right">
                      <asp:Label ID="lbl_StartKms" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="50px" Font-Bold="True" meta:resourcekey="lbl_StartKmsResource1" /></td>
                    <td style="width: 10%" class="TD1">
                      <b>
                        <asp:Label ID="lbl_End_Kms" runat="server" Text="End Kms :" Font-Bold="True" meta:resourcekey="lbl_End_KmsResource1"></asp:Label></b></td>
                    <td style="width: 20%">
                      <asp:Label ID="lbl_EndKms" runat="server" CssClass="LABELNOS" BorderWidth="1px" Width="50px"
                        Font-Bold="True" meta:resourcekey="lbl_EndKmsResource1" /></td>
                    <td style="width: 10%" class="TD1">
                      <b>
                        <asp:Label ID="lbl_Kms_Run" runat="server" Text="Kms Run :" meta:resourcekey="lbl_Kms_RunResource1"></asp:Label></b></td>
                    <td style="width: 30%">
                    </td>
                  </tr>--%>
                  <%--                  <tr>
                    <td style="width: 19%" class="TD1">
                      <b>
                        <asp:Label ID="lbl_Kms_Run" runat="server" Text="Kms Run :" meta:resourcekey="lbl_Kms_RunResource1"></asp:Label></b></td>
                    <td style="width: 30%">
                      <asp:Label ID="lbl_TotalKmsRun" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="97%" Font-Bold="True" meta:resourcekey="lbl_TotalKmsRunResource1" /></td>
                    <td style="width: 1%" class="TD1">
                    </td>
                    <td style="width: 20%" class="TD1">
                    </td>
                    <td style="width: 29%" class="TD1">
                    </td>
                    <td style="width: 1%">
                    </td>
                  </tr>--%>
                </table>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td style="width: 100%" colspan="6">
            <fieldset>
              <legend>Fuel Details</legend>
              <asp:UpdatePanel ID="upd_pnl_dg_TripFuelDetails" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_TripFuelDetails" />
                </Triggers>
                <ContentTemplate>
                  <div id="div_Fuel" class="DIV">
                    <asp:DataGrid ID="dg_TripFuelDetails" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
                      CellPadding="2" CssClass="GRID" runat="server" OnCancelCommand="dg_TripFuelDetails_CancelCommand"
                      OnDeleteCommand="dg_TripFuelDetails_DeleteCommand" OnEditCommand="dg_TripFuelDetails_EditCommand"
                      OnItemCommand="dg_TripFuelDetails_ItemCommand" OnItemDataBound="dg_TripFuelDetails_ItemDataBound"
                      OnUpdateCommand="dg_TripFuelDetails_UpdateCommand" meta:resourcekey="dg_TripFuelDetailsResource2">
                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <Columns>
                        <asp:TemplateColumn HeaderText="Is Cash">
                          <ItemStyle HorizontalAlign="Center" />
                          <FooterTemplate>
                            <asp:CheckBox ID="chk_IsCash" runat="server" meta:resourcekey="chk_IsCashResource3" AutoPostBack="True" OnCheckedChanged="chk_IsCash_CheckedChanged" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Is_Cash") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:CheckBox ID="chk_IsCash" runat="server" meta:resourcekey="chk_IsCashResource4" AutoPostBack="True" OnCheckedChanged="chk_IsCash_CheckedChanged"/>
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Fuel Date">
                          <FooterTemplate>
                            <ComponentArt:Calendar ID="dtp_FuelDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                              ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                              MinDate="1900-01-01" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Fuel_Date", "{0:dd/MM/yyyy}") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <ComponentArt:Calendar ID="dtp_FuelDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                              ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                              MinDate="1900-01-01" />
                          </EditItemTemplate>
                          <ItemStyle Width="10%" />
                          <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Place">
                          <FooterTemplate>
                            <asp:TextBox ID="txt_Place" CssClass="TEXTBOX" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="25" onblur="Uppercase(this)" meta:resourcekey="txt_PlaceResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Petrol_Pump_Place") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_Place" CssClass="TEXTBOX" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="25" onblur="Uppercase(this)" meta:resourcekey="txt_PlaceResource4" />
                          </EditItemTemplate>
                          <HeaderStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Pump">
                          <FooterTemplate>
                            <asp:DropDownList ID="ddl_Pump" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_PumpResource2" />
                            <asp:TextBox ID="txt_Pump" CssClass="TEXTBOX" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="25" Visible="False" onblur="Uppercase(this)" meta:resourcekey="txt_PumpResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Petrol_Pump_Name") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:DropDownList ID="ddl_Pump" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_PumpResource1" />
                            <asp:TextBox ID="txt_Pump" CssClass="TEXTBOX" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="25" Visible="False" meta:resourcekey="txt_PumpResource4" />
                          </EditItemTemplate>
                          <HeaderStyle Width="13%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Memo No/ Slip No">
                          <FooterTemplate>
                            <asp:TextBox ID="txt_MemoSlipNo" CssClass="TEXTBOX" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="25" meta:resourcekey="txt_MemoSlipNoResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Fuel_Slip_No") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_MemoSlipNo" CssClass="TEXTBOX" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="25" meta:resourcekey="txt_MemoSlipNoResource4" />
                          </EditItemTemplate>
                          <HeaderStyle Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Qty">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_Qty" CssClass="TEXTBOXNOS" Text="0" Width="95%" runat="server"
                              BorderWidth="1px" MaxLength="8" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_QtyResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Quantity") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_Qty" CssClass="TEXTBOXNOS" Text="0" Width="95%" runat="server"
                              BorderWidth="1px" MaxLength="8" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_QtyResource4" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="7%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Rate">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_Rate" CssClass="TEXTBOXNOS" Text="0" Width="95%" runat="server"
                              BorderWidth="1px" MaxLength="8" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_RateResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Rate") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_Rate" CssClass="TEXTBOXNOS" Text="0" Width="95%" runat="server"
                              BorderWidth="1px" MaxLength="8" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_RateResource4" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="7%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Fuel Amt">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_FuelAmount" CssClass="TEXTBOXNOS" Text="0" Width="96%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_FuelAmountResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Fuel_Amount") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_FuelAmount" CssClass="TEXTBOXNOS" Text="0" Width="96%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_FuelAmountResource4" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Oil Amount">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_OilAmount" CssClass="TEXTBOXNOS" Text="0" Width="96%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_OilAmountResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Oil_Amount") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_OilAmount" CssClass="TEXTBOXNOS" Text="0" Width="96%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_OilAmountResource4" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Total">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:Label ID="lbl_Total" CssClass="TEXTBOXNOS" Text="0" Width="96%" runat="server"
                              Font-Bold="True" meta:resourcekey="lbl_TotalResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total_Amount") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:Label ID="lbl_Total" CssClass="TEXTBOXNOS" Text="0" Width="96%" runat="server"
                              Font-Bold="True" meta:resourcekey="lbl_TotalResource4" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="8%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="On Kms">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_OnKms" CssClass="TEXTBOXNOS" Text="0" Width="95%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Integers(this,event)"
                              meta:resourcekey="txt_OnKmsResource3" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "On_Kms") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_OnKms" CssClass="TEXTBOXNOS" Text="0" Width="95%" runat="server"
                              BorderWidth="1px" MaxLength="10" onkeypress="return Only_Integers(this,event)"
                              meta:resourcekey="txt_OnKmsResource4" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="7%" />
                        </asp:TemplateColumn>
                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                          meta:resourcekey="EditCommandColumnResource2">
                          <HeaderStyle Width="5%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Delete">
                          <FooterTemplate>
                            <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource2" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                              meta:resourcekey="lbtn_DeleteResource2" /></ItemTemplate>
                          <HeaderStyle Width="5%" />
                        </asp:TemplateColumn>
                      </Columns>
                    </asp:DataGrid>
                  </div>
                </ContentTemplate>
              </asp:UpdatePanel>
            </fieldset>
          </td>
        </tr>
        <tr>
          <td colspan="6" style="width: 100%">
            <asp:UpdatePanel ID="updt_Pnl_Fuel_Details" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_TripFuelDetails" />
              </Triggers>
              <ContentTemplate>
                <table width="100%">
                  <tr>
                    </td>
                    <td style="width: 19%" class="TD1">
                      <asp:Label ID="lbl_Total_Fuel_Exp" runat="server" CssClass="LABELNOS" Width="50px"
                        Font-Bold="True" Text="Totals :" />
                    </td>
                    <td style="width: 20%" align="right">
                      <asp:Label ID="lbl_Tot_Fuel_Qty" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                    <td style="width: 20%" class="TD1">
                      <asp:Label ID="lbl_Total_Fuel_Amount" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                    <td style="width: 20%" class="TD1">
                      <asp:Label ID="lbl_Total_Oil_Amount" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                    <td style="width: 20%" align="right">
                      <asp:Label ID="lbl_TotalFuel_Oil_Amount" runat="server" CssClass="LABELNOS" BorderWidth="1px"
                        Width="100px" Font-Bold="True" />
                    </td>
                    <td style="width: 1%">
                    </td>
                  </tr>
                </table>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td>
            <asp:HiddenField ID="hdn_FuelRate" runat="server" />
            <asp:HiddenField ID="hdn_FuelAmt" runat="server" />
            <asp:HiddenField ID="hdn_TotalAmt" runat="server" />
            <asp:HiddenField ID="hdn_total_diesel_cash" runat="server" />
            <asp:HiddenField ID="hdn_total_diesel_credit" Value="0" runat="server" />
          </td>
        </tr>
        <tr>
          <td style="width: 100%" colspan="6">
            <fieldset>
              <legend>Trip Expense Details</legend>
              <asp:UpdatePanel ID="upd_pnl_dg_TripExpense" runat="server">
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_TripExpense" />
                </Triggers>
                <ContentTemplate>
                  <div id="div_Exp" class="DIV">
                    <asp:DataGrid ID="dg_TripExpense" Width="100%" AutoGenerateColumns="False" ShowFooter="True"
                      CellPadding="2" CssClass="GRID" runat="server" OnCancelCommand="dg_TripExpense_CancelCommand"
                      OnDeleteCommand="dg_TripExpense_DeleteCommand" OnEditCommand="dg_TripExpense_EditCommand"
                      OnItemCommand="dg_TripExpense_ItemCommand" OnItemDataBound="dg_TripExpense_ItemDataBound"
                      OnUpdateCommand="dg_TripExpense_UpdateCommand" meta:resourcekey="dg_TripExpenseResource1">
                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <Columns>
                        <asp:TemplateColumn HeaderText="Expense Head">
                          <FooterTemplate>
                            <asp:DropDownList ID="ddl_ExpenseHead" CssClass="DROPDOWN" runat="server" Width="100%"
                              meta:resourcekey="ddl_ExpenseHeadResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Expense_Head") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:DropDownList ID="ddl_ExpenseHead" CssClass="DROPDOWN" runat="server" Width="100%"
                              meta:resourcekey="ddl_ExpenseHeadResource2" />
                          </EditItemTemplate>
                          <HeaderStyle Width="20%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Amount">
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterTemplate>
                            <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_AmountResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Expense_Amount") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_Amount" CssClass="TEXTBOXNOS" Width="96%" runat="server" BorderWidth="1px"
                              MaxLength="10" onkeypress="return Only_Numbers(this,event)" meta:resourcekey="txt_AmountResource2" />
                          </EditItemTemplate>
                          <HeaderStyle HorizontalAlign="Right" Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Description">
                          <FooterTemplate>
                            <asp:TextBox ID="txt_Description" CssClass="TEXTBOX" Width="99%" runat="server" BorderWidth="1px"
                              MaxLength="200" meta:resourcekey="txt_DescriptionResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Description") %>
                          </ItemTemplate>
                          <EditItemTemplate>
                            <asp:TextBox ID="txt_Description" CssClass="TEXTBOX" Width="99%" runat="server" BorderWidth="1px"
                              MaxLength="200" meta:resourcekey="txt_DescriptionResource2" />
                          </EditItemTemplate>
                          <HeaderStyle Width="60%" />
                        </asp:TemplateColumn>
                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                          meta:resourcekey="EditCommandColumnResource1">
                          <HeaderStyle Width="5%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Delete">
                          <FooterTemplate>
                            <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                          </FooterTemplate>
                          <ItemTemplate>
                            <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                              meta:resourcekey="lbtn_DeleteResource1" /></ItemTemplate>
                          <HeaderStyle Width="5%" />
                        </asp:TemplateColumn>
                      </Columns>
                    </asp:DataGrid>
                  </div>
                </ContentTemplate>
              </asp:UpdatePanel>
            </fieldset>
          </td>
        </tr>
        <tr>
          <td colspan="6" style="width: 100%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_TripExpense" />
              </Triggers>
              <ContentTemplate>
                <table width="100%">
                  <tr>
                    <td style="width: 80%" class="TD1" colspan="5">
                      <asp:Label ID="lbl_Total_Expense_Details" runat="server" CssClass="LABELNOS" Width="50px"
                        Font-Bold="True" Text="Totals :" />
                    </td>
                    <td style="width: 20%" align="right">
                      <asp:Label ID="lbl_Total_expense" runat="server" CssClass="LABELNOS" Font-Bold="True" />
                    </td>
                  </tr>
                </table>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td>
            <asp:HiddenField ID="hdn_total_expense" Value="0" runat="server" />
          </td>
        </tr>
        <tr>
          <td style="width: 100%" colspan="6">
            <asp:UpdatePanel ID="updatePanel4" runat="Server" UpdateMode="Conditional">
              <ContentTemplate>
                <table width="100%">
                  <tr>
                    <td class="TD1" style="width: 20%">
                      <asp:Label ID="lbl_TotalTripCost" runat="server" Text="Total Trip Cost :" meta:resourcekey="lbl_Total_Trip_CostResource1"></asp:Label></td>
                    <td class="TD1" style="width: 29%">
                      <asp:Label ID="lbl_Total_Trip_Cost" Text="0.00" Width="100%" runat="server" CssClass="LABELNOS"
                        BorderWidth="1px" Font-Bold="True" meta:resourcekey="lbl_TotalTripCostResource1"></asp:Label>
                    </td>
                    <td class="TD1" style="width: 1%" />
                    <td class="TD1" style="width: 20%">
                      <asp:Label ID="lbl_Driver_ClosingBalance" runat="server" Text="Driver Closing Balance :"
                        meta:resourcekey="lbl_Driver_ClosingBalanceResource1"></asp:Label></td>
                    <td class="TD1" style="width: 29%">
                      <asp:Label ID="lbl_DriverClosingBalance" Text="0.00" Width="100%" runat="server"
                        CssClass="LABELNOS" BorderWidth="1px" Font-Bold="True" meta:resourcekey="lbl_DriverClosingBalanceResource1"></asp:Label>
                    </td>
                    <td class="TD1" style="width: 1%">
                      <asp:Label ID="lbl_CBcrdr" Width="100%" runat="server" CssClass="LABEL" meta:resourcekey="lbl_CBcrdrResource1" /></td>
                  </tr>
                </table>
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                <asp:AsyncPostBackTrigger ControlID="dg_TripHireChallans" />
                <asp:AsyncPostBackTrigger ControlID="dg_TripFuelDetails" />
                <asp:AsyncPostBackTrigger ControlID="dg_TripExpense" />
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td class="TD1" style="width: 20%">
            Remarks :</td>
          <td style="width: 79%" colspan="4">
            <asp:TextBox ID="txt_Remarks" Width="100%" runat="Server" CssClass="TEXTBOX" BorderWidth="1px"
              TextMode="MultiLine" Rows="5" meta:resourcekey="txt_RemarksResource1" /></td>
          <td class="TDMANDATORY" style="width: 1%">
          </td>
        </tr>
      </table>
      <table class="TABLE">
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
        <tr>
          <td style="text-align: center; width: 100%">
            <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" Text="Save" OnClientClick="return Allow_To_Save_TripSettlement()"
              OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
          </td>
        </tr>
        <tr>
          <td style="width: 100%">
            <asp:UpdatePanel ID="updatePanelSave" runat="Server" UpdateMode="Conditional">
              <ContentTemplate>
                &nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                  Text="Fields with * mark are mandatory" meta:resourcekey="lbl_ErrorsResource1" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                <asp:AsyncPostBackTrigger ControlID="dg_TripHireChallans" />
                <asp:AsyncPostBackTrigger ControlID="dg_TripFuelDetails" />
                <asp:AsyncPostBackTrigger ControlID="dg_TripExpense" />
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td colspan="6">
            &nbsp;</td>
        </tr>
      </table>
      <asp:UpdatePanel ID="UpdatePanel1" runat="Server" UpdateMode="Conditional">
        <ContentTemplate>
          <table class="TABLE">
            <tr>
              <td colspan="6">
                &nbsp;</td>
            </tr>
            <tr>
              <td colspan="6">
                &nbsp;</td>
            </tr>
            <tr>
              <td colspan="6">
                <asp:HiddenField ID="hdf_ResourecString" runat="server" />
              </td>
            </tr>
          </table>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_TripHireChallans" />
          <asp:AsyncPostBackTrigger ControlID="dg_TripFuelDetails" />
          <asp:AsyncPostBackTrigger ControlID="dg_TripExpense" />
          <asp:AsyncPostBackTrigger ControlID="ddl_Driver" />
        </Triggers>
      </asp:UpdatePanel>
    </div>
  </form>
</body>
</html>
