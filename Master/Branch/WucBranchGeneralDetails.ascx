<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBranchGeneralDetails.ascx.cs"
  Inherits="Master_Branch_WucBranchGeneralDetails" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
  TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucAddress.ascx" TagName="WucAddress" TagPrefix="uc2" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Master/Branch/Branch.js"></script>

<script type="text/javascript" language="javascript">
 function EnableDisable_Agency()
 {
    var ddl_BranchType = document.getElementById('<%=DDL_BranchType.ClientID%>');
    var tr_Agency = document.getElementById('tr_Agency');
    var chk_franchisee = document.getElementById('WucBranch1_WucBranchDeptServices1_chkbx_isFranchiseebranch');

    if (ddl_BranchType.value == '2')
    { 
        tr_Agency.style.display ='inline';
        chk_franchisee.disabled = true;
    }
    else
    {
        tr_Agency.style.display='none';
        chk_franchisee.disabled = false;
    }
 }

</script>

<table class="TABLE" width="100%">
  <tr>
    <td colspan="6">
      &nbsp;</td>
  </tr>
  <tr>
    <td colspan="6" style="width: 100%">
      <asp:Panel ID="pnl_generalDetails" GroupingText="General Details" Font-Bold="False"
        runat="server" Width="100%" meta:resourcekey="pnl_generalDetailsResource1" CssClass="PANEL">
        <table width="100%">
          <tr>
            <td colspan="6">
              &nbsp;</td>
          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_BrcCode" runat="server" CssClass="LABEL" Text="Branch Code :"
                meta:resourcekey="lbl_BrcCodeResource1"></asp:Label>
            </td>
            <td class="TD1" style="width: 29%">
              <asp:TextBox ID="Txt_BranchCode" runat="server" BorderWidth="1px" onblur="Uppercase(this)"
                CssClass="TEXTBOX" MaxLength="5" meta:resourcekey="Txt_BranchCodeResource1" Width="250px"></asp:TextBox>
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
            <td class="TD1" style="width: 20%">
            </td>
            <td class="TD1" style="width: 29%">
            </td>
            <td class="TD1" style="width: 1%">
            </td>
          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_BrcName" runat="server" CssClass="LABEL" Text="Branch Name :"
                meta:resourcekey="lbl_BrcNameResource1"></asp:Label>
            </td>
            <td class="TD1" style="width: 29%">
              <asp:TextBox ID="Txt_BranchName" runat="server" BorderWidth="1px" onblur="Uppercase(this)"
                CssClass="TEXTBOX" MaxLength="50" meta:resourcekey="Txt_BranchNameResource1" Width="250px"></asp:TextBox>
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_BrcType" runat="server" CssClass="LABEL" Text="Branch Type :"
                meta:resourcekey="lbl_BrcTypeResource1"></asp:Label>
            </td>
            <td class="TD1" style="width: 29%">
              <asp:DropDownList ID="DDL_BranchType" runat="server" CssClass="DROPDOWN" onchange="EnableDisable_Agency()"
                meta:resourcekey="DDL_BranchTypeResource1">
              </asp:DropDownList>
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
          </tr>
          <tr id="tr_Agency">
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_Brcagency" runat="server" CssClass="LABEL" Text="Agency A/C :"
                meta:resourcekey="lbl_BrcagencyResource1"></asp:Label>
            </td>
            <td style="width: 29%">
              <cc1:DDLSearch ID="DDL_Agency" runat="server" AllowNewText="False" IsCallBack="True"
                DBTableName="FA_Master_Ledger" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAcountAgencyForBranch"
                CallBackAfter="2" PostBack="False" InjectJSFunction="" meta:resourcekey="DDL_AgencyResource1"
                Text="" />
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
            <td class="TD1" style="width: 20%">
            </td>
            <td class="TD1" style="width: 29%">
            </td>
            <td class="TD1" style="width: 1%">
            </td>
          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_BrcMemo" runat="server" CssClass="LABEL" Text="Manifest Destination :"
                meta:resourcekey="lbl_BrcMemoResource1"></asp:Label>
            </td>
            <td style="width: 29%">
              <cc1:DDLSearch ID="DDL_MemoDestination" runat="server" AllowNewText="False" IsCallBack="True"
                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetMemoDestForBranch" CallBackAfter="2"
                OnTxtChange="DDL_MemoDestination_TxtChange" PostBack="False" InjectJSFunction=""
                meta:resourcekey="DDL_MemoDestinationResource1" Text="" />
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_BrcHub" runat="server" CssClass="LABEL" Text="Default Hub :" meta:resourcekey="lbl_BrcHubResource1"></asp:Label>
            </td>
            <td style="width: 29%">
              <cc1:DDLSearch ID="DDL_DefaultHub" runat="server" AllowNewText="False" PostBack="True"
                IsCallBack="True" DBTableName="EC_Master_Branch" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetDefaultHubForBranch"
                CallBackAfter="2" Text="" OnTxtChange="DDL_DefaultHub_TxtChange" InjectJSFunction=""
                meta:resourcekey="DDL_DefaultHubResource1" />
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_BrcDelivery" runat="server" CssClass="LABEL" Text="Delivery At :"
                meta:resourcekey="lbl_BrcDeliveryResource1"></asp:Label>
            </td>
            <td style="width: 29%">
              <cc1:DDLSearch ID="DDL_DeliveryAt" runat="server" AllowNewText="False" PostBack="True"
                IsCallBack="True" DBTableName="EC_Master_Branch" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetDeliveryAtForBranch"
                CallBackAfter="2" Text="" OnTxtChange="DDL_DeliveryAt_TxtChange" InjectJSFunction=""
                meta:resourcekey="DDL_DeliveryAtResource1" />
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
              
            <td class="TD1" style="width: 20%">
              <asp:Label ID="Label1" runat="server" CssClass="LABEL" Text="Default Dly Type :"></asp:Label>
            </td>
            <td class="TD1" style="width: 29%">
              <asp:DropDownList ID="ddl_delivery_Type" runat="server" CssClass="DROPDOWN"/>
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
              

          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_STaxRegistrationNo" runat="server" CssClass="LABEL" Text="Service Tax Reg.No. :"></asp:Label>
            </td>
            <td class="TD1" style="width: 29%">
              <asp:TextBox ID="txt_STaxRegistrationNo" MaxLength="50" onblur="Uppercase(this)"
                runat="server" CssClass="TEXTBOX"></asp:TextBox>
            </td>
            <td class="TD1" style="width: 1%">
            </td>
            <td class="TD1" style="width: 20%">

            </td>
            <td class="TD1" style="width: 29%">

            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
          </tr>
          <tr>
            <td class="TD1" style="width: 20%;">
              <asp:Label ID="lbl_CntPerson" runat="server" CssClass="LABEL" Text="Contact Person :"
                meta:resourcekey="lbl_CntPersonResource1"></asp:Label>
            </td>
            <td class="TD1" style="width: 79%; height: 24px;" colspan="4">
              <asp:TextBox ID="Txt_ContactPerson" Width="100%" BorderWidth="1px" MaxLength="100"
                runat="server" CssClass="TEXTBOX" meta:resourcekey="Txt_ContactPersonResource1"></asp:TextBox>
            </td>
            <td class="TDMANDATORY" style="width: 1%; height: 24px;">
              *</td>
          </tr>
          <tr>
            <td colspan="6">
              <uc2:WucAddress ID="WucAddress1" runat="server"></uc2:WucAddress>
            </td>
          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_Region" runat="server" CssClass="LABEL" Text="Region :"></asp:Label>
            </td>
            <td style="width: 29%">
              <cc1:DDLSearch ID="ddl_Region" runat="server" AllowNewText="True" IsCallBack="True"
                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetRegionForBranch" CallBackAfter="2"
                PostBack="True" InjectJSFunction="" Text="" OnTxtChange="ddl_Region_TxtChange" />
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              <asp:Label ID="lbl_MandatoryRegion" Text="*" runat="server" /></td>
            <td class="TD1" style="width: 20%" />
            <td style="width: 29%" />
            <td style="width: 1%" />
          </tr>
          <tr>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_Area" runat="server" CssClass="LABEL" Text="Area :" meta:resourcekey="lbl_AreaResource1"></asp:Label>
            </td>
            <td style="width: 29%">
              <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                  <cc1:DDLSearch ID="DDL_Area" runat="server" AllowNewText="True" IsCallBack="True"
                    DBTableName="EC_Master_Area" OnTxtChange="DDL_Area_TextChanged" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetAreaForBranch"
                    CallBackAfter="2" PostBack="False" InjectJSFunction="" Text="" />
                  <asp:HiddenField ID="hdn_area" runat="server" />
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="ddl_Region" />
                </Triggers>
              </asp:UpdatePanel>
            </td>
            <td class="TDMANDATORY" style="width: 1%">
              *</td>
            <td class="TD1" style="width: 20%">
              <asp:Label ID="lbl_Started" runat="server" CssClass="LABEL" Text="Started On :" meta:resourcekey="lbl_StartedResource1"></asp:Label>
            </td>
            <td style="width: 29%" class="TDMANDATORY">
              <uc1:WucDatePicker ID="dtp_StartedOn" runat="server"></uc1:WucDatePicker>
            </td>
            <td class="TDMANDATORY" style="width: 1%">
            </td>
          </tr>
        </table>
      </asp:Panel>
    </td>
  </tr>
  <tr>
    <td colspan="6" style="height: 21px">
      &nbsp;</td>
  </tr>
  <tr>
    <td colspan="6" style="width: 100%">
      <asp:Panel ID="pnl_Devision" Font-Bold="True" GroupingText="Divisions" runat="server"
        meta:resourcekey="pnl_DevisionResource1">
        <asp:UpdatePanel ID="upnl_Division" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
            <table width="100%">
              <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 80%" colspan="5">
                  <asp:CheckBoxList ID="chk_List_Division" CssClass="CHECKBOXLIST" runat="server" RepeatDirection="Horizontal"
                    RepeatColumns="5" meta:resourcekey="chk_List_DivisionResource1">
                  </asp:CheckBoxList>
                </td>
              </tr>
            </table>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DDL_Area" />
          </Triggers>
        </asp:UpdatePanel>
      </asp:Panel>
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:HiddenField ID="hdn_BranchId" runat="server" />
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
    </td>
  </tr>
</table>
<%--<script type="text/javascript" language="javascript">
 EnableDisable_Agency();
</script>
--%>
