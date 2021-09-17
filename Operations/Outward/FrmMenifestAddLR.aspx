<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMenifestAddLR.aspx.cs"
  Inherits="Operations_Outward_FrmMenifestAddLR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Outward/Menifest.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Add LR To Manifest</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table class="TABLE">
      <tr>
        <td class="TDGRADIENT" colspan="4">
          &nbsp;
          <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="MANIFEST"></asp:Label>
        </td>
      </tr>
      <tr>
        <td style="width: 5%" valign="top">
          Area:
        </td>
        <td style="width: 20%" valign="top" align="left">
          <asp:DropDownList ID="ddlArea" CssClass="DROPDOWN" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" />
        </td>
        <td style="width: 65%">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div id="Div_servicelocations" class="DIV" style="height: 150px">
                <asp:DataGrid ID="dg_servicelocations" runat="server" AutoGenerateColumns="False"
                  DataKeyField="ServiceLocationID" CellPadding="2" CssClass="GRID" Style="border-top-style: none"
                  Width="90%">
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                  <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                      <HeaderTemplate>
                        <input id="chkAllItems" type="checkbox" onclick="Check_All_Add_LR(this,'dg_servicelocations');" />
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:CheckBox ID="Chk_Attach" runat="server" />
                        <asp:HiddenField ID="hdnServiceLocationID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ServiceLocationID") %>' />
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="ServiceLocationName" HeaderText="Branch/Service Location">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NoOfLR" HeaderText="No Of LR"></asp:BoundColumn>
                    <asp:BoundColumn DataField="NoOkPkg" HeaderText="No Of Pkg"></asp:BoundColumn>
                  </Columns>
                </asp:DataGrid>
              </div>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="ddlArea" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
        <td style="width: 10%" valign="top">
          <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Text="Go" OnClick="btnGo_Click" />
        </td>
      </tr>
      <tr>
        <td colspan="4">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <div id="Div_Memo" class="DIV" style="height: 350px">
                <asp:DataGrid ID="dg_Memo" runat="server" AutoGenerateColumns="False" DataKeyField="GC_Id"
                  CellPadding="2" CssClass="GRID" Style="border-top-style: none" Width="98%">
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                  <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                      <HeaderTemplate>
                        <input id="chkAllItems" type="checkbox" onclick="Check_All_Add_LR(this,'dg_Memo');" />
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:CheckBox ID="Chk_Attach" runat="server" />
                        <asp:HiddenField ID="hdnGcNo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "GC_No_For_Print") %>' />
                      </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="Gc No" />
                    <asp:BoundColumn DataField="GC_Date" HeaderText="Booking Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Booking Branch" />
                    <asp:BoundColumn DataField="Delivery_Location_Name" HeaderText="Delivery Location" />
                    <asp:BoundColumn DataField="Delivery_Type" HeaderText="Delivery Type" />
                    <asp:BoundColumn DataField="Payment_Type" HeaderText="Payment Type" />
                    <asp:BoundColumn DataField="Balance_Articles" HeaderText="Balance Articles" />
                    <asp:BoundColumn DataField="Balance_Actual_Wt" HeaderText="Balance Wt" />
                    <asp:BoundColumn DataField="Loaded_Articles" HeaderText="Loaded Articles" />
                    <asp:BoundColumn DataField="Loaded_Weight" HeaderText="Loaded Weight" />
                  </Columns>
                </asp:DataGrid>
              </div>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="ddlArea" />
              <asp:AsyncPostBackTrigger ControlID="btnGo" />
              <asp:AsyncPostBackTrigger ControlID="btnAddLR" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
    <table class="TABLE">
      <tr>
        <td align="left" style="width: 5%">
          <asp:Button ID="btnAddLR" runat="server" CssClass="BUTTON" Text="Add LR" OnClick="btnAddLR_Click" />
        </td>
        <td align="center" colspan="2" style="width: 90%">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:Label ID="lblLRAdded" runat="server" CssClass="LABELERROR" Text=""></asp:Label>&nbsp;
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btnAddLR" />
              <asp:AsyncPostBackTrigger ControlID="ddlArea" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
        <td align="right" style="width: 5%">
          <asp:Button ID="btnAddLRToMemo" runat="server" CssClass="BUTTON" Text="Add LR to Invoice"
            OnClick="btnAddLRToMemo_Click" />
        </td>
      </tr>
      <tr>
        <td style="width: 100%; font-weight: bold" colspan="4">
          &nbsp;
        </td>
      </tr>
      <tr>
        <td style="width: 5%; font-weight: bold" align="right" valign="top">
          Selected LR(s):
        </td>
        <td colspan="3" style="width: 95%">
          <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <asp:TextBox ID="lblSelectedLRs" runat="server" CssClass="TEXTBOXASLABEL" Text="" Font-Bold="true" TextMode ="MultiLine" Height="51px" Width="600px"></asp:TextBox>&nbsp;
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btnAddLR" />
            </Triggers>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
  </form>
  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
      <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
        z-index: 100">
        <span id="ajaxloading">
          <table>
            <tr>
              <td>
                <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
            </tr>
            <tr>
              <td align="center">
                Wait! Action in Progress...</td>
            </tr>
          </table>
        </span>
      </div>
    </ProgressTemplate>
  </asp:UpdateProgress>
</body>
</html>
