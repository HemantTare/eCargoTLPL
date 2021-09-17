<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMRDelivery.ascx.cs"
  Inherits="Finance_Accounting_Vouchers_WucMRDelivery" %>
<%@ Register Src="WucMRGeneralDetails.ascx" TagName="WucMRGeneralDetails" TagPrefix="uc1" %>
<%@ Register Src="WucMRCashChequeDetails.ascx" TagName="WucMRCashChequeDetails" TagPrefix="uc2" %>
<%@ Register Src="WucMRDeliveryDetails.ascx" TagName="WucMRDeliveryDetails" TagPrefix="uc3" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/MRDelivery.js"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script type="text/javascript">

function GetTotalAmount()
{
    var txt_Total_Receivable = document.getElementById('<%=txt_TotalReceivables.ClientID %>');
    return val(txt_Total_Receivable.value);
}
</script>

<ComponentArt:TabStrip ID="TabStrip1" SiteMapXmlFile="~/XML/MRDeliveryDetails.xml"
  EnableViewState="false" MultiPageId="MultiPage1" runat="server">
</ComponentArt:TabStrip>
<ComponentArt:MultiPage ID="MultiPage1" CssClass="MultiPage" runat="server" Width="100%"
  Style="left: 0px; top: 0px">
  <ComponentArt:PageView runat="server">
    <table class="TABLE" width="100%" border="0">
      <tr>
        <td colspan="6">
          <uc1:WucMRGeneralDetails ID="WucMRGeneralDetails1" runat="server" />
        </td>
      </tr>
      <tr>
        <td class="TD1" style="width: 20%">
          <asp:Label ID="lbl_Total_Amount" runat="server" CssClass="LABEL" Text="Total GC Amount :"></asp:Label></td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Total_Amount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 59%" colspan="3">
        </td>
      </tr>
      <tr id="tr_Advance_Amount" runat="server">
        <td class="TD1" style="width: 20%">
          <asp:Label ID="lbl_Advance_Amount" runat="server" CssClass="LABEL" Text="Advance Amount :"></asp:Label>
        </td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Advance_Amount" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOXNOS" Font-Bold="True" ReadOnly="True" Width="86%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td colspan="3" style="width: 59%">
        </td>
      </tr>
      <tr id="tr_TotalAmtCreditFor" runat="server">
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_GC_Total" runat="server" Text="GC Sub Total :" CssClass="LABEL"></asp:Label></td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_GCTotal" BackColor="Transparent" BorderColor="Transparent" BorderStyle="Solid"
                ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True" Width="86%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%" runat="server" id="TD_CrMemoForText">
          Credit Memo For :</td>
        <td style="width: 38%" runat="server" id="TD_CrMemoForValue">
          <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:RadioButtonList ID="rbtn_CreditMemoFor" runat="server" RepeatDirection="Horizontal"
                onclick="Disable_Control_On_Octroi()">
              </asp:RadioButtonList>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_Octr_form_Charges" runat="server" CssClass="LABEL" Text="Octroi Form Charges :"></asp:Label></td>
        <td style="width: 20%;">
          <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <%--<asp:TextBox ID="txt_Octr_Form_Charges" CssClass="TEXTBOXNOS" runat="server" Width="86%"
                onkeypress="return Only_Numbers(this,event)" onblur="Validate_Discount(this);Calculate_GrandTotal()"
                MaxLength="9" ReadOnly ="true"></asp:TextBox>--%>
              <asp:TextBox ID="txt_Octr_Form_Charges" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%">
              </asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%;">
        </td>
        <td style="width: 20%" class="TD1">
          <asp:UpdatePanel ID="UpdatePanel15" runat="server">
            <ContentTemplate>
              <asp:Label ID="lbl_Octr_Service_Charges" runat="server" CssClass="LABEL"></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <%--<asp:TextBox ID="txt_Octr_Service_Charges" CssClass="TEXTBOXNOS" runat="server" Width="50%"
                onkeypress="return Only_Numbers(this,event)" onblur="Validate_Discount(this);Calculate_GrandTotal()"
                MaxLength="9" ReadOnly ="true"></asp:TextBox>--%>
              <asp:TextBox ID="txt_Octr_Service_Charges" runat="server" BackColor="Transparent"
                BorderColor="Transparent" BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True"
                ReadOnly="True" />
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_GI_Charges" runat="server" CssClass="LABEL" Text="GI Charges :"></asp:Label></td>
        <td style="width: 20%;">
          <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <%--<asp:TextBox ID="txt_GI_Charges" CssClass="TEXTBOXNOS" runat="server" Width="86%"
                onkeypress="return Only_Numbers(this,event)" onblur="Validate_Discount(this);Calculate_GrandTotal()"
                MaxLength="9" ReadOnly ="true"></asp:TextBox>--%>
              <asp:TextBox ID="txt_GI_Charges" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%" />
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%;">
        </td>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Detention_Charges" runat="server" CssClass="LABEL" Text="Detention Charges :"></asp:Label></td>
        <td style="width: 20%">
          <%--<asp:TextBox ID="txt_Detention_Charges" CssClass="TEXTBOXNOS" runat="server" Width="50%"
            onkeypress="return Only_Numbers(this,event)" onblur="Calculate_GrandTotal()" MaxLength="9"
            ReadOnly ="true" ></asp:TextBox>--%>
          <asp:TextBox ID="txt_Detention_Charges" runat="server" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" />
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_Hamali_Charges" runat="server" CssClass="LABEL" Text="Hamali Charges :"></asp:Label></td>
        <td style="width: 20%;">
          <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
              <asp:TextBox ID="txt_Hamali_Charges" CssClass="TEXTBOXNOS" runat="server" Width="86%"
                onkeypress="return Only_Numbers(this,event)" onblur="Validate_Discount(this);Calculate_GrandTotal()"
                MaxLength="9"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%;">
        </td>
        <td style="width: 20%" class="HIDEGRIDCOL">
          <asp:Label ID="lbl_Local_Charges" runat="server" CssClass="LABEL" Text="Local Charges :"></asp:Label></td>
        <td style="width: 20%" class="HIDEGRIDCOL">
          <asp:TextBox ID="txt_Local_Charges" CssClass="TEXTBOXNOS" runat="server" Width="50%"
            onkeypress="return Only_Numbers(this,event)" onblur="Calculate_GrandTotal()" MaxLength="9"></asp:TextBox>
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Demurage_Charges" runat="server" CssClass="LABEL" Text="Demurage Charges :"></asp:Label></td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Demurage_Charges" CssClass="TEXTBOXNOS" runat="server" Width="86%"
                onkeypress="return Only_Numbers(this,event)" onblur="Validate_Discount(this);Calculate_GrandTotal()"
                MaxLength="9"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Demurage_Days" runat="server" CssClass="LABEL" Text="Demurage Days :"></asp:Label></td>
        <td style="width: 38%">
          <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Demurage_Days" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                Width="40%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr id="tr_DeliveryCommission" runat="server">
        <td class="TD1" style="width: 20%">
          <asp:Label ID="lbl_DeliveryCommission" runat="server" CssClass="LABEL" Text="Service Charges :"></asp:Label></td>
        <td style="width: 20%">
          <asp:TextBox ID="txt_DeliveryCommission" runat="server" CssClass="TEXTBOXNOS" MaxLength="9"
            onblur="Calculate_GrandTotal()" onkeypress="return Only_Numbers(this,event)" Width="86%"></asp:TextBox></td>
        <td style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 38%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_Addition_Charges" runat="server" CssClass="LABEL" Text="Additional Charges :"></asp:Label></td>
        <td style="width: 20%;">
          <asp:TextBox ID="txt_Addition_Charges" CssClass="TEXTBOXNOS" runat="server" Width="86%"
            onkeypress="return Only_Numbers(this,event)" onblur="Calculate_GrandTotal()" MaxLength="9"></asp:TextBox>
        </td>
        <td style="width: 1%;">
        </td>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_Add_Charge_remark" runat="server" CssClass="LABEL" Text="Add. Chrg Remark :"></asp:Label></td>
        <td style="width: 38%;">
          <asp:TextBox ID="txt_Add_Charge_remark" CssClass="TEXTBOX" runat="server" Width="98%"
            MaxLength="200"></asp:TextBox>
        </td>
        <td style="width: 1%;">
        </td>
      </tr>
      <tr>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Discount_Amount" runat="server" CssClass="LABEL" Text="Discount :"></asp:Label></td>
        <td style="width: 20%">
          <%--<asp:TextBox ID="txt_Discount_Amount" Enabled="false" CssClass="TEXTBOXNOS" runat="server"
            Width="86%" onkeypress="return Only_Numbers(this,event)" onblur="Calculate_GrandTotal()"
            MaxLength="9"></asp:TextBox>--%>
          <asp:TextBox ID="txt_Discount_Amount" runat="server" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="86%" />
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_discount_remark" runat="server" CssClass="LABEL" Text="Discount Reason :"></asp:Label></td>
        <td style="width: 38%">
          <%--<asp:TextBox ID="txt_discount_remark" CssClass="TEXTBOX" runat="server" Width="98%"
            MaxLength="100"></asp:TextBox>--%>
          <asp:TextBox ID="txt_discount_remark" runat="server" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" />
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Sub_Total" runat="server" CssClass="LABEL" Text="Memo Sub Total :"></asp:Label></td>
        <td style="width: 20%">
          <asp:TextBox ID="txt_Sub_Total" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True"
            Width="86%"></asp:TextBox></td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%;" class="HIDEGRIDCOL">
          <asp:Label ID="lbl_Rebooked_Charges" runat="server" CssClass="HIDEGRIDCOL" Text="Rebooked Charges :"></asp:Label></td>
        <td class="HIDEGRIDCOL" style="width: 20%;">
          <asp:TextBox ID="txt_Rebooked_Charges" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" ReadOnly="True" CssClass="HIDEGRIDCOL" runat="server" Font-Bold="True"
            Width="50%"></asp:TextBox></td>
        <td style="width: 1%;">
        </td>
      </tr>
      <tr>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_Tax_Abate" runat="server" CssClass="LABEL" Text="Tax Abatement :"></asp:Label></td>
        <td style="width: 20%;">
          <asp:TextBox ID="txt_Tax_Abate" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True"
            Width="86%"></asp:TextBox></td>
        <td style="width: 1%;">
        </td>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Amount_Taxable" runat="server" CssClass="LABEL" Text="Amount Taxable :"></asp:Label></td>
        <td style="width: 20%">
          <asp:TextBox ID="txt_Amount_Taxable" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True"
            Width="40%"></asp:TextBox>&nbsp;
        </td>
        <td style="width: 1%">
        </td>
      </tr>
      <tr>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_Service_Tax" runat="server" CssClass="LABEL" Text="Delivery Service Tax :"></asp:Label></td>
        <td style="width: 20%;">
          <asp:TextBox ID="txt_Service_Tax" BackColor="Transparent" BorderColor="Transparent"
            BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True"
            Width="86%"></asp:TextBox>&nbsp;
        </td>
        <td style="width: 1%;">
        </td>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_service_tax_By" runat="server" CssClass="LABEL" Text="Service Tax By :"></asp:Label></td>
        <td style="width: 38%;">
          <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_service_tax_By" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                Width="40%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%;">
        </td>
      </tr>
      <tr id="tr_Octr" runat="server">
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Octr_Amount" runat="server" CssClass="LABEL" Text="Octr Amount :"></asp:Label>&nbsp;
        </td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Octr_Amount" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True"
                Width="86%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_Octr_rec_no" runat="server" CssClass="LABEL" Text="Octr Rec No :"></asp:Label></td>
        <td colspan="2">
          <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Octr_rec_no" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                Width="40%"></asp:TextBox>
              <asp:Label ID="lbl_Octr_Rec_Date" runat="server" CssClass="LABEL" Text="Octr Rec Date :"></asp:Label>
              <asp:TextBox ID="txt_Octr_Rec_Date" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                Width="25%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td style="width: 20%" class="TD1">
          Round Off(+/-) :
        </td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:Label ID="lbl_RoundOff" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"
              BorderColor="Transparent"
                BorderStyle="Solid" Width="88%"></asp:Label>
              <asp:HiddenField ID="hdn_RoundOff" runat="server"></asp:HiddenField>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1" id="td1" runat="server">
        </td>
        <td colspan="2" id="td2" runat="server">
        </td>
      </tr>
      <tr>
        <td style="width: 20%" class="TD1">
          <asp:Label ID="lbl_TotalReceivables" runat="server" CssClass="LABEL" Text="Total Receivables :"></asp:Label>
        </td>
        <td style="width: 20%">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_TotalReceivables" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOXNOS" runat="server" Font-Bold="True"
                Width="86%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 20%" class="TD1" id="td_lbl_Octr_Form_Type" runat="server">
          <asp:Label ID="lbl_Octr_Form_Type" runat="server" CssClass="LABEL" Text="Octr Form Type  :"></asp:Label></td>
        <td colspan="2" id="td_txt_OctrFormType" runat="server">
          <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:TextBox ID="txt_Octr_Form_Type" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                Width="40%"></asp:TextBox>
              <asp:Label ID="lbl_Octr_Paid_By" runat="server" CssClass="LABEL" Text="Octr paid By :"></asp:Label><asp:TextBox
                ID="txt_Octr_Paid_By" runat="server" BackColor="Transparent" BorderColor="Transparent"
                BorderStyle="Solid" CssClass="TEXTBOX" Font-Bold="True" ReadOnly="True" Width="25%"></asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr runat="server" id="tr_ReceivedBy">
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_ReceivedBy" runat="server" CssClass="LABEL" Text="Received By:"></asp:Label>
        </td>
        <td style="width: 50%;" colspan="3">
          <asp:RadioButtonList ID="Rbl_Receivedby" runat="server" RepeatDirection="Horizontal"
            AutoPostBack="false" onclick="HideReceivedByControl();">
            <asp:ListItem Value="1" Text="Cash Bank" Selected="true"></asp:ListItem>
            <asp:ListItem Value="2" Text="Debit To"></asp:ListItem>
          </asp:RadioButtonList>
        </td>
      </tr>
      <tr>
        <td colspan="6" runat="server" id="TR_Cheque">
          <uc2:WucMRCashChequeDetails ID="WucMRCashChequeDetails1" runat="server" />
        </td>
      </tr>
      <tr runat="server" id="TR_DebitTo">
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_DebitTo" runat="server" CssClass="LABEL" Text="Debit To :"></asp:Label></td>
        <td style="width: 20%;">
          <cc1:DDLSearch ID="ddl_DebitTo" runat="server" AllowNewText="True" IsCallBack="True"
            CallBackFunction="Raj.EC.FinanceModel.MRDeliveryModel.GetLedger" CallBackAfter="2"
            Text="" PostBack="False" />
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
          *</td>
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_BillingBranch" runat="server" CssClass="LABEL" Text="Billing Branch :"></asp:Label></td>
        <td style="width: 20%;">
          <cc1:DDLSearch ID="ddl_BillingBranch" runat="server" AllowNewText="True" IsCallBack="True"
            CallBackFunction="Raj.EC.FinanceModel.MRDeliveryModel.GetCreditToBranch" CallBackAfter="2"
            Text="" PostBack="False" />
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
          *</td>
      </tr>
      <tr runat="server" id="tr_Credit_Memo_for">
        <td style="width: 20%;" class="TD1">
          <asp:Label ID="lbl_CreditFor" runat="server" CssClass="LABEL" Text="Credit For :"></asp:Label>
        </td>
        <td style="width: 50%;" colspan="3">
          <asp:RadioButtonList ID="rbtn_CMFConsignee" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="0" Text="Consignor"></asp:ListItem>
            <asp:ListItem Value="1" Text="Consignee" Selected="True"></asp:ListItem>
          </asp:RadioButtonList>
        </td>
        <td style="width: 30%;" colspan="2">
        </td>
      </tr>
      <tr id="tr_chk" runat="server">
        <td style="width: 41%" colspan="3">
        </td>
        <td style="width: 20%;">
        </td>
        <td style="width: 38%;">
          &nbsp;
          <asp:UpdatePanel ID="UpdatePanel16" runat="server">
            <ContentTemplate>
              <asp:CheckBox ID="chk_Is_Service_Tax_App_Commodity" runat="server" />
              <asp:CheckBox ID="chk_Is_Octr" runat="server" />
              <asp:CheckBox ID="chk_Is_Octr_To_Be_Added_In_MR" runat="server" />
              <asp:CheckBox ID="chk_Is_Service_Tax_by_Consignee" runat="server" />
              <asp:CheckBox ID="chk_Is_Oct_Recovered_From_Consignee" runat="server" />
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%;">
        </td>
      </tr>
      <tr>
        <td align="center" colspan="5">
          <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="conditional">
            <ContentTemplate>
              <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save & Exit" OnClick="btn_Save_Click" />
              <asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON" AccessKey="p" Text="Save & Print"
                OnClick="btn_Save_Print_Click" />
              <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="EXIT" AccessKey="E"
                OnClick="btn_Close_Click" />
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btn_Save" />
              <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
              <asp:AsyncPostBackTrigger ControlID="btn_Close" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td colspan="6">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                Font-Bold="True" Text="Fields With * Mark Are Mandatory"></asp:Label>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btn_Save" />
              <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
            </Triggers>
          </asp:UpdatePanel>
          <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Always">
            <ContentTemplate>
              <asp:HiddenField ID="hdn_Booking_Type_ID" runat="server" />
              <asp:HiddenField ID="hdn_Payment_Type_ID" runat="server" />
              <asp:HiddenField ID="hdn_OctAmount" runat="server" />
              <asp:HiddenField ID="hdn_SubTotal" runat="server" />
              <asp:HiddenField ID="hdn_Total_GC_Amount" runat="server" />
              <asp:HiddenField ID="hdn_Charges_Wt" runat="server" />
              <asp:HiddenField ID="hdn_Other_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Tax_Abate" runat="server" />
              <asp:HiddenField ID="hdn_Amount_Taxable" runat="server" />
              <asp:HiddenField ID="hdn_Octr_Pay_Type_ID" runat="server" />
              <asp:HiddenField ID="hdn_Octr_Form_Type_ID" runat="server" />
              <asp:HiddenField ID="hdn_Service_Tax_Amount" runat="server" />
              <asp:HiddenField ID="hdn_Delivery_Sub_Total" runat="server" />
              <asp:HiddenField ID="hdn_Service_Tax_Percent" runat="server" />
              <asp:HiddenField ID="hdn_Delivery_Total_Receivables" runat="server" />
              <asp:HiddenField ID="hdn_Additional_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Discount_Amount" runat="server" />
              <asp:HiddenField ID="hdn_Std_Octroi_Form_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Std_Octroi_Service_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Std_GI_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Std_Hamali_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Std_Demurage_Charges" runat="server" />
              <asp:HiddenField ID="hdn_Service_Pay_By_ID" runat="server" />
              <asp:HiddenField ID="hdn_Oct_Form_Chg_Discount" runat="server" />
              <asp:HiddenField ID="hdn_Oct_Service_Chg_Discount" runat="server" />
              <asp:HiddenField ID="hdn_GI_Chg_Discount" runat="server" />
              <asp:HiddenField ID="hdn_Hamali_Chg_Discount" runat="server" />
              <asp:HiddenField ID="hdn_Demurage_Chg_Discount" runat="server" />
              <asp:HiddenField ID="hdn_DocumentID" runat="server" />
              <asp:HiddenField ID="hdn_MenuItemID" runat="server" />
              <asp:HiddenField ID="hdn_CreditMemoFor_Id" runat="server" />
              <asp:HiddenField ID="hdn_KeyID" runat="server" />
              <asp:HiddenField ID="hdn_DeliveryDate" runat="server" />
              <asp:HiddenField ID="hdn_Service_Tax_On_Advance" runat="server" />
              <asp:HiddenField ID="hdn_ReceivedBy" runat="server" />
              &nbsp;
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
  </ComponentArt:PageView>
  <ComponentArt:PageView CssClass="PageContent" runat="server">
    <table class="TABLE" width="100%" border="0">
      <tr>
        <td colspan="6">
          <uc3:WucMRDeliveryDetails ID="WucMRDeliveryDetails1" runat="server" />
        </td>
      </tr>
    </table>
  </ComponentArt:PageView>
</ComponentArt:MultiPage>

<script type="text/javascript">
Hide_Octr_Controls();
HideReceivedByControl();
</script>

