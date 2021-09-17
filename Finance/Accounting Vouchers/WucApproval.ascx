<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucApproval.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucApproval" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/WucSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript">
 function Open_View_Window(Voucher_Id)
    {
         var Path ='';
         Path='../VoucherView/FrmVoucher.aspx?Id=' + Voucher_Id;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function CalculateTotal(chk,CashAmt,ChqAmt,MRAmt)
    {    

        var chk = document.getElementById(chk);
        var ddl_DocumentType = document.getElementById('WucApproval1_ddl_DocumentType');
        var ddl_DocumentTypeValue = ddl_DocumentType.options[ddl_DocumentType.selectedIndex].value;
        
        if (ddl_DocumentTypeValue == "195")
        {
          var MrAmt;

          var lblTotalMRAmt = document.getElementById('WucApproval1_lblTotalMRAmt');
          
          var TotalMRAmt;

          MrAmt = val(MRAmt);

          if(chk.checked == true)
          {
            TotalMRAmt = val(lblTotalMRAmt.innerHTML) + val(MrAmt);
          }
          else
          {
            TotalMRAmt = val(lblTotalMRAmt.innerHTML) - val(MrAmt);
          }
          
          lblTotalMRAmt.innerHTML = parseFloat(TotalMRAmt).toFixed(2);
        }
        else
        {
          var checkbox,CashAmt,ChqAmt,MrAmt;

          var lblTotalCashAmt = document.getElementById('WucApproval1_lblTotalCashAmt');
          var lblTotalChqAmt = document.getElementById('WucApproval1_lblTotalChqAmt');
          var lblTotalMRAmt = document.getElementById('WucApproval1_lblTotalMRAmt');
          
          var TotalCashAmt;
          var TotalChqAmt;
          var TotalMRAmt;
          
          CashAmt = val(CashAmt);
          ChqAmt = val(ChqAmt);
          MrAmt = val(MRAmt);

          if(chk.checked == true)
          {
            TotalCashAmt= val(lblTotalCashAmt.innerHTML) + val(CashAmt);
            TotalChqAmt = val(lblTotalChqAmt.innerHTML) + val(ChqAmt);
            TotalMRAmt = val(lblTotalMRAmt.innerHTML) + val(MrAmt);
          }
          else
          {
            TotalCashAmt= val(lblTotalCashAmt.innerHTML) - val(CashAmt);
            TotalChqAmt = val(lblTotalChqAmt.innerHTML) - val(ChqAmt);
            TotalMRAmt = val(lblTotalMRAmt.innerHTML) - val(MrAmt);
          }
          lblTotalCashAmt.innerHTML = parseFloat(TotalCashAmt).toFixed(2);
          lblTotalChqAmt.innerHTML = parseFloat(TotalChqAmt).toFixed(2);
          lblTotalMRAmt.innerHTML = parseFloat(TotalMRAmt).toFixed(2);
        }
    }
</script>

<asp:ScriptManager ID="scm_ApprovalGrid" runat="server">
</asp:ScriptManager>
<table cellpadding="0" cellspacing="0" style="width: 100%">
  <tr>
    <td style="width: 13%;" align="left">
      <asp:Label ID="lbl_DocumentType" Font-Bold="true" runat="server" CssClass="LABEL"
        Text="Document Type :"></asp:Label>
    </td>
    <td style="width: 24%;" align="left">
      <asp:DropDownList ID="ddl_DocumentType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged">
      </asp:DropDownList>
    </td>
    <td style="width: 24%;" align="left">
      <asp:RadioButtonList ID="rbl_Approved" runat="server" RepeatDirection="Horizontal"
        AutoPostBack="false" OnSelectedIndexChanged="rbl_Approved_SelectedIndexChanged">
        <asp:ListItem Value="1" Selected="True" Text="Approved"></asp:ListItem>
        <asp:ListItem Value="0" Text="Not Approved"></asp:ListItem>
      </asp:RadioButtonList>
    </td>
    <td style="width: 13%" align="left">
    </td>
    <td style="width: 26%">
    </td>
  </tr>
</table>
<tr>
  <td colspan="2" style="font-size: xx-small; vertical-align: middle; width: 100%">
    &nbsp;
  </td>
</tr>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
  <tr>
    <td style="width: 1%; vertical-align: middle;" class="TDTEXT">
      <asp:Label ID="lbl_From" runat="server" Text="From :"></asp:Label>
    </td>
    <td style="width: 29%; vertical-align: middle;">
      <uc3:WucDatePicker ID="PickerFrom" runat="server"></uc3:WucDatePicker>
    </td>
    <td style="width: 1%; vertical-align: middle;" class="TDTEXT">
      <asp:Label ID="lbl_To" runat="server" Text="To :"></asp:Label>
    </td>
    <td style="width: 29%; vertical-align: middle;">
      <uc3:WucDatePicker ID="PickerTo" runat="server"></uc3:WucDatePicker>
    </td>
    <td class="TD1" style="width: 60%; vertical-align: Top;">
      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
          <td style="width: 100%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                <asp:DropDownList ID="ddl_Search" runat="server" Font-Names="Verdana" Font-Size="11px">
                </asp:DropDownList>
                <asp:TextBox ID="txt_Search" runat="server" BorderWidth="1px" CssClass="TEXTBOXSEARCH"
                  MaxLength="50"></asp:TextBox>
                <asp:ImageButton ID="btn_Search" runat="server" ImageAlign="TextTop" ImageUrl="~/Images/Search.GIF"
                  OnClick="btn_Search_Click" />
              </ContentTemplate>
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
                <asp:AsyncPostBackTrigger ControlID="rbl_Approved" />
              </Triggers>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td colspan="2" style="font-size: xx-small; vertical-align: middle; width: 100%">
      &nbsp;
    </td>
  </tr>
</table>
<tr>
  <td>
    <table cellpadding="0" cellspacing="0" style="width: 100%">
      <tr>
        <td style="width: 100%;">
          <asp:UpdatePanel ID="UP_Grid" runat="server">
            <ContentTemplate>
              <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="True" CssClass="GRID" PageSize="15"
                AutoGenerateColumns="True" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                OnItemCreated="dg_Grid_ItemCreated">
                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                <HeaderStyle CssClass="GRIDHEADERCSS" />
                <FooterStyle CssClass="GRIDFOOTERCSS" />
                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                <Columns>
                  <asp:TemplateColumn HeaderText="Is Approved">
                    <ItemTemplate>
                      <asp:CheckBox ID="Chk_IsApproved" CssClass="CHECKBOX" Checked='<%#  ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Is Approved").ToString()) %>'
                        runat="server" />
                    </ItemTemplate>
                  </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                      <asp:Label ID="lbl_VoucherId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>' />
                    </ItemTemplate>
                  </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderText="Voucher Details">
                    <ItemTemplate>
                      <asp:LinkButton ID="lnk_View" runat="server" Text="Voucher Details"></asp:LinkButton>
                    </ItemTemplate>
                  </asp:TemplateColumn>
                </Columns>
              </asp:DataGrid>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              <asp:AsyncPostBackTrigger ControlID="btn_Search" />
              <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
              <%-- <asp:AsyncPostBackTrigger ControlID="rbl_Approved" /> --%>
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
  </td>
</tr>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
  <tr>
    <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="3">
      &nbsp;
    </td>
  </tr>
  <tr>
    <td style="width: 50%">
      &nbsp;<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
    <td style="width: 25%" align="right">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:Label ID="Lbl_Total_Records" runat="server" CssClass="LABEL" Font-Bold="True"
            EnableViewState="False"></asp:Label>
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td class="TD">
      <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <table class ="TABLE" style="width:50%; color:Yellow; font-weight:bold; background-color:Purple;" >
            <tr>
              <td class="TD" id = "tdTotalCashAmtCaption" runat="server" >
                Total Cash Amt :
              </td>
              <td id="tdTotalCashAmt" runat="server" style="width: 11px;">
                <asp:Label ID="lblTotalCashAmt" runat="server" CssClass="LABEL" Text="0" Font-Bold="True" />
              </td>
             </tr>
             <tr> 
              <td class="TD" id = "tdTotalChqAmtCaption" runat="server">
                Total Chq Amt :
              </td>
              <td id="tdTotalChqAmt" runat="server" style="width: 11px">
                <asp:Label ID="lblTotalChqAmt" runat="server" CssClass="LABEL" Text="0" Font-Bold="True" />
              </td>
              </tr>
              <tr>
              <td id="tdTotalMRAmtCaption" class="TD" runat="server">
                Total MR Amt :
              </td>
              <td id="tdTotalMRAmt" runat="server" style="width: 11px">
                <asp:Label ID="lblTotalMRAmt" runat="server" CssClass="LABEL" Text="0" Font-Bold="True" />
              </td>
              </tr>
          </table>
        </ContentTemplate>
        <Triggers>
              <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              <asp:AsyncPostBackTrigger ControlID="btn_Search" />
              <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td class="TD1" colspan="6" style="text-align: center">
      <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Search" />
          <asp:AsyncPostBackTrigger ControlID="rbl_Approved" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td colspan="3">
      <asp:Button ID="btn_Export_To_Excel" runat="server" Text="Export to Excel" CssClass="BUTTON"
        OnClick="btn_Export_To_Excel_Click" /></td>
  </tr>
</table>
